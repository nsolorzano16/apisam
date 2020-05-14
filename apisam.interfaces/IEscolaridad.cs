using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IEscolaridad
    {
        Task<List<Escolaridad>> Escolaridades();
        Task<RespuestaMetodos> Add(Escolaridad religion);
        Task<RespuestaMetodos> Update(Escolaridad religion);
        Task<Escolaridad> GetEscolaridadById(int id);
    }
}
