using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IConsulta
    {
        Task<ConsultaViewModel> GetDetalleConsulta(int doctorId, int pacienteId, int preclinicaId);
        Task<RespuestaMetodos> AddConsultaGeneral(ConsultaGeneral consulta);
        Task<RespuestaMetodos> UpdateConsultaGeneral(ConsultaGeneral consulta);
        Task<ConsultaGeneral> GetConsultaGeneral(int pacienteId, int doctorId, int preclinicaId);
       Task<ExpedienteViewModel> GetExpediente(int pacienteId, int doctorId);
        string PacienteEstadoCivil(string sexo);
    }
}
