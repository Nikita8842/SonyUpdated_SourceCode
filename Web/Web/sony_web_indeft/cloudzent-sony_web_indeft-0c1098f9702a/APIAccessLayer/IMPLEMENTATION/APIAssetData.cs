using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using AMBOModels.Modules;
using APIAccessLayer.INTERFACE;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIAssetData : IAPIAssetData
    {
        public Envelope<bool> CreateAsset(AssetMaster objFormData)
        {
            Envelope<AssetMaster> postData = new Envelope<AssetMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<AssetMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<AssetMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateAsset";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteAsset(AssetMaster objFormData)
        {
            Envelope<AssetMaster> postData = new Envelope<AssetMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<AssetMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<AssetMaster>>();
                string apiUrl = "api/MasterMaintenance/DeleteAsset";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateAsset(AssetMaster objFormData)
        {
            Envelope<AssetMaster> postData = new Envelope<AssetMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<AssetMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<AssetMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateAsset";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
    }
}
