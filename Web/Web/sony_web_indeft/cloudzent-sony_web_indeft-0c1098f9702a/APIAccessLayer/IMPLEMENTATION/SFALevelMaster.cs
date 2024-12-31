using AMBOModels.Global;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;
using System;
using AMBOModels.MasterMaintenance;
using System.Collections.Generic;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class SFALevelMaster : ISFALevelMaster
    {
        #region SFA Level Master
        public ErrorModel CreateSFALevel(SFALevelMasterData Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<SFALevelMasterData>> client;
            Envelope<SFALevelMasterData> Input = new Envelope<SFALevelMasterData>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<SFALevelMasterData>>();
                string apiUrl = "api/MasterMaintenance/CreateSFALevel";//checked
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

        public ErrorModel DeleteSFALevel(SFALevelInput Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<SFALevelInput>> client;
            Envelope<SFALevelInput> Input = new Envelope<SFALevelInput>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<SFALevelInput>>();
                string apiUrl = "api/MasterMaintenance/DeleteSFALevel";
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

        public SFALevel GetSFALevelById(SFALevelInput Data)
        {
            SFALevel SFALevel = new SFALevel();
            Envelope<SFALevel> output;
            APIClient<Envelope<SFALevelInput>> client;
            Envelope<SFALevelInput> Input = new Envelope<SFALevelInput>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<SFALevelInput>>();
                string apiUrl = "api/MasterMaintenance/GetSFALevelById";//checked
                var apiOutput = client.Post(apiUrl, Input);
                output = JsonConvert.DeserializeObject<Envelope<SFALevel>>(apiOutput.Result);
                SFALevel = output.Data;
            }
            catch (Exception)
            {

            }
            return SFALevel;
        }

        public ErrorModel UpdateSFALevel(SFALevelMasterData Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<SFALevelMasterData>> client;
            Envelope<SFALevelMasterData> Input = new Envelope<SFALevelMasterData>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<SFALevelMasterData>>();
                string apiUrl = "api/MasterMaintenance/UpdateSFALevel";//checked
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

        #region SFA Sub Level
        public ErrorModel CreateSFASubLevel(SFASubLevelMaster Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<SFASubLevelMaster>> client;
            Envelope<SFASubLevelMaster> Input = new Envelope<SFASubLevelMaster>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<SFASubLevelMaster>>();
                string apiUrl = "api/MasterMaintenance/CreateSFASubLevel";//checked
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

        public ErrorModel DeleteSFASubLevel(SFASubLevelInput Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<SFASubLevelInput>> client;
            Envelope<SFASubLevelInput> Input = new Envelope<SFASubLevelInput>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<SFASubLevelInput>>();
                string apiUrl = "api/MasterMaintenance/DeleteSFASubLevel";//checked
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

        public SFASubLevel GetSFASubLevelMasterDataById(SFASubLevelInput Data)
        {
            SFASubLevel SFALevel = new SFASubLevel();
            Envelope<SFASubLevel> output;
            APIClient<Envelope<SFASubLevelInput>> client;
            Envelope<SFASubLevelInput> Input = new Envelope<SFASubLevelInput>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<SFASubLevelInput>>();
                string apiUrl = "api/Grid/GetSFASubLevelMasterDataById";
                var apiOutput = client.Post(apiUrl, Input);
                output = JsonConvert.DeserializeObject<Envelope<SFASubLevel>>(apiOutput.Result);
                SFALevel = output.Data;
            }
            catch (Exception)
            {

            }
            return SFALevel;
        }

        public ErrorModel UpdateSFASubLevel(SFASubLevelMaster Data)
        {
            ErrorModel ErrorDetails = new ErrorModel();
            Envelope<bool> output;
            APIClient<Envelope<SFASubLevelMaster>> client;
            Envelope<SFASubLevelMaster> Input = new Envelope<SFASubLevelMaster>();
            Input.Data = Data;
            try
            {
                client = new APIClient<Envelope<SFASubLevelMaster>>();
                string apiUrl = "api/MasterMaintenance/UpdateSFASubLevel";//checked
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
