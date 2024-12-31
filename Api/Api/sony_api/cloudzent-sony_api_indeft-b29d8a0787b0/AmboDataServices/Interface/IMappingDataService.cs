using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Mappings;
using AmboLibrary.UserManagement;

namespace AmboDataServices.Interface
{
    public interface IMappingDataService
    {
        #region Dealer SFA Mapping
        bool CreateDealerSFAMapping(DealerSFAMapping mappingData, out string Message);

        bool UpdateDealerSFAMapping(DealerSFAMapping mappingData, out string Message);
        
        bool DeleteDealerSFAMapping(DealerSFAMapping mappingData, out string Message);
        #endregion Dealer SFA Mapping

        #region Dealer Classification Mapping
        DealerClassificationMappingTable GetDealerClassificationMappingTable(DealerClassificationMappingSearch mappingData, out string Message);

        bool CreateDealerClassificationMapping(DealerClassificationMappingTable mappingData, out string Message);

        bool UpdateDealerClassificationMapping(DealerClassificationMapping mappingData, out string Message);

        bool DeleteDealerClassificationMapping(DealerClassificationMapping mappingData, out string Message);

        bool ManageDealerClassificationMapping(DealerProductMapping mappingData, out string Message);
        #endregion Dealer Classification Mapping

        #region Product Category SFA Mapping
        bool CreateProductCategorySFAMapping(ProductCategorySFAMapping mappingData, out string Message);

        bool DeleteProductCategorySFAMapping(ProductCategorySFAMapping mappingData, out string Message);
        #endregion Product Category SFA Mapping

        #region Employee Structure Mapping
        bool CreateEmployeeStructureMapping(EmployeeStructureMapping mappingData, out string Message);

        bool DeleteEmployeeStructureMapping(EmployeeStructureMapping mappingData, out string Message);

        IEnumerable<SFAFormData> GetSFAForStructureMapping(SFAForStructureMappingInput objSearchData);
        #endregion Employee Structure Mapping

        #region User Branch Channel Product Category Mapping
        bool ManageUserBranchChannelPCMapping(UserBranchChannelPCMapping mappingData, out string Message);

        UserBranchChannelPCMapping GetUserBranchChannelPCMapping(Int64 MappingUserId);
        #endregion User Branch Channel Product Category Mapping

        #region Incentive Target Category Mapping
        bool ManageIncentiveTargetCategoryMapping(IncentiveTargetCategoryMapping mappingData, out string Message);

        IncentiveTargetCategoryMapping GetIncentiveTargetCategoryMapping(Int64 IncentiveCatId);
        #endregion Incentive Target Category Mapping

        #region Assign Incentive Category to SFA
        IncentiveCategorySFAMapping GetSFAForIncentiveCategorySFAMapping(Int64 RDIId);

        IncentiveCategorySFAMapping ManageIncentiveCategorySFAMapping(IncentiveCategorySFAMapping mappingData, out string Message);
        #endregion Assign Incentive Category to SFA

        #region Product Definition Under Target Category
        bool ManageProductDefinitionUnderTargetCategory(ProdDefUnderTargetCatGridOutput mappingData, out string Message);

        ProdDefUnderTargetCatGridOutput GetProductsByCategorySubCategory(ProdDefUnderTargetCat mappingData, out string Message);

        ProdDefUnderTargetCatGridOutput GetProductsByCategorySubCategoryforallMat(ProdDefUnderTargetCatforAllMat mappingData, out string Message);
        #endregion Product Definition Under Target Category

        #region Assign Target To SFA Data Service Methods
        DataTable UploadTargetToSFAByMonth(AssignTargetToSFAUpload targetData);

        AssignTargetToSFAGet GetBranchByUserId(Int64 UserId);
        #endregion Assign Target To SFA Data Service Methods

        #region Navigation Overloads
        bool CreateIncentiveCategorySFAMapping(NavigationIncentiveCategorySFAMapping mappingData, out string Message);
        #endregion Navigation Overloads

        #region Sales PIC Outlet Mapping
        bool ManageSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData, out string Message);

        ManageSalesPICOutletMappingForm GetSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData, out string Message);

        bool DeleteSalesPICOutletMapping(DeleteSalesPICOutletMappingForm objFormData, out string Message);
        #endregion Sales PIC Outlet Mapping

        #region Assign Festival Target To SFA Data Service Methods
        List<AssignFestivalTargetGrid> GetFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput);

        List<AssignFestivalTargetGrid> ShowFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput);

        DataTable UploadFestivalTargetToSFAByScheme(AssignFestivalTargetUpload objInput);
        #endregion Assign Festival Target To SFA Data Service Methods

        #region QSR nikita 9/9/2024
        DataTable UploadReportQSRQuantity(QSRQuantityUpload targetData);
        #endregion
    }
}
