using System;
using System.Collections.Generic;
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
        Task<PacientesViewModel> GetPacienteByIdentificacion(string identificacion);
        Task<PageResponse<PacientesViewModel>> GetPacientes(int pageNo, int limit, string filter);
        Task<PacientesViewModel> GetInfoPaciente(int pacienteId);
        bool ExistsPaciente(Paciente paciente);

        Task <List<PacientesViewModel>> PacientesByCreadoPor(string creadoPor);

    }


}
