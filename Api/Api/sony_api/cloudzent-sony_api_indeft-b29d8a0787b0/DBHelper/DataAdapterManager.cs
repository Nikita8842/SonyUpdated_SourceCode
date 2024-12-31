using System;
using System.Data;
using System.Data.Common;

namespace DBHelper
{
    /// <summary>
    /// Team (ValueFirst) 
    /// </summary>
    public class DataAdapterManager
    {
        private readonly CommandBuilder _commandBuilder;
        private readonly AssemblyProvider _assemblyProvider;

        public DataAdapterManager()
        {
            var providerName = Configuration.GetProviderName(Configuration.DefaultConnection);
            _commandBuilder = new CommandBuilder(providerName);
            _assemblyProvider = new AssemblyProvider(providerName);
        }

        public DataAdapterManager(string providerName)
        {
            _commandBuilder = new CommandBuilder(providerName);
            _assemblyProvider = new AssemblyProvider(providerName);
        }

        public DbDataAdapter GetDataAdapter(string sqlCommand, IDbConnection connection)
        {
            return GetDataAdapter(sqlCommand, connection, CommandType.Text);
        }


        public DbDataAdapter GetDataAdapter(string sqlCommand, IDbConnection connection, DbParameter param, CommandType commandType)
        {
            var command = _commandBuilder.GetCommand(sqlCommand, connection, param, commandType);
            var adapter = GetDataAdapter(command);
            return adapter;
        }

        public DbDataAdapter GetDataAdapter(string sqlCommand, IDbConnection connection, DbParameterCollection paramCollection, CommandType commandType)
        {
            var command = _commandBuilder.GetCommand(sqlCommand, connection, paramCollection, commandType);
            var adapter = GetDataAdapter(command);
            return adapter;
        }

        private DbDataAdapter GetDataAdapter(string sqlCommand, IDbConnection connection, CommandType commandType)
        {
            var command = _commandBuilder.GetCommand(sqlCommand, connection, commandType);
            var adapter = GetDataAdapter(command);
            return adapter;
        }

        public DataTable GetDataTable(string sqlCommand, DbParameterCollection paramCollection, IDbConnection connection, string tableName, CommandType commandType)
        {
            var table = tableName != string.Empty ? new DataTable(tableName) : new DataTable();

            IDbCommand command;
            if (paramCollection != null)
            {
                command = paramCollection.Parameters.Count > 0 ? _commandBuilder.GetCommand(sqlCommand, connection, paramCollection, commandType) : _commandBuilder.GetCommand(sqlCommand, connection, commandType);
            }
            else
                command = _commandBuilder.GetCommand(sqlCommand, connection, commandType);


            var adapter = GetDataAdapter(command);
            var constructor = adapter.GetType().GetConstructor(new[] { command.GetType() });
            if (constructor != null) adapter = (DbDataAdapter)constructor.Invoke(new object[] { command });
            var method = adapter.GetType().GetMethod("Fill", new[] { typeof(DataTable) });

            try
            {
                method.Invoke(adapter, new object[] { table });
            }
            catch (Exception err)
            {
                throw err;
            }
            return table;
        }

        public DataTable GetDataTable(string sqlCommand, DbParameter param, IDbConnection connection, string tableName, CommandType commandType)
        {
            var paramCollection = new DbParameterCollection();
            paramCollection.Add(param);
            return GetDataTable(sqlCommand, paramCollection, connection, tableName, commandType);
        }

        public DataTable GetDataTable(string sqlCommand, IDbConnection connection, string tableName, CommandType commandType)
        {
            return GetDataTable(sqlCommand, new DbParameterCollection(), connection, tableName, commandType);
        }

        public DataTable GetDataTable(string sqlCommand, IDbConnection connection, CommandType commandType)
        {
            return GetDataTable(sqlCommand, new DbParameterCollection(), connection, string.Empty, commandType);
        }

        public DataTable GetDataTable(string sqlCommand, IDbConnection connection)
        {
            return GetDataTable(sqlCommand, new DbParameterCollection(), connection, string.Empty, CommandType.Text);
        }


        private DbDataAdapter GetDataAdapter(IDbCommand command)
        {
            var adapter = _assemblyProvider.Factory.CreateDataAdapter();

            if (adapter != null)
            {
                adapter.SelectCommand = (DbCommand)command;
                return adapter;
            }
            else
            {
                return null;
            }
        }
    }
}
