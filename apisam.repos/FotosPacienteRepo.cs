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

    public class FotosPacienteRepo : IFotosPaciente
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;


        public FotosPacienteRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }


      public async  Task<RespuestaMetodos> AddFoto(FotosPaciente foto) {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            var _db = dbFactory.Open();
            try
            {
                foto.CreadoFecha = dateTime_HN;
                foto.ModificadoFecha = dateTime_HN;
              await _db.SaveAsync<FotosPaciente>(foto);             
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;



        }
        public async Task<RespuestaMetodos> UpdateFoto(FotosPaciente foto) {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            var _db = dbFactory.Open();
            try
            {              
                foto.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<FotosPaciente>(foto);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

     public async   Task<PageResponse<FotosPaciente>> GetFotos(int pageNo, int limit, string filter,int pacienteId)
        {
            var _response = new PageResponse<FotosPaciente>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@"SELECT * FROM FotosPaciente f  WHERE f.PacienteId = {pacienteId} AND f.Activo = 1";

            if (!string.IsNullOrEmpty(filter)) _qry += $" AND (f.Notas LIKE '%{filter}%' ";

            var _qry2 = _qry;
            _qry += " ORDER BY f.CreadoFecha DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            using var _db = dbFactory.Open();
            var _fotos = await _db.SelectAsync<FotosPaciente>(_qry);

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

                _response.Items = _fotos;
                _response.ItemCount = _response.Items.Count;
            }

            return _response;
        }

        public async Task<int> ImagenesConsumidas(string userid)
        {
            using var _db = dbFactory.Open();
            return Convert.ToInt32(await _db.CountAsync<FotosPaciente>(x => x.Activo == true && x.UsuarioId==userid));
        }
    }
}
