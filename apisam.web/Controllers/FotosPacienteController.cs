namespace apisam.web.Controllers
{
    using apisam.entities;
    using apisam.interfaces;
    using apisam.web.HandleErrors;
    using ImageMagick;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.Threading.Tasks;

    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FotosPacienteController : ControllerBase
    {
        public IFotosPaciente FotosRepo;

        private readonly IConfiguration _config;

        public FotosPacienteController(IFotosPaciente fotos, IConfiguration config)
        {
            FotosRepo = fotos;
            _config = config;
        }

        [Authorize(Roles = "2")]
        [HttpPost("pacienteid/{pacienteid}/username/{username}/usuarioid/{userid}/asistenteid/{asistenteid}", Name = "Add")]
        public async Task<IActionResult> Add([FromRoute] int pacienteid, [FromRoute] string username, [FromForm] IFormFile foto, [FromForm] string notas, [FromRoute] string userid, [FromRoute] string asistenteid)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var _resp = new RespuestaMetodos();
            var _fotoPaciente = new FotosPaciente();

            string _newFileName = pacienteid + "-";
            _newFileName += DateTime.Now.Ticks.ToString() + "-";
            _newFileName += Guid.NewGuid().ToString();
            string _folderName = "paciente00" + pacienteid;

            var _connString = _config.GetValue<string>("ConnectionStrings:azure_storage");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connString);
            // Create a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to a container named "mycontainer."

            CloudBlobContainer container = blobClient.GetContainerReference("containersam");
            // If "mycontainer" doesn't exist, create it.
            // await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            try
            {
                if (foto != null)
                {
                    // Get a reference to a blob named "myblob".
                    var fileExtension = System.IO.Path.GetExtension(foto.FileName);
                    if (fileExtension.ToLower().Equals(".png")
                        || fileExtension.ToLower().Equals(".jpg")
                        || fileExtension.ToLower().Equals(".jpeg"))
                    {
                        CloudBlockBlob blockBlob =
                            container.GetBlockBlobReference($"{_folderName}/" + _newFileName + fileExtension);
                        using (var _fileStream = foto.OpenReadStream())
                        {
                            using (var image = new MagickImage(_fileStream))
                            {

                                //image.Resize(250, 250);
                                image.Strip();
                                image.Quality = 70;

                                await blockBlob.UploadFromByteArrayAsync(image.ToByteArray(), 0, image.ToByteArray().Length);
                            }
                            _resp.Ok = true;
                        }
                        var _urlStorage = _config.GetValue<string>("UrlsWebSites:urlStorage");
                        _fotoPaciente.FotoUrl = _urlStorage + blockBlob.Name;
                    }
                    else
                    {
                        return BadRequest("Formato no soportado");
                    }
                }
                if (_resp.Ok)
                {
                    _fotoPaciente.UsuarioId = userid;
                    _fotoPaciente.AsistenteId = asistenteid;
                    _fotoPaciente.PacienteId = pacienteid;
                    _fotoPaciente.Activo = true;
                    _fotoPaciente.CreadoPor = username;
                    _fotoPaciente.ModificadoPor = username;
                    _fotoPaciente.Notas = notas;

                    RespuestaMetodos _resp2 = await FotosRepo.AddFoto(_fotoPaciente);

                    if (_resp2.Ok) return Ok(_fotoPaciente);
                }
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return BadRequest(_resp);
        }


        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] FotosPaciente foto)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await FotosRepo.UpdateFoto(foto);
            if (_resp.Ok) return Ok(foto);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "1,2")]
        [HttpGet("page/{pageNo}/limit/{limit}/pacienteid/{pacienteId}", Name = "GetFotos")]
        public async Task<IActionResult> GetFotos(int pageNo, int limit, [FromQuery] string filter, int pacienteId)
        {
            try
            {
                var _pageResponse = await FotosRepo.GetFotos(pageNo, limit, filter, pacienteId);
                return Ok(_pageResponse);
            }
            catch (Exception e)
            {

                var a = e.Message;
            }

            return BadRequest("no se han podido obtener registros");
        }



    }
}
