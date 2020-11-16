using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IConsulta
    {
        Task<ConsultaViewModel> GetDetalleConsulta(string doctorId, int pacienteId, int preclinicaId);
        Task<RespuestaMetodos> AddConsultaGeneral(ConsultaGeneral consulta);
        Task<RespuestaMetodos> UpdateConsultaGeneral(ConsultaGeneral consulta);
        Task<ConsultaGeneral> GetConsultaGeneral(int pacienteId, string doctorId, int preclinicaId);
       Task<ExpedienteViewModel> GetExpediente(int pacienteId, string doctorId);
       Task<List<ConsultaExpedienteViewModel>> GetConsultasByDoctorId(string doctorId);
        
        
       
        string PacienteEstadoCivil(string sexo);
    }
}
