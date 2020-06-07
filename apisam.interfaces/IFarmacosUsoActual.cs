using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IFarmacosUsoActual
    {
        Task<RespuestaMetodos> AddFarmaco(FarmacosUsoActual farmaco);
        Task<RespuestaMetodos> UpdateFarmaco(FarmacosUsoActual farmaco);
        Task<RespuestaMetodos> AddFarmacoLista(List<FarmacosUsoActual> farmacos);
        Task<RespuestaMetodos> UpdateFarmacoLista(List<FarmacosUsoActual> farmacos);
        Task<List<FarmacosUsoActual>> GetFarmacos(int pacienteId);
    }
}
