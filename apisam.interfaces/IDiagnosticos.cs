using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IDiagnosticos
    {

        Task<RespuestaMetodos> AddDiagnostico(Diagnosticos diagnostico);
        Task<RespuestaMetodos> UpdateDiagnostico(Diagnosticos diagnostico);
        Task<RespuestaMetodos> AddDiagnosticoLista(List<Diagnosticos> diagnosticos);
        Task<RespuestaMetodos> UpdateDiagnosticoLista(List<Diagnosticos> diagnosticos);
        Task<List<DiagnosticosViewModel>> GetDiagnosticos(int pacienteId, int doctorId, int preclinicaId);

        Task<DiagnosticosViewModel> GetDiagnostico(int diagnosticoId);


    }
}
