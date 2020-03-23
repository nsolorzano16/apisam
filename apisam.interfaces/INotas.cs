using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface INotas
    {
        bool AddNota(Notas nota);
        bool UpdateNota(Notas nota);
        bool AddNotaLista(List<Notas> notas);
        List<Notas> GetNotas(int pacienteId, int doctorId);
    }
}
