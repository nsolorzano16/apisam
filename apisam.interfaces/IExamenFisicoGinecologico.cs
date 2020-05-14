using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenFisicoGinecologico
    {
        Task<RespuestaMetodos> AddExamenFisicoGinecologico(ExamenFisicoGinecologico examen);
        Task<RespuestaMetodos> UpdateExamenFisicoGinecologico(ExamenFisicoGinecologico examen);
        Task<List<ExamenFisicoGinecologico>> GetExamenesGinecologicos(int pacienteId, int doctorId);
        Task<ExamenFisicoGinecologico> GetExamenGinecologico(int pacienteId, int doctorId, int preclinicaId);
    }
}
