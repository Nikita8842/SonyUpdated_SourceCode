using AmboLibrary.GlobalAccessible;
using AmboServices.Interface;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ErrorLogController : ApiController
    {
        private readonly IErrorLogService _IErrorLogService;

        public ErrorLogController(IErrorLogService IErrorLogService)
        {
            _IErrorLogService = IErrorLogService;
        }

        [HttpPost]
        public IHttpActionResult CreateErrorLogWeb(Envelope<ErrorDetailsInput> InputParam)
        {
            _IErrorLogService.CreateErrorLogWeb(InputParam.Data);
            return Ok();
        }
    }
}
