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
    public class APIStateData : IAPIStateData
    {
        public Envelope<bool> CreateState(CreateStateForm objFormData)
        {
            Envelope<CreateStateForm> postData = new Envelope<CreateStateForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateStateForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateStateForm>>();
                string apiUrl = "api/MasterMaintenance/CreateState";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteState(DeleteStateForm objFormData)
        {
            Envelope<DeleteStateForm> postData = new Envelope<DeleteStateForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteStateForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteStateForm>>();
                string apiUrl = "api/MasterMaintenance/DeleteState";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateState(UpdateStateForm objFormData)
        {
            Envelope<UpdateStateForm> postData = new Envelope<UpdateStateForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UpdateStateForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<UpdateStateForm>>();
                string apiUrl = "api/MasterMaintenance/UpdateState";//checked
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
