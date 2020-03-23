using System;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IHabitos
    {
        bool AddAHabito(Habitos habito);
        bool UpdateAHabito(Habitos habito);
        Habitos GetHabito(int pacienteId, int doctorId);

    }
}
