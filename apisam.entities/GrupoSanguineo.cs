using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class GrupoSanguineo
    {
        public GrupoSanguineo()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int GrupoSanguineoId { get; set; }
        public string Nombre { get; set; }
    }
}
