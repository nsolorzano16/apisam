using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IExamenIndicado
    {
        Task<RespuestaMetodos> AddExamenIndicado(ExamenIndicado examen);
        Task<RespuestaMetodos> UpdateExamenIndicado(ExamenIndicado examen);
        Task<ExamenIndicado> GetExamenIndicadoById(int examenId);
        Task<List<ExamenIndicado>> GetExamenes(int pacienteId, int doctorId, int preclinicaId);
        Task<List<ExamenesIndicadosViewModel>> GetDetalleExamenesIndicados(int pacienteId, int doctorId, int preclinicaId);
        Task<ExamenesIndicadosViewModel> GetInfoExamenIndicado(int id);
    }
}
