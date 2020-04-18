using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IEscolaridad
    {
        List<Escolaridad> Escolaridades { get; }
        RespuestaMetodos Add(Escolaridad religion);
        RespuestaMetodos Update(Escolaridad religion);
        Escolaridad GetEscolaridadById(int id);
    }
}
