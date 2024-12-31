using AMBOModels.UserValidation;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.UserManagement;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class UserValidation: IUserValidation
    {        
        /// <summary>
        /// To validate user login for both Mobile and Web.
        /// </summary>
        /// <param name="UserDetails">User Cridentials</param>
        /// <returns>User Id, Name, Role Id, Role Name</returns>
        public UserDetailsModel ValidateLogin(UserValidationModel UserDetails)
        {
            UserDetailsModel UserDetailsModel = new UserDetailsModel();
            Envelope<UserDetailsModel> output;
            APIClient<Envelope<UserValidationModel>> client;
            Envelope<UserValidationModel> Input = new Envelope<UserValidationModel>();
            Input.Data = UserDetails;
            try
            {
                client = new APIClient<Envelope<UserValidationModel>>();
                string apiUrl = "api/UserValidation/ValidateLogin";
                var apiOutput = client.Post(apiUrl, Input);
                output = JsonConvert.DeserializeObject<Envelope<UserDetailsModel>>(apiOutput.Result);
                UserDetailsModel = output.Data;
            }
            catch (Exception ex)
            {

            }
            return UserDetailsModel;
        }

        public Envelope<bool> UpdateUserPassword(UserUpdatePasswordModel UserDetails)
        {
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UserUpdatePasswordModel>> client;
            Envelope<UserUpdatePasswordModel> Input = new Envelope<UserUpdatePasswordModel>();
            Input.Data = UserDetails;
            try
            {
                client = new APIClient<Envelope<UserUpdatePasswordModel>>();
                string apiUrl = "api/SFAMobileManagement/UpdateUserPassword";
                var apiOutput = client.Post(apiUrl, Input);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {

            }
            return output;
        }

    }
}
