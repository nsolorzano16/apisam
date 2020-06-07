using System;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IHistorialGinecoObstetra
    {
        Task<RespuestaMetodos> AddAHistorial(HistorialGinecoObstetra historial);
        Task<RespuestaMetodos> UpdateAHistorial(HistorialGinecoObstetra historial);
        Task<HistorialGinecoObstetra> GetHistorial(int pacienteId);

    }
}
