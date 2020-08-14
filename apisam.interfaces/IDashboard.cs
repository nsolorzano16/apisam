using apisam.entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace apisam.interfaces
{
public    interface IDashboard
    {
        Task<List<TotalPacientesAnioMes>> GetPacientesTotalAnioMes(string username);
    }
}
