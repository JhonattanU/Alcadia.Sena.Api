using Alcadia.Sena.Models.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Alcadia.Sena.Repository
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly ConnectionStrings _connectionStrings;

        public ConnectionFactory(IOptions<ConnectionStrings> connectionStringsAccessor)
        {
            if (connectionStringsAccessor == null) throw new ArgumentNullException(nameof(connectionStringsAccessor));
            _connectionStrings = connectionStringsAccessor.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionStrings.DefaultConnection);
            return connection;
        }

    }
}
