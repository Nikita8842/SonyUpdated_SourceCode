using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APICityData: IAPICityData
    {
        public Envelope<bool> CreateCity(CreateCityForm objFormData)
        {
            Envelope<CreateCityForm> postData = new Envelope<CreateCityForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateCityForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateCityForm>>();
                string apiUrl = "api/MasterMaintenance/CreateCity";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteCity(DeleteCityForm objFormData)
        {
            Envelope<DeleteCityForm> postData = new Envelope<DeleteCityForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteCityForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteCityForm>>();
                string apiUrl = "api/MasterMaintenance/DeleteCity";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateCity(UpdateCityForm objFormData)
        {
            Envelope<UpdateCityForm> postData = new Envelope<UpdateCityForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UpdateCityForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<UpdateCityForm>>();
                string apiUrl = "api/MasterMaintenance/UpdateCity";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public List<CityMaster> GetAllCities()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetAllCities";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<CityMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}