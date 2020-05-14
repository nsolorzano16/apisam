using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenDetalle
    {
        Task<List<ExamenDetalle>> GetDetalleExamenes(int examenTipoId, int examenCategoriaId);
        Task<ExamenDetalle> GetExamenDetalleById(int examenId);

    }


}
