using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIRoleRightsData : IAPIRoleRightsData
    {
        public Envelope<bool> CreateRoleRightsMapping(RoleRightsMappingMaster objFormData)
        {
            Envelope<RoleRightsMappingMaster> postData = new Envelope<RoleRightsMappingMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<RoleRightsMappingMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<RoleRightsMappingMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateRoleRightsMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }

        public Envelope<bool> UpdateRoleRightsMapping(RoleRightsMappingMaster objFormData)
        {
            Envelope<RoleRightsMappingMaster> postData = new Envelope<RoleRightsMappingMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<RoleRightsMappingMaster>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<RoleRightsMappingMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateRoleRightsMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }
    }
}
