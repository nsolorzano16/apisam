using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IDepartamento
    {
        List<Departamento> Departamentos { get; }
        
        Departamento GetDepartamentoById(int id);
    }
}
