using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AmboServices.Interface;
using System.Web.Http.Cors;
using AmboUtilities;
using AmboLibrary.Modules;
using AmboUtilities.Helper;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class ModuleManagementController : ApiController
    {
        private readonly IModuleManagementService _IModuleManagementService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModuleManagementController(IModuleManagementService IModuleManagementService)
        {
            _IModuleManagementService = IModuleManagementService;
        }

        /// <summary>
        /// get all the master code with same text code
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDealerMasterCodeList()
        {
            var output = _IModuleManagementService.GetDealerMasterCodeList();
            return Ok(output);
        }

        /// <summary>
        /// get all the dealer details mapped with same master code
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDealerByMasterCode(Envelope<DealerMasterCodeUpdate> param)
        {
            var output = _IModuleManagementService.GetDealerByMasterCode(param.Data.MasterCode);
            return Ok(output);
        }

        /// <summary>
        /// update Master Code corresponding to Dealers
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateDealerMasterCode(Envelope<DealerMasterCodeUpdate> param)
        {
            var output = _IModuleManagementService.UpdateDealerMasterCode(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// validate master code in Dealer Master table
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ValidateDealerMasterCode(Envelope<DealerMasterCodeUpdate> param)
        {
            var output = _IModuleManagementService.ValidateDealerMasterCode(param.Data.MasterCode);
            return Ok(output);
        }

        /// <summary>
        /// Assign Asset to SFA
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult IssueAssetToSFA(Envelope<AssetAssignmentToSFA> param)
        {
            var output = _IModuleManagementService.IssueAssestToSFA(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// to get Asset issued to sfa by userid
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAssetIssuedToSFA(Envelope<AssetIssuedToSFAGet> param)
        {
            var output = _IModuleManagementService.GetAssetIssuedToSFA(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// to get list of Assets issued to rdi
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAssetsDropDown(Envelope<AssetIssuedToSFAGet> param)
        {
            var output = _IModuleManagementService.GetAssetsDropDown(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update Issued Qty in the Asset assignment to RDI.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateAssetAssignmentToRDI(Envelope<AssetAssignmentToRDIUpdate> param)
        {
            var output = _IModuleManagementService.UpdateAssetAssignmentToRDI(param.Data);
            return Ok(output);
        }



        /// <summary>
        /// to get Asset assignment to RDI row detail by Id.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAssetAssignmentToRDIById(Envelope<AssetAssignmentToRDI> param)
        {
            var output = _IModuleManagementService.GetAssetAssignmentToRDIById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Upload Asset assignment to RDI records from Excel sheet
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UploadAssetAssignmentToRDI(Envelope<AssetAssignmentToRDIUpload> param)
        {
            var output = _IModuleManagementService.UploadAssetAssignmentToRDI(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Upload Asset collection from SFA records from Excel sheet
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UploadAssetCollectionFromSFA(Envelope<AssetCollectionFromSFA> param)
        {
            var output = _IModuleManagementService.UploadAssetCollectionFromSFA(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetAssetCollectionFormatFromSFA(Envelope<AssetCollectionFromSFAGet> param)
        {
            var output = _IModuleManagementService.GetAssetCollectionFormatFromSFA(param.Data);
            return Ok(output);
        }
    }
}
