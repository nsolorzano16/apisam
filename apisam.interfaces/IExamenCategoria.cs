using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenCategoria
    {
        List<ExamenCategoria> CategoriasExamenes { get; }
        ExamenCategoria GetExamenById(int examenId);
    }
}
