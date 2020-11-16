using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenFisico
    {
        Task<RespuestaMetodos> AddExamenFisico(ExamenFisico examen);
        Task<RespuestaMetodos> UpdateExamenFisico(ExamenFisico examen);
        Task<List<ExamenFisico>> GetExamenes(int pacienteId, string doctorId);
        Task<ExamenFisico> GetExamenFisico(int pacienteId, string doctorId, int preclinicaId);
    }
}
