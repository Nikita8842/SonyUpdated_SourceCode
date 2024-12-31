using AMBOModels.MasterMaintenance;
using APIAccessLayer.INTERFACE;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using System;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIDealerData : IAPIDealerData
    {
        public DealerMaster GetDealerById(DealerMaster InputParam)
        {
            Envelope<DealerMaster> PostData = new Envelope<DealerMaster>();
            PostData.Data = InputParam;
            APIClient<Envelope<DealerMaster>> client = new APIClient<Envelope<DealerMaster>>();
            
            try
            {
                string apiUrl = "api/MasterMaintenance/GetDealerById";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<DealerMaster>>(apiOutput.Result);
                return output.Data;
            }
            catch
            {
                return null;
            }
        }

        public Envelope<PayerDetails> GetDealerCode(PayerDetails InputParam)
        {
            Envelope<PayerDetails> PostData = new Envelope<PayerDetails>();
            PostData.Data = InputParam;
            APIClient<Envelope<PayerDetails>> client = new APIClient<Envelope<PayerDetails>>();
            try
            {
                string apiUrl = "api/MasterMaintenance/GetDealerCode";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<PayerDetails>>(apiOutput.Result);
                return output;
            }
            catch
            {
                return null;
            }

        }



        public Envelope<bool> InsertDealer(DealerMaster InputParam)
        {
            Envelope<DealerMaster> PostData = new Envelope<DealerMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerMaster>> client;
            PostData.Data = InputParam;

            try
            {
                client = new APIClient<Envelope<DealerMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateDealer";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                
            }
            catch (Exception ex)
            {
               
            }
            return output;
        }

        public Envelope<bool> UpdateDealer(DealerMaster InputParam)
        {
            Envelope<DealerMaster> PostData = new Envelope<DealerMaster>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerMaster>> client;
            PostData.Data = InputParam;

            try
            {
                client = new APIClient<Envelope<DealerMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateDealer";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                
            }
            catch (Exception ex)
            {
               
            }
            return output;

        }

        public Envelope<bool> DeleteDealer(DealerMaster InputParam)
        {
            Envelope<DealerMaster> PostData = new Envelope<DealerMaster>();
            PostData.Data = InputParam;
            APIClient<Envelope<DealerMaster>> client = new APIClient<Envelope<DealerMaster>>();
            var output =new Envelope<bool>();
            try
            {
                string apiUrl = "api/MasterMaintenance/DeleteDealer";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
             
            }
            return output;

        }

        public Envelope<bool> CheckPSIOutlet(DealerMaster InputParam)
        {
            Envelope<DealerMaster> PostData = new Envelope<DealerMaster>();
            PostData.Data = InputParam;
            APIClient<Envelope<DealerMaster>> client = new APIClient<Envelope<DealerMaster>>();
            Envelope<bool> output = new Envelope<bool>();
            try
            {
                string apiUrl = "api/MasterMaintenance/CheckPSIOutlet";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {

            }
            return output;
        }
    }
    }

