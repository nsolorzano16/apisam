using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace apisam.web.Controllers
{


    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IUsuario UsuariosRepo;
        private readonly IConfiguration _config;

        public UsuariosController(IUsuario usuariorepository, IConfiguration config)
        {
            UsuariosRepo = usuariorepository;
            _config = config;
        }
        [Authorize(Roles = "1")]
        [HttpGet("")]
        public IEnumerable<Usuario> Get()
        {
            return UsuariosRepo.Usuarios;
        }

        [Authorize(Roles = "1")]
        [HttpPost("")]
        public IActionResult Add([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (UsuariosRepo.AddUsuario(usuario)) return Ok(usuario);
            return BadRequest("error salvando usuario");
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
            return Ok(
                new
                {
                    token = GenerarToken(claims),
                    usuario.UsuarioId,
                    usuario.UserName,
                    usuario.Nombres,
                    usuario.PrimerApellido,
                    usuario.SegundoApellido,
                    usuario.Email,

                });

        }

        private bool VerificarPasswordHash(string password,
            byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return new ReadOnlySpan<byte>
                (passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
        }

        private string GenerarToken(List<Claim> pclaims)
        {
            var _key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
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