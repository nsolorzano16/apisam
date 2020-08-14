using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace apisam.entities
{
    public class Planes : RegistroBase
    {
        [PrimaryKey,AutoIncrement]
        public int PlanId { get; set; }
        public string Nombre { get; set; }
        public int Consultas { get; set; }
        public int Imagenes { get; set; }
        public int Asistentes { get; set; }
   
    }
}
