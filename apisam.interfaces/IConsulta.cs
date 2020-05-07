using System;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface IConsulta
    {
        ConsultaViewModel GetDetalleConsulta(int doctorId, int pacienteId, int preclinicaId);
        RespuestaMetodos AddConsultaGeneral(ConsultaGeneral consulta);
        RespuestaMetodos UpdateConsultaGeneral(ConsultaGeneral consulta);
        ConsultaGeneral GetConsultaGeneral(int pacienteId, int doctorId, int preclinicaId);
    }
}
