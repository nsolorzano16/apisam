using System;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IHistorialGinecoObstetra
    {
        Task<RespuestaMetodos> AddAHistorial(HistorialGinecoObstetra historial);
        Task<RespuestaMetodos> UpdateAHistorial(HistorialGinecoObstetra historial);
        Task<HistorialGinecoObstetra> GetHistorial(int pacienteId);
        Task<HistorialGinecoViewModel> GetDetalleHistorial(int pacienteId);

    }
}
