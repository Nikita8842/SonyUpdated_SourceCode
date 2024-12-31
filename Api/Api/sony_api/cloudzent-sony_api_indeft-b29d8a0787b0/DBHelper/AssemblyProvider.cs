using System.Data.Common;

namespace DBHelper
{
    /// <summary>
    /// Team (ValueFirst)
    /// A Singleton class which provides and loads the necessary assembly
    /// </summary>
    public class AssemblyProvider
    {
        private readonly string _providerName;

        static AssemblyProvider()
        { }

        public AssemblyProvider()
        {
            _providerName = Configuration.GetProviderName(Configuration.DefaultConnection);
        }

        public AssemblyProvider(string providerName)
        {
            _providerName = providerName;
        }

        public DbProviderFactory Factory
        {
            get
            {
                var factory = DbProviderFactories.GetFactory(_providerName);

                return factory;
            }
        }

    }
}
