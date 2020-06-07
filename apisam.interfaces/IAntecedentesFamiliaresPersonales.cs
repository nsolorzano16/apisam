using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IAntecedentesFamiliaresPersonales
    {
        Task<RespuestaMetodos> AddAntecedentes(AntecedentesFamiliaresPersonales antecedentes);
        Task<RespuestaMetodos> UpdateAntecedentes(AntecedentesFamiliaresPersonales antecedentes);
        Task<AntecedentesFamiliaresPersonales> GetAntecedente(int pacienteId);
    }
}
