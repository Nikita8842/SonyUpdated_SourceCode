using AmboLibrary.IncentiveManagement;
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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class IncentiveCalculationDateSettingController : ApiController
    {
        private readonly IIncentiveCalculationDateSettingService _IIncentiveCalculationDateSettingService;

        public IncentiveCalculationDateSettingController(IIncentiveCalculationDateSettingService IIncentiveCalculationDateSettingService)
        {
            _IIncentiveCalculationDateSettingService = IIncentiveCalculationDateSettingService;
        }
        /// <summary>
        /// To fetch Incentive calculation date setting Report.
        /// </summary>
        /// <returns>Incentive calculation date setting Report.</returns>
        [HttpPost]
        public IHttpActionResult GetIncentiveCalculationDateSettingReport(Envelope<IncentiveCalculationDateSettingParam> param)
        {
            var output = _IIncentiveCalculationDateSettingService.GetIncentiveCalculationDateSettingReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an incentive calculation date in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateIncentiveCalculationDateSetting(Envelope<IncentiveCalculationDateSetting> param)
        {
            var output = _IIncentiveCalculationDateSettingService.UpdateIncentiveCalculationDateSetting(param.Data);
            return Ok(output);
        }
    }
}
