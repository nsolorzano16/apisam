using System;
using System.Threading.Tasks;
using apisam.entities;

namespace apisam.interfaces
{
    public interface IDevices
    {
        Task<RespuestaMetodos> AddDevice(Devices device);
        Task<Devices> ExistDevice(int doctorId, string token);
    }
}
