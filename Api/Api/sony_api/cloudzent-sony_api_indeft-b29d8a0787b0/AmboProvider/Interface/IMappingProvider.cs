using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Mappings;
using AmboLibrary.UserManagement;

namespace AmboProvider.Interface
{
    public interface IMappingProvider
    {
        #region Dealer SFA Mapping
        int CreateDealerSFAMapping(DealerSFAMapping mappingData, out string Message);

        int UpdateDealerSFAMapping(DealerSFAMapping mappingData, out string Message);

        int DeleteDealerSFAMapping(DealerSFAMapping mappingData, out string Message);
        #endregion Dealer SFA Mapping

        #region Dealer Classification Mapping
        DealerClassificationMappingTable GetDealerClassificationMappingTable(DealerClassificationMappingSearch mappingData, out string Message);
        
        int CreateDealerClassificationMapping(DealerClassificationMappingTable mappingData, out string Message);

        int UpdateDealerClassificationMapping(DealerClassificationMapping mappingData, out string Message);

        int DeleteDealerClassificationMapping(DealerClassificationMapping mappingData, out string Message);

        int ManageDealerClassificationMapping(DealerProductMapping mappingData, out string Message);
        #endregion Dealer Classification Mapping

        #region Product Category SFA Mapping
        int CreateProductCategorySFAMapping(ProductCategorySFAMapping mappingData, out string Message);

        int DeleteProductCategorySFAMapping(ProductCategorySFAMapping mappingData, out string Message);
        #endregion Product Category SFA Mapping

        #region Employee Structure Mapping
        int CreateEmployeeStructureMapping(EmployeeStructureMapping mappingData, out string Message);

        int DeleteEmployeeStructureMapping(EmployeeStructureMapping mappingData, out string Message);

        IEnumerable<SFAFormData> GetSFAForStructureMapping(SFAForStructureMappingInput objSearchData);
        #endregion Employee Structure Mapping

        #region User Branch Channel Product Category Mapping
        int ManageUserBranchChannelPCMapping(UserBranchChannelPCMapping mappingData, out string Message);

        UserBranchChannelPCMapping GetUserBranchChannelPCMapping(Int64 MappingUserId);
        #endregion User Branch Channel Product Category Mapping

        #region Incentive Target Category Mapping
        int ManageIncentiveTargetCategoryMapping(IncentiveTargetCategoryMapping mappingData, out string Message);

        IncentiveTargetCategoryMapping GetIncentiveTargetCategoryMapping(Int64 IncentiveCatId);
        #endregion Incentive Target Category Mapping

        #region Assign Incentive Category to SFA
        IncentiveCategorySFAMapping GetSFAForIncentiveCategorySFAMapping(Int64 RDIId);

        IncentiveCategorySFAMapping ManageIncentiveCategorySFAMapping(IncentiveCategorySFAMapping mappingData, out string Message);
        #endregion Assign Incentive Category to SFA

        #region Product Definition Under Target Category
        ProdDefUnderTargetCatGridOutput GetProductsByCategorySubCategory(ProdDefUnderTargetCat mappingData, out string Message);

        bool ManageProductDefinitionUnderTargetCategory(ProdDefUnderTargetCatGridOutput mappingData, out string Message);

        ProdDefUnderTargetCatGridOutput GetProductsByCategorySubCategoryforallMat(ProdDefUnderTargetCatforAllMat mappingData, out string Message);
        #endregion Product Definition Under Target Category

        #region Assign Target To SFA API
        DataTable UploadTargetToSFAByMonth(AssignTargetToSFAUpload targetData);

        AssignTargetToSFAGet GetBranchByUserId(Int64 UserId);
        #endregion Assign Target To SFA API

        #region Navigation Overloads
        int CreateIncentiveCategorySFAMapping(NavigationIncentiveCategorySFAMapping mappingData, out string Message);
        #endregion Navigation Overloads

        #region Sales PIC Outlet Mapping
        bool ManageSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData, out string Message);

        ManageSalesPICOutletMappingForm GetSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData, out string Message);

        bool DeleteSalesPICOutletMapping(DeleteSalesPICOutletMappingForm objFormData, out string Message);
        #endregion Sales PIC Outlet Mapping

        #region Assign Festival Target To SFA API
        DataTable UploadFestivalTargetToSFAByScheme(AssignFestivalTargetUpload objInput);

        List<AssignFestivalTargetGrid> GetFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput);

        List<AssignFestivalTargetGrid> ShowFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput);
        #endregion Assign Festival Target To SFA API

        #region Nikita 9/9/2024
        DataTable UploadReportQSRQuantity(QSRQuantityUpload targetData);
        #endregion
    }
}
