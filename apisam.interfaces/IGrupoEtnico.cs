using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IGrupoEtnico
    {
        List<GrupoEtnico> GruposEtnicos { get; }
        RespuestaMetodos Add(GrupoEtnico grupo);
        RespuestaMetodos Update(GrupoEtnico grupo);
        GrupoEtnico GetGrupoEtnicoById(int id);
    }
}
