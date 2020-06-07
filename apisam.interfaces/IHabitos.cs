using System;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IHabitos
    {
        Task<RespuestaMetodos> AddAHabito(Habitos habito);
        Task<RespuestaMetodos> UpdateAHabito(Habitos habito);
        Task<Habitos> GetHabito(int pacienteId);

    }
}
