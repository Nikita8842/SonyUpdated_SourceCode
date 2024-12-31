using AmboLibrary.IncentiveManagement;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class DeviationController : ApiController
    {
        private readonly IDeviationService _IDeviationService;

        public DeviationController(IDeviationService IDeviationService)
        {
            _IDeviationService = IDeviationService;
        }

        [HttpPost]
        public IHttpActionResult GetDeviationUploadByRDIExcel(Envelope<DeviationUploadByRDISearch> objSearchData)
        {
            var output = _IDeviationService.GetDeviationUploadByRDIExcel(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDeviationUploadByRDIReasons(Envelope<DeviationUploadByRDISearch> objSearchData)
        {
            var output = _IDeviationService.GetDeviationUploadByRDIReasons(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult ManageDeviationUploadByRDI(Envelope<DeviationUploadByRDIExcel> objSearchData)
        {
            var output = _IDeviationService.ManageDeviationUploadByRDI(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult ManageDeviationUploadByRDI_Festival(Envelope<DeviationUploadByRDIExcel> objSearchData)
        {
            var output = _IDeviationService.ManageDeviationUploadByRDI_Festival(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult ManageDeviationUploadByRDI_SaveReasons(Envelope<DeviationUploadByRDISaveReasons> objSearchData)
        {
            var output = _IDeviationService.ManageDeviationUploadByRDI_SaveReasons(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult ManageDeviationUploadByRDI_ApproveReasons(Envelope<DeviationUploadByRDISaveReasons> objSearchData)
        {
            var output = _IDeviationService.ManageDeviationUploadByRDI_ApproveReasons(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDeviationApprovalExcel(Envelope<DeviationApprovalSearch> objSearchData)
        {
            var output = _IDeviationService.GetDeviationApprovalExcel(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult UploadDeviationApprovalExcel(Envelope<DeviationApprovalExcel> objSearchData)
        {
            var output = _IDeviationService.UploadDeviationApprovalExcel(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDeviationFinalUploadExcel(Envelope<DeviationFinalUploadSearch> objSearchData)
        {
            var output = _IDeviationService.GetDeviationFinalUploadExcel(objSearchData.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult UploadDeviationFinalUploadExcel(Envelope<DeviationFinalUploadExcel> objSearchData)
        {
            var output = _IDeviationService.UploadDeviationFinalUploadExcel(objSearchData.Data);
            return Ok(output);
        }
    }
}