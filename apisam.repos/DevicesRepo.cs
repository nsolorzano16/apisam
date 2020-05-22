using System;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class DevicesRepo : IDevices
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();

        public DevicesRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public async Task<RespuestaMetodos> AddDevice(Devices device)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();

                var dev = await ExistDevice(device.UsuarioId, device.TokenDevice);
                if (dev == null)
                {
                    await _db.SaveAsync<Devices>(device);
                    _resp.Ok = true;

                }
                else
                {
                    var nuevo = new Devices
                    {
                        DeviceId = dev.DeviceId,
                        UsuarioId = dev.UsuarioId,
                        TokenDevice = dev.TokenDevice
                    };
                    await _db.SaveAsync<Devices>(nuevo);
                }

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }



        public async Task<Devices> ExistDevice(int doctorId, string token)
        {


            using var _db = dbFactory.Open();
            var qry = $@"SELECT *  FROM Devices d WHERE  d.UsuarioId = {doctorId} AND
                            CONVERT(VARCHAR(MAX), d.TokenDevice) ='{token}' ";
            return await _db.SingleAsync<Devices>(qry);

        }
    }
}
