using apisam.entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace apisam.interfaces
{
    public interface IFotosPaciente
    {
        Task<RespuestaMetodos> AddFoto(FotosPaciente foto);
        Task<RespuestaMetodos> UpdateFoto(FotosPaciente foto);
        Task<PageResponse<FotosPaciente>> GetFotos(int pageNo, int limit, string filter,int pacienteId);

        Task<int> ImagenesConsumidas(string userid);

    }
}
