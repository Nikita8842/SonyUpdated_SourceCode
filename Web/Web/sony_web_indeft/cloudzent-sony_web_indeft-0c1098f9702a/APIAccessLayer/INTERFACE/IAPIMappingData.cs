﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.Mappings;
using AMBOModels.UserManagement;
using APIAccessLayer.Helper;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIMappingData
    {
        #region Dealer SFA Mapping
        Envelope<bool> CreateDealerSFAMapping(DealerSFAMapping mappingData);

        Envelope<bool> UpdateDealerSFAMapping(DealerSFAMapping mappingData);

        Envelope<bool> DeleteDealerSFAMapping(DealerSFAMapping mappingData);

        DealerSFAMapping GetSFADetails(string LoginId);
        #endregion Dealer SFA Mapping

        #region Dealer Classification Mapping
        Envelope<DealerClassificationMappingTable> GetDealerClassificationMappingTable(DealerClassificationMappingSearch mappingData);
        Envelope<bool> CreateDealerClassificationMapping(DealerClassificationMappingTable mappingData);

        Envelope<bool> UpdateDealerClassificationMapping(DealerClassificationMapping mappingData);

        Envelope<bool> DeleteDealerClassificationMapping(DealerClassificationMapping mappingData);

        Envelope<bool> ManageDealerClassificationMapping(DealerProductMapping mappingData);
        #endregion Dealer Classification Mapping

        #region Product Category SFA Mapping
        Envelope<bool> CreateProductCategorySFAMapping(ProductCategorySFAMapping mappingData);

        Envelope<bool> DeleteProductCategorySFAMapping(ProductCategorySFAMapping mappingData);
        #endregion Product Category SFA Mapping

        #region Employee Structure Mapping
        Envelope<bool> CreateEmployeeStructureMapping(EmployeeStructureMapping mappingData);

        Envelope<bool> DeleteEmployeeStructureMapping(EmployeeStructureMapping mappingData);

        List<SFAFormData> GetSFAForStructureMapping(SFAForStructureMappingInput objSearchData);
        #endregion Employee Structure Mapping

        #region User Branch Channel Product Category Mapping
        Envelope<bool> ManageUserBranchChannelPCMapping(UserBranchChannelPCMapping mappingData);

        UserBranchChannelPCMapping GetUserBranchChannelPCMapping(Int64 MappingUserId);
        #endregion User Branch Channel Product Category Mapping

        #region Incentive Target Category Mapping
        Envelope<bool> ManageIncentiveTargetCategoryMapping(IncentiveTargetCategoryMapping mappingData);

        IncentiveTargetCategoryMapping GetIncentiveTargetCategoryMapping(Int64 IncentiveCatId);
        #endregion Incentive Target Category Mapping

        #region Assign Incentive Category to SFA
        IncentiveCategorySFAMapping GetSFAForIncentiveCategorySFAMapping(Int64 RDIId);

        Envelope<IncentiveCategorySFAMapping> ManageIncentiveCategorySFAMapping(IncentiveCategorySFAMapping mappingData);
        #endregion Assign Incentive Category to SFA

        #region Product Definition Under Target Category
        Envelope<bool> ManageProductDefinitionUnderTargetCategory(ProdDefUnderTargetCatGridOutput mappingData);

        Envelope<ProdDefUnderTargetCatGridOutput> GetProductsByCategorySubCategory(ProdDefUnderTargetCat mappingData);

        Envelope<ProdDefUnderTargetCatGridOutput> GetProductsByCategorySubCategoryforallMat(ProdDefUnderTargetCatforAllMat mappingData);
        #endregion Product Definition Under Target Category

        #region Assign Target To SFA API
        Envelope<DataTable> UploadTargetToSFAByMonth(AssignTargetToSFAUpload targetData);

        AssignTargetToSFAGet GetBranchByUserId(Int64 UserId);
        #endregion Assign Target To SFA API

        #region Navigation Overloads
        Envelope<bool> CreateIncentiveCategorySFAMapping(NavigationIncentiveCategorySFAMapping mappingData);
        #endregion Navigation Overloads

        #region Sales PIC Outlet Mapping
        Envelope<bool> ManageSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData);

        Envelope<ManageSalesPICOutletMappingForm> GetSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData);

        Envelope<bool> DeleteSalesPICOutletMapping(DeleteSalesPICOutletMappingForm objFormData);
        #endregion Sales PIC Outlet Mapping

        #region Assign Festival Target To SFA API
        List<AssignFestivalTargetGrid> GetFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput);

        List<AssignFestivalTargetGrid> ShowFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput);

        Envelope<DataTable> UploadFestivalTargetToSFAByScheme(AssignFestivalTargetUpload objInput);
        #endregion Assign Festival Target To SFA API

        #region Nikita kawade 9/9/2024
        Envelope<DataTable> UploadQSRQuantity(QSRQuantityUpload targetData);
        #endregion
    }
}
