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
    public class APIChannelData : IAPIChannelData
    {
        public Envelope<bool> CreateChannel(ChannelMaster objFormData)
        {
            Envelope<ChannelMaster> postData = new Envelope<ChannelMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ChannelMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ChannelMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateChannel";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateChannel(ChannelMaster objFormData)
        {
            Envelope<ChannelMaster> postData = new Envelope<ChannelMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ChannelMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ChannelMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateChannel";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteChannel(ChannelMaster objFormData)
        {
            Envelope<ChannelMaster> postData = new Envelope<ChannelMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ChannelMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ChannelMaster>>();
                string apiUrl = "api/MasterMaintenance/DeleteChannel";
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
