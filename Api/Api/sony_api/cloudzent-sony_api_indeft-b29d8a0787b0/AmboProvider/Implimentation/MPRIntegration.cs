using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboProvider.Interface;
using AmboLibrary.MPRIntegration;
using DBHelper;
using System.Data;
using AmboUtilities.Helper;

namespace AmboProvider.Implimentation
{
    public class MPRIntegration : IMPRIntegration
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public MPRIntegration(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }        

        public IEnumerable<SalesThroughQuantityOutput> GetSalesThroughQuantity(SalesThroughQuantityInput InputParam, out string Message)
        {
            Message = string.Empty;
            IEnumerable<SalesThroughQuantityOutput> output = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            DbParameterCollection objDBParam1 = new DbParameterCollection();

            try
            {
                objDBParam.Add(new DbParameter("@LoginId", InputParam.LoginId, DbType.String));
                InputParam.Password = AMBOEcryption.GetHashValue(InputParam.Password.Trim());
                objDBParam.Add(new DbParameter("@Password", InputParam.Password, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[ValidateUser]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                var isdone = Convert.ToInt32(outputFromSP.Rows[0]["Status"]);

                if (isdone == 1)
                {
                    objDBParam1.Add(new DbParameter("@PrimaryCode", InputParam.PrimaryCode, DbType.String));

                    var outputFromSP1 = _dataHelper.ExecuteDataTable("[dbo].[GetSalesThroughQuantity]", objDBParam1, CommandType.StoredProcedure);

                    if (outputFromSP1 == null || outputFromSP1.Rows.Count == 0)
                    {
                        Message = "No data returned from database.";
                        return null;
                    }

                    else
                    {
                        foreach(DataColumn col in outputFromSP1.Columns)
                        {
                            var dt = col.ColumnName + " " + col.DataType;
                        }
                        output = (from outputdata in outputFromSP1.AsEnumerable()
                                  select new SalesThroughQuantityOutput
                                  {
                                      Month = Convert.ToString(outputdata.Field<string>("Month")),
                                      Branch = Convert.ToString(outputdata.Field<string>("Branch")),
                                      State = Convert.ToString(outputdata.Field<string>("State")),
                                      City = Convert.ToString(outputdata.Field<string>("City")),
                                      CityType = Convert.ToString(outputdata.Field<string>("CityType")),
                                      Location = Convert.ToString(outputdata.Field<string>("Location")),
                                      DealerName = Convert.ToString(outputdata.Field<string>("DealerName")),
                                      ProductCategory = Convert.ToString(outputdata.Field<string>("ProductCategory")),
                                      Company = Convert.ToString(outputdata.Field<string>("Company")),
                                      SalesThroughQuantity = Convert.ToInt64(outputdata.Field<Int64>("SalesThroughQuantity")),
                                      CounterSIze = Convert.ToInt32(outputdata.Field<int>("CounterSize")),
                                      Value = Convert.ToInt64(outputdata.Field<Int64>("Value")),
                                  });
                        Message = "Data fetched successfully.";
                        return output;
                    }
                }
                else
                {
                    return output;
                }
            }

            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("", ex.StackTrace, ex.Message);
                Message = "Error occured while fetching data.";
                return output;            
            }
        }

        public IEnumerable<GetSFADetailsBySFACodeOutput> GetSFADetailsBySFACode(GetSFADetailsBySFACodeInput InputParam, out string Message)
        {
            Message = string.Empty;
            IEnumerable<GetSFADetailsBySFACodeOutput> output = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            DbParameterCollection objDBParam1 = new DbParameterCollection();

            try
            {
                objDBParam.Add(new DbParameter("@LoginId", InputParam.LoginId, DbType.String));
                InputParam.Password = AMBOEcryption.GetHashValue(InputParam.Password.Trim());
                objDBParam.Add(new DbParameter("@Password", InputParam.Password, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[ValidateUser]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                var isdone = Convert.ToInt32(outputFromSP.Rows[0]["Status"]);

                if (isdone == 1)
                {
                    objDBParam1.Add(new DbParameter("@SFACode", InputParam.SFACode, DbType.String));

                    var outputFromSP1 = _dataHelper.ExecuteDataTable("[dbo].[GetSFADetailsBySFACode]", objDBParam1, CommandType.StoredProcedure);

                    if (outputFromSP1 == null || outputFromSP1.Rows.Count == 0)
                    {
                        Message = "No data returned from database.";
                        return null;
                    }

                    else
                    {
                        output = (from outputdata in outputFromSP1.AsEnumerable()
                                  select new GetSFADetailsBySFACodeOutput
                                  {
                                      SFA = Convert.ToString(outputdata.Field<string>("SFAName")),
                                      Branch = Convert.ToString(outputdata.Field<string>("Branch")),
                                      State = Convert.ToString(outputdata.Field<string>("State")),
                                      City = Convert.ToString(outputdata.Field<string>("City")),
                                      CityType = Convert.ToString(outputdata.Field<string>("CityType")),
                                      Dealer = Convert.ToString(outputdata.Field<string>("Dealer")),
                                      Division = Convert.ToString(outputdata.Field<string>("Division")),
                                      SFAStatus = Convert.ToString(outputdata.Field<string>("SFAStatus")),
                                  });
                        Message = "Data fetched successfully.";
                        return output;
                    }
                }
                else
                {
                    return output;
                }
            }

            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("", ex.StackTrace, ex.Message);
                Message = "Error occured while fetching data.";
                return output;
            }
        }

        public IEnumerable<GetBranchDivisionWise_SFACountOutput> GetBranchDivisionWise_SFACount(GetBranchDivisionWise_SFACountInput InputParam, out string Message)
        {
            Message = string.Empty;
            IEnumerable<GetBranchDivisionWise_SFACountOutput> output = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@LoginId", InputParam.LoginId, DbType.String));
                InputParam.Password = AMBOEcryption.GetHashValue(InputParam.Password.Trim());
                objDBParam.Add(new DbParameter("@Password", InputParam.Password, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[ValidateUser]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                var isdone = Convert.ToInt32(outputFromSP.Rows[0]["Status"]);

                if (isdone == 1)
                {                    
                    var outputFromSP1 = _dataHelper.ExecuteDataTable("[dbo].[GetSalesThroughQuantity]", CommandType.StoredProcedure);

                    if (outputFromSP1 == null || outputFromSP1.Rows.Count == 0)
                    {
                        Message = "No data returned from database.";
                        return null;
                    }

                    else
                    {
                        output = (from outputdata in outputFromSP1.AsEnumerable()
                                  select new GetBranchDivisionWise_SFACountOutput
                                  {
                                      BranchId = Convert.ToInt32(outputdata.Field<int>("BranchId")),
                                      DivisionId = Convert.ToInt32(outputdata.Field<int>("DivisionId")),
                                      Branch = Convert.ToString(outputdata.Field<string>("Branch")),
                                      Division = Convert.ToString(outputdata.Field<string>("Division")),
                                      SFACount = Convert.ToInt64(outputdata.Field<Int64>("SFACount")),
                                      Status = Convert.ToString(outputdata.Field<Int64>("Status")),
                                      
                                  });
                        Message = "Data fetched successfully.";
                        return output;
                    }
                }
                else
                {
                    return output;
                }
            }

            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("", ex.StackTrace, ex.Message);
                Message = "Error occured while fetching data.";
                return output;
            }
        }
    }
}
