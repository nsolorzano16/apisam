using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IFarmacosUsoActual
    {
        bool AddFarmaco(FarmacosUsoActual farmaco);
        bool AddFarmacoLista(List<FarmacosUsoActual> farmacos);
        bool UpdateFarmaco(FarmacosUsoActual farmaco);
        List<FarmacosUsoActual> GetFarmacos(int pacienteId, int doctorId);
    }
}
