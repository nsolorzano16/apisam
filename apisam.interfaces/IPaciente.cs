using System;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IPaciente
    {

        RespuestaMetodos AddPaciente(Paciente paciente);
        RespuestaMetodos UpdatePaciente(Paciente paciente);
        Paciente GetPacienteById(int id);
        Paciente GetPacienteByIdentificacion(string identificacion);
        PageResponse<PacientesViewModel>
               GetPacientes(int pageNo, int limit, string filter, int doctorId);
        PacientesViewModel GetInfoPaciente(int pacienteId);
    }


}
