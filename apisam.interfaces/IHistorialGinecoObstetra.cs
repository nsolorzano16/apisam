using System;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IHistorialGinecoObstetra
    {
        RespuestaMetodos AddAHistorial(HistorialGinecoObstetra historial);
        RespuestaMetodos UpdateAHistorial(HistorialGinecoObstetra historial);
        HistorialGinecoObstetra GetHistorial(int pacienteId, int doctorId);

    }
}
