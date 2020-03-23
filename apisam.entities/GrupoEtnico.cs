using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class GrupoEtnico
    {
        public GrupoEtnico()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int GrupoEtnicoId { get; set; }
        public string Nombre { get; set; }
    }
}
