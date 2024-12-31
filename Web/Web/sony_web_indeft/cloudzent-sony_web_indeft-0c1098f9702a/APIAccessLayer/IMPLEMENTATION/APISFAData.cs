using AMBOModels.UserManagement;
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
    public class APISFAData : IAPISFAData
    {
        public Envelope<bool> CreateSFA(SFAFormData objFormData)
        {
            Envelope<SFAFormData> postData = new Envelope<SFAFormData>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFAFormData>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFAFormData>>();
                string apiUrl = "api/UserManagement/CreateSFA";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public SFAFormData GetSFAById(Int64? ID)
        {
            Envelope<Int64?> postData = new Envelope<Int64?>();
            Envelope<SFAFormData> output = new Envelope<SFAFormData>();
            APIClient<Envelope<Int64?>> client;
            postData.Data = ID;
            try
            {
                client = new APIClient<Envelope<Int64?>>();
                string apiUrl = "api/UserManagement/GetEmployeeById";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<SFAFormData>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }

        public Envelope<bool> UpdateSFA(SFAFormData objFormData)
        {
            Envelope<SFAFormData> postData = new Envelope<SFAFormData>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFAFormData>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFAFormData>>();
                string apiUrl = "api/UserManagement/UpdateSFA";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteSFA(SFAFormData objFormData)
        {
            Envelope<SFAFormData> postData = new Envelope<SFAFormData>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFAFormData>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFAFormData>>();
                string apiUrl = "api/UserManagement/DeleteSFA";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public SFAFormData GetOfferedEmployeeById(Int64? ID)
        {
            Envelope<Int64?> postData = new Envelope<Int64?>();
            Envelope<SFAFormData> output = new Envelope<SFAFormData>();
            APIClient<Envelope<Int64?>> client;
            postData.Data = ID;
            try
            {
                client = new APIClient<Envelope<Int64?>>();
                string apiUrl = "api/UserManagement/GetOfferedEmployeeById";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<SFAFormData>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }

        public Envelope<bool> ManageOfferedEmployee(SFAFormData objFormData)
        {
            Envelope<SFAFormData> postData = new Envelope<SFAFormData>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFAFormData>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFAFormData>>();
                string apiUrl = "api/UserManagement/ManageOfferedEmployee";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<SFACodeAutoGenerate> GetSFACode()
        {            
            Envelope<SFACodeAutoGenerate> output = new Envelope<SFACodeAutoGenerate>();
            APIClient<DBNull> client;
            try
            {
                client = new APIClient<DBNull>();
                string apiUrl = "api/UserManagement/GetSFACode";//checked
                var apiOutput = client.Get(apiUrl);
                output = JsonConvert.DeserializeObject<Envelope<SFACodeAutoGenerate>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
    }
}
