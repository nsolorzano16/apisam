using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenFisicoGinecologico
    {
        bool AddExamenFisicoGinecologico(ExamenFisicoGinecologico examen);
        bool UpdateExamenFisicoGinecologico(ExamenFisicoGinecologico examen);
        List<ExamenFisicoGinecologico> GetExamenesGinecologicos(int pacienteId, int doctorId);
    }
}
