using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Mappings;
using AmboUtilities;
using AmboDataServices.Interface;
using AmboUtilities.Helper;
using AmboLibrary.UserManagement;
using System.Data;
using AmboServices.Interface;

namespace AmboServices.Implimentation
{
    public class MappingService : IMappingService
    {
        private readonly IMappingDataService _IMappingDataService;
        public MappingService(IMappingDataService IMappingDataService)
        {
            _IMappingDataService = IMappingDataService;
        }

        #region Dealer SFA Mapping
        public Envelope<bool> CreateDealerSFAMapping(DealerSFAMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.CreateDealerSFAMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateDealerSFAMapping(DealerSFAMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.UpdateDealerSFAMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteDealerSFAMapping(DealerSFAMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.DeleteDealerSFAMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Dealer SFA Mapping

        #region Dealer Classification Mapping
        public Envelope<DealerClassificationMappingTable> GetDealerClassificationMappingTable(DealerClassificationMappingSearch mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetDealerClassificationMappingTable(mappingData, out Message);
            return (output.MappingTable.Count > 0) ?
                new Envelope<DealerClassificationMappingTable> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<DealerClassificationMappingTable> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }

        public Envelope<bool> CreateDealerClassificationMapping(DealerClassificationMappingTable mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.CreateDealerClassificationMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateDealerClassificationMapping(DealerClassificationMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.UpdateDealerClassificationMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteDealerClassificationMapping(DealerClassificationMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.DeleteDealerClassificationMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> ManageDealerClassificationMapping(DealerProductMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.ManageDealerClassificationMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Dealer Classification Mapping

        #region Product Category SFA Mapping
        public Envelope<bool> CreateProductCategorySFAMapping(ProductCategorySFAMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.CreateProductCategorySFAMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteProductCategorySFAMapping(ProductCategorySFAMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.DeleteProductCategorySFAMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Product Category SFA Mapping

        #region Employee Structure Mapping
        public Envelope<bool> CreateEmployeeStructureMapping(EmployeeStructureMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.CreateEmployeeStructureMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteEmployeeStructureMapping(EmployeeStructureMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.DeleteEmployeeStructureMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<SFAFormData>> GetSFAForStructureMapping(SFAForStructureMappingInput objSearchData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetSFAForStructureMapping(objSearchData);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No data found." } :
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA for structure mapping fetched successfully." };
        }
        #endregion Employee Structure Mapping

        #region User Branch Channel Product Category Mapping
        public Envelope<bool> ManageUserBranchChannelPCMapping(UserBranchChannelPCMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.ManageUserBranchChannelPCMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<UserBranchChannelPCMapping> GetUserBranchChannelPCMapping(Int64 MappingUserId)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetUserBranchChannelPCMapping(MappingUserId);
            return (output.BranchIds == null && output.ChannelIds == null && output.ProdCatIds == null && output.DivisionIds == null) ?
                 new Envelope<UserBranchChannelPCMapping> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                 new Envelope<UserBranchChannelPCMapping> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }
        #endregion User Branch Channel Product Category Mapping

        #region Incentive Target Category Mapping
        public Envelope<bool> ManageIncentiveTargetCategoryMapping(IncentiveTargetCategoryMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.ManageIncentiveTargetCategoryMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IncentiveTargetCategoryMapping> GetIncentiveTargetCategoryMapping(Int64 IncentiveCatId)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetIncentiveTargetCategoryMapping(IncentiveCatId);
            return (output.TargetCategoryMappings == null && output.IncentiveCategoryId == 0) ?
                 new Envelope<IncentiveTargetCategoryMapping> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                 new Envelope<IncentiveTargetCategoryMapping> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }
        #endregion Incentive Target Category Mapping

        #region Assign Incentive Category to SFA
        public Envelope<IncentiveCategorySFAMapping> GetSFAForIncentiveCategorySFAMapping(Int64 RDIId)
        {
            var output = _IMappingDataService.GetSFAForIncentiveCategorySFAMapping(RDIId);
            return (output.MappingExcelData == null) ?
                 new Envelope<IncentiveCategorySFAMapping> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No SFA Found for given RDI/Admin ID." } :
                 new Envelope<IncentiveCategorySFAMapping> { Data = output, MessageCode = (int)Acceptable.Created, Message = "SFA List for mapping fetched successfully." };
        }

        public Envelope<IncentiveCategorySFAMapping> ManageIncentiveCategorySFAMapping(IncentiveCategorySFAMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.ManageIncentiveCategorySFAMapping(mappingData, out Message);
            return (output == null) ?
                new Envelope<IncentiveCategorySFAMapping> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<IncentiveCategorySFAMapping> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Assign Incentive Category to SFA

        #region Product Definition Under Target Category
        public Envelope<bool> ManageProductDefinitionUnderTargetCategory(ProdDefUnderTargetCatGridOutput mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.ManageProductDefinitionUnderTargetCategory(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ProdDefUnderTargetCatGridOutput> GetProductsByCategorySubCategory(ProdDefUnderTargetCat mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetProductsByCategorySubCategory(mappingData, out Message);
            return (output == null) ?
                new Envelope<ProdDefUnderTargetCatGridOutput> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<ProdDefUnderTargetCatGridOutput> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ProdDefUnderTargetCatGridOutput> GetProductsByCategorySubCategoryforallMat(ProdDefUnderTargetCatforAllMat mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetProductsByCategorySubCategoryforallMat(mappingData, out Message);
            return (output == null) ?
                new Envelope<ProdDefUnderTargetCatGridOutput> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<ProdDefUnderTargetCatGridOutput> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Product Definition Under Target Category

        #region Assign Target To SFA API
        public Envelope<DataTable> UploadTargetToSFAByMonth(AssignTargetToSFAUpload targetData)
        {
            var output = _IMappingDataService.UploadTargetToSFAByMonth(targetData);
            return (output != null && output.Rows.Count > 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Created, Message = "Full/Partial data updated/inserted successfully." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = "Error occured while updating data." };
        }

        public Envelope<AssignTargetToSFAGet> GetBranchByUserId(Int64 UserId)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetBranchByUserId(UserId);
            return (output == null) ?
                new Envelope<AssignTargetToSFAGet> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Data fetched successfully" } :
                new Envelope<AssignTargetToSFAGet> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No data found" };
        }
        #endregion Assign Target To SFA API

        #region Navigation Overloads
        public Envelope<bool> CreateIncentiveCategorySFAMapping(NavigationIncentiveCategorySFAMapping mappingData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.CreateIncentiveCategorySFAMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Navigation Overloads

        #region Sales PIC Outlet Mapping
        public Envelope<bool> ManageSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.ManageSalesPICOutletMapping(objFormData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ManageSalesPICOutletMappingForm> GetSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetSalesPICOutletMapping(objFormData, out Message);
            return (output.DealerIds != null && output.DealerIds.Count > 0) ?
                new Envelope<ManageSalesPICOutletMappingForm> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<ManageSalesPICOutletMappingForm> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }

        public Envelope<bool> DeleteSalesPICOutletMapping(DeleteSalesPICOutletMappingForm objFormData)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.DeleteSalesPICOutletMapping(objFormData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Sales PIC Outlet Mapping

        #region Assign Festival Target To SFA API
        public Envelope<List<AssignFestivalTargetGrid>> GetFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.GetFestivalTargetToSFABySchemeId(objInput);
            return (output != null && output.Count>0) ?
                new Envelope<List<AssignFestivalTargetGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Data fetched successfully" } :
                new Envelope<List<AssignFestivalTargetGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No data found" };
        }

        public Envelope<List<AssignFestivalTargetGrid>> ShowFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput)
        {
            var Message = string.Empty;
            var output = _IMappingDataService.ShowFestivalTargetToSFABySchemeId(objInput);
            return (output != null && output.Count > 0) ?
                new Envelope<List<AssignFestivalTargetGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Data fetched successfully" } :
                new Envelope<List<AssignFestivalTargetGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No data found" };
        }

        public Envelope<DataTable> UploadFestivalTargetToSFAByScheme(AssignFestivalTargetUpload objInput)
        {
            var output = _IMappingDataService.UploadFestivalTargetToSFAByScheme(objInput);
            return (output != null && output.Rows.Count > 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Created, Message = "Partial data updated/inserted successfully." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = "Error occured while updating data." };
        }
        #endregion Assign Festival Target To SFA API


        public Envelope<DataTable> UploadQSRQuantityReport(QSRQuantityUpload targetData)
        {
            var output = _IMappingDataService.UploadReportQSRQuantity(targetData);
            return (output != null && output.Rows.Count > 0) ?
            new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Created, Message = "Full/Partial data updated/inserted successfully." } :
            new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = "Error occured while updating data." };
        }
    }
}
