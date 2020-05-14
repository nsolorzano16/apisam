using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IMunicipio
    {
        Task<List<Municipio>> Municipios();
        Task<List<Municipio>> GetMunicipiosByDepartamento(int id);



    }
}
