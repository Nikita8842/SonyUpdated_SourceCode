using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APITargetDateSettingMaster : IAPITargetDateSettingMaster
    {
        public Envelope<bool> CreateTargetDateSettingMaster(TargetDateSettingMaster objFormData)
        {
            Envelope<TargetDateSettingMaster> postData = new Envelope<TargetDateSettingMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<TargetDateSettingMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<TargetDateSettingMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateTargetDateSettingMaster";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public TargetDateSettingMaster GetTargetDateSettingMasterById(TargetDateSettingMaster InputParam)
        {
            Envelope<TargetDateSettingMaster> PostData = new Envelope<TargetDateSettingMaster>();
            PostData.Data = InputParam;
            APIClient<Envelope<TargetDateSettingMaster>> client = new APIClient<Envelope<TargetDateSettingMaster>>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetTargetDateSettingMasterById";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<TargetDateSettingMaster>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<bool> UpdateTargetDateSettingMaster(TargetDateSettingMaster objFormData)
        {
            Envelope<TargetDateSettingMaster> postData = new Envelope<TargetDateSettingMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<TargetDateSettingMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<TargetDateSettingMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateTargetDateSettingMaster";//checked
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
