using System;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IHistorialGinecoObstetra
    {
        bool AddAHistorial(HistorialGinecoObstetra historial);
        bool UpdateAHistorial(HistorialGinecoObstetra historial);
        HistorialGinecoObstetra GetHistorial(int pacienteId, int doctorId);

    }
}
