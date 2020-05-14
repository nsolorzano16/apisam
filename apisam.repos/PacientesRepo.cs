using System;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.repositories;
using Microsoft.EntityFrameworkCore;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class PacientesRepo : IPaciente
    {

        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public PacientesRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");

        }


        public async Task<RespuestaMetodos> AddPaciente(Paciente paciente)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                var pacienteBuscado = await _db.SingleAsync<Paciente>
                 (x => x.Identificacion == paciente.Identificacion);

                if (pacienteBuscado == null)
                {
                    paciente.CreadoFecha = dateTime_HN;
                    paciente.ModificadoFecha = dateTime_HN;
                    paciente.Edad = CalculateAge(paciente.FechaNacimiento);
                    paciente.FotoUrl = "https://storagedesam.blob.core.windows.net/profilesphotos/avatar-default.png";
                    await _db.SaveAsync<Paciente>(paciente);
                    _resp.Ok = true;
                }
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }


        public async Task<RespuestaMetodos> UpdatePaciente(Paciente paciente)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                paciente.ModificadoFecha = dateTime_HN;
                paciente.Edad = CalculateAge(paciente.FechaNacimiento);
                using var _db = dbFactory.Open();
                await _db.SaveAsync<Paciente>(paciente);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public async Task<PacientesViewModel> GetInfoPaciente(int pacienteId)
        {
            var _qry = $@"SELECT
                            p.PacienteId,
                            p.DoctorId,
                            p.PaisId,
                            p.ProfesionId,
                            p.EscolaridadId,
                            p.ReligionId,
                            p.GrupoSanguineoId,
                            p.GrupoEtnicoId,
                            p.DepartamentoId,
                            p.MunicipioId,
                            p.DepartamentoResidenciaId,
                            p.MunicipioResidenciaId,
                            p.Nombres,
                            p.PrimerApellido,
                            p.SegundoApellido,
                            p.Identificacion,
                            p.Email,
                            p.Sexo,
                            p.FechaNacimiento,
                            p.EstadoCivil,
                            p.Edad,
                            p.Direccion,
                            p.Telefono1,
                            p.Telefono2,
                            p.NombreEmergencia,
                            p.TelefonoEmergencia,
                            p.Parentesco,
                            p.MenorDeEdad,
                            p.NombreMadre,
                            p.IdentificacionMadre,
                            p.NombrePadre,
                            p.IdentificacionPadre,
                            p.CarneVacuna,
                            p.FotoUrl,
                            pa.Nombre as 'Pais',
                            pr.Nombre as 'Profesion',
                            e.Nombre as 'Escolaridad',
                            r.Nombre as 'Religion',
                            gs.Nombre as 'Grupo Sanguineo',
                            ge.Nombre as 'Grupo Etnico',
                            d.Nombre as 'Departamento',
                            m.Nombre as 'Municipio',
                            dd.Nombre as 'DepartamentoResidencia',
                            mm.Nombre as 'MunicipioResidencia',
                            p.Activo,
                            p.CreadoPor,
                            p.CreadoFecha,
                            p.ModificadoPor,
                            p.ModificadoFecha,
                            p.Notas
                        FROM

                            Paciente p INNER JOIN Pais pa ON p.PaisId = pa.PaisId
                            INNER JOIN Profesion pr ON p.ProfesionId = pr.ProfesionId
                            INNER JOIN Escolaridad e ON p.EscolaridadId = e.EscolaridadId
                            INNER JOIN Religion r ON p.ReligionId = r.ReligionId
                            INNER JOIN GrupoSanguineo gs ON p.GrupoSanguineoId = gs.GrupoSanguineoId
                            INNER JOIN GrupoEtnico ge ON p.GrupoEtnicoId = ge.GrupoEtnicoId
                            LEFT JOIN Departamento d ON p.DepartamentoId = d.DepartamentoId
                            LEFT join Municipio m on p.MunicipioId = m.MunicipioId
                            LEFT JOIN Departamento dd ON p.DepartamentoResidenciaId = dd.DepartamentoId 
                            LEFT JOIN Municipio mm on p.MunicipioResidenciaId = mm.MunicipioId
                            WHERE p.PacienteId = {pacienteId}
                        ";
            using var _db = dbFactory.Open();

            return await _db.SingleAsync<PacientesViewModel>(_qry);


        }

        public async Task<Paciente> GetPacienteById(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleByIdAsync<Paciente>(id);
        }



        public async Task<Paciente> GetPacienteByIdentificacion(string identificacion)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleAsync<Paciente>(x => x.Identificacion == identificacion);
        }

        public async Task<PageResponse<PacientesViewModel>>
            GetPacientes(int pageNo, int limit, string filter, int doctorId)
        {
            using var _db = dbFactory.Open();
            var _response = new PageResponse<PacientesViewModel>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@"SELECT
                            p.PacienteId,
                            p.DoctorId,
                            p.PaisId,
                            p.ProfesionId,
                            p.EscolaridadId,
                            p.ReligionId,
                            p.GrupoSanguineoId,
                            p.GrupoEtnicoId,
                            p.DepartamentoId,
                            p.MunicipioId,
                            p.DepartamentoResidenciaId,
                            p.MunicipioResidenciaId,
                            p.Nombres,
                            p.PrimerApellido,
                            p.SegundoApellido,
                            p.Identificacion,
                            p.Email,
                            p.Sexo,
                            p.FechaNacimiento,
                            p.EstadoCivil,
                            p.Edad,
                            p.Direccion,
                            p.Telefono1,
                            p.Telefono2,
                            p.NombreEmergencia,
                            p.TelefonoEmergencia,
                            p.Parentesco,
                            p.MenorDeEdad,
                            p.NombreMadre,
                            p.IdentificacionMadre,
                            p.NombrePadre,
                            p.IdentificacionPadre,
                            p.CarneVacuna,
                            p.FotoUrl,
                            pa.Nombre as 'Pais',
                            pr.Nombre as 'Profesion',
                            e.Nombre as 'Escolaridad',
                            r.Nombre as 'Religion',
                            gs.Nombre as 'Grupo Sanguineo',
                            ge.Nombre as 'Grupo Etnico',
                            d.Nombre as 'Departamento',
                            m.Nombre as 'Municipio',
                            dd.Nombre as 'DepartamentoResidencia',
                            mm.Nombre as 'MunicipioResidencia',
                            p.Activo,
                            p.CreadoPor,
                            p.CreadoFecha,
                            p.ModificadoPor,
                            p.ModificadoFecha,
                            p.Notas
                        FROM

                            Paciente p INNER JOIN Pais pa ON p.PaisId = pa.PaisId
                            INNER JOIN Profesion pr ON p.ProfesionId = pr.ProfesionId
                            INNER JOIN Escolaridad e ON p.EscolaridadId = e.EscolaridadId
                            INNER JOIN Religion r ON p.ReligionId = r.ReligionId
                            INNER JOIN GrupoSanguineo gs ON p.GrupoSanguineoId = gs.GrupoSanguineoId
                            INNER JOIN GrupoEtnico ge ON p.GrupoEtnicoId = ge.GrupoEtnicoId
                            LEFT JOIN Departamento d ON p.DepartamentoId = d.DepartamentoId
                            LEFT join Municipio m on p.MunicipioId = m.MunicipioId
                            LEFT JOIN Departamento dd ON p.DepartamentoResidenciaId = dd.DepartamentoId 
                            LEFT JOIN Municipio mm on p.MunicipioResidenciaId = mm.MunicipioId
                            WHERE p.DoctorId = {doctorId}
                        ";

            if (!string.IsNullOrEmpty(filter)) _qry += $" AND (p.Nombres LIKE '%{filter}%' " +
                    $"OR p.PrimerApellido LIKE '%{filter}%' OR p.SegundoApellido LIKE '%{filter}%' " +
                    $"OR p.Identificacion LIKE '%{filter}%')";

            var _qry2 = _qry;
            _qry += " ORDER BY p.PacienteId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            var _pacientes = await _db.SelectAsync<PacientesViewModel>(_qry);

            if (limit > 0)
            {
                _response.TotalItems = _db.Select<PacientesViewModel>(_qry2).ToList().Count();

                _response.TotalPages
                    = (int)Math.Ceiling((decimal)_response.TotalItems / (decimal)limit);

                if (pageNo < _response.TotalPages)
                    _response.CurrentPage = pageNo;
                else
                    _response.CurrentPage = _response.TotalPages;

                _response.Items = _pacientes;
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
