using System;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class HistorialGinecoObstetraRepo : IHistorialGinecoObstetra
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public HistorialGinecoObstetraRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<RespuestaMetodos> AddAHistorial(HistorialGinecoObstetra historial)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                historial.CreadoFecha = dateTime_HN;
                historial.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<HistorialGinecoObstetra>(historial);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;

        }
        public async Task<RespuestaMetodos> UpdateAHistorial(HistorialGinecoObstetra historial)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                historial.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<HistorialGinecoObstetra>(historial);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public async Task<HistorialGinecoObstetra> GetHistorial(int pacienteId)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleAsync<HistorialGinecoObstetra>
                 (x => x.PacienteId == pacienteId && x.Activo == true);

        }

        public async Task<HistorialGinecoViewModel> GetDetalleHistorial(int pacienteId)
        {
            using var _db = dbFactory.Open();
            var _qry = $@"SELECT
                                        h.HistorialId,
                                        h.PacienteId,
                                        h.AnticonceptivoId,
                                        h.FechaMenarquia,
                                        h.Fum,
                                        h.G,
                                        h.P,
                                        h.C,
                                        h.Hv,
                                        h.Hm,
                                        h.DescripcionAnticonceptivos,
                                        h.VacunaVph,
                                        h.FechaMenopausia,
                                        a.Nombre as 'AnticonceptivoTexto',
                                        h.Activo,
                                        h.CreadoPor,
                                        h.CreadoFecha,
                                        h.ModificadoPor,
                                        h.ModificadoFecha,
                                        h.Notas,
                                        h.PreclinicaId
                                        FROM HistorialGinecoObstetra h 
                                        INNER JOIN Anticonceptivos a ON h.AnticonceptivoId = a.AnticonceptivoId
                                        WHERE h.PacienteId = {pacienteId} AND h.Activo = 1";
            return await _db.SingleAsync<HistorialGinecoViewModel> (_qry);

        }
    }
}
