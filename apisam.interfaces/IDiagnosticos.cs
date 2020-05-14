using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IDiagnosticos
    {

        Task<RespuestaMetodos> AddDiagnostico(Diagnosticos diagnostico);
        Task<RespuestaMetodos> UpdateDiagnostico(Diagnosticos diagnostico);
        Task<RespuestaMetodos> AddDiagnosticoLista(List<Diagnosticos> diagnosticos);
        Task<RespuestaMetodos> UpdateDiagnosticoLista(List<Diagnosticos> diagnosticos);
        Task<List<Diagnosticos>> GetDiagnosticos(int pacienteId, int doctorId, int preclinicaId);

    }
}
