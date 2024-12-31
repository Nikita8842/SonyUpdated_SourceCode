using AmboDataServices.Interface;
using AmboLibrary.UserValidation;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Implimentation
{
    public class UserValidationService: IUserValidationService
    {
        private readonly IUserValidationDataService _UserValidation;
        public UserValidationService(IUserValidationDataService IUserValidationDataService)
        {
            _UserValidation = IUserValidationDataService;
        }

        public Envelope<UserDetailsModel> ValidateLogin(UserValidationModel List)
        {
            string Message = "";
            var getlist = _UserValidation.ValidateLogin(List, out Message);

            return (getlist != null) ?
                new Envelope<UserDetailsModel> { Data = getlist, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<UserDetailsModel> { Data = getlist, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }
    }
}
