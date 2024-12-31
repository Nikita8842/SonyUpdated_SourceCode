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
    public class APIProductTargetCategoryData : IAPIProductTargetCategoryData
    {
        public Envelope<bool> CreateProductTargetCategoryMapping(ProductTargetCategoryMaster objFormData)
        {
            Envelope<ProductTargetCategoryMaster> postData = new Envelope<ProductTargetCategoryMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ProductTargetCategoryMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ProductTargetCategoryMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateProductTargetCategory";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteProductTargetCategoryMapping(ProductTargetCategoryMaster objFormData)
        {
            Envelope<ProductTargetCategoryMaster> postData = new Envelope<ProductTargetCategoryMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ProductTargetCategoryMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ProductTargetCategoryMaster>>();
                string apiUrl = "api/MasterMaintenance/DeleteProductTargetCategory";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateProductTargetCategoryMapping(ProductTargetCategoryMaster objFormData)
        {
            Envelope<ProductTargetCategoryMaster> postData = new Envelope<ProductTargetCategoryMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ProductTargetCategoryMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ProductTargetCategoryMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateProductTargetCategory";//checked
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
