using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IAntecedentesFamiliaresPersonales
    {
        RespuestaMetodos AddAntecedentes(AntecedentesFamiliaresPersonales antecedentes);
        RespuestaMetodos UpdateAntecedentes(AntecedentesFamiliaresPersonales antecedentes);
        AntecedentesFamiliaresPersonales GetAntecedente(int pacienteId, int doctorId);
    }
}
