using System;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IPaciente
    {

        bool AddPaciente(Paciente paciente);
        bool UpdatePaciente(Paciente paciente);
        Paciente GetPacienteById(int id);
        Paciente GetPacienteByIdentificacion(string identificacion);
        PageResponse<Paciente>
               GetPacientes(int pageNo, int limit, string filter);
    }


}
