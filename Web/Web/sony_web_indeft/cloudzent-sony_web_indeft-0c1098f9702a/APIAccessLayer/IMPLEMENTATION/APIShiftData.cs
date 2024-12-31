using AMBOModels.Global;
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
    public class APIShiftData: IAPIShiftData
    {
        #region SFA Level Master
        public ErrorModel CreateShiftTiming(ShiftMaster Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<ShiftMaster>> client;
            Envelope<ShiftMaster> Input = new Envelope<ShiftMaster>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<ShiftMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateShiftTiming";//checked
                var apiOutput = client.Post(apiUrl, Input);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                ErrorDetails.ErrorCode = Convert.ToInt32(output.Data);
                ErrorDetails.ErrorMessage = output.Message;
            }
            catch (Exception)
            {
                ErrorDetails.ErrorCode = 99;
                ErrorDetails.ErrorMessage = "Something went wrong. Please try again.";
            }
            return ErrorDetails;
        }

        public ErrorModel DeleteShift(ShiftMaster Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<ShiftMaster>> client;
            Envelope<ShiftMaster> Input = new Envelope<ShiftMaster>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<ShiftMaster>>();
                string apiUrl = "api/MasterMaintenance/DeleteShift";
                var apiOutput = client.Post(apiUrl, Input);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                ErrorDetails.ErrorCode = Convert.ToInt32(output.Data);
                ErrorDetails.ErrorMessage = output.Message;
            }
            catch (Exception)
            {
                ErrorDetails.ErrorCode = 99;
                ErrorDetails.ErrorMessage = "Something went wrong. Please try again.";
            }
            return ErrorDetails;
        }

        public ShiftMaster GetShiftById(ShiftMaster Data)
        {
            ShiftMaster Shift = new ShiftMaster();
            Envelope<ShiftMaster> output;
            APIClient<Envelope<ShiftMaster>> client;
            Envelope<ShiftMaster> Input = new Envelope<ShiftMaster>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<ShiftMaster>>();
                string apiUrl = "api/MasterMaintenance/GetShiftById";//checked
                var apiOutput = client.Post(apiUrl, Input);
                output = JsonConvert.DeserializeObject<Envelope<ShiftMaster>>(apiOutput.Result);
                Shift = output.Data;
            }
            catch (Exception)
            {

            }
            return Shift;
        }

        public ErrorModel UpdateShiftTiming(ShiftMaster Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<ShiftMaster>> client;
            Envelope<ShiftMaster> Input = new Envelope<ShiftMaster>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<ShiftMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateShiftTiming";//checked
                var apiOutput = client.Post(apiUrl, Input);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                ErrorDetails.ErrorCode = Convert.ToInt32(output.Data);
                ErrorDetails.ErrorMessage = output.Message;
            }
            catch (Exception)
            {
                ErrorDetails.ErrorCode = 99;
                ErrorDetails.ErrorMessage = "Something went wrong. Please try again.";
            }
            return ErrorDetails;
        }
        #endregion
    }
}
