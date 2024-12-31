using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Mappings;
using DBHelper;
using System.Data;
using System.Data.SqlClient;
using AmboLibrary.UserManagement;
using AmboProvider.Interface;

namespace AmboProvider.Implimentation
{
    public class MappingProvider : IMappingProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public MappingProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        #region Dealer SFA Mapping
        public int CreateDealerSFAMapping(DealerSFAMapping mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DealerId", mappingData.DealerId, DbType.Int64));
                objDBParam.Add(new DbParameter("@EmployeeId", mappingData.EmployeeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@CreatedBy", mappingData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateDealerSFAMapping]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateDealerSFAMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateDealerSFAMapping(DealerSFAMapping mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DealerId", mappingData.DealerId, DbType.Int64));
                objDBParam.Add(new DbParameter("@MappingId", mappingData.ID, DbType.Int64));
                objDBParam.Add(new DbParameter("@CreatedBy", mappingData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateDealerSFAMapping]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateDealerSFAMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteDealerSFAMapping(DealerSFAMapping mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MappingId", mappingData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", mappingData.UserId, DbType.Int32));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteDealerSFAMapping]", objDBParam, CommandType.StoredProcedure);

                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteDealerSFAMapping", ex.StackTrace, ex.Message);
                Message = "Could not delete Dealer-SFA Mapping. Exception occured in API.";
            }
            return 0;
        }
        #endregion Dealer SFA Mapping

        #region Dealer Classification Mapping
        public DealerClassificationMappingTable GetDealerClassificationMappingTable(DealerClassificationMappingSearch mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            DealerClassificationMappingTable objOutput = new DealerClassificationMappingTable();
            objOutput.ProductCategoryId = mappingData.ProductCategoryId;
            objOutput.BranchId = mappingData.BranchId;
            try
            {
                objDBParam.Add(new DbParameter("ProductCategoryId", mappingData.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("BranchId", mappingData.BranchId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealerClassificationTable]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP != null && outputFromSP.Rows.Count > 0)
                {
                    objOutput.MappingTable = (from row in outputFromSP.AsEnumerable()
                                              select new DealerClassificationMap
                                              {
                                                  DealerId = row.Field<Int64>("DealerId"),
                                                  DealerCode = row.Field<string>("DealerCode"),
                                                  DealerName = row.Field<string>("DealerName"),
                                                  LocationName = row.Field<string>("LocationName"),
                                                  ClassificationId = row.Field<Int64?>("ClassificationId"),
                                              }).ToList();
                    Message = "Dealer Classification Mapping fetched successfully.";
                }
                else
                {
                    Message = "Could not fetch dealers on basis of Product Category and Branch.";
                    objOutput.MappingTable = new List<DealerClassificationMap>();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerClassificationMappingTable", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return objOutput;
        }

        public int CreateDealerClassificationMapping(DealerClassificationMappingTable mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[1];
            DataTable dt = new DataTable();
            DataRow row;
            try
            {
                dt.Columns.Add("DealerId");
                dt.Columns.Add("BranchId");
                dt.Columns.Add("ProductCategoryId");
                dt.Columns.Add("ClassificationId");
                dt.Columns.Add("CreatedBy");

                if (mappingData.MappingTable != null)
                {
                    foreach (DealerClassificationMap mapping in mappingData.MappingTable)
                    {
                        row = dt.NewRow();
                        row[0] = mapping.DealerId;
                        row[1] = mappingData.BranchId;
                        row[2] = mappingData.ProductCategoryId;
                        row[3] = mapping.ClassificationId;
                        row[4] = mappingData.UserId;
                        if (mapping.ClassificationId != null && mapping.ClassificationId != 0)
                            dt.Rows.Add(row);
                    }

                    objDBParam[0] = new SqlParameter("@DataForInsert", dt);
                    var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[CreateDealerClassificationMapping]", objDBParam);
                    Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                    if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                        return 1;
                }
                //added by bela against bug no #1962 as there was no condition to whether mapping exists or is it null
                else
                {
                    Message = "No dealer classification mapping to update";
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateDealerClassificationMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateDealerClassificationMapping(DealerClassificationMapping mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MappingId", mappingData.ID, DbType.Int64));
                objDBParam.Add(new DbParameter("@ClassificationId", mappingData.ClassificationId, DbType.Int64));
                objDBParam.Add(new DbParameter("@CreatedBy", mappingData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateDealerClassificationMapping]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateDealerClassificationMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteDealerClassificationMapping(DealerClassificationMapping mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MappingId", mappingData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", mappingData.UserId, DbType.Int32));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteDealerClassificationMapping]", objDBParam, CommandType.StoredProcedure);

                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteDealerClassificationMapping", ex.StackTrace, ex.Message);
                Message = "Could not delete Dealer-Classification Mapping. Exception occured in API.";
            }
            return 0;
        }

        public int ManageDealerClassificationMapping(DealerProductMapping mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[4];
            DataTable dtMappings = new DataTable();
            DataRow row;
            try
            {
                dtMappings.Columns.Add("ProductCategoryId");
                dtMappings.Columns.Add("ClassificationId");

                foreach (ProductClassificationList mapping in mappingData.listProductClassification)
                {
                    row = dtMappings.NewRow();
                    row[0] = mapping.ProductCategoryId;
                    row[1] = mapping.ClassificationId;
                    dtMappings.Rows.Add(row);
                }

                objDBParam[0] = new SqlParameter("@DealerId", mappingData.DealerId);
                objDBParam[1] = new SqlParameter("@BranchId", mappingData.BranchId);
                objDBParam[2] = new SqlParameter("@CreatedBy", mappingData.UserId);
                objDBParam[3] = new SqlParameter("@DataForInsert", dtMappings);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageDealerClassificationMapping]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ManageDealerClassificationMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            finally
            {
                dtMappings.Dispose();
            }
            return 0;
        }
        #endregion Dealer Classification Mapping

        #region Product Category SFA Mapping
        public int CreateProductCategorySFAMapping(ProductCategorySFAMapping mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[1];
            DataTable dt = new DataTable();
            DataRow row;
            try
            {
                dt.Columns.Add("EmployeeId");
                dt.Columns.Add("ProductCategoryId");
                dt.Columns.Add("CreatedBy");

                foreach (Int64 prodCatId in mappingData.ProductCategoryIds)
                {
                    row = dt.NewRow();
                    row[0] = mappingData.EmployeeId;
                    row[1] = prodCatId;
                    row[2] = mappingData.UserId;
                    dt.Rows.Add(row);
                }

                objDBParam[0] = new SqlParameter("@DataForInsert", dt);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[CreateProductCategorySFAMapping]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateProductCategorySFAMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteProductCategorySFAMapping(ProductCategorySFAMapping mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MappingId", mappingData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", mappingData.UserId, DbType.Int32));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteProductCategorySFAMapping]", objDBParam, CommandType.StoredProcedure);

                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteProductCategorySFAMapping", ex.StackTrace, ex.Message);
                Message = "Could not delete Product Category-SFA Mapping. Exception occured in API.";
            }
            return 0;
        }
        #endregion Product Category SFA Mapping

        #region Employee Structure Mapping
        public int CreateEmployeeStructureMapping(EmployeeStructureMapping mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[1];
            DataTable dt = new DataTable();
            DataRow row;
            try
            {
                dt.Columns.Add("RDIId");
                dt.Columns.Add("SFAId");
                dt.Columns.Add("CreatedBy");

                foreach (Int64 sfaId in mappingData.SFAIds)
                {
                    row = dt.NewRow();
                    row[0] = mappingData.RDIId;
                    row[1] = sfaId;
                    row[2] = mappingData.UserId;
                    dt.Rows.Add(row);
                }

                objDBParam[0] = new SqlParameter("@DataForInsert", dt);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[CreateEmployeeStructureMapping]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateEmployeeStructureMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteEmployeeStructureMapping(EmployeeStructureMapping mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MappingId", mappingData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", mappingData.UserId, DbType.Int32));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteEmployeeStructureMapping]", objDBParam, CommandType.StoredProcedure);

                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteEmployeeStructureMapping", ex.StackTrace, ex.Message);
                Message = "Could not delete Employee Structure Mapping. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<SFAFormData> GetSFAForStructureMapping(SFAForStructureMappingInput objSearchData)
        {            
            IEnumerable<SFAFormData> ListofSFA = null;           
            DataTable dtdivision = new DataTable();
            DataTable dtsfatype = new DataTable();
            DataRow row;
            try
            {
                dtdivision.Columns.Add("FilterId");
                if (objSearchData.DivisionIds != null)
                {
                    foreach (Int64 divisionid in objSearchData.DivisionIds)
                    {
                        row = dtdivision.NewRow();
                        row["FilterId"] = divisionid;
                        dtdivision.Rows.Add(row);
                    }
                }

                dtsfatype.Columns.Add("FilterId");
                if (objSearchData.SFATypeId != null)
                {
                    foreach (Int64 sfatypeid in objSearchData.SFATypeId)
                    {
                        row = dtsfatype.NewRow();
                        row["FilterId"] = sfatypeid;
                        dtsfatype.Rows.Add(row);
                    }
                }
                SqlParameter[] objDBParam = new SqlParameter[3];

                objDBParam[0] = new SqlParameter("@BranchId", objSearchData.BranchId);
                objDBParam[1] = new SqlParameter("@DivisionId", dtdivision);
                objDBParam[2] = new SqlParameter("@SFATypeId", dtsfatype);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSFAForStructureMapping]", objDBParam);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFA = (from sfa in outputFromSP.AsEnumerable()
                                 select new SFAFormData
                                 {
                                     EmployeeId = sfa.Field<Int64>("ID"),
                                     SFAFullName = sfa.Field<string>("SFANAME")
                                 });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAForStructureMapping", ex.StackTrace, ex.Message);
            }
            return ListofSFA;
        }
        #endregion Employee Structure Mapping

        #region User Branch Channel Product Category Mapping
        public int ManageUserBranchChannelPCMapping(UserBranchChannelPCMapping mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[6];
            DataTable dtBranchMap = new DataTable();
            DataTable dtChannelMap = new DataTable();
            DataTable dtProdCatMap = new DataTable();
            DataTable dtDivisionMap = new DataTable();
            DataRow row;
            try
            {
                #region Branch parameters
                dtBranchMap.Columns.Add("UserId");
                dtBranchMap.Columns.Add("BranchId");
                if (mappingData.BranchIds != null)
                {
                    foreach (Int64 branchId in mappingData.BranchIds)
                    {
                        row = dtBranchMap.NewRow();
                        row[0] = mappingData.UserIdForMapping;
                        row[1] = branchId;
                        dtBranchMap.Rows.Add(row);
                    }
                }
                #endregion Branch parameters

                #region Channel parameters
                dtChannelMap.Columns.Add("UserId");
                dtChannelMap.Columns.Add("ChannelId");

                if (mappingData.ChannelIds != null)
                {
                    foreach (Int64 channelId in mappingData.ChannelIds)
                    {
                        row = dtChannelMap.NewRow();
                        row[0] = mappingData.UserIdForMapping;
                        row[1] = channelId;
                        dtChannelMap.Rows.Add(row);
                    }
                }
                #endregion Channel parameters

                #region Product Category parameters
                dtProdCatMap.Columns.Add("UserId");
                dtProdCatMap.Columns.Add("ProdCatId");

                if (mappingData.ProdCatIds != null)
                {
                    foreach (Int64 prodCatId in mappingData.ProdCatIds)
                    {
                        row = dtProdCatMap.NewRow();
                        row[0] = mappingData.UserIdForMapping;
                        row[1] = prodCatId;
                        dtProdCatMap.Rows.Add(row);
                    }
                }
                #endregion Product Category parameters

                #region Division parameters
                dtDivisionMap.Columns.Add("UserId");
                dtDivisionMap.Columns.Add("DivisionId");

                if (mappingData.DivisionIds != null)
                {
                    foreach (Int64 divisionId in mappingData.DivisionIds)
                    {
                        row = dtDivisionMap.NewRow();
                        row[0] = mappingData.UserIdForMapping;
                        row[1] = divisionId;
                        dtDivisionMap.Rows.Add(row);
                    }
                }
                #endregion Division parameters

                objDBParam[0] = new SqlParameter("@BranchMappings", dtBranchMap);
                objDBParam[1] = new SqlParameter("@ChannelMappings", dtChannelMap);
                objDBParam[2] = new SqlParameter("@ProdCatMappings", dtProdCatMap);
                objDBParam[3] = new SqlParameter("@DivisionMappings", dtDivisionMap);
                objDBParam[4] = new SqlParameter("@UserId", mappingData.UserId);
                objDBParam[5] = new SqlParameter("@MappingUserId", mappingData.UserIdForMapping);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageUserBranchChannelPCMapping]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ManageUserBranchChannelPCMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            finally
            {
                dtBranchMap.Dispose();
                dtChannelMap.Dispose();
                dtProdCatMap.Dispose();
                dtDivisionMap.Dispose();
            }
            return 0;
        }

        public UserBranchChannelPCMapping GetUserBranchChannelPCMapping(Int64 MappingUserId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            UserBranchChannelPCMapping outputMapping = new UserBranchChannelPCMapping();
            outputMapping.UserIdForMapping = MappingUserId;
            outputMapping.BranchIds = null;
            outputMapping.ChannelIds = null;
            outputMapping.DivisionIds = null;
            outputMapping.ProdCatIds = null;
            try
            {
                objDBParam.Add(new DbParameter("@UserId", MappingUserId, DbType.Int64));
                //role id
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetUserRoleId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputMapping.RoleId = Convert.ToInt64(outputFromSP.Rows[0]["ROLEID"]);
                    outputMapping.Role = Convert.ToString(outputFromSP.Rows[0]["ROLENAME"]);
                }
                //branch mappings
                outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetUserBranchMapping]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                    outputMapping.BranchIds = outputFromSP.AsEnumerable().Select(r => r.Field<Int64>("BRANCHIDS")).ToArray();

                //channel mappings
                outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetUserChannelMapping]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                    outputMapping.ChannelIds = outputFromSP.AsEnumerable().Select(r => r.Field<Int64>("CHANNELIDS")).ToArray();

                //product category mappings
                outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetUserProductCategoryhMapping]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                    outputMapping.ProdCatIds = outputFromSP.AsEnumerable().Select(r => r.Field<Int64>("PRODCATIDS")).ToArray();

                //division mappings
                outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetUserDivisionMapping]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                    outputMapping.DivisionIds = outputFromSP.AsEnumerable().Select(r => r.Field<Int64>("DIVISIONIDS")).ToArray();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetUserBranchChannelPCMapping", ex.StackTrace, ex.Message);
            }
            return outputMapping;
        }
        #endregion User Branch Channel Product Category Mapping

        #region Incentive Target Category Mapping
        public int ManageIncentiveTargetCategoryMapping(IncentiveTargetCategoryMapping mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[5];
            DataTable dtMappings = new DataTable();
            DataRow row;
            try
            {
                dtMappings.Columns.Add("IncentiveCategoryId");
                dtMappings.Columns.Add("TargetCategoryId");
                dtMappings.Columns.Add("Weightage");
                dtMappings.Columns.Add("TargetTypeId");

                foreach (TargetCategoryList mapping in mappingData.TargetCategoryMappings)
                {
                    row = dtMappings.NewRow();
                    row[0] = 0;
                    row[1] = mapping.TargetCategoryId;
                    row[2] = mapping.Weightage;
                    row[3] = mapping.TargetTypeId;
                    dtMappings.Rows.Add(row);
                }

                objDBParam[0] = new SqlParameter("@IncentiveCategoryId", mappingData.IncentiveCategoryId);
                objDBParam[1] = new SqlParameter("@IncentiveCategoryName", mappingData.IncentiveCategoryName);
                objDBParam[2] = new SqlParameter("@CategoryStatus", mappingData.Status);
                objDBParam[3] = new SqlParameter("@Mappings", dtMappings);
                objDBParam[4] = new SqlParameter("@CreateUpdateBy", mappingData.UserId);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageIncentiveTargetCategoryMapping]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ManageIncentiveTargetCategoryMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            finally
            {
                dtMappings.Dispose();
            }
            return 0;
        }

        public IncentiveTargetCategoryMapping GetIncentiveTargetCategoryMapping(Int64 IncentiveCatId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IncentiveTargetCategoryMapping outputMapping = new IncentiveTargetCategoryMapping();
            outputMapping.IncentiveCategoryId = IncentiveCatId;
            outputMapping.TargetCategoryMappings = null;
            try
            {
                objDBParam.Add(new DbParameter("@IncentiveCategoryId", IncentiveCatId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetIncentiveCategory]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputMapping.IncentiveCategoryName = Convert.ToString(outputFromSP.Rows[0]["INCENTIVECATEGORYNAME"]);
                    outputMapping.Status = Convert.ToBoolean(outputFromSP.Rows[0]["STATUS"]);
                }

                outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetIncentiveTargetCategoryMapping]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputMapping.TargetCategoryMappings = (from mapping in outputFromSP.AsEnumerable()
                                                            select new TargetCategoryList
                                                            {
                                                                TargetCategoryId = Convert.ToInt64(mapping.Field<Int64>("TARGETCATEGORYID")),
                                                                TargetCategoryName = Convert.ToString(mapping.Field<string>("TARGETCATEGORYNAME")),
                                                                Weightage = Convert.ToInt64(mapping.Field<Int64>("WEIGHTAGE")),
                                                                WeightageValue = Convert.ToString(mapping.Field<Int64>("WEIGHTAGE")),
                                                                TargetTypeId = Convert.ToInt64(mapping.Field<Int64>("TARGETTYPEID")),
                                                                TargetTypeName = Convert.ToString(mapping.Field<string>("TARGETTYPENAME"))
                                                            }).ToList();

                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetIncentiveTargetCategoryMapping", ex.StackTrace, ex.Message);
            }
            return outputMapping;
        }
        #endregion Incentive Target Category Mapping

        #region Assign Incentive Category to SFA
        public IncentiveCategorySFAMapping GetSFAForIncentiveCategorySFAMapping(Int64 RDIId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IncentiveCategorySFAMapping outputMapping = new IncentiveCategorySFAMapping();
            outputMapping.MappingExcelData = new DataTable();
            outputMapping.MappingExcelData.Columns.Add("Branch Name");
            outputMapping.MappingExcelData.Columns.Add("City");
            outputMapping.MappingExcelData.Columns.Add("Location");
            outputMapping.MappingExcelData.Columns.Add("SFA Code");
            outputMapping.MappingExcelData.Columns.Add("SFA Name");
            outputMapping.MappingExcelData.Columns.Add("Dealer Code");
            outputMapping.MappingExcelData.Columns.Add("Master Code");
            outputMapping.MappingExcelData.Columns.Add("Dealer Name");
            outputMapping.MappingExcelData.Columns.Add("SFA Category");
            outputMapping.MappingExcelData.Columns.Add("Incentive Category");
            outputMapping.MappingExcelData.Columns.Add("Festival Incentive Category");
            try
            {
                objDBParam.Add(new DbParameter("@RDIId", RDIId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFAForIncentiveCategorySFAMapping]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputMapping.MappingExcelData = outputFromSP;
                    return outputMapping;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAForIncentiveCategorySFAMapping", ex.StackTrace, ex.Message);
            }
            outputMapping.MappingExcelData.Rows.Add(outputMapping.MappingExcelData.NewRow());
            return outputMapping;
        }

        public IncentiveCategorySFAMapping ManageIncentiveCategorySFAMapping(IncentiveCategorySFAMapping mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[2];

            try
            {
                objDBParam[0] = new SqlParameter("@Mappings", mappingData.MappingExcelData);
                objDBParam[1] = new SqlParameter("@CreateUpdateBy", mappingData.UserId);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageIncentiveCategorySFAMapping]", objDBParam);
                mappingData.MappingExcelData = null;
                if (outputFromSP != null)
                {
                    if (outputFromSP.Rows.Count > 0)
                    {
                        mappingData.MappingExcelData = outputFromSP;
                        if (outputFromSP.Columns[0].ColumnName.ToUpper() == "STATUS")
                            Message = Convert.ToString(outputFromSP.Rows[0][1]);
                        else
                            Message = "Excel file for assigning Incentive Category to SFA has been uploaded successfuly.";
                    }
                    else
                        Message = "No data returned. Please contact your administrator";
                }
                else
                    Message = "No data returned. Please contact your administrator";
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ManageIncentiveCategorySFAMapping", ex.StackTrace, ex.Message);
                mappingData.MappingExcelData = null;
                Message = "Unable to save excelsheet to database. Please contact your administrator.";
            }
            finally
            {
                //dtMappings.Dispose();
            }
            return mappingData;
        }
        #endregion Assign Incentive Category to SFA

        #region Product Definition Under Target Category
        public ProdDefUnderTargetCatGridOutput GetProductsByCategorySubCategory(ProdDefUnderTargetCat mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            ProdDefUnderTargetCatGridOutput objOutput = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCatId", mappingData.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCatId", mappingData.ProductSubCategoryId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetProductsByCategorySubCategory]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables.Count > 0)
                {
                    if (outputFromSP.Tables[0].Rows.Count != 0)
                    {

                        //populate all aplicable target category list for category & sub category
                        if (outputFromSP.Tables[1].Rows.Count != 0)
                        {
                            objOutput = new ProdDefUnderTargetCatGridOutput();
                            objOutput.TargetCategoryList = (from category in outputFromSP.Tables[1].AsEnumerable()
                                                            select new TargetCategory
                                                            {
                                                                TargetCategoryId = category.Field<Int64>("TargetCategoryId"),
                                                                TargetCategoryName = category.Field<string>("TargetCategoryName")
                                                            }).ToList();

                            objOutput.objGridRows = (from data in outputFromSP.Tables[0].AsEnumerable()
                                                     select new ProdDefUnderTargetCatGridData
                                                     {
                                                         ProductCategoryId = Convert.ToInt64(data.Field<Int64>("ProductCategoryId")),
                                                         ProductCategoryName = Convert.ToString(data.Field<string>("ProductCategoryName")),
                                                         ProductSubCategoryId = Convert.ToInt64(data.Field<Int64>("ProductSubCategoryId")),
                                                         ProductSubCategoryName = Convert.ToString(data.Field<string>("ProductSubCategoryName")),
                                                         MaterialCode = Convert.ToString(data.Field<string>("MaterialCode")),
                                                         MaterialName = Convert.ToString(data.Field<string>("MaterialName")),
                                                         MOP = Convert.ToString(data.Field<string>("MOP")),
                                                         SelectedTargetCategoryIds = outputFromSP.Tables[2].Rows.Count != 0 ?
                                                        (from x in outputFromSP.Tables[2].Select("MaterialCode='" + data.Field<string>("MaterialCode").Trim() + "'").AsEnumerable()
                                                         select new TargetCategory
                                                         {
                                                             TargetCategoryId = x.Field<Int64>("TargetCategoryId")
                                                         }).ToList() : new List<TargetCategory>()
                                                     }).ToList();

                            Message = "All products under a category and sub category fetched successfully.";
                        }
                        else
                            Message = "No Target Categories are defined for selected Product Category";
                    }
                    else
                        Message = "No products found for given category/sub category.";
                }
                else
                    Message = "Could not fetch products from database. Please contact administrator.";
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductsByCategorySubCategory", ex.StackTrace, ex.Message);
                Message = "Unable to fetch materials. Please contact administrator.";
            }
            return objOutput;
        }

        public bool ManageProductDefinitionUnderTargetCategory(ProdDefUnderTargetCatGridOutput mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[2];
            DataTable dtMappings = new DataTable();
            DataRow row;
            try
            {
                dtMappings.Columns.Add("ProductCategoryId");
                dtMappings.Columns.Add("ProductSubCategoryId");
                dtMappings.Columns.Add("MaterialCode");
                dtMappings.Columns.Add("TargetCategoryId");

                if (mappingData.objGridRows != null && mappingData.objGridRows.Count > 0)
                {
                    foreach (ProdDefUnderTargetCatGridData gridrow in mappingData.objGridRows)
                    {
                        if (gridrow.SelectedTargetCategoryIds == null)
                        {
                            row = dtMappings.NewRow();
                            row["ProductCategoryId"] = gridrow.ProductCategoryId;
                            row["ProductSubCategoryId"] = gridrow.ProductSubCategoryId;
                            row["MaterialCode"] = gridrow.MaterialCode;
                            row["TargetCategoryId"] = 0;
                            dtMappings.Rows.Add(row);
                        }
                        else
                        {
                            foreach (TargetCategory target in gridrow.SelectedTargetCategoryIds)
                            {
                                //merge all targets
                                row = dtMappings.NewRow();
                                row["ProductCategoryId"] = gridrow.ProductCategoryId;
                                row["ProductSubCategoryId"] = gridrow.ProductSubCategoryId;
                                row["MaterialCode"] = gridrow.MaterialCode;
                                row["TargetCategoryId"] = target.TargetCategoryId;
                                dtMappings.Rows.Add(row);
                            }
                        }
                    }
                }

                objDBParam[0] = new SqlParameter("@Mappings", dtMappings);
                objDBParam[1] = new SqlParameter("@CreateUpdateBy", mappingData.objGridRows[0].UserId);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageProductDefinitionUnderTargetCategory]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ManageProductDefinitionUnderTargetCategory", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            finally
            {
                dtMappings.Dispose();
            }
            return false;
        }

        public ProdDefUnderTargetCatGridOutput GetProductsByCategorySubCategoryforallMat(ProdDefUnderTargetCatforAllMat mappingData, out string Message)
        {
            Message = string.Empty;
            
            ProdDefUnderTargetCatGridOutput objOutput = null;
            DataTable dtprocats = new DataTable();
            DataTable dtprosubcats = new DataTable();
            DataRow row;
            try
            {
                dtprocats.Columns.Add("FilterId");
                if (mappingData.ProductCategoryId != null)
                {
                    foreach (Int64 procatid in mappingData.ProductCategoryId)
                    {
                        row = dtprocats.NewRow();
                        row["FilterId"] = procatid;
                        dtprocats.Rows.Add(row);
                    }
                }

                dtprosubcats.Columns.Add("FilterId");
                if (mappingData.ProductSubCategoryId != null)
                {
                    foreach (Int64 prosuccatid in mappingData.ProductSubCategoryId)
                    {
                        row = dtprosubcats.NewRow();
                        row["FilterId"] = prosuccatid;
                        dtprosubcats.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[2];
                objDBParam[0] = new SqlParameter("@ProductIds", dtprocats);
                objDBParam[1] = new SqlParameter("@SubProductIds", dtprosubcats);
               

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetProductsByCategorySubCategoryforAllMat]", objDBParam);

                if (outputFromSP.Tables.Count > 0)
                {
                    if (outputFromSP.Tables[0].Rows.Count != 0)
                    {

                        //populate all aplicable target category list for category & sub category
                        if (outputFromSP.Tables[1].Rows.Count != 0)
                        {
                            objOutput = new ProdDefUnderTargetCatGridOutput();
                            objOutput.TargetCategoryList = (from category in outputFromSP.Tables[1].AsEnumerable()
                                                            select new TargetCategory
                                                            {
                                                                TargetCategoryId = category.Field<Int64>("TargetCategoryId"),
                                                                TargetCategoryName = category.Field<string>("TargetCategoryName")
                                                            }).ToList();

                            objOutput.objGridRows = (from data in outputFromSP.Tables[0].AsEnumerable()
                                                     select new ProdDefUnderTargetCatGridData
                                                     {
                                                         ProductCategoryId = Convert.ToInt64(data.Field<Int64>("ProductCategoryId")),
                                                         ProductCategoryName = Convert.ToString(data.Field<string>("ProductCategoryName")),
                                                         ProductSubCategoryId = Convert.ToInt64(data.Field<Int64>("ProductSubCategoryId")),
                                                         ProductSubCategoryName = Convert.ToString(data.Field<string>("ProductSubCategoryName")),
                                                         MaterialCode = Convert.ToString(data.Field<string>("MaterialCode")),
                                                         MaterialName = Convert.ToString(data.Field<string>("MaterialName")),
                                                         MOP = Convert.ToString(data.Field<string>("MOP")),


                                                         SelectedTargetCategoryIds = outputFromSP.Tables[2].Rows.Count != 0 ?

                                                         (from x in outputFromSP.Tables[2].Select("MaterialId='" + data.Field<string>("MaterialCode").Trim() + "'").AsEnumerable()
                                                          select new TargetCategory
                                                          {
                                                              TargetCategoryId = x.Field<Int64>("TargetCategoryId")
                                                          }).ToList() : new List<TargetCategory>()

                                                     }).ToList();

                            
                            Message = "All products under a category and sub category fetched successfully.";
                        }
                        else
                            Message = "No Target Categories are defined for selected Product Category";
                    }
                    else
                        Message = "No products found for given category/sub category.";
                }
                else
                    Message = "Could not fetch products from database. Please contact administrator.";
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductsByCategorySubCategory", ex.StackTrace, ex.Message);
                Message = "Unable to fetch materials. Please contact administrator.";
            }
            return objOutput;
        }
        #endregion Product Definition Under Target Category

        #region Assign Target To SFA API
        public DataTable UploadTargetToSFAByMonth(AssignTargetToSFAUpload targetData)
        {
            var datatable = new DataTable();
            SqlParameter[] objParameter = new SqlParameter[3];
            var dt = new DataTable();
            DataRow dr;
            //IDbTransaction transaction = null;
            
            dt.Columns.Add("Month");
            dt.Columns.Add("Branch");
            dt.Columns.Add("City");
            dt.Columns.Add("Location");
            dt.Columns.Add("SFACode");
            dt.Columns.Add("SFAName");
            dt.Columns.Add("DealerCode");
            dt.Columns.Add("MasterCode");
            dt.Columns.Add("DealerName");
            dt.Columns.Add("SFACategory");
            dt.Columns.Add("IncentiveCategory");
            dt.Columns.Add("TargetCategory");
            dt.Columns.Add("ProductCategory");
            dt.Columns.Add("QtyTarget");
            dt.Columns.Add("ValueTarget");

            for (int i = 0; i < targetData.dtAsset.Rows.Count; i++)
            {

                dr = dt.NewRow();
                dr["Month"] = targetData.dtAsset.Rows[i][0].ToString();
                dr["Branch"] = targetData.dtAsset.Rows[i][1].ToString();
                dr["City"] = targetData.dtAsset.Rows[i][2].ToString();
                dr["Location"] = targetData.dtAsset.Rows[i][3].ToString();
                dr["SFACode"] = targetData.dtAsset.Rows[i][4].ToString();
                dr["SFAName"] = targetData.dtAsset.Rows[i][5].ToString();
                dr["DealerCode"] = targetData.dtAsset.Rows[i][6].ToString();
                dr["MasterCode"] = targetData.dtAsset.Rows[i][7].ToString();
                dr["DealerName"] = targetData.dtAsset.Rows[i][8].ToString();
                dr["SFACategory"] = targetData.dtAsset.Rows[i][9].ToString();
                dr["IncentiveCategory"] = targetData.dtAsset.Rows[i][10].ToString();
                dr["TargetCategory"] = targetData.dtAsset.Rows[i][11].ToString();
                dr["ProductCategory"] = targetData.dtAsset.Rows[i][12].ToString();
                dr["QtyTarget"] = targetData.dtAsset.Rows[i][13].ToString();
                dr["ValueTarget"] = targetData.dtAsset.Rows[i][14].ToString();
                dt.Rows.Add(dr);
            }
            try
            {
                //transaction = _dataHelper.BeginTransaction();
                objParameter[0] = new SqlParameter("@TargetToSFA", dt);
                objParameter[1] = new SqlParameter("@UserId", targetData.UserId);
                objParameter[2] = new SqlParameter("@TargetMonth", targetData.TargetDate);
                datatable = _dataHelper.ExecuteProcedure("[dbo].[UploadTargetToSFAByMonth]", objParameter);
                if (datatable.Rows.Count > 0)
                {
                    //_dataHelper.CommitTransaction(transaction);
                    return datatable;
                }

            }

            catch (Exception ex)
            {
                //transaction.Rollback();
                _IErrorLogProvider.CreateErrorLog("UploadTargetToSFAByMonth", ex.StackTrace, ex.Message);
            }

            return null;
        }

        public AssignTargetToSFAGet GetBranchByUserId(Int64 UserId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            AssignTargetToSFAGet output = new AssignTargetToSFAGet();
            output = null;
            try
            {
                objDBParam.Add(new DbParameter("@UserId", UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBranchByUserId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from targettosfa in outputFromSP.AsEnumerable()
                              select new AssignTargetToSFAGet
                              {
                                  BranchId = targettosfa.Field<Int64>("BRANCHID"),
                                  Branch = targettosfa.Field<string>("BRANCH")
                              }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetBranchByUserId", ex.StackTrace, ex.Message);
            }
            return output;
        }
        #endregion Assign Target To SFA API

        #region Navigation Overloads
        public int CreateIncentiveCategorySFAMapping(NavigationIncentiveCategorySFAMapping mappingData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DealerId", mappingData.DealerId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", mappingData.EmployeeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IncentiveId", mappingData.IncentiveCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@CreatedBy", mappingData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateIncentiveCategorySFAMapping]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateIncentiveCategorySFAMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }
        #endregion Navigation Overloads

        #region Sales PIC Outlet Mapping
        public bool ManageSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[1];
            DataTable dt = new DataTable();
            DataRow row;
            try
            {
                dt.Columns.Add("BranchId");
                dt.Columns.Add("SalesPICId");
                dt.Columns.Add("DealerId");
                dt.Columns.Add("CreatedBy");
                if (objFormData.DealerIds != null)
                {
                    foreach (Int64 dealerId in objFormData.DealerIds)
                    {
                        row = dt.NewRow();
                        row[0] = objFormData.BranchId;
                        row[1] = objFormData.SalesPICId;
                        row[2] = dealerId;
                        row[3] = objFormData.UserId;
                        dt.Rows.Add(row);
                    }
                }
                objDBParam[0] = new SqlParameter("@Mappings", dt);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageSalesPICOutletMapping]", objDBParam);
                if (outputFromSP != null)
                {
                    if (outputFromSP.Rows.Count > 0)
                    {
                        Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                        if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                            return true;
                    }
                    else
                        Message = "No data returned. Please contact your administrator";
                }
                else
                    Message = "No data returned. Please contact your administrator";
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ManageSalesPICOutletMapping", ex.StackTrace, ex.Message);
                Message = "Unable to save Sales PIC Outlet mapping to database. Please contact your administrator.";
            }
            finally
            {
                //dtMappings.Dispose();
            }
            return false;
        }

        public ManageSalesPICOutletMappingForm GetSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SalesPICId", objFormData.SalesPICId, DbType.Int64));
                var output = _dataHelper.ExecuteDataSet("GetSalesPICOutletMapping", objDBParam, CommandType.StoredProcedure);
                if (output != null)
                {
                    if (output.Tables.Count == 2)
                    {
                        if (output.Tables[0].Rows.Count == 1)
                            objFormData.BranchId = Convert.ToInt64(output.Tables[0].Rows[0][0]);
                        else
                            Message = "Could not populate Branch of employee. Please contact your administrator.";

                        if (output.Tables[1].Rows.Count > 0)
                            objFormData.DealerIds = output.Tables[1].Rows.OfType<DataRow>().Select(dr => dr.Field<Int64>("DealerId")).Distinct().ToList();
                        else
                            Message = "Could not populate mapped Non SFA outlets. Please contact your administrator.";

                        if (output.Tables[1].Rows.Count > 0)
                            objFormData.NonSFAMasterCodes = output.Tables[1].Rows.OfType<DataRow>().Select(dr => dr.Field<string>("MasterCode")).Distinct().ToList();
                        else
                            Message = "Could not populate mapped Non SFA dealer master codes. Please contact your administrator.";
                    }
                    else
                        Message = "Invalid data returned from database. Please contact your administrator.";
                }
                else
                    Message = "No data returned from database. Please contact your administrator.";
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSalesPICOutletMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            finally { }
            return objFormData;
        }

        public bool DeleteSalesPICOutletMapping(DeleteSalesPICOutletMappingForm objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SalesPICId", objFormData.SalesPICId, DbType.Int64));
                objDBParam.Add(new DbParameter("@CreatedBy", objFormData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteSalesPICOutletMapping]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteSalesPICOutletMapping", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return false;
        }
        #endregion Sales PIC Outlet Mapping

        #region Assign Festival Target To SFA API
        public List<AssignFestivalTargetGrid> GetFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<AssignFestivalTargetGrid> targetlist = null;
            try
            {
                objDBParam.Add(new DbParameter("@FestivalSchemeId", objInput.FestivalSchemeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", objInput.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetFestivalTargetToSFABySchemeId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {

                    targetlist = (from data in outputFromSP.AsEnumerable()
                                  select new AssignFestivalTargetGrid
                                  {
                                      FestivalScheme = Convert.ToString(data.Field<string>("FestivalScheme")),
                                      Branch = Convert.ToString(data.Field<string>("Branch")),
                                      City = Convert.ToString(data.Field<string>("City")),
                                      Location = Convert.ToString(data.Field<string>("Location")),
                                      SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                      SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                      DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                      MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                      DealerName = Convert.ToString(data.Field<string>("DealerName")),
                                      SFACategory = Convert.ToString(data.Field<string>("SFACategory")),
                                      FestivalIncentiveCategory = Convert.ToString(data.Field<string>("FestivalIncentiveCategory")),
                                      TargetCategory = Convert.ToString(data.Field<string>("TargetCategory")),
                                      ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                      QtyTarget = Convert.ToString(data.Field<string>("QtyTarget")),
                                      ValueTarget = Convert.ToString(data.Field<string>("ValueTarget"))
                                  }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetFestivalTargetToSFABySchemeId", ex.StackTrace, ex.Message);
            }
            return targetlist;
        }

        public List<AssignFestivalTargetGrid> ShowFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<AssignFestivalTargetGrid> targetlist = null;
            try
            {
                objDBParam.Add(new DbParameter("@FestivalSchemeId", objInput.FestivalSchemeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", objInput.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[ShowFestivalTargetToSFABySchemeId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {

                    targetlist = (from data in outputFromSP.AsEnumerable()
                                  select new AssignFestivalTargetGrid
                                  {
                                      FestivalScheme = Convert.ToString(data.Field<string>("FestivalScheme")),
                                      Branch = Convert.ToString(data.Field<string>("Branch")),
                                      City = Convert.ToString(data.Field<string>("City")),
                                      Location = Convert.ToString(data.Field<string>("Location")),
                                      SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                      SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                      DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                      MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                      DealerName = Convert.ToString(data.Field<string>("DealerName")),
                                      SFACategory = Convert.ToString(data.Field<string>("SFACategory")),
                                      FestivalIncentiveCategory = Convert.ToString(data.Field<string>("FestivalIncentiveCategory")),
                                      TargetCategory = Convert.ToString(data.Field<string>("TargetCategory")),
                                      ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                      QtyTarget = Convert.ToString(data.Field<string>("QtyTarget")),
                                      ValueTarget = Convert.ToString(data.Field<string>("ValueTarget"))
                                  }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetFestivalTargetToSFABySchemeId", ex.StackTrace, ex.Message);
            }
            return targetlist;
        }

        public DataTable UploadFestivalTargetToSFAByScheme(AssignFestivalTargetUpload objInput)
        {
            DataTable datatable = new DataTable();
            datatable = null;
            SqlParameter[] objParameter = new SqlParameter[3];
            var dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("FestivalScheme");
            dt.Columns.Add("Branch");
            dt.Columns.Add("City");
            dt.Columns.Add("Location");
            dt.Columns.Add("SFACode");
            dt.Columns.Add("SFAName");
            dt.Columns.Add("DealerCode");
            dt.Columns.Add("MasterCode");
            dt.Columns.Add("DealerName");
            dt.Columns.Add("SFACategory");
            dt.Columns.Add("FestivalIncentiveCategory");
            dt.Columns.Add("TargetCategory");
            dt.Columns.Add("ProductCategory");
            dt.Columns.Add("QtyTarget");
            dt.Columns.Add("ValueTarget");

            for (int i = 0; i < objInput.dtAsset.Rows.Count; i++)
            {

                dr = dt.NewRow();
                dr["FestivalScheme"] = objInput.dtAsset.Rows[i][0].ToString();
                dr["Branch"] = objInput.dtAsset.Rows[i][1].ToString();
                dr["City"] = objInput.dtAsset.Rows[i][2].ToString();
                dr["Location"] = objInput.dtAsset.Rows[i][3].ToString();
                dr["SFACode"] = objInput.dtAsset.Rows[i][4].ToString();
                dr["SFAName"] = objInput.dtAsset.Rows[i][5].ToString();
                dr["DealerCode"] = objInput.dtAsset.Rows[i][6].ToString();
                dr["MasterCode"] = objInput.dtAsset.Rows[i][7].ToString();
                dr["DealerName"] = objInput.dtAsset.Rows[i][8].ToString();
                dr["SFACategory"] = objInput.dtAsset.Rows[i][9].ToString();
                dr["FestivalIncentiveCategory"] = objInput.dtAsset.Rows[i][10].ToString();
                dr["TargetCategory"] = objInput.dtAsset.Rows[i][11].ToString();
                dr["ProductCategory"] = objInput.dtAsset.Rows[i][12].ToString();
                dr["QtyTarget"] = objInput.dtAsset.Rows[i][13].ToString();
                dr["ValueTarget"] = objInput.dtAsset.Rows[i][14].ToString();
                dt.Rows.Add(dr);
            }
            try
            {
                objParameter[0] = new SqlParameter("@FestivalTargetAssignToSFA", dt);
                objParameter[1] = new SqlParameter("@UserId", objInput.UserId);
                objParameter[2] = new SqlParameter("@SchemeId", objInput.FestivalSchemeId);
                datatable = _dataHelper.ExecuteProcedure("[dbo].[UploadFestivalTargetToSFAByScheme]", objParameter);
                if (datatable.Rows.Count > 0)
                    return datatable;
            }

            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UploadFestivalTargetToSFAByScheme", ex.StackTrace, ex.Message);
            }

            return null;
        }
        #endregion Assign Festival Target To SFA API

        #region Nikita kawade 9/9/2024

        public DataTable UploadReportQSRQuantity(QSRQuantityUpload targetData)
        {
            var datatable = new DataTable();
            SqlParameter[] objParameter = new SqlParameter[3];
            var dt = new DataTable();
            DataRow dr;
            //IDbTransaction transaction = null;
            dt.Columns.Add("Branch");
            dt.Columns.Add("Date");
            dt.Columns.Add("DealerName");
            dt.Columns.Add("City");
            dt.Columns.Add("Location");
            dt.Columns.Add("PayerName");
            dt.Columns.Add("Channel");
            dt.Columns.Add("DealerState");
            dt.Columns.Add("MasterCode");
            dt.Columns.Add("DealerCode");
            dt.Columns.Add("DealerClassification");
            dt.Columns.Add("SFACode");
            dt.Columns.Add("SFAName");
            dt.Columns.Add("SFALevel");
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("ProductCategory");
            dt.Columns.Add("Material");
            dt.Columns.Add("SFAType");
            dt.Columns.Add("AmboQuantity");
            dt.Columns.Add("QSRQuantity");
            dt.Columns.Add("FinalQuantity");

            for (int i = 0; i < targetData.dtAsset.Rows.Count; i++)
            {

                dr = dt.NewRow();
                dr["Branch"] = targetData.dtAsset.Rows[i][0].ToString();
                dr["Date"] = targetData.dtAsset.Rows[i][1].ToString();
                dr["DealerName"] = targetData.dtAsset.Rows[i][2].ToString();
                dr["City"] = targetData.dtAsset.Rows[i][3].ToString();
                dr["Location"] = targetData.dtAsset.Rows[i][4].ToString();
                dr["PayerName"] = targetData.dtAsset.Rows[i][5].ToString();
                dr["Channel"] = targetData.dtAsset.Rows[i][6].ToString();
                dr["DealerState"] = targetData.dtAsset.Rows[i][7].ToString();
                dr["MasterCode"] = targetData.dtAsset.Rows[i][8].ToString();
                dr["DealerCode"] = targetData.dtAsset.Rows[i][9].ToString();
                dr["DealerClassification"] = targetData.dtAsset.Rows[i][10].ToString();
                dr["SFACode"] = targetData.dtAsset.Rows[i][11].ToString();
               // dr["SFAName"] = targetData.dtAsset.Rows[i][12].ToString();
                dr["SFAName"] = targetData.dtAsset.Rows[i][12].ToString();
                dr["SFALevel"] = targetData.dtAsset.Rows[i][13].ToString();
                dr["CompanyName"] = targetData.dtAsset.Rows[i][14].ToString();
                dr["ProductCategory"] = targetData.dtAsset.Rows[i][15].ToString();
                dr["Material"] = targetData.dtAsset.Rows[i][16].ToString();
                dr["SFAType"] = targetData.dtAsset.Rows[i][17].ToString();
                dr["AmboQuantity"] = targetData.dtAsset.Rows[i][18].ToString();
                dr["QSRQuantity"] = targetData.dtAsset.Rows[i][19].ToString();
                dr["FinalQuantity"] = targetData.dtAsset.Rows[i][20].ToString();
                dt.Rows.Add(dr);
            }
            try
            {
                //transaction = _dataHelper.BeginTransaction();
                objParameter[0] = new SqlParameter("@TargetToSFA", dt);
                objParameter[1] = new SqlParameter("@UserId", targetData.UserId);
                objParameter[2] = new SqlParameter("@TargetMonth", targetData.TargetDate);
                datatable = _dataHelper.ExecuteProcedure("[dbo].[UploadQSRQuantity]", objParameter);
                if (datatable.Rows.Count > 0)
                {
                    //_dataHelper.CommitTransaction(transaction);
                    return datatable;
                }

            }

            catch (Exception ex)
            {
                //transaction.Rollback();
                _IErrorLogProvider.CreateErrorLog("UploadTargetToSFAByMonth", ex.StackTrace, ex.Message);
            }

            return null;
        }

        #endregion


    }
}
