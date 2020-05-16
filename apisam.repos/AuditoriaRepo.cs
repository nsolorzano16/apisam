using System;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class AuditoriaRepo : IAuditoria
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public AuditoriaRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            //hondurasTime = TimeZoneInfo.Local;
        }


        public async Task<PageResponse<Auditoria>>
          GetAuditorias(int pageNo, int limit, string filter)
        {
            using var _db = dbFactory.Open();
            var _response = new PageResponse<Auditoria>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@"SELECT * FROM Auditoria a";

            if (!string.IsNullOrEmpty(filter)) _qry += $"  WHERE (a.Accion LIKE '%{filter}%' " +
                    $"OR a.Tabla LIKE '%{filter}%' OR a.NombreUsuario LIKE '%{filter}%' " +
                    $"OR a.FechaHora LIKE '%{filter}%')";

            var _qry2 = _qry;
            _qry += " ORDER BY a.CodigoAuditoriaId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            var _auditorias = await _db.SelectAsync<Auditoria>(_qry);
            _auditorias.ForEach(x =>
            x.FechaHora = TimeZoneInfo.ConvertTimeFromUtc(x.FechaHora, hondurasTime)
            );

            if (limit > 0)
            {
                _response.TotalItems = _db.Select<Auditoria>(_qry2).ToList().Count();

                _response.TotalPages
                    = (int)Math.Ceiling((decimal)_response.TotalItems / (decimal)limit);

                if (pageNo < _response.TotalPages)
                    _response.CurrentPage = pageNo;
                else
                    _response.CurrentPage = _response.TotalPages;

                _response.Items = _auditorias;
                _response.ItemCount = _response.Items.Count;
            }

            return _response;

        }

    }
}
