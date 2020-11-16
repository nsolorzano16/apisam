using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class NotasRepo : INotas
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public NotasRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<RespuestaMetodos> AddNota(Notas nota)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                nota.CreadoFecha = dateTime_HN;
                nota.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Notas>(nota);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public async Task<RespuestaMetodos> UpdateNota(Notas nota)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                nota.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Notas>(nota);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public async Task<RespuestaMetodos> AddNotaLista(List<Notas> notas)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                notas.ForEach(x =>
                {
                    x.CreadoFecha = dateTime_HN;
                    x.ModificadoFecha = dateTime_HN;
                });
                using var _db = dbFactory.Open();
                await _db.SaveAllAsync<Notas>(notas);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public async Task<RespuestaMetodos> UpdateNotaLista(List<Notas> notas)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                notas.ForEach(x =>
                {
                    x.ModificadoFecha = dateTime_HN;
                });
                using var _db = dbFactory.Open();
                await _db.SaveAllAsync<Notas>(notas);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public async Task<List<Notas>> GetNotas(int pacienteId, string doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Notas>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.PreclinicaId == preclinicaId && x.Activo == true);
        }
    }
}
