using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IPlanTerapeutico
    {
        RespuestaMetodos AddPlanTerapeutico(PlanTerapeutico planTerapeutico);
        RespuestaMetodos UpdatePlanTerapeutico(PlanTerapeutico planTerapeutico);
        PlanTerapeutico GetPlanTerapeutico(int pacienteId, int doctorId, int preclinicaId);
        List<PlanTerapeutico> GetPlanes(int pacienteId, int doctorId, int preclinicaId);
    }
}
