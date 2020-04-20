using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IDiagnosticos
    {

        RespuestaMetodos AddDiagnostico(Diagnosticos diagnostico);
        RespuestaMetodos UpdateDiagnostico(Diagnosticos diagnostico);
        RespuestaMetodos AddDiagnosticoLista(List<Diagnosticos> diagnosticos);
        RespuestaMetodos UpdateDiagnosticoLista(List<Diagnosticos> diagnosticos);
        List<Diagnosticos> GetDiagnosticos(int pacienteId, int doctorId);

    }
}
