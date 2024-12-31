using AmboProvider.Interface;
using DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.IncentiveManagement;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;

namespace AmboProvider.Implimentation
{
    public class DeviationProvider : IDeviationProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public DeviationProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        public DeviationUploadByRDIExcel GetDeviationUploadByRDIExcel(DeviationUploadByRDISearch objSearchData, out string Message)
        {
            DeviationUploadByRDIExcel objOutput = new DeviationUploadByRDIExcel();
            DbParameterCollection objDBParam;
            try
            {
                objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", objSearchData.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IncentiveMonth", DateTime.ParseExact(objSearchData.IncentiveMonth, "MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));

                var output = _dataHelper.ExecuteDataTable("GetDeviationUploadByRDIExcel", objDBParam, CommandType.StoredProcedure);
                if (output != null && output.Columns.Count > 0 && output.Rows.Count > 0)
                {
                    objOutput.ExcelData = output;
                    Message = "Deviation Upload By RDI Excel Data fetched successfully.";
                }
                else
                {
                    objOutput.ExcelData = null;
                    Message = "No data available.";
                }
            }
            catch(Exception ex)
            {
                objOutput.ExcelData = null;
                Message = "Exception occured in API while downloading excel file. Please contact your administrator.";
            }
            return objOutput;
        }

        public DeviationUploadByRDIExcel GetDeviationUploadByRDIReasons(DeviationUploadByRDISearch objSearchData, out string Message)
        {
            DeviationUploadByRDIExcel objOutput = new DeviationUploadByRDIExcel();
            DbParameterCollection objDBParam;
            try
            {
                objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", objSearchData.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IncentiveMonth", DateTime.ParseExact(objSearchData.IncentiveMonth, "MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));

                var output = _dataHelper.ExecuteDataTable("GetDeviationUploadByRDIReasons", objDBParam, CommandType.StoredProcedure);
                if (output != null && output.Columns.Count > 0 && output.Rows.Count > 0)
                {
                    objOutput.ExcelData = output;
                    Message = "Deviation Upload By RDI reasons fetched successfully.";
                }
                else
                {
                    objOutput.ExcelData = null;
                    Message = "No data available.";
                }
            }
            catch (Exception ex)
            {
                objOutput.ExcelData = null;
                Message = "Exception occured in API while fetching saved deviation reasons. Please contact your administrator.";
            }
            return objOutput;
        }

        public DeviationUploadByRDIExcel ManageDeviationUploadByRDI(DeviationUploadByRDIExcel objData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[4];
            try
            {
                
                objDBParam[0] = new SqlParameter("@UserId", objData.UserId);
                objDBParam[1] = new SqlParameter("@Month", objData.Month);
                objDBParam[2] = new SqlParameter("@flag", objData.Flag);
                objDBParam[3] = new SqlParameter("@dtDeviation", objData.ExcelData);

                //var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageDeviationUploadByRDI]", objDBParam);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[SubmitDeviationUploadByRDI]", objDBParam);
                if (outputFromSP != null && outputFromSP.Rows.Count > 0 && outputFromSP.Columns.Count > 0 && Convert.ToInt32(outputFromSP.Rows[0]["Status"])==1)
                {
                    objData.ExcelData = outputFromSP;
                    Message = "Proposed deviation amount uploaded successfully.";
                }
                else
                {
                    objData.ExcelData = null;
                    Message = "Error occured while uploading proposed deviation amount. Please contact your administrator.";
                }
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                objData.ExcelData = null;
                Message = ex.Message;
            }
            finally
            {
                
            }
            return objData;
        }

        public DeviationUploadByRDIExcel ManageDeviationUploadByRDI_Festival(DeviationUploadByRDIExcel objData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[4];
            try
            {

                objDBParam[0] = new SqlParameter("@UserId", objData.UserId);
                objDBParam[1] = new SqlParameter("@FestivalSchemeId", objData.FestivalSchemeId);
                objDBParam[2] = new SqlParameter("@flag", objData.Flag);
                objDBParam[3] = new SqlParameter("@dtDeviation", objData.ExcelData);

                //var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageDeviationUploadByRDI]", objDBParam);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[SubmitDeviationUploadByRDI_Festival]", objDBParam);
                if (outputFromSP != null && outputFromSP.Rows.Count > 0 && outputFromSP.Columns.Count > 0 && Convert.ToInt32(outputFromSP.Rows[0]["Status"]) == 1)
                {
                    objData.ExcelData = outputFromSP;
                    Message = "Proposed deviation amount uploaded successfully.";
                }
                else
                {
                    objData.ExcelData = null;
                    Message = "Error occured while uploading proposed deviation amount. Please contact your administrator.";
                }
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                objData.ExcelData = null;
                Message = ex.Message;
            }
            finally
            {

            }
            return objData;
        }

        public bool ManageDeviationUploadByRDI_SaveReasons(DeviationUploadByRDISaveReasons objData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[2];
            DataTable dt = new DataTable();
            DataRow row;
            try
            {
                dt.Columns.Add("RecordId");
                dt.Columns.Add("ReasonId");
                dt.Columns.Add("Remark1");
                dt.Columns.Add("Remark2");
                if (objData.SaveReasonsDataList != null)
                {
                    foreach (SaveReasonsData reason in objData.SaveReasonsDataList)
                    {
                        row = dt.NewRow();
                        row["RecordId"] = Convert.ToInt64(reason.RecordId);
                        row["ReasonId"] = Convert.ToInt64(reason.ReasonId);
                        row["Remark1"] = Convert.ToString(reason.Remark1);
                        row["Remark2"] = Convert.ToString(reason.Remark2);
                        dt.Rows.Add(row);
                    }
                }

                objDBParam[0] = new SqlParameter("@UserId", objData.UserId);
                objDBParam[1] = new SqlParameter("@dtDeviationReasons", dt);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageDeviationUploadByRDI_SaveReasons]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            return false;
        }

        public bool ManageDeviationUploadByRDI_ApproveReasons(DeviationUploadByRDISaveReasons objData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[2];
            DataTable dt = new DataTable();
            DataRow row;
            try
            {
                dt.Columns.Add("RecordId");
                dt.Columns.Add("ReasonId");
                dt.Columns.Add("Remark1");
                dt.Columns.Add("Remark2");
                if (objData.SaveReasonsDataList != null)
                {
                    foreach (SaveReasonsData reason in objData.SaveReasonsDataList)
                    {
                        row = dt.NewRow();
                        row["RecordId"] = Convert.ToInt64(reason.RecordId);
                        row["ReasonId"] = Convert.ToInt64(reason.ReasonId);
                        row["Remark1"] = Convert.ToString(reason.Remark1);
                        row["Remark2"] = Convert.ToString(reason.Remark2);
                        dt.Rows.Add(row);
                    }
                }

                objDBParam[0] = new SqlParameter("@UserId", objData.UserId);
                objDBParam[1] = new SqlParameter("@dtDeviationReasons", dt);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageDeviationUploadByRDI_ApproveReasons]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            return false;
        }

        public DeviationApprovalExcel GetDeviationApprovalExcel(DeviationApprovalSearch objSearchData, out string Message)
        {
            DeviationApprovalExcel objOutput = new DeviationApprovalExcel();
            DbParameterCollection objDBParam;
            try
            {
                objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", objSearchData.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@BranchId", objSearchData.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IncentiveMonth", DateTime.ParseExact(objSearchData.IncentiveMonth, "MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));

                var output = _dataHelper.ExecuteDataTable("GetDeviationApprovalExcel", objDBParam, CommandType.StoredProcedure);
                if (output != null && output.Columns.Count > 0 && output.Rows.Count > 0)
                {
                    objOutput.ExcelData = output;
                    Message = "Deviation Approval Excel Data fetched successfully.";
                }
                else
                {
                    objOutput.ExcelData = null;
                    Message = "No data available.";
                }
            }
            catch (Exception ex)
            {
                objOutput.ExcelData = null;
                Message = "Exception occured in API while downloading excel file. Please contact your administrator.";
            }
            return objOutput;
        }

