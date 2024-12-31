using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APISFASalaryMaster : IAPISFASalaryMaster
    {
        public Envelope<bool> CreateSFASalaryMaster(SFASalaryMasterGrid objFormData)
        {
            Envelope<SFASalaryMasterGrid> postData = new Envelope<SFASalaryMasterGrid>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFASalaryMasterGrid>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFASalaryMasterGrid>>();
                string apiUrl = "api/MasterMaintenance/CreateSFASalaryMaster";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteSFASalaryMaster(SFASalaryMasterDelete objFormData)
        {
            Envelope<SFASalaryMasterDelete> postData = new Envelope<SFASalaryMasterDelete>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFASalaryMasterDelete>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFASalaryMasterDelete>>();
                string apiUrl = "api/MasterMaintenance/DeleteSFASalaryMaster";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public SFASalaryMasterGrid GetSFASalaryMasterbyId(SFASalaryMasterGrid InputParam)
        {
            Envelope<SFASalaryMasterGrid> PostData = new Envelope<SFASalaryMasterGrid>();
            PostData.Data = InputParam;
            APIClient<Envelope<SFASalaryMasterGrid>> client = new APIClient<Envelope<SFASalaryMasterGrid>>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetSFASalaryMasterById";
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<SFASalaryMasterGrid>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SFASalaryMasterGrid> SFASalaryMasterDataDownload(SFASalaryMasterDownload InputParam)
        {
            Envelope<SFASalaryMasterDownload> postData = new Envelope<SFASalaryMasterDownload>();
            Envelope<List<SFASalaryMasterGrid>> output = new Envelope<List<SFASalaryMasterGrid>>();
            APIClient<Envelope<SFASalaryMasterDownload>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<SFASalaryMasterDownload>>();
                string apiUrl = "api/MasterMaintenance/SFASalaryMasterDataDownload";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<SFASalaryMasterGrid>>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
                output.Data = null;
            }
            return output.Data;
        }

        public Envelope<bool> UpdateSFASalaryMaster(SFASalaryMasterGrid objFormData)
        {
            Envelope<SFASalaryMasterGrid> postData = new Envelope<SFASalaryMasterGrid>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<SFASalaryMasterGrid>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<SFASalaryMasterGrid>>();
                string apiUrl = "api/MasterMaintenance/UpdateSFASalaryMaster";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DataTable> ManageSFASalaryMasterData(List<SFASalaryMasterGrid> InputParam)
        {
            Envelope<List<SFASalaryMasterGrid>> postData = new Envelope<List<SFASalaryMasterGrid>>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<List<SFASalaryMasterGrid>>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<List<SFASalaryMasterGrid>>>();
                string apiUrl = "api/MasterMaintenance/ManageSFASalaryMasterData";
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
