using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IMunicipio
    {
        List<Municipio> Municipios { get; }

        List<Municipio> GetMunicipiosByDepartamento(int id);


    }
}
