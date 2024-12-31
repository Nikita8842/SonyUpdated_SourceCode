using System.Data;

namespace DBHelper
{
    /// <summary>
    /// Manbeer Singh Makhloga (ValueFirst)
    /// </summary>
    public class CommandBuilder
    {
        private readonly DbParamBuilder _paramBuilder;
        private readonly AssemblyProvider _assemblyProvider;

        public CommandBuilder()
        {
            var providerName = Configuration.GetProviderName(Configuration.DefaultConnection);
            _paramBuilder = new DbParamBuilder(providerName);
            _assemblyProvider = new AssemblyProvider(providerName);
        }

        public CommandBuilder(string providerName)
        {
            _paramBuilder = new DbParamBuilder(providerName);
            _assemblyProvider = new AssemblyProvider(providerName);
        }


        public IDbCommand GetCommand(string commandText, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            var command = GetCommand();
            command.CommandText = commandText;
            command.Connection = connection;
            command.CommandType = commandType;
            command.CommandTimeout = 300;

            return command;
        }

        public IDbCommand GetCommand(string commandText, IDbConnection connection, DbParameter parameter, CommandType commandType = CommandType.Text)
        {
            var param = _paramBuilder.GetParameter(parameter);
            var command = GetCommand(commandText, connection, commandType);
            command.Parameters.Add(param);

            return command;
        }

        public IDbCommand GetCommand(string commandText, IDbConnection connection, DbParameterCollection parameterCollection, CommandType commandType = CommandType.Text)
        {
            var paramArray = _paramBuilder.GetParameterCollection(parameterCollection);
            var command = GetCommand(commandText, connection, commandType);

            foreach (var param in paramArray)
                command.Parameters.Add(param);

            return command;
        }

        #region Private Method
        private IDbCommand GetCommand()
        {
            IDbCommand command = _assemblyProvider.Factory.CreateCommand();
            return command;
        }
        #endregion
    }
}
