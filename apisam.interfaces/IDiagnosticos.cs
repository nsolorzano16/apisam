using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IDiagnosticos
    {

        bool AddDiagnostico(Diagnosticos diagnostico);
        bool UpdateDiagnostico(Diagnosticos diagnostico);
        bool AddDiagnosticoLista(List<Diagnosticos> diagnosticos);
        List<Diagnosticos> GetDiagnosticos(int pacienteId, int doctorId);

    }
}
