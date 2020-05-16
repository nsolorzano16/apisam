using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IPlanTerapeutico
    {
        Task<RespuestaMetodos> AddPlanTerapeutico(PlanTerapeutico planTerapeutico);
        Task<RespuestaMetodos> UpdatePlanTerapeutico(PlanTerapeutico planTerapeutico);
        Task<PlanTerapeutico> GetPlanTerapeutico(int pacienteId, int doctorId, int preclinicaId);
        Task<List<PlanTerapeutico>> GetPlanes(int pacienteId, int doctorId, int preclinicaId);
        Task<List<PlanTerapeuticoViewModel>> GetPlanesLista(int pacienteId, int doctorId, int preclinicaId);
        Task<PlanTerapeuticoViewModel> GetPlanInfo(int planId);

    }
}
