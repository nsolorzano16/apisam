using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenTipo
    {
        List<ExamenTipo> GetTipoExamenes(int examenCategoriaId);

        ExamenTipo GetExamenTipoById(int examenId);
    }
}
