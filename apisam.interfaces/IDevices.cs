using System;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IDevices
    {
        Task<RespuestaMetodos> AddDevice(Devices device);
        Task<bool> ExistDevice(string usuarioId,string token);
    }
}
