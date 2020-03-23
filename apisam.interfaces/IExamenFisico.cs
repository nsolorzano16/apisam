using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenFisico
    {
        bool AddExamenFisico(ExamenFisico examen);
        bool UpdateExamenFisico(ExamenFisico examen);
        List<ExamenFisico> GetExamenes(int pacienteId, int doctorId);
    }
}
