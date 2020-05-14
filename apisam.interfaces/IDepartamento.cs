using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IDepartamento
    {
        Task<List<Departamento>> Departamentos();

        Task<Departamento> GetDepartamentoById(int id);
    }
}
