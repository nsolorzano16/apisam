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

            //return "Server=DESKTOP-EL76SJK;Database=testdb;Integrated Security=true;";
        }

        public String GetAzureDbConnection()
        {
            return "DefaultEndpointsProtocol=https;AccountName=storagedesam;" +
                "AccountKey=5J5s3UrMxv7LcEfZpuAoXVc0bZcRiG2t2HCwVi6BSV8oNZKgstd+rLHo3AA4fFU7jEyH3ea2i/hIGeeUO7XPkQ==;EndpointSuffix=core.windows.net";

        }
    }
}
