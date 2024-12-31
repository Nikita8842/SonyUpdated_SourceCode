using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APICompetitorMasterData: IAPICompetitorMasterData
    {
        #region Competitor Master
        /// <summary>
        /// Create a new competitor company record in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> InsertCompetitorMaster(CompetitorMaster InputParam)
        {
            Envelope<CompetitorMaster> PostData = new Envelope<CompetitorMaster>();
            Envelope<bool> output = new Envelope<bool>();
            PostData.Data = InputParam;
            APIClient<Envelope<CompetitorMaster>> client;

            try
            {
                client = new APIClient<Envelope<CompetitorMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateCompetitor";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);                
            }
            catch (Exception ex)
            {
                
            }
            return output;
        }

        /// <summary>
        /// Update an existing competitor company in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> UpdateCompetitorMaster(CompetitorMaster InputParam)
        {
            Envelope<CompetitorMaster> PostData = new Envelope<CompetitorMaster>();
            Envelope<bool> output = new Envelope<bool>();
            PostData.Data = InputParam;
            APIClient<Envelope<CompetitorMaster>> client;

            try
            {
                client = new APIClient<Envelope<CompetitorMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateCompetitor";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;

        }

        /// <summary>
        /// Delete a competitor company if it is not mapped to any competitor product.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> DeleteCompetitorMaster(CompetitorMaster InputParam)
        {
            Envelope<CompetitorMaster> PostData = new Envelope<CompetitorMaster>();
            Envelope<bool> output = new Envelope<bool>();
            PostData.Data = InputParam;
            APIClient<Envelope<CompetitorMaster>> client;
            try
            {
                client = new APIClient<Envelope<CompetitorMaster>>();
                string apiUrl = "api/MasterMaintenance/DeleteCompetitor";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion

        #region Competitor Product Master
        /// <summary>
        /// Create a new competitor company record in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> InsertCompetitorProduct(CompetitorProductData InputParam)
        {
            Envelope<CompetitorProductData> PostData = new Envelope<CompetitorProductData>();
            PostData.Data = InputParam;
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CompetitorProductData>> client;

            try
            {
                client = new APIClient<Envelope<CompetitorProductData>>();
                string apiUrl = "api/MasterMaintenance/CreateCompetitorProduct";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        /// <summary>
        /// Update an existing competitor company in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> UpdateCompetitorProduct(CompetitorProductData InputParam)
        {
            Envelope<CompetitorProductData> PostData = new Envelope<CompetitorProductData>();
            PostData.Data = InputParam;
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CompetitorProductData>> client;

            try
            {
                client = new APIClient<Envelope<CompetitorProductData>>();
                string apiUrl = "api/MasterMaintenance/UpdateCompetitorProduct";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;

        }

        /// <summary>
        /// Delete a competitor company if it is not mapped to any competitor product.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> DeleteCompetitorProduct(CompetitorProductData InputParam)
        {
            Envelope<CompetitorProductData> PostData = new Envelope<CompetitorProductData>();
            PostData.Data = InputParam;
            APIClient<Envelope<CompetitorProductData>> client;
            Envelope<bool> output = new Envelope<bool>();
            try
            {
                client = new APIClient<Envelope<CompetitorProductData>>();
                string apiUrl = "api/MasterMaintenance/DeleteCompetitorProduct";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion

        #region Competitor Model Master
        /// <summary>
        /// Create a new competitor Model  record in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> InsertCompetitorModel(CompetitorModelMaster InputParam)
        {
            Envelope<CompetitorModelMaster> PostData = new Envelope<CompetitorModelMaster>();
            PostData.Data = InputParam;
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CompetitorModelMaster>> client;

            try
            {
                client = new APIClient<Envelope<CompetitorModelMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateCompetitorModel";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;

        }

        /// <summary>
        /// Update an existing competitor model in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> UpdateCompetitorModel(CompetitorModelMaster InputParam)
        {
            Envelope<CompetitorModelMaster> PostData = new Envelope<CompetitorModelMaster>();
            PostData.Data = InputParam;
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CompetitorModelMaster>> client;

            try
            {
                client = new APIClient<Envelope<CompetitorModelMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateCompetitorModel";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }

            return output;

        }

        /// <summary>
        /// Delete a competitor company if it is not mapped to any competitor model.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Envelope<bool> DeleteCompetitorModel(CompetitorModelMaster InputParam)
        {
            Envelope<CompetitorModelMaster> PostData = new Envelope<CompetitorModelMaster>();
            PostData.Data = InputParam;
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CompetitorModelMaster>> client;
            
            try
            {
                client = new APIClient<Envelope<CompetitorModelMaster>>();
                string apiUrl = "api/MasterMaintenance/DeleteCompetitorModel";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }

            return output;
        }

        public CompetitorModelList GetCompetitorModelById(CompetitorDataInput competitorModelId)
        {
            Envelope<CompetitorDataInput> PostData = new Envelope<CompetitorDataInput>();
            PostData.Data = competitorModelId;
            CompetitorModelList Data = new CompetitorModelList();
            APIClient<Envelope<CompetitorDataInput>> client = new APIClient<Envelope<CompetitorDataInput>>();
            var output = new Envelope<CompetitorModelList>();
            try
            {
                string apiUrl = "api/MasterMaintenance/GetCompetitorModelByID";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<CompetitorModelList>>(apiOutput.Result);
                Data = output.Data;
                return Data;
            }
            catch (Exception ex)
            {
            }
            return Data;
        }

        public IEnumerable<CompetitorMaster> GetCompetitors()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            
            try
            {
                string apiUrl = "api/MasterMaintenance/GetAllCompetitors";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<IEnumerable<CompetitorMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public IEnumerable<CompetitorModelMaster> GetAllCompetitorModels()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetAllCompetitorModels";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<IEnumerable<CompetitorModelMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        #endregion
    }
}
