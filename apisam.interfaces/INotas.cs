using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface INotas
    {
        Task<RespuestaMetodos> AddNota(Notas nota);
        Task<RespuestaMetodos> UpdateNota(Notas nota);
        Task<RespuestaMetodos> AddNotaLista(List<Notas> notas);
        Task<RespuestaMetodos> UpdateNotaLista(List<Notas> notas);
        Task<List<Notas>> GetNotas(int pacienteId, int doctorId, int preclinicaId);
    }
}
