using apisam.entities.Dashboard;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace apisam.repos
{
    public class DashboardRepo : IDashboard
    {
        private readonly OrmLiteConnectionFactory dbFactory;

        private readonly Conexion con = new Conexion();

        public DashboardRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }
        public async Task<List<TotalPacientesAnioMes>> GetPacientesTotalAnioMes(string username)
        {
            using var _db = dbFactory.Open();
            var _qry = $@"SELECT DATEPART(Year, CreadoFecha) [Year], FORMAT(CreadoFecha, 'MMMM', 'es-es') [Mes], COUNT(PacienteId) [Total]
                                    FROM Paciente
                                    WHERE CreadoPor = '{username}'
                                    GROUP BY DATEPART(Year, CreadoFecha), FORMAT(CreadoFecha, 'MMMM', 'es-es')
                                    ORDER BY [Year] desc,[Mes]";

            return await _db.SelectAsync<TotalPacientesAnioMes>(_qry);
        }
    }
}
