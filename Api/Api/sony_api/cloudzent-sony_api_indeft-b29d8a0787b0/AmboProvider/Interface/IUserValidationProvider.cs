using AmboLibrary.UserValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboProvider.Interface
{
    public interface IUserValidationProvider
    {
        UserDetailsModel ValidateLogin(UserValidationModel UserDetails, out string Message);
    }
}
