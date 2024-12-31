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
    public class APIIncentiveCalculationDateSetting : IAPIIncentiveCalculationDateSetting
    {
        public Envelope<IncentiveCalculationDateSettingList> GetIncentiveCalculationDateSetting(IncentiveCalculationDateSettingParam objFormData)
        {
            Envelope<IncentiveCalculationDateSettingParam> PostData = new Envelope<IncentiveCalculationDateSettingParam>();
            PostData.Data = objFormData;
            APIClient<Envelope<IncentiveCalculationDateSettingParam>> client = new APIClient<Envelope<IncentiveCalculationDateSettingParam>>();
            try
            {
                string apiUrl = "api/IncentiveCalculationDateSetting/GetIncentiveCalculationDateSettingReport";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<IncentiveCalculationDateSettingList>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Envelope<bool> UpdateIncentiveCalculationDateSetting(IncentiveCalculationDateSetting objFormData)
        {
            Envelope<IncentiveCalculationDateSetting> postData = new Envelope<IncentiveCalculationDateSetting>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<IncentiveCalculationDateSetting>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<IncentiveCalculationDateSetting>>();
                string apiUrl = "api/IncentiveCalculationDateSetting/UpdateIncentiveCalculationDateSetting";//checked
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
