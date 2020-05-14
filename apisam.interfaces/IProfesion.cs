using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IProfesion
    {
        Task<List<Profesion>> Profesiones();
        Task<RespuestaMetodos> Add(Profesion profesion);
        Task<RespuestaMetodos> Update(Profesion profesion);
        Task<Profesion> GetProfesionById(int id);
    }
}
