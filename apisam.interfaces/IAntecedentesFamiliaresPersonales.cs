using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IAntecedentesFamiliaresPersonales
    {
        bool AddAntecedentes(AntecedentesFamiliaresPersonales antecedentes);
        bool UpdateAntecedentes(AntecedentesFamiliaresPersonales antecedentes);
        AntecedentesFamiliaresPersonales GetAntecedente(int pacienteId, int doctorId);
    }
}
