using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIRegionData : IAPIRegionData
    {
        public Envelope<bool> CreateRegion(RegionMaster objFormData)
        {
            Envelope<RegionMaster> postData = new Envelope<RegionMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<RegionMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<RegionMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateRegion";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteRegion(RegionMaster objFormData)
        {
            Envelope<RegionMaster> postData = new Envelope<RegionMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<RegionMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<RegionMaster>>();
                string apiUrl = "api/MasterMaintenance/DeleteRegion";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateRegion(RegionMaster objFormData)
        {
            Envelope<RegionMaster> postData = new Envelope<RegionMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<RegionMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<RegionMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateRegion";//checked
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
