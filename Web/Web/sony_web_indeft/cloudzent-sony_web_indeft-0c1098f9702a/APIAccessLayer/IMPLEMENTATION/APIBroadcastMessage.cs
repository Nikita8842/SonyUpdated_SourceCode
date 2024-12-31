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
    public class APIBroadcastMessage : IAPIBroadcastMessage
    {
        public Envelope<CreateBroadcastMessageFormOutput> CreateBroadcastMessage(CreateBroadcastMessageForm objBroadcastData)
        {
            Envelope<CreateBroadcastMessageForm> postData = new Envelope<CreateBroadcastMessageForm>();
            Envelope<CreateBroadcastMessageFormOutput> output = new Envelope<CreateBroadcastMessageFormOutput>();
            APIClient<Envelope<CreateBroadcastMessageForm>> client;
            postData.Data = objBroadcastData;
            try
            {
                client = new APIClient<Envelope<CreateBroadcastMessageForm>>();
                string apiUrl = "api/MasterMaintenance/CreateBroadcastMessage";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<CreateBroadcastMessageFormOutput>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }


        public Envelope<bool> InActiveBroadcastMessage(CreateBroadcastMessageForm objFormData)
        {
            Envelope<CreateBroadcastMessageForm> postData = new Envelope<CreateBroadcastMessageForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateBroadcastMessageForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateBroadcastMessageForm>>();
                string apiUrl = "api/MasterMaintenance/InActiveBroadcastMessage";//checked
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
