using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class ExamenDetalle
    {
        public ExamenDetalle()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ExamenDetalleId { get; set; }
        public int ExamenTipoId { get; set; }
        public int ExamenCategoriaId { get; set; }
        public string Nombre { get; set; }



    }
}