        public bool UploadDeviationApprovalExcel(DeviationApprovalExcel objData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[2];
            try
            {

                //objDBParam[0] = new SqlParameter("@UserId", objData.UserId);
                //objDBParam[1] = new SqlParameter("@dtDeviationApprovalStatus", objData.ExcelData);

                objDBParam[0] = new SqlParameter("@UserId", objData.UserId);
                objDBParam[1] = new SqlParameter("@Month", objData.Month);
                objDBParam[2] = new SqlParameter("@flag", objData.Flag);
                objDBParam[3] = new SqlParameter("@dtDeviation", objData.ExcelData);

                ///var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageDeviationApproval]", objDBParam);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[SubmitDeviationUploadByRDI]", objDBParam);

                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            return false;
        }

        public DeviationFinalUploadExcel GetDeviationFinalUploadExcel(DeviationFinalUploadSearch objSearchData, out string Message)
        {
            DeviationFinalUploadExcel objOutput = new DeviationFinalUploadExcel();
            DbParameterCollection objDBParam;
            try
            {
                objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", objSearchData.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IncentiveMonth", DateTime.ParseExact(objSearchData.IncentiveMonth, "MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));

                var output = _dataHelper.ExecuteDataTable("GetDeviationFinalUploadExcel", objDBParam, CommandType.StoredProcedure);
                if (output != null && output.Columns.Count > 0 && output.Rows.Count > 0)
                {
                    objOutput.ExcelData = output;
                    Message = "Deviation FinalUpload Excel Data fetched successfully.";
                }
                else
                {
                    objOutput.ExcelData = null;
                    Message = "No data available.";
                }
            }
            catch (Exception ex)
            {
                objOutput.ExcelData = null;
                Message = "Exception occured in API while downloading excel file. Please contact your administrator.";
            }
            return objOutput;
        }

        public bool UploadDeviationFinalUploadExcel(DeviationFinalUploadExcel objData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[2];
            try
            {

                objDBParam[0] = new SqlParameter("@UserId", objData.UserId);
                objDBParam[1] = new SqlParameter("@dtDeviationFinalUploadStatus", objData.ExcelData);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageDeviationFinalUpload]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            return false;
        }
    }
}
