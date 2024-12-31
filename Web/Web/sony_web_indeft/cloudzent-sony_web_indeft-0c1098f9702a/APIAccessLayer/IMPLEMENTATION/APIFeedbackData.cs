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
    public class APIFeedbackData : IAPIFeedbackData
    {
        public Envelope<bool> CreateFeedbackForm(CreateFeedbackForm objFormData)
        {
            Envelope<CreateFeedbackForm> postData = new Envelope<CreateFeedbackForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateFeedbackForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateFeedbackForm>>();
                string apiUrl = "api/MasterMaintenance/CreateFeedbackForm";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteFeedbackForm(DeleteFeedbackForm objFormData)
        {
            Envelope<DeleteFeedbackForm> postData = new Envelope<DeleteFeedbackForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteFeedbackForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteFeedbackForm>>();
                string apiUrl = "api/MasterMaintenance/DeleteFeedbackForm";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<CreateFeedbackForm> ViewFeedbackFormDesign(ViewFeedbackForm objFormData)
        {
            Envelope<ViewFeedbackForm> postData = new Envelope<ViewFeedbackForm>();
            Envelope<CreateFeedbackForm> output = new Envelope<CreateFeedbackForm>();
            APIClient<Envelope<ViewFeedbackForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ViewFeedbackForm>>();
                string apiUrl = "api/MasterMaintenance/ViewFeedbackFormDesign";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<CreateFeedbackForm>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> CreateTrainingForm(CreateTrainingForm objFormData)
        {
            Envelope<CreateTrainingForm> postData = new Envelope<CreateTrainingForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateTrainingForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateTrainingForm>>();
                string apiUrl = "api/MasterMaintenance/CreateTrainingForm";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> InActiveTrainingMessage(CreateTrainingForm objFormData)
        {
            Envelope<CreateTrainingForm> postData = new Envelope<CreateTrainingForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateTrainingForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateTrainingForm>>();
                string apiUrl = "api/MasterMaintenance/InActiveTrainingMessage";//checked
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
