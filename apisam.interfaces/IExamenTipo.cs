using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenTipo
    {
        Task<List<ExamenTipo>> GetTipoExamenes(int examenCategoriaId);
        Task<ExamenTipo> GetExamenTipoById(int examenId);
        Task<List<ExamenTipo>> GetTipoExamenesAll();

    }
}
