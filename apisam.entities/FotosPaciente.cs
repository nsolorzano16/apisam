using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace apisam.entities
{
 public   class FotosPaciente : RegistroBase
    {
        [PrimaryKey,AutoIncrement]
        public int FotoId { get; set; }
        public int PacienteId { get; set; }
        public string FotoUrl { get; set; }

    }
}
