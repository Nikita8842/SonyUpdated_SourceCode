using System;
using AMBOModels.Modules;
using APIAccessLayer.INTERFACE;
using APIAccessLayer.Helper;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIAssetIssuedToSFA : IAPIAssetIssuedToSFA
    {
        public Envelope<IEnumerable<AssetIssuedToSFAGrid>> GetAssetIssuedToSFA(AssetIssuedToSFAGet InputParam)
        {
            Envelope<AssetIssuedToSFAGet> postData = new Envelope<AssetIssuedToSFAGet>();
            Envelope<IEnumerable<AssetIssuedToSFAGrid>> output=new Envelope<IEnumerable<AssetIssuedToSFAGrid>>();
            APIClient<Envelope<AssetIssuedToSFAGet>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<AssetIssuedToSFAGet>>();
                string apiUrl = "api/ModuleManagement/GetAssetIssuedToSFA";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<IEnumerable<AssetIssuedToSFAGrid>>>(apiOutput.Result);
                
            }
            catch (Exception ex)
            {
                output.Data = null;
            }
            return output;
        }

        public Envelope<IEnumerable<AssetsDropDownData>> GetAssetsDropDown(AssetIssuedToSFAGet InputParam)
        {
            Envelope<AssetIssuedToSFAGet> postData = new Envelope<AssetIssuedToSFAGet>();
            Envelope<IEnumerable<AssetsDropDownData>> output = new Envelope<IEnumerable<AssetsDropDownData>>();
            APIClient<Envelope<AssetIssuedToSFAGet>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<AssetIssuedToSFAGet>>();
                string apiUrl = "api/ModuleManagement/GetAssetsDropDown";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<IEnumerable<AssetsDropDownData>>>(apiOutput.Result);

            }
            catch (Exception ex)
            {
                output.Data = null;
            }
            return output;
        }

        public Envelope<DataTable> IssueAssetToSFA(AssetAssignmentToSFA InputParam)
        {
            Envelope<AssetAssignmentToSFA> postData = new Envelope<AssetAssignmentToSFA>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<AssetAssignmentToSFA>> client;
            postData.Data = InputParam;
            try
            {
                client = new APIClient<Envelope<AssetAssignmentToSFA>>();
                string apiUrl = "api/ModuleManagement/IssueAssetToSFA";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
    }
}
