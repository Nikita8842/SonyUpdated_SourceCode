using System;
using System.Data;
using System.Data.SqlClient;

namespace DBHelper
{
    /// <summary>
    /// Team (ValueFirst)
    /// ConnectionManager takes care of establishing the connection to the database parameters specified into web.config or app.config file.
    /// </summary>
    public class ConnectionManager
    {
        private readonly string _connectionString;
        private readonly string _providerName;
        private readonly AssemblyProvider _assemblyProvider;

        public ConnectionManager()
        {
            _connectionString = Configuration.ConnectionString;
            _providerName = Configuration.ProviderName;
            _assemblyProvider = new AssemblyProvider(_providerName);
        }

        public ConnectionManager(string connectionName)
        {
            _connectionString = Configuration.GetConnectionString(connectionName);
            _providerName = Configuration.GetProviderName(connectionName);
            _assemblyProvider = new AssemblyProvider(_providerName);
        }

        public ConnectionManager(string connectionString, string providerName)
        {
            _connectionString = connectionString;
            _providerName = providerName;
            _assemblyProvider = new AssemblyProvider(_providerName);
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

        }

        public string ProviderName
        {
            get
            {
                return _providerName;
            }
        }

        /// <summary>
        /// Establish Connection to the database and Return an open connection.
        /// </summary>
        /// <returns>Open connection to the database</returns>
        public IDbConnection GetConnection()
        {
            IDbConnection connection = _assemblyProvider.Factory.CreateConnection();
            if (connection != null)
            {
                connection.ConnectionString = _connectionString;

                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return connection;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a connection to the sql server database
        /// </summary>
        /// <returns>
        /// Returns a SqlConnection object
        /// </returns>
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
