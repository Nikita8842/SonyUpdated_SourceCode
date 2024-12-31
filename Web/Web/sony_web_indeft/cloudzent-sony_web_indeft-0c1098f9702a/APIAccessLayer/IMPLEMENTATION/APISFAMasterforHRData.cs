using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.UserManagement;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APISFAMasterforHRData : IAPISFAMasterforHRData
    {
        public SFAMasterforHR GetSFAMasterforHRbyId(SFAMasterforHR InputParam)
        {
            Envelope<SFAMasterforHR> PostData = new Envelope<SFAMasterforHR>();
            PostData.Data = InputParam;
            APIClient<Envelope<SFAMasterforHR>> client = new APIClient<Envelope<SFAMasterforHR>>();

            try
            {
                string apiUrl = "api/UserManagement/GetSFAMasterforHRById";
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<SFAMasterforHR>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<bool> DeleteSFAMasterforHR(SFAMasterforHRDelete objFormData)
        {
            Envelope<SFAMasterforHRDelete> postData = new Envelope<SFAMasterforHRDelete>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFAMasterforHRDelete>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFAMasterforHRDelete>>();
                string apiUrl = "api/UserManagement/DeleteSFAMasterforHR";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateSFAMasterforHR(SFAMasterforHR objFormData)
        {
            Envelope<SFAMasterforHR> postData = new Envelope<SFAMasterforHR>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFAMasterforHR>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFAMasterforHR>>();
                string apiUrl = "api/UserManagement/UpdateSFAMasterforHR";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> CreateSFAMasterforHR(SFAMasterforHR objFormData)
        {
            Envelope<SFAMasterforHR> postData = new Envelope<SFAMasterforHR>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFAMasterforHR>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFAMasterforHR>>();
                string apiUrl = "api/UserManagement/CreateSFAMasterforHR";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public IEnumerable<SFAMasterforHR> GetAllDetailsSFAMasterforHR()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/UserManagement/GetAllDetailsSFAMasterforHR";
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFAMasterforHR>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SFAMasterforHR> SFAMasterforHRDataDownload(SFAMasterforHRDownload InputParam)
        {
            Envelope<SFAMasterforHRDownload> postData = new Envelope<SFAMasterforHRDownload>();
            Envelope<List<SFAMasterforHR>> output = new Envelope<List<SFAMasterforHR>>();
            APIClient<Envelope<SFAMasterforHRDownload>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<SFAMasterforHRDownload>>();
                string apiUrl = "api/UserManagement/SFAMasterforHRDataDownload";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<SFAMasterforHR>>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }

        public Envelope<DataTable> ManageSFAMasterforHRData(List<SFAMasterforHR> InputParam)
        {
            Envelope<List<SFAMasterforHR>> postData = new Envelope<List<SFAMasterforHR>>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<List<SFAMasterforHR>>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<List<SFAMasterforHR>>>();
                string apiUrl = "api/UserManagement/ManageSFAMasterforHRData";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DataTable> ManageSFADetails(List<EncryptionInput> InputParam)
        {
            Envelope<List<EncryptionInput>> postData = new Envelope<List<EncryptionInput>>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<List<EncryptionInput>>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<List<EncryptionInput>>>();
                string apiUrl = "api/UserManagement/ManageSFADetails";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
    }
}
