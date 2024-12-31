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
    public class APIProductCategoryData : IAPIProductCategoryData
    {
        public Envelope<bool> CreateProductCategory(CreateProductCategoryForm objFormData)
        {
            Envelope<CreateProductCategoryForm> postData = new Envelope<CreateProductCategoryForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateProductCategoryForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateProductCategoryForm>>();
                string apiUrl = "api/MasterMaintenance/CreateProductCategory";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateProductCategory(UpdateProductCategoryForm objFormData)
        {
            Envelope<UpdateProductCategoryForm> postData = new Envelope<UpdateProductCategoryForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UpdateProductCategoryForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<UpdateProductCategoryForm>>();
                string apiUrl = "api/MasterMaintenance/UpdateProductCategory";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteProductCategory(DeleteProductCategoryForm objFormData)
        {
            Envelope<DeleteProductCategoryForm> postData = new Envelope<DeleteProductCategoryForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteProductCategoryForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteProductCategoryForm>>();
                string apiUrl = "api/MasterMaintenance/DeleteProductCategory";//checked
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
