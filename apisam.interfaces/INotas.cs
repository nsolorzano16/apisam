using System;
using System.Collections.Generic;
using apisam.entities;

namespace apisam.interfaces
{
    public interface INotas
    {
        RespuestaMetodos AddNota(Notas nota);
        RespuestaMetodos UpdateNota(Notas nota);
        RespuestaMetodos AddNotaLista(List<Notas> notas);
        RespuestaMetodos UpdateNotaLista(List<Notas> notas);
        List<Notas> GetNotas(int pacienteId, int doctorId);
    }
}
