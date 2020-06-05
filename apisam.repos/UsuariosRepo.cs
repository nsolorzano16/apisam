using System;
using System.Collections.Generic;
using System.Configuration;
using apisam.interfaces;
using ServiceStack.OrmLite;
using System.Linq;
using apisam.entities;
using apisam.entities.ViewModels;
//using apisam.entities.ViewModels.UsuariosTable;
using System.Threading.Tasks;
using apisam.repos;

namespace apisam.repositories
{
    public class UsuariosRepo : IUsuario
    {


        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public UsuariosRepo()
        {

            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            //hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            hondurasTime = TimeZoneInfo.Local;
        }

        public async Task<List<Usuario>> Usuarios()
        {
            var _db = dbFactory.Open();
            return await _db.SelectAsync<Usuario>();

        }

        public List<Rol> Roles
        {
            get
            {
                var _db = dbFactory.Open();
                return _db.Select<Rol>().ToList();
            }
        }

        public async Task<RespuestaMetodos> AddUsuario(Usuario usuario)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            var _db = dbFactory.Open();
            try
            {

                CreatePasswordHash(usuario.Password, out byte[] _passwordHash, out byte[] _passwordSalt);
                usuario.PasswordHash = _passwordHash;
                usuario.PasswordSalt = _passwordSalt;
                usuario.CreadoFecha = dateTime_HN;
                usuario.ModificadoFecha = dateTime_HN;
                usuario.Password = "";
                usuario.Edad = CalculateAge(usuario.FechaNacimiento);
                usuario.FotoUrl = "https://storagedesam.blob.core.windows.net/profilesphotos/avatar-default.png";
                await _db.SaveAsync<Usuario>(usuario);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;


        }

        public async Task<RespuestaMetodos> UpdateUsuario(Usuario usuario)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                var _db = dbFactory.Open();
                usuario.ModificadoFecha = dateTime_HN;
                usuario.Edad = CalculateAge(usuario.FechaNacimiento);
                await _db.SaveAsync(usuario);
                _resp.Ok = true;


            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;


        }

        public async Task<Usuario> GetUsuarioByUserName(LoginViewModel usuario)
        {
            var _db = dbFactory.Open();
            return await _db.SingleAsync<Usuario>(
                x => x.UserName == usuario.Usuario &&
            x.Activo == true);


        }

        public async Task<Usuario> GerUserById(int id)
        {
            var _db = dbFactory.Open();
            return await _db.SingleByIdAsync<Usuario>(id);

        }

        public async Task<PageResponse<Usuario>> GetAsistentes(int pageNo, int limit, string filter, int doctorId)
        {
            var _response = new PageResponse<Usuario>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@"SELECT * FROM Usuario u
                          WHERE u.AsistenteId = {doctorId} ";

            if (!string.IsNullOrEmpty(filter)) _qry += $" AND (u.Nombres LIKE '%{filter}%' " +
                    $"OR u.PrimerApellido LIKE '%{filter}%' OR u.SegundoApellido LIKE '%{filter}%')";

            var _qry2 = _qry;
            _qry += " ORDER BY u.UsuarioId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            using var _db = dbFactory.Open();
            var _usuarios = await _db.SelectAsync<Usuario>(_qry);

            if (limit > 0)
            {
                _response.TotalItems =
                    _db.Select<Usuario>(_qry2).ToList().Count();
                _response.TotalPages
                    = (int)Math.Ceiling((decimal)_response.TotalItems / (decimal)limit);

                if (pageNo < _response.TotalPages)
                    _response.CurrentPage = pageNo;
                else
                    _response.CurrentPage = _response.TotalPages;

                _response.Items = _usuarios;
                _response.ItemCount = _response.Items.Count;
            }

            return _response;

        }

        public async Task<PageResponse<Usuario>>
       GetUsuarios(int pageNo, int limit, string filter)
        {
            var _response = new PageResponse<Usuario>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@"SELECT * FROM Usuario u";

            if (!string.IsNullOrEmpty(filter)) _qry += $"  WHERE (u.Nombres LIKE '%{filter}%' " +
                    $"OR u.PrimerApellido LIKE '%{filter}%' OR u.SegundoApellido LIKE '%{filter}%')";

            var _qry2 = _qry;
            _qry += " ORDER BY u.UsuarioId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            using var _db = dbFactory.Open();
            var _usuarios = await _db.SelectAsync<Usuario>(_qry);
            if (limit > 0)
            {
                _response.TotalItems =
                    _db.Select<Usuario>(_qry2).ToList().Count();
                _response.TotalPages
                    = (int)Math.Ceiling((decimal)_response.TotalItems / (decimal)limit);

                if (pageNo < _response.TotalPages)
                    _response.CurrentPage = pageNo;
                else
                    _response.CurrentPage = _response.TotalPages;

                _response.Items = _usuarios;
                _response.ItemCount = _response.Items.Count;
            }

            return _response;
        }


        public async Task<Usuario> UpdatePassword(UserChangePassword model)
        {
            var _usuario = new Usuario();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            using (var _db = dbFactory.Open())
            {
                _usuario = await _db.SingleAsync<Usuario>(x => x.UsuarioId == model.Id);
                if (_usuario != null)
                {
                    CreatePasswordHash(model.Password, out byte[] _passwordHash, out byte[] _passwordSalt);
                    _usuario.PasswordHash = _passwordHash;
                    _usuario.PasswordSalt = _passwordSalt;
                    _usuario.ModificadoPor = model.ModificadoPor;
                    _usuario.ModificadoFecha = dateTime_HN;
                    await _db.SaveAsync(_usuario);
                }
            }

            return _usuario;
        }




        private void CreatePasswordHash(string pPassword, out byte[]
            pPasswordHash, out byte[] pPasswordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            pPasswordSalt = hmac.Key;
            pPasswordHash = hmac.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(pPassword));
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
