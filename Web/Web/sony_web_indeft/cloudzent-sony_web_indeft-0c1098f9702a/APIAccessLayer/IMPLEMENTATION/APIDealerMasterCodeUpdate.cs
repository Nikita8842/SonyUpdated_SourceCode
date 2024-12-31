using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.Modules;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;
using System.Configuration;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIDealerMasterCodeUpdate : IAPIDealerMasterCodeUpdate
    {
        public List<Dealerdetails> GetDealerByMasterCode(DealerMasterCodeUpdate InputParam)
        {
            Envelope<DealerMasterCodeUpdate> PostData = new Envelope<DealerMasterCodeUpdate>();
            PostData.Data = InputParam;
            APIClient<Envelope<DealerMasterCodeUpdate>> client = new APIClient<Envelope<DealerMasterCodeUpdate>>();

            try
            {                
                string apiUrl = "api/ModuleManagement/GetDealerByMasterCode";//checked
                var apiOutput = client.Post(apiUrl, PostData);                
                var output = JsonConvert.DeserializeObject<Envelope<List<Dealerdetails>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<string> GetDealerMasterCodeList()
        {            
            APIClient<Envelope<string>> client = new APIClient<Envelope<string>>();

            try
            {
                string apiUrl = "api/ModuleManagement/GetDealerMasterCodeList";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<string>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<bool> UpdateDealerMasterCode(DealerMasterCodeUpdate objFormData)
        {
            Envelope<DealerMasterCodeUpdate> postData = new Envelope<DealerMasterCodeUpdate>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerMasterCodeUpdate>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DealerMasterCodeUpdate>>();
                string apiUrl = "api/ModuleManagement/UpdateDealerMasterCode";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> ValidateDealerMasterCode(DealerMasterCodeUpdate InputParam)
        {
            Envelope<DealerMasterCodeUpdate> PostData = new Envelope<DealerMasterCodeUpdate>();
            PostData.Data = InputParam;
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerMasterCodeUpdate>> client = new APIClient<Envelope<DealerMasterCodeUpdate>>();

            try
            {
                string apiUrl = "api/ModuleManagement/ValidateDealerMasterCode";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);               
               
            }
            catch (Exception ex)
            {
               
            }
            return output;
        }
    }
}
