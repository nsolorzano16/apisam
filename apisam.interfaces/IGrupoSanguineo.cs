using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IGrupoSanguineo
    {
        List<GrupoSanguineo> GruposSanguineos { get; }
      
        GrupoSanguineo GetGrupoSanguineoById(int id);
    }
}
