using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class TestController : ApiController
    {
        private readonly IValidateService _IValidateService;
        /// <summary>
        /// Constructor
        /// </summary>
        public TestController(IValidateService IValidateService)
        {
            _IValidateService = IValidateService;
        }
        
        /// <summary>
        /// testing Only
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Compression]
        public IHttpActionResult TestMethod()
        {
            var IsValid = _IValidateService.Validate();
            return Ok(IsValid);
        }

        /// <summary>
        /// Test Method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ValidateMethod()
        {
            var IsValid = _IValidateService.Validate();
            return Ok(IsValid);
        }

    }

}
