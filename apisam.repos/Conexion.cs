using System;
namespace apisam.repositories
{
    public class Conexion
    {
        public Conexion()
        {
        }

        public String GetConnectionString()
        {
            return "Data Source=serversam.database.windows.net;Initial " +
                "Catalog=samdb;Persist Security Info=True;User ID=adminsam;Password=2019Prism@Soft";
        }
    }
}
