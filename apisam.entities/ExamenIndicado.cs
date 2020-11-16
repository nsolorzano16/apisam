using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class ExamenIndicado : RegistroBase
    {
        public ExamenIndicado()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ExamenIndicadoId { get; set; }
        public int PacienteId { get; set; }
        public string DoctorId { get; set; }
        public int PreclinicaId { get; set; }
        public int ExamenCategoriaId { get; set; }
        public int ExamenTipoId { get; set; }
        public int ExamenDetalleId { get; set; }
        public string Nombre { get; set; }



    }
}
