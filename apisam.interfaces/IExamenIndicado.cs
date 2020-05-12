using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenIndicado
    {
        RespuestaMetodos AddExamenIndicado(ExamenIndicado examen);
        RespuestaMetodos UpdateExamenIndicado(ExamenIndicado examen);
        ExamenIndicado GetExamenIndicadoById(int examenId);
        List<ExamenIndicado> GetExamenes(int pacienteId, int doctorId, int preclinicaId);
    }
}
