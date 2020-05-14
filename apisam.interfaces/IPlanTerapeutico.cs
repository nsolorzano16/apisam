using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IPlanTerapeutico
    {
        Task<RespuestaMetodos> AddPlanTerapeutico(PlanTerapeutico planTerapeutico);
        Task<RespuestaMetodos> UpdatePlanTerapeutico(PlanTerapeutico planTerapeutico);
        Task<PlanTerapeutico> GetPlanTerapeutico(int pacienteId, int doctorId, int preclinicaId);
        Task<List<PlanTerapeutico>> GetPlanes(int pacienteId, int doctorId, int preclinicaId);
    }
}
