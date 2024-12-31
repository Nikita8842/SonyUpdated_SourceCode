using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIAccessLayer.INTERFACE;
using APIAccessLayer.Helper;
using AMBOModels.Modules;
using Newtonsoft.Json;
using System.Data;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIAssetAssignment : IAPIAssetAssignment
    {
        public Envelope<DataTable> UploadAssetCollectionFromSFA(AssetCollectionFromSFA objFormData)
        {
            Envelope<AssetCollectionFromSFA> postData = new Envelope<AssetCollectionFromSFA>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<AssetCollectionFromSFA>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<AssetCollectionFromSFA>>();
                string apiUrl = "api/ModuleManagement/UploadAssetCollectionFromSFA";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public AssetAssignmentToRDIUpdate GetAssetAssignmentToRDIById(AssetAssignmentToRDI InputParam)
        {
            Envelope<AssetAssignmentToRDI> PostData = new Envelope<AssetAssignmentToRDI>();
            PostData.Data = InputParam;
            APIClient<Envelope<AssetAssignmentToRDI>> client = new APIClient<Envelope<AssetAssignmentToRDI>>();

            try
            {
                string apiUrl = "api/ModuleManagement/GetAssetAssignmentToRDIById";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<AssetAssignmentToRDIUpdate>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<bool> UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate objFormData)
        {
            Envelope<AssetAssignmentToRDIUpdate> postData = new Envelope<AssetAssignmentToRDIUpdate>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<AssetAssignmentToRDIUpdate>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<AssetAssignmentToRDIUpdate>>();
                string apiUrl = "api/ModuleManagement/UpdateAssetAssignmentToRDI";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DataTable> UploadAssetAssignmentToRDI(AssetAssignmentToRDIUpload objFormData)
        {
            Envelope<AssetAssignmentToRDIUpload> postData = new Envelope<AssetAssignmentToRDIUpload>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<AssetAssignmentToRDIUpload>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<AssetAssignmentToRDIUpload>>();
                string apiUrl = "api/ModuleManagement/UploadAssetAssignmentToRDI";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<IEnumerable<AssetCollectionFromSFAData>> GetAssetCollectionFormatFromSFA(AssetCollectionFromSFAGet InputParam)
        {
            Envelope<AssetCollectionFromSFAGet> PostData = new Envelope<AssetCollectionFromSFAGet>();
            PostData.Data = InputParam;
            APIClient<Envelope<AssetCollectionFromSFAGet>> client = new APIClient<Envelope<AssetCollectionFromSFAGet>>();

            try
            {
                string apiUrl = "api/ModuleManagement/GetAssetCollectionFormatFromSFA";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<IEnumerable<AssetCollectionFromSFAData>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
