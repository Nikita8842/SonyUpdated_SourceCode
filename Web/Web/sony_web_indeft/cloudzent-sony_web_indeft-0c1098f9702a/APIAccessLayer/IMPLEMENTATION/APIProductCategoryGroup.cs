using AMBOModels.GlobalAccessible;
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
    public class APIProductCategoryGroup : IAPIProductCategoryGroup
    {
        public Envelope<bool> CreateProductCategoryGroup(ProductCategoryGroupMaster objFormData)
        {
            Envelope<ProductCategoryGroupMaster> postData = new Envelope<ProductCategoryGroupMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ProductCategoryGroupMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ProductCategoryGroupMaster>>();
                string apiUrl = "api/ProductCategoryGroup/CreateProductCategoryGroup";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                return output; 
            }
            catch (Exception ex)
            {
                return output;
            }
        }

        public Envelope<bool> UpdateProductCategoryGroup(ProductCategoryGroupMaster objFormData)
        {
            Envelope<ProductCategoryGroupMaster> postData = new Envelope<ProductCategoryGroupMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ProductCategoryGroupMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ProductCategoryGroupMaster>>();
                string apiUrl = "api/ProductCategoryGroup/UpdateProductCategoryGroup";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);

                return output;
            }
            catch (Exception ex)
            {

                return output;
            }
           
        }

        public Envelope<bool> DeleteProductCategoryGroup(Common InputParam)
        {
            Envelope<Common> postData = new Envelope<Common>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<Common>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<Common>>();
                string apiUrl = "api/ProductCategoryGroup/DeleteProductCategoryGroup";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        

        /// <summary>
        /// To fetch all Product category group by Id.
        /// </summary>
        /// <returns>Product Category Group By ID.</returns>
        public ProductCategoryGroupMaster GetProductCategoryGroupById(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/ProductCategoryGroup/GetProductCategoryGroupById";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<ProductCategoryGroupMaster>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
