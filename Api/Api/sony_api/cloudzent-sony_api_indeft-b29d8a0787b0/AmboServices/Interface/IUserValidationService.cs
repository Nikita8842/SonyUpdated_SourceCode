using AmboLibrary.UserValidation;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IUserValidationService
    {
        Envelope<UserDetailsModel> ValidateLogin(UserValidationModel List);
    }
}
