using System.Configuration;

namespace DBHelper
{
    /// <summary>
    ///Team (ValueFirst) 
    /// A Configuration class provide connectionString & providerName
    /// </summary>
    public static class Configuration
    {
        const string DefaultConnectionKey = "defaultConnection";

        public static string DefaultConnection
        {
            get
            {
                return ConfigurationManager.AppSettings[DefaultConnectionKey];
            }
        }

        public static string ProviderName
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DefaultConnectionKey].ProviderName;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DefaultConnectionKey].ConnectionString;
            }
        }

        public static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static string GetProviderName(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ProviderName;
        }
    }
}
