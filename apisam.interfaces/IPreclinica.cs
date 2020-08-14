using System;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IPreclinica
    {
        Task<RespuestaMetodos> AddPreclinica(Preclinica preclinica);
        Task<RespuestaMetodos> UpdatePreclinica(Preclinica preclinica);

        Task<PageResponse<Preclinica>>
            GetPreclinicasPaginado(int pageNo, int limit, int doctorId);

        Task<PageResponse<PreclinicaViewModel>> GetPreclinicasSinAtender
            (int pageNo, int limit, int doctorId, int atendida);

        Task<PreclinicaViewModel> GetInfoPreclinica(int id);

        Task<int> GetTotalConsultasAtendidas(int doctorId);
    }
}
