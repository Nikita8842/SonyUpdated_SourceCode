using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Modules;
using AmboProvider.Interface;
using DBHelper;
using System.Data;
using System.Data.SqlClient;

namespace AmboProvider.Implimentation
{
    public class ModuleManagementProvider : IModuleManagementProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public ModuleManagementProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        #region Dealer Master Code Update API
        public List<string> GetDealerMasterCodeList()
        {
            List<string> ListOfMasterCode = null;
            try
            {
                //objDBParam.Add(new DbParameter("@MasterCode", masterCode, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealerMasterCodeList]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfMasterCode = (from data in outputFromSP.AsEnumerable()
                                        select data.Field<string>("SAPCode")).ToList();
                }
                else
                    ListOfMasterCode = new List<string>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerMasterCodeList", ex.StackTrace, ex.Message);
            }
            return ListOfMasterCode;
        }

        public List<Dealerdetails> GetDealerByMasterCode(string masterCode)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<Dealerdetails> DealerList = null;
            try
            {
                objDBParam.Add(new DbParameter("@MasterCode", masterCode, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealerMasterCodeDetails]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {

                    DealerList = (from data in outputFromSP.AsEnumerable()
                                        select new Dealerdetails
                                        {
                                            Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                            DealerDetail = Convert.ToString(data.Field<string>("DealerDetail")),
                                            MasterCode = masterCode
                }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerByMasterCode", ex.StackTrace, ex.Message);
            }
            return DealerList;
        }

        public int UpdateDealerMasterCode(DealerMasterCodeUpdate dealerMasterCodeData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            
            try
            {
                int[] DealerData = dealerMasterCodeData.DealerIds;
                foreach (var dealerid in DealerData)
                {
                    DbParameterCollection objDBParam = new DbParameterCollection();
                    objDBParam.Add(new DbParameter("@Id", dealerid, DbType.Int64));
                    objDBParam.Add(new DbParameter("@MasterCode", dealerMasterCodeData.MasterCode, DbType.String));
                    objDBParam.Add(new DbParameter("@NewMasterCode", dealerMasterCodeData.NewMasterCode, DbType.String));
                    objDBParam.Add(new DbParameter("@UserId", dealerMasterCodeData.UserId, DbType.Int64));
                    var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateDealerMasterCode]", objDBParam, CommandType.StoredProcedure);
                    if (!Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    {
                        Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                        break;
                    }
                    Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                    if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                        IsDone = true;
                }
                
                
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateDealerMasterCode", ex.StackTrace, ex.Message);
                Message = "Error occured while updating Dealer Master Code";
            }

            return IsDone == true ? 1 : 0;
        }

        public int ValidateDealerMasterCode(string masterCode, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MasterCode", masterCode, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[ValidateDealerMasterCode]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ValidateDealerMasterCode", ex.StackTrace, ex.Message);
                Message = "Error occured while retrieving Dealer Master Code";
            }

            return IsDone == true ? 1 : 0;
        }
        #endregion Dealer Master Code Update API

        #region Asset issue to SFA
        public  DataTable IssueAssestToSFA(AssetAssignmentToSFA InputParam)
        {
            var datatable = new DataTable();
            var dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("MaterialCode");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("IssuedQuantity");
            dt.Columns.Add("IssuedDate");
            dt.Columns.Add("SFAName");
            dt.Columns.Add("SFACode");
            dt.Columns.Add("Remarks");
            
         for(int i=0;i< InputParam.assetAssignmentToSFAdt.Rows.Count;i++)
            {

                dr = dt.NewRow();
                dr["MaterialCode"] = InputParam.assetAssignmentToSFAdt.Rows[i][0].ToString();
                dr["ProductName"] = InputParam.assetAssignmentToSFAdt.Rows[i][1].ToString();
                dr["IssuedQuantity"] = InputParam.assetAssignmentToSFAdt.Rows[i][2].ToString();
                dr["IssuedDate"] = InputParam.assetAssignmentToSFAdt.Rows[i][3].ToString();
                dr["SFAName"] = InputParam.assetAssignmentToSFAdt.Rows[i][4].ToString();
                dr["SFACode"] = InputParam.assetAssignmentToSFAdt.Rows[i][5].ToString();
                dr["Remarks"] = InputParam.assetAssignmentToSFAdt.Rows[i][6].ToString();
                dt.Rows.Add(dr);
            }

            SqlParameter[] objParameter = new SqlParameter[2];
            try
            {

                objParameter[0] = new SqlParameter("@AssetIssuedToSFA", dt);
                objParameter[1] = new SqlParameter("@UserId", InputParam.UserId);
                datatable = _dataHelper.ExecuteProcedure("[dbo].[AssetIssuedToSFA]", objParameter);
                if (datatable.Rows.Count > 0)
                    return datatable;
                        
            }

            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("IssueAssestToSFA", ex.StackTrace, ex.Message);
            }

            return null;
        }

        public IEnumerable<AssetIssuedToSFAGrid> GetAssetIssuedToSFA(AssetIssuedToSFAGet InputParam)
        {
            IEnumerable<AssetIssuedToSFAGrid> output = null;

            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@RoleName", InputParam.RoleName, DbType.String));
                //objDBParam.Add(new DbParameter("@MaterialName", InputParam.MaterialName, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAssetIssuedToSFA]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from assetissuedtosfa in outputFromSP.AsEnumerable()
                              select new AssetIssuedToSFAGrid
                              {
                                  Id = Convert.ToInt64(assetissuedtosfa.Field<Int64>("Id")),
                                  IssuedDate = assetissuedtosfa.Field<string>("IssuedDate"),
                                  MaterialCode = Convert.ToString(assetissuedtosfa.Field<string>("MaterialCode")),
                                  ProductName = Convert.ToString(assetissuedtosfa.Field<string>("ProductName")),
                                  SFAName = Convert.ToString(assetissuedtosfa.Field<string>("SFAName")),
                                  SFACode = Convert.ToString(assetissuedtosfa.Field<string>("SFACode")),
                                  Remarks = Convert.ToString(assetissuedtosfa.Field<string>("Remarks")),
                                  IssuedQuantity = Convert.ToInt32(assetissuedtosfa.Field<int>("IssuedQty")),
                              });
                }
                else
                    output = new List<AssetIssuedToSFAGrid>();

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAssetIssuedToSFA", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public IEnumerable<AssetsDropDownData> GetAssetsDropDown(AssetIssuedToSFAGet InputParam)
        {
            IEnumerable<AssetsDropDownData> output = null;

            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAssetsDropDown]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from assetissuedtosfa in outputFromSP.AsEnumerable()
                              select new AssetsDropDownData
                              {
                                  Id = Convert.ToInt64(assetissuedtosfa.Field<Int64>("Id")),                                  
                                  MaterialName = Convert.ToString(assetissuedtosfa.Field<string>("MaterialName")),
                                  
                              });
                }
                else
                    output = new List<AssetsDropDownData>();

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("AssetsDropDownData", ex.StackTrace, ex.Message);
            }
            return output;
        }
        #endregion

        #region Asset Assignment To RDI API
        public AssetAssignmentToRDIUpdate GetAssetAssignmentToRDIById(Int64? Id)
        {
            AssetAssignmentToRDIUpdate assetAssignmentToRDI = null;
            DbParameterCollection paramcollection = new DbParameterCollection();
            try
            {
                paramcollection.Add(new DbParameter("@Id", Id, DbType.Int64));
                var outputDBParam = _dataHelper.ExecuteDataTable("[dbo].[GetAssignmentToRDIById]", paramcollection, CommandType.StoredProcedure);
                if (outputDBParam.Rows.Count > 0)
                {
                    assetAssignmentToRDI = (from assetassignmenttoRDI in outputDBParam.AsEnumerable()
                                      select new AssetAssignmentToRDIUpdate
                                      {
                                          Id = Convert.ToInt64(assetassignmenttoRDI.Field<Int64?>("ID")),
                                          IssuedQty = Convert.ToInt32(assetassignmenttoRDI.Field<int>("ISSUEDQTY")),                             

                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAssetAssignmentToRDIById", ex.StackTrace, ex.Message);
            }

            return assetAssignmentToRDI;
        }

        public int UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate assetAssignmentToRDI, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", assetAssignmentToRDI.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@IssuedQty", assetAssignmentToRDI.IssuedQty, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateAssignmentToRDI]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateAssetAssignmentToRDI", ex.StackTrace, ex.Message);
                Message = "Error occured while updating Issued Qty in Asset Assignment to RDI";
            }

            return IsDone == true ? 1 : 0;
        }

        public DataTable UploadAssetAssignmentToRDI(AssetAssignmentToRDIUpload InputParam)
        {
            DataTable datatable = new DataTable();
            datatable = null;
            SqlParameter[] objDBParam = new SqlParameter[2];

            var dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("MaterialCode");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("IssuedQty");
            dt.Columns.Add("IssuedDate");
            dt.Columns.Add("RDIName");
            dt.Columns.Add("RDICode");
            dt.Columns.Add("Place");
            dt.Columns.Add("Reason");
            dt.Columns.Add("ReturnDate");
            dt.Columns.Add("Reference");

            for (int i = 0; i < InputParam.dtAsset.Rows.Count; i++)
            {

                dr = dt.NewRow();
                dr["MaterialCode"] = InputParam.dtAsset.Rows[i][0].ToString();
                dr["ProductName"] = InputParam.dtAsset.Rows[i][1].ToString();
                dr["IssuedQty"] = InputParam.dtAsset.Rows[i][2].ToString();
                dr["IssuedDate"] = InputParam.dtAsset.Rows[i][3].ToString();
                dr["RDIName"] = InputParam.dtAsset.Rows[i][4].ToString();
                dr["RDICode"] = InputParam.dtAsset.Rows[i][5].ToString();
                dr["Place"] = InputParam.dtAsset.Rows[i][6].ToString();
                dr["Reason"] = InputParam.dtAsset.Rows[i][7].ToString();
                dr["ReturnDate"] = InputParam.dtAsset.Rows[i][8].ToString();
                dr["Reference"] = InputParam.dtAsset.Rows[i][9].ToString();
                dt.Rows.Add(dr);
            }


            try
            {
                objDBParam[0] = new SqlParameter("@AssetAssignmentToRDI", dt);
                objDBParam[1] = new SqlParameter("@UserId", InputParam.UserId);
                datatable = _dataHelper.ExecuteProcedure("[dbo].[UploadAssetAssignmentToRDI]", objDBParam);
                
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UploadAssetAssignmentToRDI", ex.StackTrace, ex.Message);
            }

            return datatable;
        }


        #endregion Asset Assignment To RDI API

        #region Asset Collection From SFA API
        public DataTable UploadAssetCollectionFromSFA(AssetCollectionFromSFA InputParam)
        {
            DataTable datatable = new DataTable();
            datatable = null;
            SqlParameter[] objDBParam = new SqlParameter[2];

            var dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("SFACode");
            dt.Columns.Add("SFAName");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("MaterialCode");
            dt.Columns.Add("IssuedQuantity");
            dt.Columns.Add("IssuedDate");
            dt.Columns.Add("ReturnQuantity");
            dt.Columns.Add("Remarks");


            for (int i = 0; i < InputParam.dtAsset.Rows.Count; i++)
            {

                dr = dt.NewRow();
                dr["SFACode"] = InputParam.dtAsset.Rows[i][0].ToString();
                dr["SFAName"] = InputParam.dtAsset.Rows[i][1].ToString();
                dr["ProductName"] = InputParam.dtAsset.Rows[i][2].ToString();
                dr["MaterialCode"] = InputParam.dtAsset.Rows[i][3].ToString();
                dr["IssuedQuantity"] = InputParam.dtAsset.Rows[i][4].ToString();
                dr["IssuedDate"] = InputParam.dtAsset.Rows[i][5].ToString();
                dr["ReturnQuantity"] = InputParam.dtAsset.Rows[i][6].ToString();
                dr["Remarks"] = InputParam.dtAsset.Rows[i][7].ToString();
                
                dt.Rows.Add(dr);
            }

            try
            {
                objDBParam[0] = new SqlParameter("@AssetCollectionFromSFA", dt);
                objDBParam[1] = new SqlParameter("@UserId", InputParam.UserId);
                datatable = _dataHelper.ExecuteProcedure("[dbo].[UploadAssetCollectionFromSFA]", objDBParam);
                if (datatable.Rows.Count > 0)
                    return datatable;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UploadAssetCollectionFromSFA", ex.StackTrace, ex.Message);
                //Message = "Error occured while uploading data to Asset Collection From SFA";
            }

            return datatable;
        }

        public IEnumerable<AssetCollectionFromSFAData> GetAssetCollectionFormatFromSFA(AssetCollectionFromSFAGet Input)
        {
            DataTable dtcode = new DataTable();
            DataRow row; 
            IEnumerable<AssetCollectionFromSFAData> output = null;
            SqlParameter[] objDBParam = new SqlParameter[2];
          
            try
            {

                dtcode.Columns.Add("FilterName");
                if (Input.SFACode != null)
                {
                    foreach (string code in Input.SFACode)
                    {
                        row = dtcode.NewRow();
                        row["FilterName"] = code;
                        dtcode.Rows.Add(row);
                    }
                }

                objDBParam[0] = new SqlParameter("@UserId", Input.UserId);
                objDBParam[1] = new SqlParameter("@SFACode", dtcode);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetAssetCollectionFormatFromSFA]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from data in outputFromSP.AsEnumerable()
                              select new AssetCollectionFromSFAData
                              {
                                  SFAId = Convert.ToInt64(data.Field<Int64>("SFAId")),
                                  SFACode = Convert.ToString(data.Field<String>("SFACode")),
                                  SFAName = Convert.ToString(data.Field<String>("SFAName")),
                                  MaterialCode = Convert.ToString(data.Field<String>("MaterialCode")),
                                  ProductName = Convert.ToString(data.Field<String>("ProductName")),
                                  IssuedQuantity = Convert.ToInt32(data.Field<Int32>("IssuedQuantity")),
                                  ReturnQuantity = Convert.ToInt32(data.Field<Int32>("ReturnQuantity")),
                                  IssuedDate = Convert.ToString(data.Field<String>("IssuedDate")),
                                  Remarks = Convert.ToString(data.Field<String>("Remarks"))
                              }).ToList();
                }
            }

            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAssetCollectionFormatFromSFA", ex.StackTrace, ex.Message);
            }
            return output;
        }
        #endregion Asset Collection From SFA API
    }
}
