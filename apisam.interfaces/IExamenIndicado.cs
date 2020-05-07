using System;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IExamenIndicado
    {
        RespuestaMetodos AddExamenIndicado(ExamenIndicado examen);
        RespuestaMetodos UpdateExamenIndicado(ExamenIndicado examen);
        ExamenIndicado GetExamenIndicadoById(int examenId);
    }
}
