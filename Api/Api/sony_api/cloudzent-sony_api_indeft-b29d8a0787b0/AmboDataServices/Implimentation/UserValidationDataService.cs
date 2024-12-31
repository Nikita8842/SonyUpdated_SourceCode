using AmboDataServices.Interface;
using AmboLibrary.UserValidation;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Implimentation
{
    public class UserValidationDataService: IUserValidationDataService
    {
        private readonly IUserValidationProvider _IUserValidationProvider;
        public UserValidationDataService(IUserValidationProvider IUserValidationProvider)
        {
            _IUserValidationProvider = IUserValidationProvider;
        }

        public UserDetailsModel ValidateLogin(UserValidationModel UserDetails, out string Message)
        {
            return _IUserValidationProvider.ValidateLogin(UserDetails, out Message); 
        }
    }
}
