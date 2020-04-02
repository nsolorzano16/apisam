using System;
using System.Collections.Generic;
using System.Configuration;
using apisam.interfaces;
using ServiceStack.OrmLite;
using System.Linq;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.entities.ViewModels.UsuariosTable;

namespace apisam.repositories
{
    public class UsuariosRepo : IUsuario
    {


        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();

        public UsuariosRepo()
        {

            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public List<Usuario> Usuarios
        {
            get
            {
                var _db = dbFactory.Open();
                return _db.Select<Usuario>().ToList();
            }
        }

        public List<Rol> Roles
        {
            get
            {
                var _db = dbFactory.Open();
                return _db.Select<Rol>().ToList();
            }
        }

        public bool AddUsuario(Usuario usuario)
        {
            var _flag = false;
            var _db = dbFactory.Open();

            var usuarioBuscado = _db.Select<Usuario>().FirstOrDefault(x =>
            x.UserName == usuario.UserName ||
             x.Identificacion == usuario.Identificacion || x.Email == usuario.Email);
            if (usuarioBuscado == null)
            {
                CreatePasswordHash(usuario.Password, out byte[] _passwordHash, out byte[] _passwordSalt);


                usuario.PasswordHash = _passwordHash;
                usuario.PasswordSalt = _passwordSalt;
                usuario.CreadoFecha = DateTime.Now.ToLocalTime();
                usuario.ModificadoFecha = DateTime.Now.ToLocalTime();
                usuario.Password = "";
                usuario.Edad = CalculateAge(usuario.FechaNacimiento);
                usuario.FotoUrl = "https://storagedesam.blob.core.windows.net/profilesphotos/avatar-default.png";

                _db.Save<Usuario>(usuario);
                _flag = true;


            }
            return _flag;


        }

        public bool UpdateUsuario(Usuario usuario)
        {
            var _db = dbFactory.Open();
            usuario.ModificadoFecha = DateTime.Now.ToLocalTime();
            usuario.Edad = CalculateAge(usuario.FechaNacimiento);
            _db.Save(usuario);
            bool _flag = true;
            return _flag;


        }

        public Usuario GetUsuarioByUserName(LoginViewModel usuario)
        {
            var _db = dbFactory.Open();
            var user = _db.Select<Usuario>().FirstOrDefault(
                x => x.UserName == usuario.Usuario &&
            x.Activo == true);
            if (user != null) return user;

            return null;
        }

        public Usuario GerUserById(int id)
        {
            var _db = dbFactory.Open();
            var _user = _db.Select<Usuario>
                ().FirstOrDefault(x => x.UsuarioId == id);
            if (_user != null) return _user;
            return null;
        }

        public PageResponse<Usuario>
            GetAsistentes(int pageNo, int limit, string filter, int doctorId)
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
            var _usuarios = _db.Select<Usuario>(_qry).ToList();
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


        public Usuario UpdatePassword(UserChangePassword model)
        {
            var _usuario = new Usuario();
            using (var _db = dbFactory.Open())
            {
                _usuario = _db.Select<Usuario>().FirstOrDefault(x => x.UsuarioId == model.Id);
                if (_usuario != null)
                {
                    CreatePasswordHash(model.Password, out byte[] _passwordHash, out byte[] _passwordSalt);
                    _usuario.PasswordHash = _passwordHash;
                    _usuario.PasswordSalt = _passwordSalt;
                    _usuario.ModificadoPor = model.ModificadoPor;
                    _usuario.ModificadoFecha = DateTime.Now.ToLocalTime();
                    _db.Save(_usuario);



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
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }


    }
}
