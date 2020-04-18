using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IProfesion
    {
        List<Profesion> Profesiones { get; }
        RespuestaMetodos Add(Profesion profesion);
        RespuestaMetodos Update(Profesion profesion);
        Profesion GetProfesionById(int id);
    }
}
