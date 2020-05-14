using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IPais
    {
        Task<List<Pais>> Paises();
        Task<Pais> GetPaisById(int id);

    }
}
