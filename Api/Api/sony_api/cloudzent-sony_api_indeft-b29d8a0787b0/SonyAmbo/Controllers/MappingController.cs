using AmboLibrary.Mappings;
using AmboLibrary.UserManagement;
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
    public class MappingController : ApiController
    {
        private readonly IMappingService _IMappingService;

        /// <summary>
        /// Constructor
        /// </summary>
        public MappingController(IMappingService IMappingService)
        {
            _IMappingService = IMappingService;
        }

        #region Dealer SFA Mapping
        /// <summary>
        /// Create a new dealer-SFA mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateDealerSFAMapping(Envelope<DealerSFAMapping> mappingData)
        {
            var output = _IMappingService.CreateDealerSFAMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing dealer-SFA mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateDealerSFAMapping(Envelope<DealerSFAMapping> mappingData)
        {
            var output = _IMappingService.UpdateDealerSFAMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a dealer-SFA mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteDealerSFAMapping(Envelope<DealerSFAMapping> mappingData)
        {
            var output = _IMappingService.DeleteDealerSFAMapping(mappingData.Data);
            return Ok(output);
        }
        #endregion Dealer SFA Mapping

        #region Dealer Classification Mapping
        /// <summary>
        /// Get dealer-Classification mapping table. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDealerClassificationMappingTable(Envelope<DealerClassificationMappingSearch> mappingData)
        {
            var output = _IMappingService.GetDealerClassificationMappingTable(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create a new dealer-Classification mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateDealerClassificationMapping(Envelope<DealerClassificationMappingTable> mappingData)
        {
            var output = _IMappingService.CreateDealerClassificationMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing dealer-Classification mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateDealerClassificationMapping(Envelope<DealerClassificationMapping> mappingData)
        {
            var output = _IMappingService.UpdateDealerClassificationMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a dealer-Classification mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteDealerClassificationMapping(Envelope<DealerClassificationMapping> mappingData)
        {
            var output = _IMappingService.DeleteDealerClassificationMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create a dealer-Classification mapping for new dealer. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageDealerClassificationMapping(Envelope<DealerProductMapping> mappingData)
        {
            var output = _IMappingService.ManageDealerClassificationMapping(mappingData.Data);
            return Ok(output);
        }
        #endregion Dealer Classification Mapping

        #region Product Category SFA Mapping
        /// <summary>
        /// Create a new Product category-SFA mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateProductCategorySFAMapping(Envelope<ProductCategorySFAMapping> mappingData)
        {
            var output = _IMappingService.CreateProductCategorySFAMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a Product category-SFA mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteProductCategorySFAMapping(Envelope<ProductCategorySFAMapping> mappingData)
        {
            var output = _IMappingService.DeleteProductCategorySFAMapping(mappingData.Data);
            return Ok(output);
        }
        #endregion Product Category SFA Mapping

        #region Employee Structure Mapping
        /// <summary>
        /// Create an Employee Structure Mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateEmployeeStructureMapping(Envelope<EmployeeStructureMapping> mappingData)
        {
            var output = _IMappingService.CreateEmployeeStructureMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete an Employee Structure Mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteEmployeeStructureMapping(Envelope<EmployeeStructureMapping> mappingData)
        {
            var output = _IMappingService.DeleteEmployeeStructureMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get SFA for employee structure mapping
        /// on basis of Branch, Division and SFA Type
        /// </summary>
        /// <param name="objSearchData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFAForStructureMapping(Envelope<SFAForStructureMappingInput> objSearchData)
        {
            var output = _IMappingService.GetSFAForStructureMapping(objSearchData.Data);
            return Ok(output);
        }
        #endregion Employee Structure Mapping

        #region User Branch Channel Product Category Mapping
        /// <summary>
        /// Create an User Branch Channel Product Category Mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageUserBranchChannelPCMapping(Envelope<UserBranchChannelPCMapping> mappingData)
        {
            var output = _IMappingService.ManageUserBranchChannelPCMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get User Branch Channel Product Category Mapping. 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetUserBranchChannelPCMapping(Envelope<Int64> param)
        {
            var output = _IMappingService.GetUserBranchChannelPCMapping(param.Data);
            return Ok(output);
        }
        #endregion User Branch Channel Product Category Mapping

        #region Incentive Target Category Mapping

        /// <summary>
        /// Manage an Incentive Target Category Mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageIncentiveTargetCategoryMapping(Envelope<IncentiveTargetCategoryMapping> mappingData)
        {
            var output = _IMappingService.ManageIncentiveTargetCategoryMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get User Branch Channel Product Category Mapping. 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetIncentiveTargetCategoryMapping(Envelope<Int64> param)
        {
            var output = _IMappingService.GetIncentiveTargetCategoryMapping(param.Data);
            return Ok(output);
        }
        #endregion Incentive Target Category Mapping

        #region Assign Incentive Category to SFA
        /// <summary>
        /// Get SFA by RDI For Incentive Category SFAMapping. 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageIncentiveCategorySFAMapping(Envelope<IncentiveCategorySFAMapping> param)
        {
            var output = _IMappingService.ManageIncentiveCategorySFAMapping(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get SFA by RDI For Incentive Category SFAMapping. 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFAForIncentiveCategorySFAMapping(Envelope<Int64> param)
        {
            var output = _IMappingService.GetSFAForIncentiveCategorySFAMapping(param.Data);
            return Ok(output);
        }
        #endregion Assign Incentive Category to SFA

        #region Product Definition Under Target Category
        /// <summary>
        /// Manage an Incentive Target Category Mapping. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageProductDefinitionUnderTargetCategory(Envelope<ProdDefUnderTargetCatGridOutput> mappingData)
        {
            var output = _IMappingService.ManageProductDefinitionUnderTargetCategory(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Products by category/subcategory for defining under a targetcategory. 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductsByCategorySubCategory(Envelope<ProdDefUnderTargetCat> param)
        {
            var output = _IMappingService.GetProductsByCategorySubCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Products by category/subcategory for defining under a targetcategory. 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductsByCategorySubCategoryforallMat(Envelope<ProdDefUnderTargetCatforAllMat> param)
        {
            var output = _IMappingService.GetProductsByCategorySubCategoryforallMat(param.Data);
            return Ok(output);
        }
        #endregion Product Definition Under Target Category

        /// <summary>
        /// Upload target to SFA from Excel and get return data of successful/unsuccessful
        /// </summary>
        /// <param name="targetData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UploadTargetToSFAByMonth(Envelope<AssignTargetToSFAUpload> targetData)
        {
            var output = _IMappingService.UploadTargetToSFAByMonth(targetData.Data);
            return Ok(output);
        }

        /// <summary>
        /// to get Branch Details by User Id
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetBranchByUserId(Envelope<Int64> param)
        {
            var output = _IMappingService.GetBranchByUserId(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create a dealer-Classification mapping for new dealer. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateIncentiveCategorySFAMapping(Envelope<NavigationIncentiveCategorySFAMapping> mappingData)
        {
            var output = _IMappingService.CreateIncentiveCategorySFAMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create/update sales pic outlet mapping 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageSalesPICOutletMapping(Envelope<ManageSalesPICOutletMappingForm> objFormData)
        {
            var output = _IMappingService.ManageSalesPICOutletMapping(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// get sales pic outlet mapping 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSalesPICOutletMapping(Envelope<ManageSalesPICOutletMappingForm> objFormData)
        {
            var output = _IMappingService.GetSalesPICOutletMapping(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// delete sales pic outlet mapping 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteSalesPICOutletMapping(Envelope<DeleteSalesPICOutletMappingForm> objFormData)
        {
            var output = _IMappingService.DeleteSalesPICOutletMapping(objFormData.Data);
            return Ok(output);
        }

        #region Assign Festival Target to SFA
        /// <summary>
        /// Get Festival Target to SFA By Scheme 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetFestivalTargetToSFABySchemeId(Envelope<AssignFestivalTargetGet> param)
        {
            var output = _IMappingService.GetFestivalTargetToSFABySchemeId(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Festival Target to SFA By Scheme 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ShowFestivalTargetToSFABySchemeId(Envelope<AssignFestivalTargetGet> param)
        {
            var output = _IMappingService.ShowFestivalTargetToSFABySchemeId(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Upload festival target to SFA from Excel and get return data of successful/unsuccessful
        /// </summary>
        /// <param name="targetData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UploadFestivalTargetToSFAByScheme(Envelope<AssignFestivalTargetUpload> param)
        {
            var output = _IMappingService.UploadFestivalTargetToSFAByScheme(param.Data);
            return Ok(output);
        }
        #endregion Assign Festival Target to SFA

        #region Nikita kawade 9/9/2024
        [HttpPost]
        public IHttpActionResult UploadReportQSRQuantity(Envelope<QSRQuantityUpload> targetData)
        {
            var output = _IMappingService.UploadQSRQuantityReport(targetData.Data);
            return Ok(output);
        }
        #endregion
    }
}