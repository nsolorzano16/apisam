using System;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IConsulta
    {
        ConsultaViewModel GetDetalleConsulta(int doctorId, int pacienteId, int preclinicaId);
    }
}
