using APIAccessLayer.INTERFACE;
using System;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIMaterialData : IAPIMaterialData
    {
        public Envelope<bool> CreateMaterial(MaterialMaster objFormData)
        {
            Envelope<MaterialMaster> postData = new Envelope<MaterialMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<MaterialMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<MaterialMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateMaterial";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                
            }
            catch (Exception ex)
            {
            }

            return output;
        }

        public Envelope<bool> DeleteMaterial(MaterialMaster objFormData)
        {
            Envelope<MaterialMaster> postData = new Envelope<MaterialMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<MaterialMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<MaterialMaster>>();
                string apiUrl = "api/MasterMaintenance/DeleteMaterial";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {

            }
            return output;
        }

        public Envelope<bool> UpdateMaterial(MaterialMaster objFormData)
        {
            Envelope<MaterialMaster> postData = new Envelope<MaterialMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<MaterialMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<MaterialMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateMaterial";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                
            }
            catch (Exception ex)
            {
                
            }
            return output;
        }

        public MaterialMaster GetMaterialById(MaterialMaster objFormData)
        {
            Envelope<MaterialMaster> PostData = new Envelope<MaterialMaster>();
            PostData.Data = objFormData;
            APIClient<Envelope<MaterialMaster>> client = new APIClient<Envelope<MaterialMaster>>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetMaterialByID";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<MaterialMaster>>(apiOutput.Result);
                return output.Data;
            }
            catch
            {
                return null;
            }
        }

        public MaterialMaster GetMaterialByMaterialCode(MaterialMaster objFormData)
        {
            Envelope<MaterialMaster> PostData = new Envelope<MaterialMaster>();
            PostData.Data = objFormData;
            APIClient<Envelope<MaterialMaster>> client = new APIClient<Envelope<MaterialMaster>>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetMaterialByMaterialCode";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<MaterialMaster>>(apiOutput.Result);
                return output.Data;
            }
            catch
            {
                return null;
            }
        }
    }
}
