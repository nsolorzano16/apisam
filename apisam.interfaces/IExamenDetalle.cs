using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenDetalle
    {
        List<ExamenDetalle> GetDetalleExamenes(int examenTipoId, int examenCategoriaId);

        ExamenDetalle GetExamenDetalleById(int examenId);
    }


}
