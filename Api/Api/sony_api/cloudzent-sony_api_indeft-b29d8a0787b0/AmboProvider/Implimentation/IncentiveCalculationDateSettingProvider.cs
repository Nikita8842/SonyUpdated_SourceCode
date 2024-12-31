using AmboLibrary.IncentiveManagement;
using AmboProvider.Interface;
using DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboProvider.Implimentation
{
    public class IncentiveCalculationDateSettingProvider : IIncentiveCalculationDateSettingProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public IncentiveCalculationDateSettingProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        public IncentiveCalculationDateSettingList GetIncentiveCalculationDateSettingReport(IncentiveCalculationDateSettingParam Input)

        {
            IncentiveCalculationDateSettingList Data = new IncentiveCalculationDateSettingList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", Input.CreatedBy, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetIncentiveCalculationDateSetting]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.IncentiveCalculationDateSettingData = (from data in outputFromSP.AsEnumerable()
                                                      select new IncentiveCalculationDateSetting
                                                      {
                                                          Month = Convert.ToString(data.Field<string>("Month")),
                                                          CollectionDay = Convert.ToInt32(data.Field<int>("CollectionDay")),
                                                      }).ToList();
                }
                else
                {
                    Data.IncentiveCalculationDateSettingData = (from data in outputFromSP.AsEnumerable()
                                                      select new IncentiveCalculationDateSetting
                                                      {
                                                         
                                                      }).ToList();
                }//
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            return Data;
        }
        public int UpdateIncentiveCalculationDateSetting(IncentiveCalculationDateSetting IncentiveCalculationDateSetting, out string Message)
        {
            int IsDone = 0;
            Message = string.Empty;
            try
            {
                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@Month", IncentiveCalculationDateSetting.Month);
                objDBParam[1] = new SqlParameter("@CollectionDay", IncentiveCalculationDateSetting.CollectionDay);
                objDBParam[2] = new SqlParameter("@UserId", IncentiveCalculationDateSetting.CreatedBy);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[UpdateIncentiveCalculationDateSetting]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateIncentiveCalculationDateSetting", ex.StackTrace, ex.Message);
                Message = "Error occured while updating Incentive Calculation Date Setting";
            }

            return IsDone;
        }

    }
}
