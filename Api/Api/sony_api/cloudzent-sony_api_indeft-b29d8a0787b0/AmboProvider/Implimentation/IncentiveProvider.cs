using AmboProvider.Interface;
using DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.IncentiveManagement;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using AmboLibrary.WebReports;

namespace AmboProvider.Implimentation
{
    public class IncentiveProvider : IIncentiveProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public IncentiveProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        public bool ManageBaseIncentiveDefinition(CreateBaseIncentiveForm objFormData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[5];
            DataTable dtSlabDefinition = new DataTable();
            DataTable dtSlabDefinition_SFALevelMap = new DataTable();
            DataRow rowSlabDefinition;
            DataRow rowSlabDefinition_SFALevelMap;
            try
            {
                //define table 1
                dtSlabDefinition.Columns.Add("Id");
                dtSlabDefinition.Columns.Add("TargetTypeId");
                dtSlabDefinition.Columns.Add("Minimum");
                dtSlabDefinition.Columns.Add("Maximum");

                dtSlabDefinition_SFALevelMap.Columns.Add("DefinitionId");
                dtSlabDefinition_SFALevelMap.Columns.Add("SFALevelId");
                dtSlabDefinition_SFALevelMap.Columns.Add("Amount");

                foreach(BaseIncentiveDefinition definition in objFormData.objDefinitionData)
                {
                    var _id = dtSlabDefinition.Rows.Count + 1;
                    rowSlabDefinition = dtSlabDefinition.NewRow();
                    rowSlabDefinition["Id"] = _id;
                    rowSlabDefinition["TargetTypeId"] = definition.TargetTypeId;
                    rowSlabDefinition["Minimum"] = definition.Minimum;
                    rowSlabDefinition["Maximum"] = definition.Maximum;
                    dtSlabDefinition.Rows.Add(rowSlabDefinition);
                    foreach(SFALevelAmountMap sfalevelmap in definition.objAmountMappings)
                    {
                        rowSlabDefinition_SFALevelMap = dtSlabDefinition_SFALevelMap.NewRow();
                        rowSlabDefinition_SFALevelMap["DefinitionId"] = _id;
                        rowSlabDefinition_SFALevelMap["SFALevelId"] = sfalevelmap.SFALevelId;
                        rowSlabDefinition_SFALevelMap["Amount"] = sfalevelmap.Amount;
                        dtSlabDefinition_SFALevelMap.Rows.Add(rowSlabDefinition_SFALevelMap);
                    }

                }
                
                objDBParam[0] = new SqlParameter("@dtSlabDefinition", dtSlabDefinition);
                objDBParam[1] = new SqlParameter("@dtSlabDefinition_SFALevelMap", dtSlabDefinition_SFALevelMap);
                objDBParam[2] = new SqlParameter("@TargetCategoryId", objFormData.TargetCategoryId);
                objDBParam[3] = new SqlParameter("@Status", objFormData.Status);
                objDBParam[4] = new SqlParameter("@CreatedBy", objFormData.UserId);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageBaseIncentiveDefinition]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            finally
            {
                dtSlabDefinition.Dispose();
                dtSlabDefinition_SFALevelMap.Dispose();
                rowSlabDefinition = null;
                rowSlabDefinition_SFALevelMap = null;
            }
            return false;
        }

