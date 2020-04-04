using System;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IPreclinica
    {
        bool AddPreclinica(Preclinica preclinica);
        bool UpdatePreclinica(Preclinica preclinica);
        PageResponse<Preclinica>
            GetPreclinicasPaginado(int pageNo, int limit, int doctorId);
        PageResponse<PreclinicaViewModel> GetPreclinicasSinAtender(int pageNo, int limit, int doctorId);

        PreclinicaViewModel GetInfoPreclinica(int id);
    }
}
