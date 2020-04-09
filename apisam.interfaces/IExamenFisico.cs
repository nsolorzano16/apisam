using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenFisico
    {
        RespuestaMetodos AddExamenFisico(ExamenFisico examen);
        RespuestaMetodos UpdateExamenFisico(ExamenFisico examen);
        List<ExamenFisico> GetExamenes(int pacienteId, int doctorId);
    }
}
