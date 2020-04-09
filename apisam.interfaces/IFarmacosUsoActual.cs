using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IFarmacosUsoActual
    {
        RespuestaMetodos AddFarmaco(FarmacosUsoActual farmaco);
        RespuestaMetodos UpdateFarmaco(FarmacosUsoActual farmaco);
        RespuestaMetodos AddFarmacoLista(List<FarmacosUsoActual> farmacos);
        RespuestaMetodos UpdateFarmacoLista(List<FarmacosUsoActual> farmacos);
        List<FarmacosUsoActual> GetFarmacos(int pacienteId, int doctorId);
    }
}
