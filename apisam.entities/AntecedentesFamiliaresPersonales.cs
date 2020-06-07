using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class AntecedentesFamiliaresPersonales : RegistroBase
    {
        public AntecedentesFamiliaresPersonales()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int AntecedentesFamiliaresPersonalesId { get; set; }
        public int PacienteId { get; set; }
        public int? PreclinicaId { get; set; }
        public string AntecedentesPatologicosFamiliares { get; set; }
        public string AntecedentesPatologicosPersonales { get; set; }
        public string AntecedentesNoPatologicosFamiliares { get; set; }
        public string AntecedentesNoPatologicosPersonales { get; set; }
        public string AntecedentesInmunoAlergicosPersonales { get; set; }





    }
}
