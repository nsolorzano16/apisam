using System;
using System.Linq;
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

        public PreclinicaRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public bool AddPreclinica(Preclinica preclinica)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                preclinica.CreadoFecha = DateTime.Now.ToLocalTime();
                preclinica.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Preclinica>(preclinica);
                _flag = true;
            }

            return _flag;
        }
        public bool UpdatePreclinica(Preclinica preclinica)

        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {

                preclinica.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Preclinica>(preclinica);
                _flag = true;
            }

            return _flag;
        }
        public PageResponse<Preclinica> GetPreclinicasPaginado
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
            var _usuarios = _db.Select<Preclinica>(_qry).ToList();
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

                _response.Items = _usuarios;
                _response.ItemCount = _response.Items.Count;
            }

            return _response;
        }

        public PageResponse<PreclinicaViewModel> GetPreclinicasSinAtender(int pageNo, int limit, int doctorId)
        {

            var _response = new PageResponse<PreclinicaViewModel>();
            var _skip = limit * (pageNo - 1);

            var _qry = $@"SELECT
                            p.PreclinicaId,
                            p.PacienteId,
                            p.DoctorId,
                            p.Peso,
                            p.Altura,
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
                            pc.TipoDeSangre,
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
                            WHERE p.DoctorId = ${doctorId} AND p.Atendida = 0";

            var _qry2 = _qry;
            _qry += " ORDER BY p.PreclinicaId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            using var _db = dbFactory.Open();
            var _preclinicas = _db.Select<PreclinicaViewModel>(_qry).ToList();
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
