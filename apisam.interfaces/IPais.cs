using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IPais
    {
        List<Pais> Paises { get; }
        Pais GetPaisById(int id);

    }
}
