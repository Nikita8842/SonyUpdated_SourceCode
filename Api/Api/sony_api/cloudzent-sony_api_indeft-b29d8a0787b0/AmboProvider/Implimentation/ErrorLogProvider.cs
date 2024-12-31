using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboProvider.Interface;
using DBHelper;
using System.Data;
using AmboLibrary.GlobalAccessible;

namespace AmboProvider.Implimentation
{
    public class ErrorLogProvider : IErrorLogProvider
    {
        private readonly DbHelper _dataHelper;
        public ErrorLogProvider()
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
        }

        public void CreateErrorLog(string ErrorSource, string ErrorDetails, string ErrorMessage)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();

            objDBParam.Add(new DbParameter("@ErrorSource", ErrorSource, DbType.String));
            objDBParam.Add(new DbParameter("@ErrorDetails", ErrorDetails, DbType.String));
            objDBParam.Add(new DbParameter("@ErrorMessage", ErrorMessage, DbType.String));

            _dataHelper.ExecuteNonQuery("[dbo].[CreateErrorLog]", objDBParam, CommandType.StoredProcedure);

        }

        public void CreateErrorLogWeb(ErrorDetailsInput ErrorDetailsInput)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();

            objDBParam.Add(new DbParameter("@ErrorSource", ErrorDetailsInput.ErrorSource, DbType.String));
            objDBParam.Add(new DbParameter("@ErrorDetails", ErrorDetailsInput.ErrorDetails, DbType.String));
            objDBParam.Add(new DbParameter("@ErrorMessage", ErrorDetailsInput.ErrorMessage, DbType.String));

            _dataHelper.ExecuteNonQuery("[dbo].[CreateErrorLog]", objDBParam, CommandType.StoredProcedure);
        }
    }
}
