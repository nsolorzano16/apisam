using System;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IPaciente
    {

        Task<RespuestaMetodos> AddPaciente(Paciente paciente);
        Task<RespuestaMetodos> UpdatePaciente(Paciente paciente);
        Task<Paciente> GetPacienteById(int id);
        Task<Paciente> GetPacienteByIdentificacion(string identificacion);
        Task<PageResponse<PacientesViewModel>> GetPacientes(int pageNo, int limit, string filter, int doctorId);
        Task<PacientesViewModel> GetInfoPaciente(int pacienteId);
    }


}
