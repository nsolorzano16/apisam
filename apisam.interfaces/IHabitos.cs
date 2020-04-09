using System;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IHabitos
    {
        RespuestaMetodos AddAHabito(Habitos habito);
        RespuestaMetodos UpdateAHabito(Habitos habito);
        Habitos GetHabito(int pacienteId, int doctorId);

    }
}
