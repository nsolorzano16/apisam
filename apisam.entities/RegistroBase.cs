using System;
namespace apisam.entities
{
    public class RegistroBase
    {
        public RegistroBase()
        {
        }

        public bool Activo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime ModificadoFecha { get; set; }
        public string Notas { get; set; }
    }
}
