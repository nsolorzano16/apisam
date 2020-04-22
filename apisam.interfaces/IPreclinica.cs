using System;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IPreclinica
    {
        RespuestaMetodos AddPreclinica(Preclinica preclinica);
        RespuestaMetodos UpdatePreclinica(Preclinica preclinica);

        PageResponse<Preclinica>
            GetPreclinicasPaginado(int pageNo, int limit, int doctorId);

        PageResponse<PreclinicaViewModel> GetPreclinicasSinAtender
            (int pageNo, int limit, int doctorId, int atendida);

        PreclinicaViewModel GetInfoPreclinica(int id);
    }
}
