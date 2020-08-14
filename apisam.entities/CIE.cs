using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace apisam.entities
{
    public class CIE
    {
        [PrimaryKey,AutoIncrement]
        public int CieId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

    }
}
