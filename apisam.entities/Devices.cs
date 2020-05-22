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
        public int UsuarioId { get; set; }
        public string TokenDevice { get; set; }

    }
}
