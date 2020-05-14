using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IGrupoEtnico
    {
        Task<List<GrupoEtnico>> GruposEtnicos();
        Task<RespuestaMetodos> Add(GrupoEtnico grupo);
        Task<RespuestaMetodos> Update(GrupoEtnico grupo);
        Task<GrupoEtnico> GetGrupoEtnicoById(int id);
    }
}
