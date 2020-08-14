using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apisam.repos
{
  public  class CieRepo : ICie
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();

        public CieRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public async   Task<PageResponse<CIE>> GetEnfermedades(int pageNo, int limit, string filter)
        {
            using var _db = dbFactory.Open();
            var _response = new PageResponse<CIE>();
            var _skip = limit * (pageNo - 1);

            var _qry = $@"SELECT * FROM CIE c";

            if (!string.IsNullOrEmpty(filter)) _qry += $" WHERE c.Codigo LIKE '%{filter}%' OR c.Nombre LIKE '%{filter}%' ";

            var _qry2 = _qry;
            _qry += " ORDER BY c.CieId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            var _enfermedades = await _db.SelectAsync<CIE>(_qry);

            if (limit > 0)
            {
                _response.TotalItems = _db.Select<CIE>(_qry2).ToList().Count();


                _response.TotalPages
                    = (int)Math.Ceiling((decimal)_response.TotalItems / (decimal)limit);

                if (pageNo < _response.TotalPages)
                    _response.CurrentPage = pageNo;
                else
                    _response.CurrentPage = _response.TotalPages;

                _response.Items = _enfermedades;
                _response.ItemCount = _response.Items.Count;
            }

            return _response;
        }
    }
}
