using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Devices
    {
        public Devices()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int DeviceId { get; set; }
        public string UsuarioId { get; set; }
        public string TokenDevice { get; set; }
        public string Platform { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string Usuario { get; set; }

    }
}
