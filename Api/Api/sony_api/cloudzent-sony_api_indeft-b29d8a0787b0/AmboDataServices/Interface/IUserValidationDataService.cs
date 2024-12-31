using AmboLibrary.UserValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Interface
{
    public interface IUserValidationDataService
    {
        UserDetailsModel ValidateLogin(UserValidationModel UserDetails, out string Message);
    }
}
