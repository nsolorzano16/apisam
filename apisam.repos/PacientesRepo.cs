using System;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class PacientesRepo : IPaciente
    {

        private readonly AppDbContext _AppDbContext;
        public PacientesRepo(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;

        }


        public bool AddPaciente(Paciente paciente)
        {
            var _flag = false;


            var pacienteBuscado = _AppDbContext
                .Paciente.FirstOrDefault(x => x.Identificacion == paciente.Identificacion);


            if (pacienteBuscado == null)
            {
                paciente.CreadoFecha = DateTime.Now;
                paciente.ModificadoFecha = DateTime.Now;
                paciente.Edad = CalculateAge(paciente.FechaNacimiento);
                paciente.FotoUrl = "https://storagedesam.blob.core.windows.net/profilesphotos/avatar-default.png";
                _AppDbContext.Paciente.Add(paciente);
                _AppDbContext.SaveChanges();
                _flag = true;
            }
            return _flag;
        }






        public bool UpdatePaciente(Paciente paciente)
        {

            paciente.ModificadoFecha = DateTime.Now;
            paciente.Edad = CalculateAge(paciente.FechaNacimiento);
            _AppDbContext.Paciente.Update(paciente);
            _AppDbContext.SaveChanges();
            bool _flag = true;
            return _flag;
        }

        public Paciente GetPacienteById(int id)
        {

            var _user = _AppDbContext.Paciente.FirstOrDefault<Paciente>(x => x.PacienteId == id);
            if (_user != null) return _user;
            return null;
        }

        public Paciente GetPacienteByIdentificacion(string identificacion)
        {

            var _user = _AppDbContext.Paciente.FirstOrDefault(x => x.Identificacion == identificacion);
            if (_user != null) return _user;
            return null;
        }

        public PageResponse<Paciente>
            GetPacientes(int pageNo, int limit, string filter)
        {
            var _response = new PageResponse<Paciente>();
            var _skip = limit * (pageNo - 1);


            var _qry = $@"SELECT * FROM Paciente p";

            if (!string.IsNullOrEmpty(filter)) _qry += $" WHERE (p.Nombres LIKE '%{filter}%' " +
                    $"OR p.PrimerApellido LIKE '%{filter}%' OR p.SegundoApellido LIKE '%{filter}%' " +
                    $"OR p.Identificacion LIKE '%{filter}%')";

            var _qry2 = _qry;
            _qry += " ORDER BY p.PacienteId DESC";
            _qry += $" OFFSET {_skip} ROWS";
            _qry += $" FETCH NEXT {limit} ROWS ONLY";

            var _pacientes = _AppDbContext.Paciente.FromSqlRaw(_qry).ToList();
            if (limit > 0)
            {
                _response.TotalItems =
                    _AppDbContext.Paciente.FromSqlRaw(_qry2).ToList().Count();
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
