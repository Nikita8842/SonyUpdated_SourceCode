using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    //// <summary>
    /// Team (ValueFirst)
    /// </summary>
    public class DbParamBuilder
    {
        private readonly AssemblyProvider _assemblyProvider;

        public DbParamBuilder(string providerName)
        {
            _assemblyProvider = new AssemblyProvider(providerName);
        }

        public System.Data.Common.DbParameter GetParameter(DbParameter parameter)
        {
            var dbParam = GetParameter();
            dbParam.ParameterName = parameter.Name;
            dbParam.Value = parameter.Value;
            dbParam.Direction = parameter.ParamDirection;
            dbParam.DbType = parameter.Type;

            return dbParam;

        }

        public IEnumerable<System.Data.Common.DbParameter> GetParameterCollection(DbParameterCollection parameterCollection)
        {
            return parameterCollection.Parameters.Select(GetParameter).ToList();
        }

        private System.Data.Common.DbParameter GetParameter()
        {
            var dbParam = _assemblyProvider.Factory.CreateParameter();

            return dbParam;
        }
    }
}
