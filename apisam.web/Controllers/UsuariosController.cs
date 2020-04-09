using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.entities.ViewModels.UsuariosTable;
using apisam.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace apisam.web.Controllers
{

    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IUsuario UsuariosRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuario usuariorepository, IConfiguration config,
            IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            UsuariosRepo = usuariorepository;

        }
        [Authorize(Roles = "1")]
        [HttpGet("")]
        public IEnumerable<Usuario> Get()
        {
            return UsuariosRepo.Usuarios;
        }

        [HttpGet("roles", Name = "GetRoles")]
        public IEnumerable<Rol> GetRoles()
        {
            return UsuariosRepo.Roles;
        }

        [Authorize(Roles = "1,2,3")]
        [HttpPost("profilefoto/{id}", Name = "SubirFoto")]
        async public Task<IActionResult> SubirFoto([FromRoute] int id, [FromForm]  IFormFile logoImage)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var _resp = new RespuestaMetodos();

            // obtengo usuario a modificar
            var _user = UsuariosRepo.GerUserById(id);
            if (_user == null) return BadRequest();
            string _newFileNameLogo = _user.UsuarioId.ToString() + "-";
            _newFileNameLogo += DateTime.Now.Ticks.ToString() + "-";
            _newFileNameLogo += Guid.NewGuid().ToString();
            var _connString = _config.GetValue<string>("ConnectionStrings:azure_storage");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connString);
            // Create a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Get a reference to a container named "mycontainer."
            CloudBlobContainer container = blobClient.GetContainerReference("profilesphotos");
            // If "mycontainer" doesn't exist, create it.
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            try
            {
                if (logoImage != null)
                {
                    // Get a reference to a blob named "myblob".
                    var fileExtension = System.IO.Path.GetExtension(logoImage.FileName);
                    if (fileExtension.ToLower().Equals(".png")
                        || fileExtension.ToLower().Equals(".jpg")
                        || fileExtension.ToLower().Equals(".jpeg"))
                    {

                        CloudBlockBlob blockBlob =
                            container.GetBlockBlobReference(_newFileNameLogo + fileExtension);
                        using (var _fileStream = logoImage.OpenReadStream())
                        {
                            await blockBlob.UploadFromStreamAsync(_fileStream);
                            _resp.Ok = true;
                        }
                        var _urlStorage = _config.GetValue<string>("UrlsWebSites:urlStorage");
                        _user.FotoUrl = _urlStorage + blockBlob.Name;

                    }
                    else
                    {
                        return BadRequest("Formato no soportado");
                    }



                }
                if (_resp.Ok)
                {
                    RespuestaMetodos _resp2 = UsuariosRepo.UpdateUsuario(_user);

                    if (_resp2.Ok) return Ok(_user);


                }
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }





            return BadRequest(_resp);


        }



        //[Authorize(Roles = "1")]
        [HttpPost("")]
        public IActionResult Add([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = UsuariosRepo.AddUsuario(usuario);

            if (_resp.Ok) return Ok(usuario);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "1,2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] UsuarioEditarViewModel usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = UsuariosRepo.GerUserById(usuario.UsuarioId);
            if (user == null) return BadRequest("Usuario no existe");
            user = _mapper.Map<UsuarioEditarViewModel, Usuario>(usuario, user);
            RespuestaMetodos _resp = UsuariosRepo.UpdateUsuario(user);

            if (_resp.Ok) return Ok(user);
            return BadRequest(_resp);
        }

        [HttpPost("Login", Name = "Login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var usuario = UsuariosRepo.GetUsuarioByUserName(model);
            if (usuario == null) return NotFound();
            if (!VerificarPasswordHash(model.Password, usuario.PasswordHash, usuario.PasswordSalt))
            {
                return NotFound();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role,usuario.RolId.ToString()),

            };
            var rolDescripcion = "";
            if (usuario.RolId == 1)
            {
                rolDescripcion = "Administrador de Sistema";
            }
            else if (usuario.RolId == 2)
            {
                rolDescripcion = "Administrador";
            }
            else if (usuario.RolId == 3)
            {
                rolDescripcion = "Operador";
            }
            return Ok(
                new
                {
                    token = GenerarToken(claims),
                    usuario,
                    rolDescripcion

                });

        }

        [Authorize(Roles = "1,2")]
        [HttpGet("asistentes/page/{pageNo}/limit/{limit}/doctorId/{doctorId}", Name = "GetAsistentes")]
        public IActionResult GetAsistentes(int pageNo, int limit, [FromQuery] string filter, int doctorId)
        {
            try
            {
                var _pageResponse = UsuariosRepo.GetAsistentes(pageNo, limit, filter, doctorId);
                return Ok(_pageResponse);
            }
            catch (Exception e)
            {

                var a = e.Message;
            }

            return BadRequest("no se han podido obtener registros");
        }

        [Authorize(Roles = "1,2,3")]
        [HttpPut("changePassword", Name = "ChangePassword")]
        public IActionResult ChangePassword([FromBody] UserChangePassword model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = UsuariosRepo.UpdatePassword(model);
            if (user != null) return Ok(user);

            return BadRequest();
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("info/{id}", Name = "GetUserInfo")]
        public IActionResult GetUserInfo([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(UsuariosRepo.GerUserById(id));
        }


        private bool VerificarPasswordHash(string password,
            byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using var hmac = new System.Security.
                Cryptography.HMACSHA512(passwordSalt);
            var passwordHashNuevo = hmac.ComputeHash
                (System.Text.Encoding.UTF8.GetBytes(password));
            return new ReadOnlySpan<byte>
                (passwordHashAlmacenado).SequenceEqual
                (new ReadOnlySpan<byte>(passwordHashNuevo));
        }

        private string GenerarToken(List<Claim> pclaims)
        {
            var _key =
                new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var _credentials
                = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var _token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                 _config["Jwt:Issuer"],
                 expires: DateTime.Now.AddHours(8),
                 signingCredentials: _credentials,
                 claims: pclaims);
            return new JwtSecurityTokenHandler().WriteToken(_token);

        }

    }
}