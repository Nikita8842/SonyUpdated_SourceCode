using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AmboServices.Interface;
using AmboUtilities.Helper;
using AmboLibrary.MPRIntegration;
using AmboUtilities;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class MPRIntegrationController : ApiController
    {
        private readonly IMPRIntegrationService _IMPRIntegrationService;

        public MPRIntegrationController(IMPRIntegrationService IMPRIntegrationService)
        {
            _IMPRIntegrationService = IMPRIntegrationService;
        }

        /// <summary>
        /// To get all dealer sales data.
        /// Bela Nalavade, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>branch</returns>
        [HttpPost]
        public IHttpActionResult GetSalesThroughQuantity(Envelope<SalesThroughQuantityInput> InputParam)
        {
            var output = _IMPRIntegrationService.GetSalesThroughQuantity(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get all sfa details.
        /// Bela Nalavade, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>branch</returns>
        [HttpPost]
        public IHttpActionResult GetSFADetailsBySFACode(Envelope<GetSFADetailsBySFACodeInput> InputParam)
        {
            var output = _IMPRIntegrationService.GetSFADetailsBySFACode(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get total sfa count branch and division wise.
        /// Bela Nalavade, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>branch</returns>
        [HttpPost]
        public IHttpActionResult GetBranchDivisionWise_SFACount(Envelope<GetBranchDivisionWise_SFACountInput> InputParam)
        {
            var output = _IMPRIntegrationService.GetBranchDivisionWise_SFACount(InputParam.Data);
            return Ok(output);
        }
    }
}
