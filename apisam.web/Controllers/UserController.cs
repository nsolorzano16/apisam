namespace apisam.web.Controllers
{
    using apisam.entities;
    using apisam.entities.ViewModels.UsuariosTable;
    using apisam.interfaces;
    using apisam.web.Data;
    using apisam.web.HandleErrors;
    using AutoMapper;
    using ImageMagick;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using ServiceStack.OrmLite;
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        private readonly SignInManager<User> _signinManager;

        private readonly IConfiguration _config;

        private readonly RoleManager<UserRole> _roleManager;

        private readonly IOptions<MailJet> _emailOptions;

        private readonly IEmail _emailRepo;

        private readonly IUsuario _usuariosRepo;
        private readonly IPreclinica _preclinicaRepo;
        private readonly IPlanes _planesRepo;
        private readonly IFotosPaciente _fotosPacienteRepo;

        private static TimeZoneInfo hondurasTime;

        private readonly OrmLiteConnectionFactory dbFactory;


        public UserController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signinManager, IConfiguration config, RoleManager<UserRole> roleManager, IOptions<MailJet> emailOptions, IEmail emailRepo, IUsuario usuariosRepo,
            IPreclinica preclinicaRepo,IPlanes planesRepo,IFotosPaciente fotos)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signinManager = signinManager;
            _config = config;
            _roleManager = roleManager;
            _emailOptions = emailOptions;
            _emailRepo = emailRepo;
            _usuariosRepo = usuariosRepo;
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            dbFactory = new OrmLiteConnectionFactory(_config.GetConnectionString("azure_dbcon"), SqlServerDialect.Provider);
            _preclinicaRepo = preclinicaRepo;
            _planesRepo = planesRepo;
            _fotosPacienteRepo = fotos;
        }

        
        [Authorize(Roles = "1,2")]
        [HttpPost("create", Name = "Create")]
        public async Task<IActionResult> Create(CreateUserViewModel user)
        {
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);

            if (user.RolId == 1)
            {
                if (!await _roleManager.RoleExistsAsync("1"))
                {
                    //rol 1 superadministrador
                    await _roleManager.CreateAsync(new UserRole
                    {
                        Description = "Administrador de sistema",
                        Name = "1",

                    });
                }
            }
            else if (user.RolId == 2)
            {
                if (!await _roleManager.RoleExistsAsync("2"))
                {
                    //rol 2 doctorees
                    await _roleManager.CreateAsync(new UserRole
                    {
                        Description = "Doctor",
                        Name = "2",
                    });
                }
            }
            else if (user.RolId == 3)
            {
                // rol 3 asistentes
                if (!await _roleManager.RoleExistsAsync("3"))
                {
                    await _roleManager.CreateAsync(new UserRole
                    {
                        Description = "Asistente",
                        Name = "3",

                    });
                }
            }

            var _userMap = _mapper.Map<CreateUserViewModel, User>(user);
            _userMap.ModificadoPor = user.CreadoPor;
            _userMap.CreadoFecha = dateTime_HN;
            _userMap.ModificadoFecha = dateTime_HN;
            _userMap.Activo = true;
            _userMap.Edad = CalculateAge(_userMap.FechaNacimiento);
            _userMap.FotoUrl = "https://storagedesam.blob.core.windows.net/containersam/assets/avatar-default.png";



            try
            {
                var result = await _userManager.CreateAsync(_userMap, user.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                var _userFromDb = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(_userFromDb, "1");


                //Send Email

                // "http://localhost:4200/#/confirm-email";
               
                //var _uriBuilder = new UriBuilder( );
                //_uriBuilder.Scheme = "http";
                //_uriBuilder.Host = "localhost";
                //_uriBuilder.Path = "#/confirm-email";
                //_uriBuilder.Port = 4200;
                //_uriBuilder.Query = $"userid={_userMap.Id}";

                var url = $"https://frontend.app-sam.com/#/confirm-email?userId={_userMap.Id}";

                // query["userId"] = _userMap.Id;
                //uriBuilder.Query = query.ToString();
                //confirmEmailUrl = uriBuilder.ToString();

                // var urlString = confirmEmailUrl + $"?token={token}&userId={_userMap.Id}";
                //var utf8 = Encoding.UTF8;
                //byte[] utfBytes = utf8.GetBytes(urlString);
                //var stringUTF8url = utf8.GetString(utfBytes, 0, utfBytes.Length);

                var emailBody = url;
                var response = _emailRepo.SendEmail(_userMap.Email, emailBody, _emailOptions.Value);

                if (response.IsSuccessful)
                {
                    return Ok(new
                    {
                        ok = true,
                        response.StatusCode,
                        user = _userMap
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        ok = false,
                        response.StatusCode,
                    });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    databaseMessage = ex.InnerException.Message
                });
            }
        }

        [Authorize(Roles = "1,2,3")]
        [HttpPut("edit", Name = "Edit")]
        public async Task<IActionResult> Edit(EditUserViewModel user)
        {

            if (!ModelState.IsValid) return BadRequest(new { message = "Modelo no valido." });
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);

            var userFind = await _userManager.FindByIdAsync(user.Id);

            userFind.UserName = user.UserName;
            userFind.Email = user.Email;
            userFind.PhoneNumber = user.PhoneNumber;
            userFind.RolId = user.RolId;
            userFind.AsistenteId = user.AsistenteId;
            userFind.PlanId = user.PlanId;
            userFind.Nombres = user.Nombres;
            userFind.PrimerApellido = user.PrimerApellido;
            userFind.SegundoApellido = user.SegundoApellido;
            userFind.Identificacion = user.Identificacion;
            userFind.FechaNacimiento = user.FechaNacimiento;
            userFind.Edad = CalculateAge(user.FechaNacimiento);
            userFind.Sexo = user.Sexo;
            userFind.Telefono2 = user.Telefono2;
            userFind.ColegioNumero = user.ColegioNumero;
            userFind.FotoUrl = user.FotoUrl;
            userFind.ModificadoFecha = dateTime_HN;
            userFind.ModificadoPor = user.ModificadoPor;
            userFind.Activo = user.Activo;
            userFind.Notas = user.Notas;

            try
            {
                var _result = await _userManager.UpdateAsync(userFind);
                var _userMap = _mapper.Map<User, EditUserViewModel>(userFind);
                if (_result.Succeeded)
                {
                     
                    return Ok(_userMap);
                }
                else if (!_result.Succeeded)
                {
                    return BadRequest(_result.Errors);
                }


            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    databaseMessage = ex.InnerException.Message
                });
            }


            return BadRequest(new { message = "No se ha podido modificar el registro" });
        }

        [Authorize(Roles = "2")]
        [HttpGet("assistants/page/{pageNo}/limit/{limit}/doctorId/{doctorId}", Name = "GetAsistentes")]
        public async Task<IActionResult> GetAsistentes(int pageNo, int limit, [FromQuery] string filter, string doctorId)
        {
            var _pageResponse = await _usuariosRepo.GetAsistentes(pageNo, limit, filter, doctorId);
            return Ok(_pageResponse);
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("myinfo/id/{id}", Name = "GetMyInfo")]
        public async Task<IActionResult> GetMyInfo( [FromRoute] string id)
        {
            var myInfo = await _usuariosRepo.GetMyInfo(id);
            var planInfo = await _planesRepo.GetPlanById(myInfo.PlanId);
            int imagenes  = await _fotosPacienteRepo.ImagenesConsumidas(id);

            int atendidas;
            if (myInfo.RolId == 2)
            {
                atendidas = await _preclinicaRepo.GetTotalConsultasAtendidas(myInfo.Id);
            }
            else if (myInfo.RolId == 3)
            {
                atendidas = await _preclinicaRepo.GetTotalConsultasAtendidas(myInfo.AsistenteId);
            }
            else
            {
                atendidas = 0;
            }

            return Ok( new MyInfoViewModel()
            {
                ConsultasAtendidas=atendidas,
                imagenesConsumidas=imagenes,
                Plan = planInfo,
                Usuario=myInfo,

            });
        }

        [Authorize(Roles = "1")]
        [HttpGet("page/{pageNo}/limit/{limit}", Name = "GetUsuarios")]
        public async Task<IActionResult> GetUsuarios(int pageNo, int limit, [FromQuery] string filter)
        {
            return Ok(await _usuariosRepo.GetUsuarios(pageNo, limit, filter));
        }

        [Authorize(Roles = "1,2,3")]
        [HttpPost("profilephoto/{id}", Name = "SubirFoto")]
        public async Task<IActionResult> SubirFoto([FromRoute] string id, [FromForm] IFormFile logoImage)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var _flag = false;
            var _user = await _userManager.FindByIdAsync(id);
            if (_user == null) return BadRequest();

            // obtengo usuario a modificar

            string _newFileNameLogo = _user.Id.ToString() + "-";
            _newFileNameLogo += DateTime.Now.Ticks.ToString() + "-";
            _newFileNameLogo += Guid.NewGuid().ToString();
            string _folderName = "profilephotos";
            var _connString = _config.GetValue<string>("ConnectionStrings:azure_storage");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connString);
            // Create a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Get a reference to a container named "mycontainer."
            CloudBlobContainer container = blobClient.GetContainerReference("containersam");
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
                            container.GetBlockBlobReference($"{_folderName}/" + _newFileNameLogo + fileExtension);
                        using (var _fileStream = logoImage.OpenReadStream())
                        {
                            using (var image = new MagickImage(_fileStream))
                            {
                                image.Resize(250, 250);
                                image.Strip();
                                image.Quality = 70;
                                await blockBlob.UploadFromByteArrayAsync(image.ToByteArray(), 0, image.ToByteArray().Length);
                            }
                           _flag = true;
                        }
                        var _urlStorage = "https://storagedesam.blob.core.windows.net/containersam/";
                        _user.FotoUrl = _urlStorage + blockBlob.Name;
                    }
                    else
                    {
                        return BadRequest(new { message = "Formato no soportado." });
                    }
                }
                if (_flag)
                {
                    var _result = await _userManager.UpdateAsync(_user);
                    var _userMap = _mapper.Map<User, EditUserViewModel>(_user );

                    if (_result.Succeeded) return Ok(_userMap);
                }
            }
            catch (Exception ex)
            {


                return BadRequest(new {message = ex.InnerException.Message });

            }
            return BadRequest(new { message = "Ocurrio un error" });
   
        }



        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age -= 1;

            return age;
        }
    }
}
