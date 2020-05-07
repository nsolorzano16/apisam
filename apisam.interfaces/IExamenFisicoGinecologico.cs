using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenFisicoGinecologico
    {
        RespuestaMetodos AddExamenFisicoGinecologico(ExamenFisicoGinecologico examen);
        RespuestaMetodos UpdateExamenFisicoGinecologico(ExamenFisicoGinecologico examen);
        List<ExamenFisicoGinecologico> GetExamenesGinecologicos(int pacienteId, int doctorId);
        ExamenFisicoGinecologico GetExamenGinecologico(int pacienteId, int doctorId, int preclinicaId);
    }
}