        public bool DeleteBaseIncentiveDefinition(DeleteBaseIncentiveForm objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@TargetCategoryId", objFormData.TargetCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", objFormData.UserId, DbType.Int64));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteBaseIncentiveDefinition]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return false;
        }

        public CreateBaseIncentiveForm GetBaseIncentiveDefinitionByTargetCategoryId(GetBaseIncentive objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            CreateBaseIncentiveForm objOutput = null;
            try
            {
                objDBParam.Add(new DbParameter("@TargetCategoryId", objFormData.TargetCategoryId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetBaseIncentiveDefinitionByTargetCategoryId]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables.Count > 0)
                {
                    if (outputFromSP.Tables[0].Rows.Count != 0)
                    {
                        objOutput = new CreateBaseIncentiveForm();
                        objOutput.TargetCategoryId = Convert.ToInt64(outputFromSP.Tables[0].Rows[0][1]);
                        objOutput.Status = Convert.ToBoolean(outputFromSP.Tables[0].Rows[0][6]);
                        objOutput.objDefinitionData = new List<BaseIncentiveDefinition>();
                        foreach (DataRow row in outputFromSP.Tables[0].Rows)
                        {
                            objOutput.objDefinitionData.Add(new BaseIncentiveDefinition {
                                TargetTypeId = Convert.ToInt64(row["TARGETTYPEID"]),
                                TargetType = Convert.ToString(row["TARGETTYPE"]),
                                Minimum = Convert.ToDecimal(row["MINIMUM"]),
                                Maximum = Convert.ToDecimal(row["MAXIMUM"]),
                                objAmountMappings = (from amountrow in outputFromSP.Tables[1].AsEnumerable() where
                                                    amountrow.Field<Int64>("DEFINITIONID") == Convert.ToInt64(row["ID"])
                                                     select new SFALevelAmountMap
                                                     {
                                                         SFALevelId = amountrow.Field<Int64>("SFALEVELID"),
                                                         Amount = amountrow.Field<Int64>("AMOUNT")

                                                     }).ToList()
                                });
                        }
                    }
                    else
                        Message = "No base incentive definition found for selected target category.";
                }
                else
                    Message = "Could not fetch base incentive definition from database. Please contact administrator.";
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Exception while fetching base incentive definition from database. Please contact administrator.";
                return null;
            }
            return objOutput;
        }

        public PerPieceIncentiveValues GetPerPieceIncentiveExcelFile(DownloadPerPieceIncentiveExcel objDownloadData, out string Message)
        {
            Message = string.Empty;
            PerPieceIncentiveValues output = new PerPieceIncentiveValues();
            SqlParameter[] objDBParam = new SqlParameter[3];
            DataTable dtProdCatIds = new DataTable();
            DataRow row;
            output.ExcelData = new DataTable();

            output.ExcelData.Columns.Add("Product Category");
            output.ExcelData.Columns.Add("Target Category");
 
            output.ExcelData.Columns.Add("Product Name");
            output.ExcelData.Columns.Add("Material Code");
            output.ExcelData.Columns.Add("Min Qty");
            output.ExcelData.Columns.Add("Max Qty");
            output.ExcelData.Columns.Add("Incentive Amount");
            output.ExcelData.Columns.Add("Min % Ach");
            output.ExcelData.Columns.Add("Max % Ach");
            
            try
            {
                if (objDownloadData.ProductCategoryIds == null || objDownloadData.ProductCategoryIds.Count == 0)
                {
                    Message = "No product category provided for downloading excel.";
                    output.ExcelData.Rows.Add(output.ExcelData.NewRow());
                    return output;
                }

                dtProdCatIds.Columns.Add("UserId");
                dtProdCatIds.Columns.Add("ProdCatId");

                foreach (Int64 categoryId in objDownloadData.ProductCategoryIds)
                {
                    row = dtProdCatIds.NewRow();
                    row["UserId"] = objDownloadData.UserId;
                    row["ProdCatId"] = categoryId;
                    dtProdCatIds.Rows.Add(row);
                }

                objDBParam[0] = new SqlParameter("@ProductCategoryIds", dtProdCatIds);
                objDBParam[1] = new SqlParameter("@SchemeId", objDownloadData.SchemeId);
                objDBParam[2] = new SqlParameter("@Applicability", objDownloadData.Applicability);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetPerPieceIncentiveExcelFile]", objDBParam);
                if (outputFromSP.Rows.Count > 0)
                {
                    output.ExcelData = outputFromSP;
                    Message = "Excel data fetched successfully.";
                    return output;
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Could not download excel data from database. Please contact your administrator.";
            }
            output.ExcelData.Rows.Add(output.ExcelData.NewRow());
            return output;
        }

        public bool ManagePerPieceIncentiveDefinition(CreatePerPieceIncentive objFormData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[12];
            DataTable dtProdCat = new DataTable();
            DataTable dtTargetCat = new DataTable();
            DataTable dtPPTargetCat = new DataTable();
            DataTable dtDivision = new DataTable();
            DataTable dtBranch = new DataTable();
            DataTable dtChannel = new DataTable();
            DataRow row;
            try
            {

                #region Branch parameters
                dtBranch.Columns.Add("UserId");
                dtBranch.Columns.Add("BranchId");
                if (objFormData.BranchIds != null)
                {
                    foreach (Int64 branchId in objFormData.BranchIds)
                    {
                        row = dtBranch.NewRow();
                        row[0] = objFormData.UserId;
                        row[1] = branchId;
                        dtBranch.Rows.Add(row);
                    }
                }
                #endregion Branch parameters

                #region Channel parameters
                dtChannel.Columns.Add("UserId");
                dtChannel.Columns.Add("ChannelId");

                if (objFormData.ChannelIds != null)
                {
                    foreach (Int64 channelId in objFormData.ChannelIds)
                    {
                        row = dtChannel.NewRow();
                        row[0] = objFormData.UserId;
                        row[1] = channelId;
                        dtChannel.Rows.Add(row);
                    }
                }
                #endregion Channel parameters

                #region Product Category parameters
                dtProdCat.Columns.Add("UserId");
                dtProdCat.Columns.Add("ProdCatId");

                if (objFormData.ProductCategoryIds != null)
                {
                    foreach (Int64 prodCatId in objFormData.ProductCategoryIds)
                    {
                        row = dtProdCat.NewRow();
                        row[0] = objFormData.UserId;
                        row[1] = prodCatId;
                        dtProdCat.Rows.Add(row);
                    }
                }
                #endregion Product Category parameters

                #region Division parameters
                dtDivision.Columns.Add("UserId");
                dtDivision.Columns.Add("DivisionId");

                if (objFormData.DivisionIds != null)
                {
                    foreach (Int64 divisionId in objFormData.DivisionIds)
                    {
                        row = dtDivision.NewRow();
                        row[0] = objFormData.UserId;
                        row[1] = divisionId;
                        dtDivision.Rows.Add(row);
                    }
                }
                #endregion Division parameters

                #region Target Category parameters
                dtTargetCat.Columns.Add("TargetCatId");

                if (objFormData.TargetCategoryIds != null)
                {
                    foreach (Int64 targetId in objFormData.TargetCategoryIds)
                    {
                        row = dtTargetCat.NewRow();
                        row[0] = targetId;
                        dtTargetCat.Rows.Add(row);
                    }
                }
                #endregion Target Category parameters

                #region PerPiece Target Category parameters
                dtPPTargetCat.Columns.Add("PPTargetCatId");

                if (objFormData.PerPieceTargetCategoryIds != null)
                {
                    foreach (Int64 targetId in objFormData.PerPieceTargetCategoryIds)
                    {
                        row = dtPPTargetCat.NewRow();
                        row[0] = targetId;
                        dtPPTargetCat.Rows.Add(row);
                    }
                }
                #endregion PerPiece Target Category parameters


                objDBParam[0] = new SqlParameter("@SchemeId", objFormData.SchemeId);
                objDBParam[1] = new SqlParameter("@SchemeName", objFormData.SchemeName);
                objDBParam[2] = new SqlParameter("@StartMonth", DateTime.ParseExact(objFormData.DateFrom, "MM/yyyy", CultureInfo.InvariantCulture));
                objDBParam[3] = new SqlParameter("@EndMonth", DateTime.ParseExact(objFormData.DateTo, "MM/yyyy", CultureInfo.InvariantCulture));
                objDBParam[4] = new SqlParameter("@Applicability", objFormData.Applicability);
                objDBParam[5] = new SqlParameter("@ProductCategoryIds", dtProdCat);
                objDBParam[6] = new SqlParameter("@TargetCategoryIds", dtTargetCat);
                objDBParam[7] = new SqlParameter("@PPTargetCategoryIds", dtPPTargetCat);
                objDBParam[8] = new SqlParameter("@SFADivisionIds", dtDivision);
                objDBParam[9] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[10] = new SqlParameter("@ChannelIds", dtChannel);
                objDBParam[11] = new SqlParameter("@ExcelData", objFormData.ExcelData);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManagePerPieceIncentiveDefinition]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            finally
            {
                dtBranch.Dispose();
                dtChannel.Dispose();
                dtDivision.Dispose();
                dtProdCat.Dispose();
                dtTargetCat.Dispose();
                row = null;
            }
            return false;
        }

        public CreatePerPieceIncentive GetPerPieceIncentiveDefinitionBySchemeId(GetPerPieceIncentiveValues objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            CreatePerPieceIncentive objOutput = null;
            try
            {
                objDBParam.Add(new DbParameter("@SchemeId", objFormData.SchemeId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetPerPieceIncentiveDefinitionBySchemeId]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables.Count == 7)
                {
                    objOutput = new CreatePerPieceIncentive();
                    objOutput.BranchIds = new List<Int64>();
                    objOutput.ChannelIds = new List<Int64>();
                    objOutput.DivisionIds = new List<Int64>();
                    objOutput.ProductCategoryIds = new List<Int64>();
                    objOutput.TargetCategoryIds = new List<Int64>();
                    objOutput.PerPieceTargetCategoryIds = new List<Int64>();

                    //Data from master table
                    if (outputFromSP.Tables[0].Rows.Count == 0)
                    {
                        Message = "No per piece incentive definition found for this record.";
                        return objOutput;
                    }
                    objOutput.SchemeId = objFormData.SchemeId;
                    objOutput.SchemeName = outputFromSP.Tables[0].Rows[0]["SchemeName"].ToString();
                    objOutput.DateFrom = outputFromSP.Tables[0].Rows[0]["StartMonth"].ToString();
                    objOutput.DateTo = outputFromSP.Tables[0].Rows[0]["EndMonth"].ToString();
                    objOutput.Applicability = Convert.ToInt64(outputFromSP.Tables[0].Rows[0]["Applicability"]);

                    //Data from product category mapping table
                    if (outputFromSP.Tables[1].Rows.Count > 0)
                        objOutput.ProductCategoryIds = outputFromSP.Tables[1].AsEnumerable().Select(r => r.Field<Int64>("ProductCategoryId")).ToList();

                    //Data from basic target category mapping table
                    if (outputFromSP.Tables[2].Rows.Count > 0)
                        objOutput.TargetCategoryIds = outputFromSP.Tables[2].AsEnumerable().Select(r => r.Field<Int64>("BasicTargetCategoryId")).ToList();

                    //Data from per piece target category mapping table
                    if (outputFromSP.Tables[3].Rows.Count > 0)
                        objOutput.PerPieceTargetCategoryIds = outputFromSP.Tables[3].AsEnumerable().Select(r => r.Field<Int64>("PerPieceTargetCategoryId")).ToList();

                    //Data from sfa division mapping table
                    if (outputFromSP.Tables[4].Rows.Count > 0)
                        objOutput.DivisionIds = outputFromSP.Tables[4].AsEnumerable().Select(r => r.Field<Int64>("SFADivisionId")).ToList();

                    //Data from branch mapping table
                    if (outputFromSP.Tables[5].Rows.Count > 0)
                        objOutput.BranchIds = outputFromSP.Tables[5].AsEnumerable().Select(r => r.Field<Int64>("BranchId")).ToList();

                    //Data from channel mapping table
                    if (outputFromSP.Tables[6].Rows.Count > 0)
                        objOutput.ChannelIds = outputFromSP.Tables[6].AsEnumerable().Select(r => r.Field<Int64>("ChannelId")).ToList();
                }
                else
                    Message = "Could not fetch per piece incentive definition from database. Please contact administrator.";
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Exception while fetching per piece incentive definition from database. Please contact administrator.";
                return null;
            }
            return objOutput;
        }
		
		public IEnumerable<PerPieceIncentiveSchemeGet> GetPerPieceIncentiveSchemeByProductId(PerPieceIncentiveSchemeByProductId objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<PerPieceIncentiveSchemeGet> output = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", objFormData.ProductCategoryId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetPerPieceSchemeonProId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from perpiecescheme in outputFromSP.AsEnumerable()
                              select new PerPieceIncentiveSchemeGet
                              {
                                  SchemeId = perpiecescheme.Field<Int64>("SchemeId"),
                                  SchemeName = perpiecescheme.Field<string>("SchemeName")
                              }).ToList();
                }
                else
                {
                    Message = "No per piece incentive scheme found for selected product category.";
                }
            }
            catch (Exception ex)
            {
                Message = "Exception while fetching per piece incentive schemes from database. Please contact administrator.";
                return null;
            }
            return output;
        }

        public int ManagePerPieceMaterialMapping(PerPieceIncentiveCreate mappingData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[4];
            DataTable dtMappings = new DataTable();
            DataRow row;
            try
            {
                dtMappings.Columns.Add("SchemeId");
                dtMappings.Columns.Add("Min");
                dtMappings.Columns.Add("Max");
                dtMappings.Columns.Add("IncentiveAmount");
                dtMappings.Columns.Add("MinAch");
                dtMappings.Columns.Add("MaxAch");

                foreach (PerPieceIncentiveList mapping in mappingData.listPerPieceVal)
                {
                    row = dtMappings.NewRow();
                    row[0] = mapping.SchemeId;
                    row[1] = mapping.Min;
                    row[2] = mapping.Max;
                    row[3] = mapping.IncentiveAmount;
                    row[4] = mapping.MinAch;
                    row[5] = mapping.MaxAch;
                    dtMappings.Rows.Add(row);
                }

                objDBParam[0] = new SqlParameter("@ProductCategoryId", mappingData.ProductCategoryId);
                objDBParam[1] = new SqlParameter("@MaterialId", mappingData.MaterialId);
                objDBParam[2] = new SqlParameter("@CreatedBy", mappingData.UserId);
                objDBParam[3] = new SqlParameter("@DataForInsert", dtMappings);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManagePerPieceMaterialMapping]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            finally
            {
                dtMappings.Dispose();
            }
            return 0;
        }

        public bool DeletePerPieceIncentiveDefinition(DeletePerPieceIncentive objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SchemeId", objFormData.SchemeId, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", objFormData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeletePerPieceIncentiveDefinition]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Could not delete per piece incentive scheme. Exception occured in API.";
            }
            return false;
        }

        #region Festival Incentive
        public FestivalIncentiveDefinitionValues GetFestivalIncentiveDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            FestivalIncentiveDefinitionValues objOutput = new FestivalIncentiveDefinitionValues();
            try
            {
                objDBParam.Add(new DbParameter("@FestivalIncentiveDefinitionId", objDownloadData.SchemeId, DbType.Int64));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetFestivalIncentiveDefinitionExcelFile]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP != null && outputFromSP.Columns.Count > 0 && outputFromSP.Rows.Count > 0)
                {
                    objOutput.ExcelData = outputFromSP;
                    Message = "Festival Incentive Definition Excel Data fetched successfully.";
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
                Message = "Exception occured in API while fetching data for festival incentive definition.";
            }
            return objOutput;
        }

        public FestivalIncentiveDefinitionValues GetFestivalIncentiveSlabDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[2];
            DataTable dtProdCat = new DataTable();
            DataRow row;
            FestivalIncentiveDefinitionValues objOutput = new FestivalIncentiveDefinitionValues();
            try
            {
                dtProdCat.Columns.Add("UserId");
                dtProdCat.Columns.Add("ProdCatId");

                if (objDownloadData.ProductCategoryIds != null)
                {
                    foreach (Int64 prodCatId in objDownloadData.ProductCategoryIds)
                    {
                        row = dtProdCat.NewRow();
                        row[0] = objDownloadData.UserId;
                        row[1] = prodCatId;
                        dtProdCat.Rows.Add(row);
                    }
                }

                objDBParam[0] = new SqlParameter("@FestivalIncentiveDefinitionId", objDownloadData.SchemeId);
                objDBParam[1] = new SqlParameter("@ProductCategoryIds", dtProdCat);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetFestivalIncentiveSlabDefinitionExcelFile]", objDBParam);

                if (outputFromSP != null && outputFromSP.Columns.Count > 0 && outputFromSP.Rows.Count > 0)
                {
                    objOutput.ExcelData = outputFromSP;
                    objOutput.ExcelData.Columns.Remove("OrderBy");
                    Message = "Festival Incentive Slab Definition Excel Data fetched successfully.";
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
                Message = "Exception occured in API while fetching data for festival incentive slab definition.";
            }
            return objOutput;
        }

        public bool ManageFestivalIncentiveDefinition(CreateFestivalIncentive objFormData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[10];
            DataTable dtDivision = new DataTable();
            DataTable dtBranch = new DataTable();
            DataTable dtChannel = new DataTable();
            //DataTable dtProCat = new DataTable();
            //DataTable dtTargetCat = new DataTable();
            DataTable dtMinAchValues = new DataTable();
            DataRow row;
            try
            {
                #region Branch parameters
                dtBranch.Columns.Add("UserId");
                dtBranch.Columns.Add("BranchId");
                if (objFormData.BranchIds != null)
                {
                    foreach (Int64 branchId in objFormData.BranchIds)
                    {
                        row = dtBranch.NewRow();
                        row[0] = objFormData.UserId;
                        row[1] = branchId;
                        dtBranch.Rows.Add(row);
                    }
                }
                #endregion Branch parameters

                #region Channel parameters
                dtChannel.Columns.Add("UserId");
                dtChannel.Columns.Add("ChannelId");

                if (objFormData.ChannelIds != null)
                {
                    foreach (Int64 channelId in objFormData.ChannelIds)
                    {
                        row = dtChannel.NewRow();
                        row[0] = objFormData.UserId;
                        row[1] = channelId;
                        dtChannel.Rows.Add(row);
                    }
                }
                #endregion Channel parameters

                #region Division parameters
                dtDivision.Columns.Add("UserId");
                dtDivision.Columns.Add("DivisionId");

                if (objFormData.DivisionIds != null)
                {
                    foreach (Int64 divisionId in objFormData.DivisionIds)
                    {
                        row = dtDivision.NewRow();
                        row[0] = objFormData.UserId;
                        row[1] = divisionId;
                        dtDivision.Rows.Add(row);
                    }
                }
                #endregion Division parameters

                //#region ProCat parameters
                //dtProCat.Columns.Add("UserId");
                //dtProCat.Columns.Add("ProCatId");

                //if (objFormData.ProCatIds != null)
                //{
                //    foreach (Int64 procatid in objFormData.ProCatIds)
                //    {
                //        row = dtProCat.NewRow();
                //        row[0] = objFormData.UserId;
                //        row[1] = procatid;
                //        dtProCat.Rows.Add(row);
                //    }
                //}
                //#endregion ProCat parameters

                //#region TargetCat parameters
                //dtTargetCat.Columns.Add("UserId");
                //dtTargetCat.Columns.Add("TargetCatId");

                //if (objFormData.TargetCatIds != null)
                //{
                //    foreach (Int64 targetcatid in objFormData.TargetCatIds)
                //    {
                //        row = dtTargetCat.NewRow();
                //        row[0] = objFormData.UserId;
                //        row[1] = targetcatid;
                //        dtTargetCat.Rows.Add(row);
                //    }
                //}
                //#endregion TargetCat parameters

                #region Min. Ach. Value Parameters
                dtMinAchValues.Columns.Add("FestivalIncentiveDefId");
                dtMinAchValues.Columns.Add("TargetCategoryName");
                dtMinAchValues.Columns.Add("IncentiveCategoryName");
                dtMinAchValues.Columns.Add("Value");
                foreach(DataRow dr in objFormData.ExcelData.Rows)
                {
                    foreach(DataColumn dc in objFormData.ExcelData.Columns)
                    {
                        if(dc.ColumnName.ToUpper() != "SCHEME")
                        {
                            row = dtMinAchValues.NewRow();
                            row["FestivalIncentiveDefId"] = objFormData.SchemeId;
                            row["TargetCategoryName"] = Convert.ToString(dr[0]);
                            row["IncentiveCategoryName"] = dc.ColumnName;
                            row["Value"] = Convert.ToDecimal(dr[dc.ColumnName]);
                            dtMinAchValues.Rows.Add(row);
                        }
                    }
                }
                #endregion Min. Ach. Value Parameters

                objDBParam[0] = new SqlParameter("@SchemeId", objFormData.SchemeId);
                objDBParam[1] = new SqlParameter("@SchemeName", objFormData.SchemeName);
                objDBParam[2] = new SqlParameter("@DateFrom", DateTime.ParseExact(objFormData.DateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                objDBParam[3] = new SqlParameter("@DateTo", DateTime.ParseExact(objFormData.DateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                objDBParam[4] = new SqlParameter("@IsCalculateBase", objFormData.IsCalculateBase);
                objDBParam[5] = new SqlParameter("@SFADivisionIds", dtDivision);
                objDBParam[6] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[7] = new SqlParameter("@ChannelIds", dtChannel);
                objDBParam[8] = new SqlParameter("@MinAchValues", dtMinAchValues);
                objDBParam[9] = new SqlParameter("@IsPayBase", objFormData.IsPayBase);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageFestivalIncentiveDefinition]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            finally
            {
                dtBranch.Dispose();
                dtChannel.Dispose();
                dtDivision.Dispose();
                dtMinAchValues.Dispose();
                row = null;
            }
            return false;
        }

        public bool ManageFestivalIncentiveSlabDefinition(CreateFestivalIncentiveSlab objFormData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[4];
            DataTable dtProdCat = new DataTable();
            DataTable dtTargetCat = new DataTable();
            DataRow row;
            try
            {
                #region Product Category parameters
                dtProdCat.Columns.Add("UserId");
                dtProdCat.Columns.Add("ProdCatId");

                if (objFormData.ProductCategoryIds != null)
                {
                    foreach (Int64 prodCatId in objFormData.ProductCategoryIds)
                    {
                        row = dtProdCat.NewRow();
                        row[0] = objFormData.UserId;
                        row[1] = prodCatId;
                        dtProdCat.Rows.Add(row);
                    }
                }
                #endregion Product Category parameters

                #region Target Category parameters
                dtTargetCat.Columns.Add("TargetCatId");

                if (objFormData.TargetCategoryIds != null)
                {
                    foreach (Int64 targetId in objFormData.TargetCategoryIds)
                    {
                        row = dtTargetCat.NewRow();
                        row[0] = targetId;
                        dtTargetCat.Rows.Add(row);
                    }
                }
                #endregion Target Category parameters

                objDBParam[0] = new SqlParameter("@SchemeId", objFormData.SchemeId);
                objDBParam[1] = new SqlParameter("@ProductCategoryIds", dtProdCat);
                objDBParam[2] = new SqlParameter("@TargetCategoryIds", dtTargetCat);
                objDBParam[3] = new SqlParameter("@ExcelData", objFormData.ExcelData);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageFestivalIncentiveSlabDefinition]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            finally
            {
                dtProdCat.Dispose();
                dtTargetCat.Dispose();
                row = null;
            }
            return false;
        }

        public CreateFestivalIncentive GetFestivalIncentiveDefinitionBySchemeId(GetFestivalIncentiveValues objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            CreateFestivalIncentive objOutput = null;
            try
            {
                objDBParam.Add(new DbParameter("@SchemeId", objFormData.SchemeId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetFestivalIncentiveDefinitionBySchemeId]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables.Count == 4)
                {
                    objOutput = new CreateFestivalIncentive();
                    objOutput.BranchIds = new List<Int64>();
                    objOutput.ChannelIds = new List<Int64>();
                    objOutput.DivisionIds = new List<Int64>();

                    //Data from master table
                    if (outputFromSP.Tables[0].Rows.Count == 0)
                    {
                        Message = "No festival incentive definition found for this record.";
                        return objOutput;
                    }
                    objOutput.SchemeId = objFormData.SchemeId;
                    objOutput.SchemeName = outputFromSP.Tables[0].Rows[0]["SchemeName"].ToString();
                    objOutput.DateFrom = outputFromSP.Tables[0].Rows[0]["StartDate"].ToString();
                    objOutput.DateTo = outputFromSP.Tables[0].Rows[0]["EndDate"].ToString();
                    objOutput.IsCalculateBase = Convert.ToBoolean(outputFromSP.Tables[0].Rows[0]["IsCalculateBase"]);
                    objOutput.IsPayBase = Convert.ToBoolean(outputFromSP.Tables[0].Rows[0]["IsPayBase"]);

                    //Data from sfa division mapping table
                    if (outputFromSP.Tables[1].Rows.Count > 0)
                        objOutput.DivisionIds = outputFromSP.Tables[1].AsEnumerable().Select(r => r.Field<Int64>("SFADivisionId")).ToList();

                    //Data from branch mapping table
                    if (outputFromSP.Tables[2].Rows.Count > 0)
                        objOutput.BranchIds = outputFromSP.Tables[2].AsEnumerable().Select(r => r.Field<Int64>("BranchId")).ToList();

                    //Data from channel mapping table
                    if (outputFromSP.Tables[3].Rows.Count > 0)
                        objOutput.ChannelIds = outputFromSP.Tables[3].AsEnumerable().Select(r => r.Field<Int64>("ChannelId")).ToList();
                }
                else
                    Message = "Could not fetch festival incentive definition from database. Please contact administrator.";
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Exception while fetching festival incentive definition from database. Please contact administrator.";
                return null;
            }
            return objOutput;
        }

        public CreateFestivalIncentiveSlab GetFestivalIncentiveSlabDefinitionBySchemeId(GetFestivalIncentiveValues objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            CreateFestivalIncentiveSlab objOutput = null;
            try
            {
                objDBParam.Add(new DbParameter("@SchemeId", objFormData.SchemeId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetFestivalIncentiveSlabDefinitionBySchemeId]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables.Count == 3)
                {
                    objOutput = new CreateFestivalIncentiveSlab();
                    objOutput.ProductCategoryIds = new List<Int64>();
                    objOutput.TargetCategoryIds = new List<Int64>();

                    //Data from master table
                    if (outputFromSP.Tables[0].Rows.Count == 0)
                    {
                        Message = "No festival incentive slab definition found for this record.";
                        return objOutput;
                    }
                    objOutput.SchemeId = objFormData.SchemeId;
                    objOutput.SchemeName = outputFromSP.Tables[0].Rows[0]["SchemeName"].ToString();
                    objOutput.DateFrom = outputFromSP.Tables[0].Rows[0]["StartDate"].ToString();
                    objOutput.DateTo = outputFromSP.Tables[0].Rows[0]["EndDate"].ToString();
                    objOutput.IsCalculateBase = Convert.ToBoolean(outputFromSP.Tables[0].Rows[0]["IsCalculateBase"]);

                    //Data from sfa division mapping table
                    if (outputFromSP.Tables[1].Rows.Count > 0)
                        objOutput.ProductCategoryIds = outputFromSP.Tables[1].AsEnumerable().Select(r => r.Field<Int64>("ProductCategoryId")).ToList();

                    //Data from branch mapping table
                    if (outputFromSP.Tables[2].Rows.Count > 0)
                        objOutput.TargetCategoryIds = outputFromSP.Tables[2].AsEnumerable().Select(r => r.Field<Int64>("TargetCategoryId")).ToList();
                }
                else
                    Message = "Could not fetch festival incentive slab definition from database. Please contact administrator.";
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Exception while fetching festival incentive slab definition from database. Please contact administrator.";
                return null;
            }
            return objOutput;
        }

        public bool DeleteFestivalIncentiveDefinition(DeleteFestivalIncentive objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SchemeId", objFormData.SchemeId, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", objFormData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteFestivalIncentiveDefinition]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Could not delete festival incentive scheme. Exception occured in API.";
            }
            return false;
        }
        #endregion Festival Incentive

        #region IncentiveReport
        /// <summary>
        /// To fetch Base Per piece incentive Report.
        /// </summary>
        /// <param name="Input">Branch	SFA Name	Division	Month 	Product Category</param>
        /// <returns>Base Per piece incentive Report.</returns>
        public BasePerPieceIncentiveDisplayReportList GetBasePerPieceIncentiveReport(BasePerPieceIncentiveReportInputParam Input)
        {
            BasePerPieceIncentiveDisplayReportList Data = new BasePerPieceIncentiveDisplayReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DivisionId", Input.DivisionId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Month", Input.Month, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCategory", Input.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@flag", 0, DbType.Int32));//0 for single record report and 1 for detail.
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBasePerPieceIncentiveReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.BasePerPieceIncentiveData = (from data in outputFromSP.AsEnumerable()
                                                      select new BasePerPieceIncentiveDisplayReportData
                                                      {
                                                          BranchName = Convert.ToString(data.Field<string>("BranchName")),
                                                          City = Convert.ToString(data.Field<string>("CityName")),
                                                          Location = Convert.ToString(data.Field<string>("LocationName")),
                                                          SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                                          SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                                          SFAType = Convert.ToString(data.Field<string>("SFAType")),
                                                          SFALevel = Convert.ToString(data.Field<string>("SFALevelName")),
                                                          SFADivision = Convert.ToString(data.Field<string>("DivisionName")),
                                                          Dealer = Convert.ToString(data.Field<string>("DealerName")),
                                                          Channel = Convert.ToString(data.Field<string>("ChannelName")),
                                                          DealerState = Convert.ToString(data.Field<string>("DealerState")),
                                                          MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                                          DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                                          IncentiveCategory = Convert.ToString(data.Field<string>("IncentiveCategory")),
                                                          QtyTarget = Convert.ToInt32(data.Field<int>("QuantityTarget")),
                                                          ValueTarget = Convert.ToInt32(data.Field<int>("ValueTarget")),
                                                          QtyAchievement = Convert.ToInt32(data.Field<int>("QuantityAchieved")),
                                                          ValueAchievement = Convert.ToInt32(data.Field<int>("ValueAchieved")),
                                                          QtyPercentage = Convert.ToInt32(data.Field<int>("QtyPercentage")),
                                                          ValuePercentage = Convert.ToInt32(data.Field<int>("ValuePercentage")),
                                                          BaseIncentiveAmount = Convert.ToInt32(data.Field<int>("BaseIncentiveAmount")),
                                                          PerPieceQty = Convert.ToInt32(data.Field<int>("PerPieceQty")),
                                                          PerPieceIncentiveAmount = Convert.ToInt32(data.Field<int>("PerPieceIncentiveAmount")),
                                                          ProposedDeviation = Convert.ToInt32(data.Field<int>("ProposedDeviation")),
                                                          Reasons = Convert.ToString(data.Field<string>("Reasons")),
                                                          FirstHeader = Convert.ToString(data.Field<string>("FirstHeader")),
                                                          FirstRemark = Convert.ToString(data.Field<string>("FirstRemark")),
                                                          SecondHeader = Convert.ToString(data.Field<string>("SecondHeader")),
                                                          SecondRemark = Convert.ToString(data.Field<string>("SecondRemark")),
                                                          FinalPayableAmount = Convert.ToInt32(data.Field<int>("FinalPayableAmount")),
                                                          HORemark = Convert.ToString(data.Field<string>("HORemark")),
                                                          DeviationStage = Convert.ToString(data.Field<string>("DeviationStage")),
                                                          ApprovedDeviationAmount = Convert.ToInt32(data.Field<int>("ApprovedDeviationAmount"))
                                                      }).ToList();
                }
                else
                {
                    Data.BasePerPieceIncentiveData = (from data in outputFromSP.AsEnumerable()
                                                      select new BasePerPieceIncentiveDisplayReportData
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

        /// <summary>
        /// To fetch Base Per piece incentive detail Report.
        /// </summary>
        /// <param name="Input">Branch	SFA Name	Division	Month 	Product Category</param>
        /// <returns>Base Per piece incentive Report.</returns>
        public BasePerPieceIncentiveDetailReportList GetBasePerPieceIncentiveDetailReport(BasePerPieceIncentiveReportInputParam Input)
        {
            BasePerPieceIncentiveDetailReportList Data = new BasePerPieceIncentiveDetailReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DivisionId", Input.DivisionId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Month", Input.Month, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCategory", Input.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@flag", 1, DbType.Int32));//0 for single record report and 1 for detail.
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBasePerPieceIncentiveReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.BasePerPieceIncentiveDetailData = (from data in outputFromSP.AsEnumerable()
                                                      select new BasePerPieceIncentiveDetailReportData
                                                      {
                                                          BranchName = Convert.ToString(data.Field<string>("BranchName")),
                                                          City = Convert.ToString(data.Field<string>("CityName")),
                                                          Location = Convert.ToString(data.Field<string>("LocationName")),
                                                          SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                                          SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                                          SFAType = Convert.ToString(data.Field<string>("SFAType")),
                                                          SFALevel = Convert.ToString(data.Field<string>("SFALevelName")),
                                                          SFADivision = Convert.ToString(data.Field<string>("DivisionName")),
                                                          Dealer = Convert.ToString(data.Field<string>("DealerName")),
                                                          Channel = Convert.ToString(data.Field<string>("ChannelName")),
                                                          DealerState = Convert.ToString(data.Field<string>("DealerState")),
                                                          MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                                          DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                                          IncentiveCategory = Convert.ToString(data.Field<string>("IncentiveCategory")),
                                                          TargetCategory = Convert.ToString(data.Field<string>("TargetCategory")),
                                                          ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                          QtyTarget = Convert.ToInt32(data.Field<int>("QuantityTarget")),
                                                          ValueTarget = Convert.ToInt32(data.Field<int>("ValueTarget")),
                                                          QtyAchievement = Convert.ToInt32(data.Field<int>("QuantityAchieved")),
                                                          ValueAchievement = Convert.ToInt32(data.Field<int>("ValueAchieved")),
                                                          QtyPercentage = Convert.ToInt32(data.Field<int>("QtyPercentage")),
                                                          ValuePercentage = Convert.ToInt32(data.Field<int>("ValuePercentage")),
                                                          BaseSlabCalculated = Convert.ToInt32(data.Field<int>("BaseSlabCalculated")),
                                                          BaseIncentiveAmount = Convert.ToInt32(data.Field<int>("BaseIncentiveAmount")),
                                                          PerPieceQty = Convert.ToInt32(data.Field<int>("PerPieceQty")),
                                                          PerPieceSlabCalculated = Convert.ToInt32(data.Field<int>("PerPieceSlabCalculated")),
                                                          PerPieceIncentiveAmount = Convert.ToInt32(data.Field<int>("PerPieceIncentiveAmount")),
                                                          ProposedDeviation = Convert.ToInt32(data.Field<int>("ProposedDeviation")),
                                                          Reasons = Convert.ToString(data.Field<string>("Reasons")),
                                                          FirstHeader = Convert.ToString(data.Field<string>("FirstHeader")),
                                                          FirstRemark = Convert.ToString(data.Field<string>("FirstRemark")),
                                                          SecondHeader = Convert.ToString(data.Field<string>("SecondHeader")),
                                                          SecondRemark = Convert.ToString(data.Field<string>("SecondRemark")),
                                                          FinalPayableAmount = Convert.ToInt32(data.Field<int>("FinalPayableAmount")),
                                                          HORemark = Convert.ToString(data.Field<string>("HORemark")),
                                                          DeviationStage = Convert.ToString(data.Field<string>("DeviationStage")),
                                                          ApprovedDeviationAmount = Convert.ToInt32(data.Field<int>("ApprovedDeviationAmount")),
                                                          PerPieceIncentiveScheme = Convert.ToString(data.Field<string>("PerPieceIncentiveScheme"))
                                                      }).ToList();
                }
                else
                {
                    Data.BasePerPieceIncentiveDetailData = (from data in outputFromSP.AsEnumerable()
                                                      select new BasePerPieceIncentiveDetailReportData
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


        /// <summary>
        /// To fetch Festival incentive Report.
        /// </summary>
        /// <param name="Input">Branch	SFA Name	Division	FestivalSchemeId 	Product Category</param>
        /// <returns>Festival incentive Report.</returns>
        public FestivalIncentiveDisplayReportList GetFestivalIncentiveReport(FestivalIncentiveReportInputParam Input)
        {
            FestivalIncentiveDisplayReportList Data = new FestivalIncentiveDisplayReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DivisionId", Input.DivisionId, DbType.Int64));
                objDBParam.Add(new DbParameter("@FestivalSchemeId", Input.FestivalSchemeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCategory", Input.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@flag", 0, DbType.Int32));//0 for single record report and 1 for detail.
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetFestivalIncentiveReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.FestivalIncentiveData = (from data in outputFromSP.AsEnumerable()
                                                      select new FestivalIncentiveDisplayReportData
                                                      {
                                                          SchemeName = Convert.ToString(data.Field<string>("SchemeName")),
                                                          StartDate = Convert.ToString(data.Field<string>("StartDate")),
                                                          EndDate = Convert.ToString(data.Field<string>("EndDate")),

                                                          BranchName = Convert.ToString(data.Field<string>("BranchName")),
                                                          City = Convert.ToString(data.Field<string>("CityName")),
                                                          Location = Convert.ToString(data.Field<string>("LocationName")),
                                                          SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                                          SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                                          SFAType = Convert.ToString(data.Field<string>("SFAType")),
                                                          SFALevel = Convert.ToString(data.Field<string>("SFALevelName")),
                                                          SFADivision = Convert.ToString(data.Field<string>("DivisionName")),
                                                          Dealer = Convert.ToString(data.Field<string>("DealerName")),
                                                          Channel = Convert.ToString(data.Field<string>("ChannelName")),
                                                          DealerState = Convert.ToString(data.Field<string>("DealerState")),
                                                          MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                                          DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                                          IncentiveCategory = Convert.ToString(data.Field<string>("IncentiveCategory")),
                                                          QtyTarget = Convert.ToInt32(data.Field<int>("QuantityTarget")),
                                                          ValueTarget = Convert.ToInt32(data.Field<int>("ValueTarget")),
                                                          QtyAchievement = Convert.ToInt32(data.Field<int>("QuantityAchieved")),
                                                          ValueAchievement = Convert.ToInt32(data.Field<int>("ValueAchieved")),
                                                          QtyPercentage = Convert.ToInt32(data.Field<int>("QtyPercentage")),
                                                          ValuePercentage = Convert.ToInt32(data.Field<int>("ValuePercentage")),
                                                          ProposedDeviation = Convert.ToInt32(data.Field<int>("ProposedDeviation")),
                                                          Reasons = Convert.ToString(data.Field<string>("Reasons")),
                                                          FirstHeader = Convert.ToString(data.Field<string>("FirstHeader")),
                                                          FirstRemark = Convert.ToString(data.Field<string>("FirstRemark")),
                                                          SecondHeader = Convert.ToString(data.Field<string>("SecondHeader")),
                                                          SecondRemark = Convert.ToString(data.Field<string>("SecondRemark")),
                                                          FinalPayableAmount = Convert.ToInt32(data.Field<int>("FinalPayableAmount")),
                                                          HORemark = Convert.ToString(data.Field<string>("HORemark")),
                                                          DeviationStage = Convert.ToString(data.Field<string>("DeviationStage")),
                                                          ApprovedDeviationAmount = Convert.ToInt32(data.Field<int>("ApprovedDeviationAmount")),
                                                          FestivalIncentiveAmount = Convert.ToInt32(data.Field<int>("FestivalIncentiveAmount"))
                                                      }).ToList();
                }
                else
                {
                    Data.FestivalIncentiveData = (from data in outputFromSP.AsEnumerable()
                                                      select new FestivalIncentiveDisplayReportData
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

        /// <summary>
        /// To fetch Festival incentive detail Report.
        /// </summary>
        /// <param name="Input">Branch	SFA Name	Division	FestivalScheme 	Product Category</param>
        /// <returns>Festival incentive Report.</returns>
        public FestivalIncentiveDetailReportList GetFestivalIncentiveDetailReport(FestivalIncentiveReportInputParam Input)
        {
            FestivalIncentiveDetailReportList Data = new FestivalIncentiveDetailReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DivisionId", Input.DivisionId, DbType.Int64));
                objDBParam.Add(new DbParameter("@FestivalSchemeId", Input.FestivalSchemeId, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCategory", Input.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@flag", 1, DbType.Int32));//0 for single record report and 1 for detail.
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetFestivalIncentiveReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.FestivalIncentiveDetailData = (from data in outputFromSP.AsEnumerable()
                                                            select new FestivalIncentiveDetailReportData
                                                            {
                                                                SchemeName = Convert.ToString(data.Field<string>("SchemeName")),
                                                                StartDate = Convert.ToString(data.Field<string>("StartDate")),
                                                                EndDate = Convert.ToString(data.Field<string>("EndDate")),

                                                                BranchName = Convert.ToString(data.Field<string>("BranchName")),
                                                                City = Convert.ToString(data.Field<string>("CityName")),
                                                                Location = Convert.ToString(data.Field<string>("LocationName")),
                                                                SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                                                SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                                                SFAType = Convert.ToString(data.Field<string>("SFAType")),
                                                                SFALevel = Convert.ToString(data.Field<string>("SFALevelName")),
                                                                SFADivision = Convert.ToString(data.Field<string>("DivisionName")),
                                                                Dealer = Convert.ToString(data.Field<string>("DealerName")),
                                                                Channel = Convert.ToString(data.Field<string>("ChannelName")),
                                                                DealerState = Convert.ToString(data.Field<string>("DealerState")),
                                                                MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                                                DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                                                IncentiveCategory = Convert.ToString(data.Field<string>("IncentiveCategory")),
                                                                TargetCategory = Convert.ToString(data.Field<string>("TargetCategory")),
                                                                ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                                QtyTarget = Convert.ToInt32(data.Field<int>("QuantityTarget")),
                                                                ValueTarget = Convert.ToInt32(data.Field<int>("ValueTarget")),
                                                                QtyAchievement = Convert.ToInt32(data.Field<int>("QuantityAchieved")),
                                                                ValueAchievement = Convert.ToInt32(data.Field<int>("ValueAchieved")),
                                                                //QtyPercentage = Convert.ToInt32(data.Field<int>("QtyPercentage")),
                                                                //ValuePercentage = Convert.ToInt32(data.Field<int>("ValuePercentage")),
                                                                FestivalSlabCalculated = Convert.ToInt32(data.Field<int>("FestivalSlabCalculated")),
                                                                ProposedDeviation = Convert.ToInt32(data.Field<int>("ProposedDeviation")),
                                                                Reasons = Convert.ToString(data.Field<string>("Reasons")),
                                                                FirstHeader = Convert.ToString(data.Field<string>("FirstHeader")),
                                                                FirstRemark = Convert.ToString(data.Field<string>("FirstRemark")),
                                                                SecondHeader = Convert.ToString(data.Field<string>("SecondHeader")),
                                                                SecondRemark = Convert.ToString(data.Field<string>("SecondRemark")),
                                                                FinalPayableAmount = Convert.ToInt32(data.Field<int>("FinalPayableAmount")),
                                                                HORemark = Convert.ToString(data.Field<string>("HORemark")),
                                                                DeviationStage = Convert.ToString(data.Field<string>("DeviationStage")),
                                                                ApprovedDeviationAmount = Convert.ToInt32(data.Field<int>("ApprovedDeviationAmount")),
                                                            }).ToList();
                }
                else
                {
                    Data.FestivalIncentiveDetailData = (from data in outputFromSP.AsEnumerable()
                                                            select new FestivalIncentiveDetailReportData
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


        /// <summary>
        /// To fetch Festival Sell Thru Tracker.
        /// </summary>
        /// <param name="Input">festival category branch flag</param>
        /// <returns>Festival sell thru tracker.</returns>
        public FestivalSellThruTrackerList GetFestivalSellThruTracker(FestivalSellThruTrackerInputParam Input)
        {
            FestivalSellThruTrackerList Data = new FestivalSellThruTrackerList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@FestivalNameId", Input.FestivalNameId, DbType.Int32));
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int32));
                objDBParam.Add(new DbParameter("@CategoryId", Input.CategoryId, DbType.Int32));
                objDBParam.Add(new DbParameter("@Flag", Input.Flag, DbType.Int32));//0 for branch & 1 for category
                objDBParam.Add(new DbParameter("@LoginUserId", Input.LoginUserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetFestivalSellThruTracker]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables.Count > 0)
                {
                    if (outputFromSP.Tables[0].Rows.Count > 0)
                    {
                        Data.FestivalSellThruTrackerFYCount = (from data in outputFromSP.Tables[0].AsEnumerable()
                                                               select new FestivalSellThruTrackerFYCount
                                                               {
                                                                   FY = Convert.ToString(data.Field<string>("FY")),
                                                                   TotalCount = Convert.ToInt32(data.Field<int>("TotalCount")),
                                                               }).ToList();
                    }
                    if (outputFromSP.Tables[1].Rows.Count > 0)
                    {
                        Data.FestivalSellThruTrackerData = (from data in outputFromSP.Tables[1].AsEnumerable()
                                                            select new FestivalSellThruTrackerData
                                                            {
                                                                //FestivalDate = Convert.ToString(data.Field<string>("FestivalDate")),
                                                                //FY = Convert.ToString(data.Field<string>("FY")),
                                                                //BranchName = Convert.ToString(data.Field<string>("BranchName")),
                                                                //CategoryName = Convert.ToString(data.Field<string>("CategoryName")),
                                                                //Quantity = Convert.ToInt64(data.Field<Int64>("Quantity")),
                                                                Name = Convert.ToString(data.Field<string>("Name")),
                                                                Y2 = Convert.ToString(data.Field<string>("Y2")),
                                                                Y1 = Convert.ToString(data.Field<string>("Y1")),
                                                                Y = Convert.ToString(data.Field<string>("Y"))
                                                            }).ToList();
                    }
                }
                else
                {
                    Data.FestivalSellThruTrackerFYCount = (from data in outputFromSP.Tables[0].AsEnumerable()
                                                           select new FestivalSellThruTrackerFYCount
                                                           {

                                                           }).ToList();
                    Data.FestivalSellThruTrackerData = (from data in outputFromSP.Tables[1].AsEnumerable()
                                                        select new FestivalSellThruTrackerData
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

        /// <summary>
        /// To fetch Festival Details.
        /// </summary>
        /// <param name="Input"></param>
        /// <returns>Festival details.</returns>
        public FestivalNameDetailList GetFestivalNameDetails()
        {
            FestivalNameDetailList Data = new FestivalNameDetailList();
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetFestivalDetails]", CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {

                    Data.FestivalNameDetailData = (from data in outputFromSP.AsEnumerable()
                                                           select new FestivalNameDetails
                                                           {
                                                               Name = Convert.ToString(data.Field<string>("Name")),
                                                               FestivalId = Convert.ToInt32(data.Field<int>("FestivalId")),
                                                               Days = Convert.ToInt32(data.Field<int>("Days"))
                                                           }).ToList();

                }
                else
                {
                    Data.FestivalNameDetailData = (from data in outputFromSP.AsEnumerable()
                                                   select new FestivalNameDetails
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
        #endregion

        #region IncentiveQSR 
        public BasePerPieceIncentiveDisplayReportList GetBasePerPieceIncentiveReportQSR(BasePerPieceIncentiveReportInputParamQSR Input)
        {
            BasePerPieceIncentiveDisplayReportList Data = new BasePerPieceIncentiveDisplayReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                //objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                // objDBParam.Add(new DbParameter("@DivisionId", Input.DivisionId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IncentiveMonth", Input.Month, DbType.String));
                // objDBParam.Add(new DbParameter("@ProductCategory", Input.ProductCategoryId, DbType.Int64));
                // objDBParam.Add(new DbParameter("@flag", 0, DbType.Int32));//0 for single record report and 1 for detail.
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDeviationUploadByRDIExcel_QSRQuantity]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.BasePerPieceIncentiveData = (from data in outputFromSP.AsEnumerable()
                                                      select new BasePerPieceIncentiveDisplayReportData
                                                      {


                                                          BranchName = Convert.ToString(data.Field<string>("Branch")),
                                                          QSRDate = Convert.ToString(data.Field<string>("Date")),
                                                          Dealer = Convert.ToString(data.Field<string>("DealerName")),
                                                          City = Convert.ToString(data.Field<string>("City")),
                                                          Location = Convert.ToString(data.Field<string>("Location")),
                                                          PayerName = Convert.ToString(data.Field<string>("PayerName")),
                                                          Channel = Convert.ToString(data.Field<string>("Channel")),
                                                          DealerState = Convert.ToString(data.Field<string>("DealerState")),
                                                          MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                                          DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                                          DealerClassification = Convert.ToString(data.Field<string>("DealerClassification")),
                                                          SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                                          SFAName = Convert.ToString(data.Field<string>("SFAName")),

                                                          SFALevel = Convert.ToString(data.Field<string>("SFALevel")),
                                                          CompanyName = Convert.ToString(data.Field<string>("CompanyName")),
                                                          ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                          Material = Convert.ToString(data.Field<string>("Material")),

                                                          SFAType = Convert.ToString(data.Field<string>("SFAType")),
                                                          AmboQuantity = Convert.ToString(data.Field<string>("AmboQuantity")),

                                                      }).ToList();
                }

                else
                {
                    Data.BasePerPieceIncentiveData = (from data in outputFromSP.AsEnumerable()
                                                      select new BasePerPieceIncentiveDisplayReportData
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
        #endregion
    }
}
