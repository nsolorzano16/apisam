using System;
using System.Collections.Generic;
using System.Configuration;
using apisam.interfaces;
using ServiceStack.OrmLite;
using System.Linq;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.repositories
{
    public class UsuariosRepo : IUsuario
    {


        private OrmLiteConnectionFactory DbFactory;
        private readonly Conexion con = new Conexion();
        public UsuariosRepo()
        {

            var _connString = con.GetConnectionString();
            DbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public List<Usuario> Usuarios
        {
            get
            {
                var _db = DbFactory.Open();
                return _db.Select<Usuario>().ToList();
            }
        }

        public bool AddUsuario(Usuario usuario)
        {
            var _flag = false;
            var _db = DbFactory.Open();

            var usuarioBuscado = _db.Select<Usuario>().FirstOrDefault(x =>
            x.UserName == usuario.UserName &&
             x.Identificacion == usuario.Identificacion);
            if (usuario != null)
            {
                CreatePasswordHash(usuario.Password, out byte[] _passwordHash, out byte[] _passwordSalt);


                usuario.PasswordHash = _passwordHash;
                usuario.PasswordSalt = _passwordSalt;
                usuario.CreadoFecha = DateTime.Now;
                usuario.ModificadoFecha = DateTime.Now;
                usuario.Password = "";
                usuario.Edad = CalculateAge(usuario.FechaNacimiento);

                _db.Save<Usuario>(usuario);
                _flag = true;


            }
            return _flag;


        }

        public bool UpdateUsuario(Usuario usuario)
        {
            var _flag = false;
            var _db = DbFactory.Open();

            var usuarioBuscado = _db.Select<Usuario>().FirstOrDefault(x =>
            x.UserName == usuario.UserName &&
             x.Identificacion == usuario.Identificacion);
            if (usuario != null) return _flag;
            usuario.ModificadoFecha = DateTime.Now;
            _db.Save(usuario);
            _flag = true;
            return _flag;


        }

        public Usuario GetUsuarioByUserName(LoginViewModel usuario)
        {
            var _db = DbFactory.Open();
            var user = _db.Select<Usuario>().FirstOrDefault(
                x => x.UserName == usuario.Usuario &&
            x.Activo == true);
            if (user != null) return user;

            return null;
        }






        private void CreatePasswordHash(string pPassword, out byte[]
            pPasswordHash, out byte[] pPasswordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            pPasswordSalt = hmac.Key;
            pPasswordHash = hmac.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(pPassword));
        }

        private bool VerificarPasswordHash(string password,
            byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            var hmac = new
                  System.Security.Cryptography.HMACSHA512(passwordSalt);
            var passwordHashNuevo =
                hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return new
                ReadOnlySpan<byte>(passwordHashAlmacenado)
                .SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
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
