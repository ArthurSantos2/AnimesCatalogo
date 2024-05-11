using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Data
{
    internal sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection")
            };

            if (!connectionStringBuilder.TrustServerCertificate)
            {
                connectionStringBuilder.TrustServerCertificate = true;
            }

            if (connectionStringBuilder.CommandTimeout < 30)
            {
                connectionStringBuilder.CommandTimeout = 30;
            }

            _connectionString = connectionStringBuilder.ToString();
        }

        public string ConnectionString => _connectionString;
    }

}
