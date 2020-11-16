namespace apisam.repositories
{
    using apisam.entities;
    using apisam.entities.ViewModels.UsuariosTable;
    using apisam.interfaces;
    using ServiceStack.OrmLite;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsuariosRepo : IUsuario
    {
        private readonly OrmLiteConnectionFactory dbFactory;

        private readonly Conexion con = new Conexion();

        private static TimeZoneInfo hondurasTime;

        public UsuariosRepo()
        {

            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public List<AspNetRoles> Roles
        {
            get
            {
                var _db = dbFactory.Open();
                return _db.Select<AspNetRoles>().ToList();
            }
        }

        public async Task<EditUserViewModel> GetMyInfo(string id)
        {

            var _qry = $@" SELECT
                                             u.Id,
                                             u.UserName,
                                             u.Email,
                                             u.PhoneNumber,
                                             u.rolId,
                                             u.AsistenteId,
                                             u.PlanId,
                                             u.Nombres,
                                             u.PrimerApellido,
                                             u.SegundoApellido,
                                             u.Identificacion,
                                             u.FechaNacimiento,
                                             u.Edad,
                                             u.Sexo,
                                             u.Telefono2,
                                             u.ColegioNumero,
                                             u.FotoUrl,
                                             u.Activo,
                                             u.CreadoPor,
                                             u.CreadoFecha,
                                             u.ModificadoPor,
                                             u.ModificadoFecha, 
                                             u.Notas,
                                             u.LockoutEnd,
                                             u.AccessFailedCount,
                                             IIF(u.AccessFailedCount = 3, 1, 0) AS 'Locked'	
                                             FROM AspNetUsers u WHERE u.Id = '{id}' ";
            var _db = dbFactory.Open();
            return await _db.SingleAsync<EditUserViewModel>(_qry);
        }

        public async Task<PageResponse<EditUserViewModel>> GetAsistentes(int pageNo, int limit, string filter, string doctorId)
        {
            var _response = new PageResponse<EditUserViewModel>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@" SELECT
                                             u.Id,
                                             u.UserName,
                                             u.Email,
                                             u.PhoneNumber,
                                             u.rolId,
                                             u.AsistenteId,
                                             u.PlanId,
                                             u.Nombres,
                                             u.PrimerApellido,
                                             u.SegundoApellido,
                                             u.Identificacion,
                                             u.FechaNacimiento,
                                             u.Edad,
                                             u.Sexo,
                                             u.Telefono2,
                                             u.ColegioNumero,
                                             u.FotoUrl,
                                             u.Activo,
                                             u.CreadoPor,
                                             u.CreadoFecha,
                                             u.ModificadoPor,
                                             u.ModificadoFecha, 
                                             u.Notas,
                                             u.LockoutEnd,
                                             u.AccessFailedCount,
                                             IIF(u.AccessFailedCount = 3, 1, 0) AS 'Locked'	
                                             FROM AspNetUsers u WHERE u.AsistenteId = '{doctorId}' ";

            if (!string.IsNullOrEmpty(filter)) _qry += $" AND (u.Nombres LIKE '%{filter}%' " +
                    $"OR u.PrimerApellido LIKE '%{filter}%' OR u.SegundoApellido LIKE '%{filter}%')";

            var _qry2 = _qry;
            _qry += " ORDER BY u.CreadoFecha DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            using var _db = dbFactory.Open();
            var _usuarios = await _db.SelectAsync<EditUserViewModel>(_qry);

            if (limit > 0)
            {
                _response.TotalItems =
                    _db.Select<EditUserViewModel>(_qry2).ToList().Count();
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

        public async Task<PageResponse<EditUserViewModel>> GetUsuarios(int pageNo, int limit, string filter)
        {
            var _response = new PageResponse<EditUserViewModel>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@" SELECT
                                         u.Id,
                                         u.UserName,
                                         u.Email,
                                         u.PhoneNumber,
                                         u.rolId,
                                         u.AsistenteId,
                                         u.PlanId,
                                         u.Nombres,
                                         u.PrimerApellido,
                                         u.SegundoApellido,
                                         u.Identificacion,
                                         u.FechaNacimiento,
                                         u.Edad,
                                         u.Sexo,
                                         u.Telefono2,
                                         u.ColegioNumero,
                                         u.FotoUrl,
                                         u.Activo,
                                         u.CreadoPor,
                                         u.CreadoFecha,
                                         u.ModificadoPor,
                                         u.ModificadoFecha, 
                                         u.Notas,
                                         u.LockoutEnd,
                                         u.AccessFailedCount,
                                         IIF(u.AccessFailedCount = 3, 1, 0) AS 'Locked'	
                                         FROM AspNetUsers u";

            if (!string.IsNullOrEmpty(filter)) _qry += $"  WHERE (u.Nombres LIKE '%{filter}%' " +
                    $"OR u.PrimerApellido LIKE '%{filter}%' OR u.SegundoApellido LIKE '%{filter}%')";

            var _qry2 = _qry;
            _qry += " ORDER BY u.CreadoFecha DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            using var _db = dbFactory.Open();
            var _usuarios = await _db.SelectAsync<EditUserViewModel>(_qry);
            if (limit > 0)
            {
                _response.TotalItems =
                    _db.Select<EditUserViewModel>(_qry2).ToList().Count();
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

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age -= 1;

            return age;
        }
    }
}
