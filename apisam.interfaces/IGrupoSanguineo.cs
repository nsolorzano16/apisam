using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IGrupoSanguineo
    {
        Task<List<GrupoSanguineo>> GruposSanguineos();
        Task<GrupoSanguineo> GetGrupoSanguineoById(int id);

    }
}
