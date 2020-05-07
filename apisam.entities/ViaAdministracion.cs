using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class ViaAdministracion
    {
        public ViaAdministracion()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ViaAdministracionId { get; set; }
        public string Nombre { get; set; }

    }
}
