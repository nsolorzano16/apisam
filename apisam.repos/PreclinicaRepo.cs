using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class PreclinicaRepo : IPreclinica
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public PreclinicaRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            //hondurasTime = TimeZoneInfo.Local;
        }

        public async Task<RespuestaMetodos> AddPreclinica(Preclinica preclinica)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                preclinica.CreadoFecha = dateTime_HN;
                preclinica.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Preclinica>(preclinica);
                // send notificacion

                var noti = new NotificacionesRepo();
                var lista = await _db.SelectAsync<Devices>(x => x.UsuarioId == preclinica.DoctorId);


                if (lista.Count > 0)
                {
                    lista.ForEach(async item =>
                    {
                        await noti.SendNoti(item.TokenDevice, "Preclinicas en lista de espera");
                    });

                }

                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public async Task<RespuestaMetodos> UpdatePreclinica(Preclinica preclinica)

        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                preclinica.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Preclinica>(preclinica);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public async Task<PreclinicaViewModel> GetInfoPreclinica(int id)
        {
            using var _db = dbFactory.Open();
            var _qry = $@"SELECT
                            p.PreclinicaId,
                            p.PacienteId,
                            p.DoctorId,
                            p.Peso,
                            p.Altura,
                            p.Temperatura,
                            p.FrecuenciaRespiratoria,
                            p.RitmoCardiaco,
                            p.PresionSistolica,
                            p.PresionDiastolica,
                            p.IMC,
                            p.Atendida,
                            p.PesoDescripcion,
                            p.Activo,
                            p.CreadoPor,
                            p.CreadoFecha,
                            p.ModificadoPor,
                            p.ModificadoFecha,
                            p.Notas,
                            pc.Nombres,
                            pc.PrimerApellido,
                            pc.SegundoApellido,
                            pc.Identificacion,
                            pc.Sexo,
                            pc.FechaNacimiento,
                            pc.EstadoCivil,
                            pc.Edad,
                            pc.MenorDeEdad,
                            pc.NombreMadre,
                            pc.IdentificacionMadre,
                            pc.NombrePadre,
                            pc.IdentificacionPadre,
                            pc.CarneVacuna,
                            pc.Notas as 'NotasPaciente',
                            pc.FotoUrl
                        FROM Preclinica p
                            INNER JOIN Paciente pc ON p.PacienteId = pc.PacienteId
                            WHERE p.PreclinicaId = ${id}";
            return await _db.SingleAsync<PreclinicaViewModel>(_qry);


        }



        public async Task<PageResponse<Preclinica>> GetPreclinicasPaginado
            (int pageNo, int limit, int doctorId)

        {
            var _response = new PageResponse<Preclinica>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@"SELECT * FROM Preclinica p
                          WHERE p.DoctorId = {doctorId} ";



            var _qry2 = _qry;
            _qry += " ORDER BY p.PreclinicaId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            using var _db = dbFactory.Open();
            var _preclinicas = await _db.SelectAsync<Preclinica>(_qry);
            if (limit > 0)
            {
                _response.TotalItems =
                    _db.Select<Preclinica>(_qry2).ToList().Count();
                _response.TotalPages
                    = (int)Math.Ceiling((decimal)_response.TotalItems / (decimal)limit);

                if (pageNo < _response.TotalPages)
                    _response.CurrentPage = pageNo;
                else
                    _response.CurrentPage = _response.TotalPages;

                _response.Items = _preclinicas;
                _response.ItemCount = _response.Items.Count;
            }

            return _response;
        }

        public async Task<PageResponse<PreclinicaViewModel>>
            GetPreclinicasSinAtender(int pageNo, int limit, int doctorId, int atendida)
        {

            var _response = new PageResponse<PreclinicaViewModel>();
            var _skip = limit * (pageNo - 1);

            var _qry = $@"SELECT
                                                        p.PreclinicaId,
                            p.PacienteId,
                            p.DoctorId,
                            p.Peso,
                            p.Altura,
                            p.Temperatura,
                            p.FrecuenciaRespiratoria,
                            p.RitmoCardiaco,
                            p.PresionSistolica,
                            p.PresionDiastolica,
                            p.IMC,
                            p.Atendida,
                            p.PesoDescripcion,
                            p.Activo,
                            p.CreadoPor,
                            p.CreadoFecha,
                            p.ModificadoPor,
                            p.ModificadoFecha,
                            p.Notas,
                            pc.Nombres,
                            pc.PrimerApellido,
                            pc.SegundoApellido,
                            pc.Identificacion,
                            pc.Sexo,
                            pc.FechaNacimiento,
                            pc.EstadoCivil,
                            pc.Edad,
                            pc.MenorDeEdad,
                            pc.NombreMadre,
                            pc.IdentificacionMadre,
                            pc.NombrePadre,
                            pc.IdentificacionPadre,
                            pc.CarneVacuna,
                            pc.Notas as 'NotasPaciente',
                            pc.FotoUrl
                        FROM Preclinica p
                            INNER JOIN Paciente pc ON p.PacienteId = pc.PacienteId
                            WHERE p.DoctorId = ${doctorId} AND p.Atendida = {atendida} AND p.Activo = 1";

            var _qry2 = _qry;
            _qry += " ORDER BY p.PreclinicaId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            using var _db = dbFactory.Open();
            var _preclinicas = await _db.SelectAsync<PreclinicaViewModel>(_qry);
            if (limit > 0)
            {
                _response.TotalItems =
                    _db.Select<PreclinicaViewModel>(_qry2).ToList().Count();
                _response.TotalPages
                    = (int)Math.Ceiling((decimal)_response.TotalItems / (decimal)limit);

                if (pageNo < _response.TotalPages)
                    _response.CurrentPage = pageNo;
                else
                    _response.CurrentPage = _response.TotalPages;

                _response.Items = _preclinicas;
                _response.ItemCount = _response.Items.Count;
            }

            return _response;
        }

    }
}
