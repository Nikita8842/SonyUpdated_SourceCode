using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBHelper
{
    /// <summary>
    /// Team (ValueFirst)
    /// DBHelper class enables to execute Sql Objects for the connection parameters specified into web.config or 
    /// App.config file.
    /// </summary>
    public class DbHelper
    {
        #region "Private Variables"
        private readonly ConnectionManager _connectionManager;
        private readonly CommandBuilder _commandBuilder;
        private readonly DataAdapterManager _dbAdapterManager;
        private readonly string _providerName;
        private readonly AssemblyProvider _assemblyProvider;
        #endregion

        #region "Constructor Methods"

        /// <summary>
        /// This Constructor creates instance of the class for defaultConnection 
        /// </summary>
        private DbHelper()
        {
            _connectionManager = new ConnectionManager();
            _commandBuilder = new CommandBuilder();
            _dbAdapterManager = new DataAdapterManager();
            _connectionManager.GetConnection();
            _providerName = _connectionManager.ProviderName;
            _assemblyProvider = new AssemblyProvider(_providerName);
        }

        /// <summary>
        /// This constructor should be used for creation of the instance for the specified app settings connection name
        /// </summary>
        /// <param name="connectionName">App Setting's connection name</param>
        private DbHelper(string connectionName)
        {
            _connectionManager = new ConnectionManager(connectionName);
            _commandBuilder = new CommandBuilder(Configuration.GetProviderName(connectionName));
            _dbAdapterManager = new DataAdapterManager(Configuration.GetProviderName(connectionName));
            _connectionManager.GetConnection();
            _providerName = _connectionManager.ProviderName;
            _assemblyProvider = new AssemblyProvider(_providerName);
        }

        /// <summary>
        /// Constructor creates instance of the class for the specified connection string and provider name
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="providerName">Provider name</param>
        private DbHelper(string connectionString, string providerName)
        {
            _connectionManager = new ConnectionManager(connectionString, providerName);
            _commandBuilder = new CommandBuilder(providerName);
            _dbAdapterManager = new DataAdapterManager(providerName);
            _connectionManager.GetConnection();
            _providerName = _connectionManager.ProviderName;
            _assemblyProvider = new AssemblyProvider(_providerName);
        }

        #endregion

        #region Private Methods
        private void DestroyUnmanagedResource()
        {
            //Do nothing. Will be used if any unmanaged resource is being used in future.
        }
        #endregion

        #region "Methods returning instance of singleton class"

        /// <summary>
        /// Creates and returns instance of DBHelper class for the default connection        
        /// </summary>
        /// <returns>Instance of DBHelper class</returns>
        public static DbHelper GetInstance()
        {
            return Helper ?? (Helper = new DbHelper());
        }

        /// <summary>
        /// Creates and returns instance of DBHelper class for the Report connection        
        /// </summary>
        /// <returns>Instance of DBHelper class</returns>
        public static DbHelper GetReportInstance()
        {
            return ReportHelper ?? (ReportHelper = new DbHelper());
        }

        /// <summary>
        ///  Creates and returns instance of DBHelper class for the connection string key specified into App.config/ web.config file
        /// </summary>
        /// <param name="connectionName">Connection key specified into web.config/ App.config</param>
        /// <returns>Instance of DBHelper class</returns>
        public static DbHelper GetInstance(string connectionName)
        {
            return Helper ?? (Helper = new DbHelper(connectionName));
        }

        /// <summary>
        ///  Creates and returns instance of DBHelper class for the connection string key specified into App.config/ web.config file
        /// </summary>
        /// <param name="connectionName">Connection key specified into web.config/ App.config</param>
        /// <returns>Instance of DBHelper class</returns>
        public static DbHelper GetReportInstance(string connectionName)
        {
            return ReportHelper ?? (ReportHelper = new DbHelper(connectionName));
        }

        /// <summary>
        /// Creates and returns instance of DBHelper class for the connection string and provider name specified as constructor parameter
        /// </summary>
        /// <param name="connectionString">ConnectionString</param>
        /// <param name="providerName">Provider Name</param>
        /// <returns>Instance of DBHelper class</returns>
        public static DbHelper GetInstance(string connectionString, string providerName)
        {
            return Helper ?? (Helper = new DbHelper(connectionString, providerName));
        }

        #endregion

        #region "Transaction Methods"        

        /// <summary>
        /// Gets the database transaction
        /// After successful execution of command call CommitTransaction(transaction)
        /// In case of failure call RollbackTransaction(transaction)
        /// </summary>
        public IDbTransaction BeginTransaction()
        {
            return GetConnObject().BeginTransaction();
        }



        /// <summary>
        /// Commits changes to the database
        /// </summary>
        /// <param name="transaction">Database Transcation to be committed</param>
        public void CommitTransaction(IDbTransaction transaction)
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Rollback changes to the database
        /// </summary>
        /// <param name="transaction">Database Transaction to be rolled back</param>
        public void RollbackTransaction(IDbTransaction transaction)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// Gets Connection String
        /// </summary>
        private string ConnectionString
        {
            get
            {
                return _connectionManager.ConnectionString;
            }
        }

        public string Database
        {
            get
            {
                //IDbConnection connection = AssemblyProvider.GetInstance(Provider).Factory.CreateConnection();
                IDbConnection connection = _assemblyProvider.Factory.CreateConnection();
                if (connection == null) return string.Empty;

                connection.ConnectionString = ConnectionString;
                return connection.Database;
            }
        }


        /// <summary>
        /// Gets Provider Name
        /// </summary>
        public string Provider
        {
            get
            {
                return _providerName;
            }
        }

        public static DbHelper Helper { get; set; }
        public static DbHelper ReportHelper { get; set; }

        #endregion

        #region "Execute Scalar"       
        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            return ExecuteScalar(commandText, (IDbTransaction)null, commandType);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <param name="commandType">Text or Stored Procedure</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, IDbTransaction transaction, CommandType commandType)
        {
            return ExecuteScalar(commandText, new DbParameterCollection(), transaction, commandType);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="param">Parameter to be associated</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, DbParameter param, CommandType commandType)
        {
            return ExecuteScalar(commandText, param, null, commandType);
        }


        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="param">Database parameter</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <param name="commandType">Text or Stored Procedure</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, DbParameter param, IDbTransaction transaction, CommandType commandType)
        {
            var paramCollection = new DbParameterCollection();
            paramCollection.Add(param);
            return ExecuteScalar(commandText, paramCollection, transaction, commandType);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="paramCollection">Parameter collection to be associated</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, DbParameterCollection paramCollection, CommandType commandType)
        {
            return ExecuteScalar(commandText, paramCollection, null, commandType);
        }


        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="paramCollection">Database parameter Collection</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <param name="commandType">Text or Stored Procedure</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        private object ExecuteScalar(string commandText, DbParameterCollection paramCollection, IDbTransaction transaction, CommandType commandType)
        {
            object objScalar;
            var connection = transaction != null ? transaction.Connection : _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, paramCollection, commandType);
            command.Transaction = transaction;
            try
            {
                objScalar = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                connection.Close();
                connection.Dispose();
                throw ex;
            }
            finally
            {
                if (transaction == null)
                {
                    if (connection != null)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }

                command.Dispose();
            }
            return objScalar;
        }

        /// <summary>
        /// Executes the Sql Command and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command </param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, IDbTransaction transaction = (IDbTransaction)null)
        {
            return ExecuteScalar(commandText, transaction, CommandType.Text);
        }


        /// <summary>
        /// Executes the Sql Command and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="param">Parameter to be associated</param>
        /// <param name="transaction">Database Transacion</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, DbParameter param, IDbTransaction transaction = (IDbTransaction)null)
        {
            return ExecuteScalar(commandText, param, transaction, CommandType.Text);
        }

        /// <summary>
        ///  Executes the Sql Command and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="paramCollection">Database  Parameter Collection</param>
        /// <param name="transaction">Database Transacion (Use DBHelper.Transaction property.)</param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, DbParameterCollection paramCollection, IDbTransaction transaction = null)
        {
            return ExecuteScalar(commandText, paramCollection, transaction, CommandType.Text);
        }
        #endregion ExecuteScalar

        #region ExecuteNonQuery

        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows affected.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, (IDbTransaction)null, commandType);
        }


        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows affected.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, IDbTransaction transaction, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, new DbParameterCollection(), transaction, commandType);
        }

        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows affected.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, DbParameter param, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, param, null, commandType);
        }


        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows affected.
        /// </summary>
        /// <param name="commandText">Sql Command </param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, DbParameter param, IDbTransaction transaction, CommandType commandType)
        {
            var paramCollection = new DbParameterCollection();
            paramCollection.Add(param);
            return ExecuteNonQuery(commandText, paramCollection, transaction, commandType);
        }

        public int ExecuteNonQuery(string commandText, Dictionary<object, object> param, CommandType commandType)
        {
            var paramCollection = SetParamterFromDictionary(param);
            return ExecuteNonQuery(commandText, paramCollection, commandType);
        }
        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows effected.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the command</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows effected.</returns>
        public int ExecuteNonQuery(string commandText, DbParameterCollection paramCollection, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, paramCollection, null, commandType);
        }

        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows affected.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="paramCollection">Parameter Collection to be associated with the comman</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, DbParameterCollection paramCollection, IDbTransaction transaction, CommandType commandType)
        {
            int rowsAffected;
            var connection = transaction != null ? transaction.Connection : _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, paramCollection, commandType);
            command.Transaction = transaction;

            try
            {
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                connection.Close();
                connection.Dispose();
                throw ex;
            }
            finally
            {
                if (transaction == null)
                {
                    if (connection != null)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
                command.Dispose();
            }
            return rowsAffected;
        }


        /// <summary>
        /// Executes Sql Command and returns number of rows affected.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, IDbTransaction transaction = (IDbTransaction)null)
        {
            return ExecuteNonQuery(commandText, transaction, CommandType.Text);
        }

        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows affected.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, DbParameter param, IDbTransaction transaction = (IDbTransaction)null)
        {
            return ExecuteNonQuery(commandText, param, transaction, CommandType.Text);
        }


        /// <summary>
        /// Executes Sql Command and returns number of rows affected.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="paramCollection">Parameter Collection to be associated with the command</param>
        /// <param name="transaction">Current Database Transaction (Use Helper.Transaction to get transaction)</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, DbParameterCollection paramCollection, IDbTransaction transaction = (IDbTransaction)null)
        {
            return ExecuteNonQuery(commandText, paramCollection, transaction, CommandType.Text);
        }
        #endregion

        #region "Get Dynamic Dataset, IEnumerable, IList"
        /// <summary>
        /// Fetching list of records
        /// </summary>
        /// <param name="sqlName"></param>
        /// <param name="sqlCmdType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetListOfRecords(string sqlName, CommandType sqlCmdType, Dictionary<object, object> sqlParams)
        {
            DataTable dt;

            try
            {
                var paramCollection = SetParamterFromDictionary(sqlParams);

                dt = ExecuteDataTable(sqlName, paramCollection, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw new Exception("Error disposing data class." + Environment.NewLine + ex.Message);
            }

            return dt.AsDynamicEnumerable();
        }
        #endregion

        #region GetDataSet

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return result set in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, DbParameter param, CommandType commandType)
        {
            var paramCollection = new DbParameterCollection();
            paramCollection.Add(param);
            return ExecuteDataSet(commandText, paramCollection, commandType);
        }


        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the command</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, DbParameterCollection paramCollection, CommandType commandType = CommandType.Text)
        {
            var dataSet = new DataSet();
            var connection = _connectionManager.GetConnection();
            IDataAdapter adapter = _dbAdapterManager.GetDataAdapter(commandText, connection, paramCollection, commandType);

            try
            {
                adapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                connection.Close();
                connection.Dispose();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

            }
            return dataSet;
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return result set in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            return ExecuteDataSet(commandText, new DbParameterCollection(), commandType);
        }




        /// <summary>
        /// Executes the Sql Command and return result set in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command </param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(commandText, new DbParameterCollection());
        }


        /// <summary>
        /// Executes the Sql Command and return result set in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command </param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, DbParameter param)
        {
            var paramCollection = new DbParameterCollection();
            paramCollection.Add(param);
            return ExecuteDataSet(commandText, paramCollection);
        }

        #endregion

        #region "Utility Function"

        private DbParameterCollection SetParamterFromDictionary(Dictionary<object, object> param)
        {
            var paramCollection = new DbParameterCollection();

            if (param.Count <= 0) return paramCollection;
            foreach (var item in param)
            {
                paramCollection.Add(new DbParameter("@" + item.Key, item.Value));
            }
            return paramCollection;
        }

        #endregion

        #region "ExecuteDataTable"
        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="tableName">Table name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the Command or Stored Procedure.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName, DbParameterCollection paramCollection, CommandType commandType = CommandType.Text)
        {
            DataTable dtReturn;
            IDbConnection connection = null;
            //IDbTransaction _transaction;
            try
            {
                connection = _connectionManager.GetConnection();
                //if(connection != null)
                //    _transaction = connection.BeginTransaction();
                dtReturn = _dbAdapterManager.GetDataTable(commandText, paramCollection, connection, tableName, commandType);
                
            }
            catch (Exception ex)
            {
                connection.Close();
                connection.Dispose();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

            }
            return dtReturn;
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return result set in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command8 or Stored Procedure name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the Command or Stored Procedure.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, DbParameterCollection paramCollection, CommandType commandType)
        {
            return ExecuteDataTable(commandText, string.Empty, paramCollection, commandType);
        }

        /// <summary>
        /// Executes the Sql Command and return result set in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="paramCollection">Parameter collection to be associated with the Command.</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, DbParameterCollection paramCollection)
        {
            return ExecuteDataTable(commandText, string.Empty, paramCollection);
        }


        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return result set in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="tableName">Table name</param>
        /// <param name="param">Parameter to be associated with the Command.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName, DbParameter param, CommandType commandType = CommandType.Text)
        {
            DbParameterCollection paramCollection = new DbParameterCollection();
            paramCollection.Add(param);
            return ExecuteDataTable(commandText, tableName, paramCollection, commandType);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return result set in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>        
        /// <param name="param">Parameter to be associated with the Command.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, DbParameter param, CommandType commandType)
        {
            return ExecuteDataTable(commandText, string.Empty, param, commandType);
        }

        /// <summary>
        /// Executes the Sql Command and return result set in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>        
        /// <param name="param">Parameter to be associated with the Command.</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, DbParameter param)
        {
            return ExecuteDataTable(commandText, string.Empty, param);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return result set in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>        
        /// <param name="tableName">Table name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName, CommandType commandType = CommandType.Text)
        {
            return ExecuteDataTable(commandText, tableName, new DbParameterCollection(), commandType);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return result set in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>        
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, CommandType commandType)
        {
            return ExecuteDataTable(commandText, string.Empty, commandType);
        }

        /// <summary>
        /// Executes the Sql Command and return result set in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>   
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(commandText, string.Empty);
        }
        #endregion

        #region "ExecuteReader"

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>        
        /// <param name="connection">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return ExecuteDataReader(commandText, connection, new DbParameterCollection(), commandType);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="connection">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <param name="param">Parameter to be associated with the Sql Command or Stored Procedure.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection connection, DbParameter param, CommandType commandType = CommandType.Text)
        {
            var paramCollection = new DbParameterCollection();
            paramCollection.Add(param);
            return ExecuteDataReader(commandText, connection, paramCollection, commandType);
        }


        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="connection">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <param name="paramCollection">Parameter to be associated with the Sql Command or Stored Procedure.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection connection, DbParameterCollection paramCollection, CommandType commandType = CommandType.Text)
        {
            var command = _commandBuilder.GetCommand(commandText, connection, paramCollection, commandType);
            var dataReader = command.ExecuteReader();
            command.Dispose();
            return dataReader;
        }


        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns the IDataReader. Do remember to Commit or Rollback the transaction
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Proc Name</param>
        /// <param name="param">Database Parameter</param>
        /// <param name="transaction">Database Transaction (Use DBHelper.Transaction property for getting the transaction.)</param>
        /// <param name="commandType">Text/ Stored Procedure</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, DbParameter param, IDbTransaction transaction, CommandType commandType = CommandType.Text)
        {
            var paramCollection = new DbParameterCollection();
            paramCollection.Add(param);
            return ExecuteDataReader(commandText, paramCollection, transaction, commandType);
        }


        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns the IDataReader. Do remember to Commit or Rollback the transaction
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Proc name</param>
        /// <param name="paramCollection">Database Parameter Collection</param>
        /// <param name="transaction">Database Transaction (Use DBHelper.Transaction property for getting the transaction.)</param>
        /// <param name="commandType">Text/ Stored Procedure</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, DbParameterCollection paramCollection, IDbTransaction transaction, CommandType commandType = CommandType.Text)
        {
            var connection = transaction.Connection;
            var command = _commandBuilder.GetCommand(commandText, connection, paramCollection, commandType);
            command.Transaction = transaction;
            var dataReader = command.ExecuteReader();
            command.Dispose();
            return dataReader;
        }

        #endregion

        #region "Methods to Prepare the Commands"

        /// <summary>
        /// Prepares command for the passed SQL Command Or Stored Procedure. 
        /// Use DisposeCommand method after use of the command.
        /// </summary> 
        /// <param name="commandText">SQL Command or Stored Procedure name</param>
        /// <param name="commandType">Type of Command i.e. Text or Stored Procedure</param>
        /// <returns>Command ready for execute</returns>
        public IDbCommand GetCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, commandType);
            return command;
        }


        public IDbCommand GetCommand(string commandText, IDbTransaction transaction, CommandType commandType = CommandType.Text)
        {
            var connection = transaction != null ? transaction.Connection : _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, commandType);
            return command;
        }

        /// <summary>
        /// Prepares command for the passed SQL Command or Stored Procedure.
        /// Command is prepared for SQL Command only not for the stored procedures.
        /// Use DisposeCommand method after use of the command.
        /// </summary>
        /// <param name="commandText">SQL Command or Stored Procedure name</param>
        /// <param name="parameter">Database parameter</param>
        /// <param name="commandType">Type of Command i.e. Text or Stored Procedure</param>
        /// <returns>Command ready for execute</returns>
        public IDbCommand GetCommand(string commandText, DbParameter parameter, CommandType commandType = CommandType.Text)
        {
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, parameter, commandType);
            return command;
        }


        public IDbCommand GetCommand(string commandText, DbParameter parameter, IDbTransaction transaction)
        {
            var paramCollection = new DbParameterCollection();
            paramCollection.Add(parameter);
            return GetCommand(commandText, paramCollection, transaction, CommandType.Text);
        }

        /// <summary>
        /// Prepares command for the passed SQL Command or Stored Procedure.
        /// Command is prepared for SQL Command only not for the stored procedures. 
        /// Use DisposeCommand method after use of the command.
        /// </summary>
        /// <param name="commandText">SQL Command or Stored Procedure name</param>
        /// <param name="parameterCollection">Database parameter collection</param>
        /// <param name="commandType">Type of Command i.e. Text or Stored Procedure</param>
        /// <returns>Command ready for execute</returns>
        public IDbCommand GetCommand(string commandText, DbParameterCollection parameterCollection, CommandType commandType = CommandType.Text)
        {
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, parameterCollection, commandType);
            return command;
        }

        /// <summary>
        /// Prepares command for the passed SQL Command or Stored Procedure.        
        /// Use DisposeCommand method after use of the command.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameterCollection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IDbCommand GetCommand(string commandText, DbParameterCollection parameterCollection, IDbTransaction transaction, CommandType commandType = CommandType.Text)
        {
            var connection = transaction != null ? transaction.Connection : _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, parameterCollection, commandType);
            return command;
        }

        #endregion

        #region "Methods To Retrieve the Parameter Values"

        /// <summary>
        /// Returns the database parameter value from the specified command
        /// </summary>
        /// <param name="parameterName">Name of the parameter</param>
        /// <param name="command">Command from which value to be retrieved</param>
        /// <returns>Parameter value</returns>
        public object GetParameterValue(string parameterName, IDbCommand command)
        {
            var param = (IDataParameter)command.Parameters[parameterName];
            var retValue = param.Value;
            return retValue;
        }

        /// <summary>
        /// Returns the database parameter value from the specified command
        /// </summary>
        /// <param name="index">Index of the parameter</param>
        /// <param name="command">Command from which value to be retrieved</param>
        /// <returns>Parameter value</returns>
        public object GetParameterValue(int index, IDbCommand command)
        {
            var param = (IDataParameter)command.Parameters[index];
            var retValue = param.Value;
            return retValue;
        }

        #endregion

        #region "Methods to Dispose the Command"

        /// <summary>
        /// Closes and Disposes the Connection associated and then disposes the command.
        /// </summary>
        /// <param name="command">Command which needs to be closed</param>
        public void DisposeCommand(IDbCommand command)
        {
            if (command == null)
                return;

            if (command.Connection != null)
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }

            command.Dispose();
        }

        #endregion

        #region "Method to Create Open Database Connection"

        /// <summary>
        /// Creates and opens the database connection for the connection parameters specified into web.config or App.config file.
        /// </summary>
        /// <returns>Database connection object in the opened state. </returns>
        public IDbConnection GetConnObject()
        {
            return _connectionManager.GetConnection();
        }
        #endregion

        #region "Method insert multiple records"

        /// <summary>
        /// for multiple insert
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="SqlParams"></param>
        /// <returns></returns>

        public DataTable ExecuteProcedure(String ProcedureName, SqlParameter[] SqlParams)
        {

            SqlConnection connection = null;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            
            

            try
            {
                connection = _connectionManager.CreateConnection();
                connection.Open();
                
                command = new SqlCommand(ProcedureName, connection);
                
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                SqlDataAdapter adp = new SqlDataAdapter();

                for (int i = 0; i < SqlParams.Length; i++)
                {
                    command.Parameters.Add(SqlParams[i]);
                }
                adp.SelectCommand = command;
                adp.Fill(dt);
                
                return dt;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Noor,for multile datatable
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="SqlParams"></param>
        /// <returns></returns>
        public DataSet ExecuteProcedureForDataset(String ProcedureName, SqlParameter[] SqlParams)
        {

            SqlConnection connection = null;
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();

            try
            {
                connection = _connectionManager.CreateConnection();
                connection.Open();

                command = new SqlCommand(ProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 600;
                SqlDataAdapter adp = new SqlDataAdapter();

                for (int i = 0; i < SqlParams.Length; i++)
                {
                    command.Parameters.Add(SqlParams[i]);
                }
                adp.SelectCommand = command;
                adp.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        #endregion
    }
}
