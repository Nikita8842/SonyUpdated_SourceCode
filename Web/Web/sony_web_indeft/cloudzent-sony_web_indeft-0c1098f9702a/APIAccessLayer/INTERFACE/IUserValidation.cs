using AMBOModels.UserManagement;
using AMBOModels.UserValidation;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IUserValidation
    {
        UserDetailsModel ValidateLogin(UserValidationModel UserDetails);
        Envelope<bool> UpdateUserPassword(UserUpdatePasswordModel UserDetails);
    }
}
