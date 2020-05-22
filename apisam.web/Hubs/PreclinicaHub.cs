using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace apisam.web.Hubs
{
    public class PreclinicaHub : Hub
    {
        public PreclinicaHub()
        {
        }

        public async Task SendPreclinicas(string user, string mensaje)
        {
            await Clients.All.SendAsync("recibirMensaje", user, mensaje);
        }
    }
}
