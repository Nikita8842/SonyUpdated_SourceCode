using AmboLibrary.UserValidation;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SonyAmbo.Controllers
{
    /// <summary>
    /// To perform User validation related operations like validate Login, change password etc.
    /// Dhruv Sharma, ValueFirst, Gurugram
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class UserValidationController : ApiController
    {
        private readonly IUserValidationService _IUserValidationService;

        public UserValidationController(IUserValidationService IUserValidationService)
        {
            _IUserValidationService = IUserValidationService;
        }

        /// <summary>
        /// To validate user login for both Mobile and Web.
        /// </summary>
        /// <param name="UserDetails">User Cridentials</param>
        /// <returns>User Id, Name, Role Id, Role Name</returns>
        public IHttpActionResult ValidateLogin(Envelope<UserValidationModel> UserDetails)
        {
            var getList = _IUserValidationService.ValidateLogin(UserDetails.Data);
            return Ok(getList);
        }
    }
}
