using System;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class DevicesRepo : IDevices
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public DevicesRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<RespuestaMetodos> AddDevice(Devices device)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();

                // var dev = await ExistDevice(device.UsuarioId, device.TokenDevice);
                if(!await ExistDevice( device.UsuarioId,device.TokenDevice))
                {
                    device.CreadoFecha = dateTime_HN;
                    if (!device.TokenDevice.IsEmpty())
                    {
                    await _db.SaveAsync<Devices>(device);

                    }
                   
                }

                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }



        public async Task<bool> ExistDevice(string usuarioid,string token)
        {
            using var _db = dbFactory.Open();
            var _qry = $@"SELECT *  FROM Devices d WHERE  CONVERT(VARCHAR(MAX), d.TokenDevice) ='{token}' AND d.UsuarioId = '{usuarioid}' ";
            return await  _db.ExistsAsync<Devices>(_qry);
        }
    }
}
