using APIAccessLayer.INTERFACE;
using System;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIBranchData : IAPIBranchData
    {
        public Envelope<bool> CreateBranch(CreateBranchForm objFormData)
        {
            Envelope<CreateBranchForm> postData = new Envelope<CreateBranchForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateBranchForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateBranchForm>>();
                string apiUrl = "api/MasterMaintenance/AddNewBranch";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteBranch(DeleteBranchForm objFormData)
        {
            Envelope<DeleteBranchForm> postData = new Envelope<DeleteBranchForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteBranchForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteBranchForm>>();
                string apiUrl = "api/MasterMaintenance/DeleteBranch";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateBranch(UpdateBranchForm objFormData)
        {
            Envelope<UpdateBranchForm> postData = new Envelope<UpdateBranchForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UpdateBranchForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<UpdateBranchForm>>();
                string apiUrl = "api/MasterMaintenance/UpdateBranch";//checked
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
