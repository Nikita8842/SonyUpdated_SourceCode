using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.IncentiveManagement;
using APIAccessLayer.Helper;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIDeviationData : IAPIDeviationData
    {
        #region Deviation Upload By RDI
        public Envelope<DeviationUploadByRDIExcel> GetDeviationUploadByRDIExcel(DeviationUploadByRDISearch objSearchData)
        {
            Envelope<DeviationUploadByRDISearch> postData = new Envelope<DeviationUploadByRDISearch>();
            Envelope<DeviationUploadByRDIExcel> output = new Envelope<DeviationUploadByRDIExcel>();
            APIClient<Envelope<DeviationUploadByRDISearch>> client;
            postData.Data = objSearchData;
            try
            {
                client = new APIClient<Envelope<DeviationUploadByRDISearch>>();
                string apiUrl = "api/Deviation/GetDeviationUploadByRDIExcel";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationUploadByRDIExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DeviationUploadByRDIExcel> GetDeviationUploadByRDIReasons(DeviationUploadByRDISearch objSearchData)
        {
            Envelope<DeviationUploadByRDISearch> postData = new Envelope<DeviationUploadByRDISearch>();
            Envelope<DeviationUploadByRDIExcel> output = new Envelope<DeviationUploadByRDIExcel>();
            APIClient<Envelope<DeviationUploadByRDISearch>> client;
            postData.Data = objSearchData;
            try
            {
                client = new APIClient<Envelope<DeviationUploadByRDISearch>>();
                string apiUrl = "api/Deviation/GetDeviationUploadByRDIReasons";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationUploadByRDIExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DeviationUploadByRDIExcel> ManageDeviationUploadByRDI(DeviationUploadByRDIExcel objData)
        {
            Envelope<DeviationUploadByRDIExcel> postData = new Envelope<DeviationUploadByRDIExcel>();
            Envelope<DeviationUploadByRDIExcel> output = new Envelope<DeviationUploadByRDIExcel>();
            APIClient<Envelope<DeviationUploadByRDIExcel>> client;
            postData.Data = objData;
            try
            {
                client = new APIClient<Envelope<DeviationUploadByRDIExcel>>();
                string apiUrl = "api/Deviation/ManageDeviationUploadByRDI";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationUploadByRDIExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DeviationUploadByRDIExcel> ManageDeviationUploadByRDI_Festival(DeviationUploadByRDIExcel objData)
        {
            Envelope<DeviationUploadByRDIExcel> postData = new Envelope<DeviationUploadByRDIExcel>();
            Envelope<DeviationUploadByRDIExcel> output = new Envelope<DeviationUploadByRDIExcel>();
            APIClient<Envelope<DeviationUploadByRDIExcel>> client;
            postData.Data = objData;
            try
            {
                client = new APIClient<Envelope<DeviationUploadByRDIExcel>>();
                string apiUrl = "api/Deviation/ManageDeviationUploadByRDI_Festival";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationUploadByRDIExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> ManageDeviationUploadByRDI_SaveReasons(DeviationUploadByRDISaveReasons objData)
        {
            Envelope<DeviationUploadByRDISaveReasons> postData = new Envelope<DeviationUploadByRDISaveReasons>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeviationUploadByRDISaveReasons>> client;
            postData.Data = objData;
            try
            {
                client = new APIClient<Envelope<DeviationUploadByRDISaveReasons>>();
                string apiUrl = "api/Deviation/ManageDeviationUploadByRDI_SaveReasons";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> ManageDeviationUploadByRDI_ApproveReasons(DeviationUploadByRDISaveReasons objData)
        {
            Envelope<DeviationUploadByRDISaveReasons> postData = new Envelope<DeviationUploadByRDISaveReasons>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeviationUploadByRDISaveReasons>> client;
            postData.Data = objData;
            try
            {
                client = new APIClient<Envelope<DeviationUploadByRDISaveReasons>>();
                string apiUrl = "api/Deviation/ManageDeviationUploadByRDI_ApproveReasons";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Deviation Upload By RDI

        #region Deviation Approval
        public Envelope<DeviationApprovalExcel> GetDeviationApprovalExcel(DeviationApprovalSearch objSearchData)
        {
            Envelope<DeviationApprovalSearch> postData = new Envelope<DeviationApprovalSearch>();
            Envelope<DeviationApprovalExcel> output = new Envelope<DeviationApprovalExcel>();
            APIClient<Envelope<DeviationApprovalSearch>> client;
            postData.Data = objSearchData;
            try
            {
                client = new APIClient<Envelope<DeviationApprovalSearch>>();
                string apiUrl = "api/Deviation/GetDeviationApprovalExcel";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationApprovalExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DeviationApprovalExcel> UploadDeviationApprovalExcel(DeviationApprovalExcel objData)
        {
            Envelope<DeviationApprovalExcel> postData = new Envelope<DeviationApprovalExcel>();
            Envelope<DeviationApprovalExcel> output = new Envelope<DeviationApprovalExcel>();
            APIClient<Envelope<DeviationApprovalExcel>> client;
            postData.Data = objData;
            try
            {
                client = new APIClient<Envelope<DeviationApprovalExcel>>();
                string apiUrl = "api/Deviation/ManageDeviationUploadByRDI";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationApprovalExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DeviationApprovalExcel> UploadDeviationApprovalExcel_Festival(DeviationApprovalExcel objData)
        {
            Envelope<DeviationApprovalExcel> postData = new Envelope<DeviationApprovalExcel>();
            Envelope<DeviationApprovalExcel> output = new Envelope<DeviationApprovalExcel>();
            APIClient<Envelope<DeviationApprovalExcel>> client;
            postData.Data = objData;
            try
            {
                client = new APIClient<Envelope<DeviationApprovalExcel>>();
                string apiUrl = "api/Deviation/ManageDeviationUploadByRDI_Festival";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationApprovalExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Deviation Approval

        #region Deviation FinalUpload
        public Envelope<DeviationFinalUploadExcel> GetDeviationFinalUploadExcel(DeviationFinalUploadSearch objSearchData)
        {
            Envelope<DeviationFinalUploadSearch> postData = new Envelope<DeviationFinalUploadSearch>();
            Envelope<DeviationFinalUploadExcel> output = new Envelope<DeviationFinalUploadExcel>();
            APIClient<Envelope<DeviationFinalUploadSearch>> client;
            postData.Data = objSearchData;
            try
            {
                client = new APIClient<Envelope<DeviationFinalUploadSearch>>();
                string apiUrl = "api/Deviation/GetDeviationFinalUploadExcel";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationFinalUploadExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<DeviationFinalUploadExcel> UploadDeviationFinalUploadExcel(DeviationFinalUploadExcel objData)
        {
            Envelope<DeviationFinalUploadExcel> postData = new Envelope<DeviationFinalUploadExcel>();
            Envelope<DeviationFinalUploadExcel> output = new Envelope<DeviationFinalUploadExcel>();
            APIClient<Envelope<DeviationFinalUploadExcel>> client;
            postData.Data = objData;
            try
            {
                client = new APIClient<Envelope<DeviationFinalUploadExcel>>();
                string apiUrl = "api/Deviation/ManageDeviationUploadByRDI";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationFinalUploadExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        public Envelope<DeviationFinalUploadExcel> UploadDeviationFinalUploadExcel_Festival(DeviationFinalUploadExcel objData)
        {
            Envelope<DeviationFinalUploadExcel> postData = new Envelope<DeviationFinalUploadExcel>();
            Envelope<DeviationFinalUploadExcel> output = new Envelope<DeviationFinalUploadExcel>();
            APIClient<Envelope<DeviationFinalUploadExcel>> client;
            postData.Data = objData;
            try
            {
                client = new APIClient<Envelope<DeviationFinalUploadExcel>>();
                string apiUrl = "api/Deviation/ManageDeviationUploadByRDI_Festival";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DeviationFinalUploadExcel>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Deviation FinalUpload
    }
}
