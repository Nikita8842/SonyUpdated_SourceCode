using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.IncentiveManagement;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.INTERFACE;
using AMBOModels.Reports;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIIncentiveData : IAPIIncentiveData
    {
        #region Base Incentive
        public Envelope<bool> DeleteBaseIncentiveDefinition(DeleteBaseIncentiveForm objFormData)
        {
            Envelope<DeleteBaseIncentiveForm> postData = new Envelope<DeleteBaseIncentiveForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteBaseIncentiveForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteBaseIncentiveForm>>();
                string apiUrl = "api/Incentive/DeleteBaseIncentiveDefinition";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<CreateBaseIncentiveForm> GetBaseIncentiveDefinitionByTargetCategoryId(GetBaseIncentive objFormData)
        {
            Envelope<GetBaseIncentive> postData = new Envelope<GetBaseIncentive>();
            Envelope<CreateBaseIncentiveForm> output = new Envelope<CreateBaseIncentiveForm>();
            APIClient<Envelope<GetBaseIncentive>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<GetBaseIncentive>>();
                string apiUrl = "api/Incentive/GetBaseIncentiveDefinitionByTargetCategoryId";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<CreateBaseIncentiveForm>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> ManageBaseIncentiveDefinition(CreateBaseIncentiveForm objFormData)
        {
            Envelope<CreateBaseIncentiveForm> postData = new Envelope<CreateBaseIncentiveForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateBaseIncentiveForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateBaseIncentiveForm>>();
                string apiUrl = "api/Incentive/ManageBaseIncentiveDefinition";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Base Incentive

        #region Per Piece Incentive
        public Envelope<PerPieceIncentiveValues> GetPerPieceIncentiveExcelFile(DownloadPerPieceIncentiveExcel objDownloadData)
        {
            Envelope<DownloadPerPieceIncentiveExcel> postData = new Envelope<DownloadPerPieceIncentiveExcel>();
            Envelope<PerPieceIncentiveValues> output = new Envelope<PerPieceIncentiveValues>();
            APIClient<Envelope<DownloadPerPieceIncentiveExcel>> client;
            postData.Data = objDownloadData;
            try
            {
                client = new APIClient<Envelope<DownloadPerPieceIncentiveExcel>>();
                string apiUrl = "api/Incentive/GetPerPieceIncentiveExcelFile";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<PerPieceIncentiveValues>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> ManagePerPieceIncentiveDefinition(CreatePerPieceIncentive objFormData)
        {
            Envelope<CreatePerPieceIncentive> postData = new Envelope<CreatePerPieceIncentive>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreatePerPieceIncentive>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreatePerPieceIncentive>>();
                string apiUrl = "api/Incentive/ManagePerPieceIncentiveDefinition";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<List<PerPieceIncentiveSchemeGet>> GetPerPieceIncentiveSchemeByProductId(PerPieceIncentiveSchemeByProductId objFormData)
        {
            Envelope<PerPieceIncentiveSchemeByProductId> PostData = new Envelope<PerPieceIncentiveSchemeByProductId>();
            PostData.Data = objFormData;
            APIClient<Envelope<PerPieceIncentiveSchemeByProductId>> client = new APIClient<Envelope<PerPieceIncentiveSchemeByProductId>>();
            try
            {
                string apiUrl = "api/Incentive/GetPerPieceIncentiveSchemeByProductId";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<PerPieceIncentiveSchemeGet>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<bool> ManagePerPieceMaterialMapping(PerPieceIncentiveCreate mappingData)
        {
            Envelope<PerPieceIncentiveCreate> postData = new Envelope<PerPieceIncentiveCreate>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<PerPieceIncentiveCreate>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<PerPieceIncentiveCreate>>();
                string apiUrl = "api/Incentive/ManagePerPieceMaterialMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<CreatePerPieceIncentive> GetPerPieceIncentiveDefinitionBySchemeId(GetPerPieceIncentiveValues objFormData)
        {
            Envelope<GetPerPieceIncentiveValues> postData = new Envelope<GetPerPieceIncentiveValues>();
            Envelope<CreatePerPieceIncentive> output = new Envelope<CreatePerPieceIncentive>();
            APIClient<Envelope<GetPerPieceIncentiveValues>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<GetPerPieceIncentiveValues>>();
                string apiUrl = "api/Incentive/GetPerPieceIncentiveDefinitionBySchemeId";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<CreatePerPieceIncentive>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }


        //to get material id (navigation module to pre fill data)
        public Envelope<MaterialCodeGet> GetMaterialIdByMaterialCode(MaterialCodeGet objFormData)
        {
            Envelope<MaterialCodeGet> PostData = new Envelope<MaterialCodeGet>();
            PostData.Data = objFormData;
            APIClient<Envelope<MaterialCodeGet>> client = new APIClient<Envelope<MaterialCodeGet>>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetMaterialIdByMaterialCode";//need to port
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<MaterialCodeGet>>(apiOutput.Result);
                return output;
            }
            catch
            {
                return null;
            }
        }

        public Envelope<bool> DeletePerPieceIncentiveDefinition(DeletePerPieceIncentive objFormData)
        {
            Envelope<DeletePerPieceIncentive> postData = new Envelope<DeletePerPieceIncentive>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeletePerPieceIncentive>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeletePerPieceIncentive>>();
                string apiUrl = "api/Incentive/DeletePerPieceIncentiveDefinition";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Per Piece Incentive

        #region Festival Incentive
        public Envelope<FestivalIncentiveDefinitionValues> GetFestivalIncentiveDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData)
        {
            Envelope<DownloadFestivalIncentiveDefinitionExcel> postData = new Envelope<DownloadFestivalIncentiveDefinitionExcel>();
            Envelope<FestivalIncentiveDefinitionValues> output = new Envelope<FestivalIncentiveDefinitionValues>();
            APIClient<Envelope<DownloadFestivalIncentiveDefinitionExcel>> client;
            postData.Data = objDownloadData;
            try
            {
                client = new APIClient<Envelope<DownloadFestivalIncentiveDefinitionExcel>>();
                string apiUrl = "api/Incentive/GetFestivalIncentiveDefinitionExcelFile";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<FestivalIncentiveDefinitionValues>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<FestivalIncentiveDefinitionValues> GetFestivalIncentiveSlabDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData)
        {
            Envelope<DownloadFestivalIncentiveDefinitionExcel> postData = new Envelope<DownloadFestivalIncentiveDefinitionExcel>();
            Envelope<FestivalIncentiveDefinitionValues> output = new Envelope<FestivalIncentiveDefinitionValues>();
            APIClient<Envelope<DownloadFestivalIncentiveDefinitionExcel>> client;
            postData.Data = objDownloadData;
            try
            {
                client = new APIClient<Envelope<DownloadFestivalIncentiveDefinitionExcel>>();
                string apiUrl = "api/Incentive/GetFestivalIncentiveSlabDefinitionExcelFile";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<FestivalIncentiveDefinitionValues>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> ManageFestivalIncentiveDefinition(CreateFestivalIncentive objFormData)
        {
            Envelope<CreateFestivalIncentive> postData = new Envelope<CreateFestivalIncentive>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateFestivalIncentive>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateFestivalIncentive>>();
                string apiUrl = "api/Incentive/ManageFestivalIncentiveDefinition";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> ManageFestivalIncentiveSlabDefinition(CreateFestivalIncentiveSlab objFormData)
        {
            Envelope<CreateFestivalIncentiveSlab> postData = new Envelope<CreateFestivalIncentiveSlab>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateFestivalIncentiveSlab>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateFestivalIncentiveSlab>>();
                string apiUrl = "api/Incentive/ManageFestivalIncentiveSlabDefinition";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<CreateFestivalIncentive> GetFestivalIncentiveDefinitionBySchemeId(GetFestivalIncentiveValues objFormData)
        {
            Envelope<GetFestivalIncentiveValues> postData = new Envelope<GetFestivalIncentiveValues>();
            Envelope<CreateFestivalIncentive> output = new Envelope<CreateFestivalIncentive>();
            APIClient<Envelope<GetFestivalIncentiveValues>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<GetFestivalIncentiveValues>>();
                string apiUrl = "api/Incentive/GetFestivalIncentiveDefinitionBySchemeId";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<CreateFestivalIncentive>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<CreateFestivalIncentiveSlab> GetFestivalIncentiveSlabDefinitionBySchemeId(GetFestivalIncentiveValues objFormData)
        {
            Envelope<GetFestivalIncentiveValues> postData = new Envelope<GetFestivalIncentiveValues>();
            Envelope<CreateFestivalIncentiveSlab> output = new Envelope<CreateFestivalIncentiveSlab>();
            APIClient<Envelope<GetFestivalIncentiveValues>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<GetFestivalIncentiveValues>>();
                string apiUrl = "api/Incentive/GetFestivalIncentiveSlabDefinitionBySchemeId";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<CreateFestivalIncentiveSlab>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteFestivalIncentiveDefinition(DeleteFestivalIncentive objFormData)
        {
            Envelope<DeleteFestivalIncentive> postData = new Envelope<DeleteFestivalIncentive>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteFestivalIncentive>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteFestivalIncentive>>();
                string apiUrl = "api/Incentive/DeleteFestivalIncentiveDefinition";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Festival Incentive

        #region IncentiveReport
        public Envelope<BasePerPieceIncentiveDisplayReportList> GetBasePerPieceIncentiveReport(BasePerPieceIncentiveReportInputParam objFormData)
        {
            Envelope<BasePerPieceIncentiveReportInputParam> PostData = new Envelope<BasePerPieceIncentiveReportInputParam>();
            PostData.Data = objFormData;
            APIClient<Envelope<BasePerPieceIncentiveReportInputParam>> client = new APIClient<Envelope<BasePerPieceIncentiveReportInputParam>>();
            try
            {
                string apiUrl = "api/Incentive/GetBasePerPieceIncentiveReport";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<BasePerPieceIncentiveDisplayReportList>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Envelope<BasePerPieceIncentiveDetailReportList> GetBasePerPieceIncentiveDetailReport(BasePerPieceIncentiveReportInputParam objFormData)
        {
            Envelope<BasePerPieceIncentiveReportInputParam> PostData = new Envelope<BasePerPieceIncentiveReportInputParam>();
            PostData.Data = objFormData;
            APIClient<Envelope<BasePerPieceIncentiveReportInputParam>> client = new APIClient<Envelope<BasePerPieceIncentiveReportInputParam>>();
            try
            {
                string apiUrl = "api/Incentive/GetBasePerPieceIncentiveDetailReport";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<BasePerPieceIncentiveDetailReportList>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<FestivalIncentiveDisplayReportList> GetFestivalIncentiveReport(FestivalIncentiveReportInputParam objFormData)
        {
            Envelope<FestivalIncentiveReportInputParam> PostData = new Envelope<FestivalIncentiveReportInputParam>();
            PostData.Data = objFormData;
            APIClient<Envelope<FestivalIncentiveReportInputParam>> client = new APIClient<Envelope<FestivalIncentiveReportInputParam>>();
            try
            {
                string apiUrl = "api/Incentive/GetFestivalIncentiveReport";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<FestivalIncentiveDisplayReportList>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Envelope<FestivalIncentiveDetailReportList> GetFestivalIncentiveDetailReport(FestivalIncentiveReportInputParam objFormData)
        {
            Envelope<FestivalIncentiveReportInputParam> PostData = new Envelope<FestivalIncentiveReportInputParam>();
            PostData.Data = objFormData;
            APIClient<Envelope<FestivalIncentiveReportInputParam>> client = new APIClient<Envelope<FestivalIncentiveReportInputParam>>();
            try
            {
                string apiUrl = "api/Incentive/GetFestivalIncentiveDetailReport";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<FestivalIncentiveDetailReportList>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region IncentiveReport
        public Envelope<BasePerPieceIncentiveDisplayReportList> GetBasePerPieceIncentiveReportQSR(BasePerPieceIncentiveReportInputParamQSR objFormData)
        {
            Envelope<BasePerPieceIncentiveReportInputParamQSR> PostData = new Envelope<BasePerPieceIncentiveReportInputParamQSR>();
            PostData.Data = objFormData;
            APIClient<Envelope<BasePerPieceIncentiveReportInputParamQSR>> client = new APIClient<Envelope<BasePerPieceIncentiveReportInputParamQSR>>();
            try
            {
                string apiUrl = "api/Incentive/GetBasePerPieceIncentiveReportQSR";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<BasePerPieceIncentiveDisplayReportList>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }

}

