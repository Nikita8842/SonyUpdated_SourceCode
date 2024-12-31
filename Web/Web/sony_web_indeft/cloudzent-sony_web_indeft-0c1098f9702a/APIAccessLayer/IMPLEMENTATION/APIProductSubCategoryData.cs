using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using APIAccessLayer.INTERFACE;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIProductSubCategoryData : IAPIProductSubCategoryData
    {
        public Envelope<bool> CreateProductSubCategory(CreateProductSubCategoryForm objFormData)
        {
            Envelope<CreateProductSubCategoryForm> postData = new Envelope<CreateProductSubCategoryForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<CreateProductSubCategoryForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<CreateProductSubCategoryForm>>();
                string apiUrl = "api/MasterMaintenance/CreateProductSubCategory";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteProductSubCategory(DeleteProductSubCategoryForm objFormData)
        {
            Envelope<DeleteProductSubCategoryForm> postData = new Envelope<DeleteProductSubCategoryForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteProductSubCategoryForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteProductSubCategoryForm>>();
                string apiUrl = "api/MasterMaintenance/DeleteProductSubCategory";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateProductSubCategory(UpdateProductSubCategoryForm objFormData)
        {
            Envelope<UpdateProductSubCategoryForm> postData = new Envelope<UpdateProductSubCategoryForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UpdateProductSubCategoryForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<UpdateProductSubCategoryForm>>();
                string apiUrl = "api/MasterMaintenance/UpdateProductSubCategory";//checked
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
