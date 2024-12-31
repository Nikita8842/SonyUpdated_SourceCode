using AMBOModels.MasterMaintenance;
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
    public class APILocationData : IAPILocationData
    {
        public Envelope<bool> CreateLocation(CreateLocationForm objFormData)
        {
            Envelope<CreateLocationForm> postData = new Envelope<CreateLocationForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateLocationForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateLocationForm>>();
                string apiUrl = "api/MasterMaintenance/CreateLocation";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteLocation(DeleteLocationForm objFormData)
        {
            Envelope<DeleteLocationForm> postData = new Envelope<DeleteLocationForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteLocationForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteLocationForm>>();
                string apiUrl = "api/MasterMaintenance/DeleteLocation";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateLocation(UpdateLocationForm objFormData)
        {
            Envelope<UpdateLocationForm> postData = new Envelope<UpdateLocationForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UpdateLocationForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<UpdateLocationForm>>();
                string apiUrl = "api/MasterMaintenance/UpdateLocation";//checked
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
