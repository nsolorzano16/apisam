using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenCategoria
    {
        Task<List<ExamenCategoria>> CategoriasExamenes();
        Task<ExamenCategoria> GetExamenById(int examenId);
    }
}
