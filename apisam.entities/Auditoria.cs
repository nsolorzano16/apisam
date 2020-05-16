using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Auditoria
    {
        public Auditoria()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int CodigoAuditoria { get; set; }
        public string Tabla { get; set; }
        public string Accion { get; set; }
        public int Registro { get; set; }
        public string NombreUsuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
