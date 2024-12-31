using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Mappings;
using AmboProvider.Interface;
using AmboLibrary.UserManagement;
using System.Data;
using AmboDataServices.Interface;

namespace AmboDataServices.Implimentation
{
    public class MappingDataService : IMappingDataService
    {
        private readonly IMappingProvider _IMappingProvider;

        public MappingDataService(IMappingProvider IMappingProvider)
        {
            _IMappingProvider = IMappingProvider;
        }

        #region Dealer SFA Mapping
        public bool CreateDealerSFAMapping(DealerSFAMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.CreateDealerSFAMapping(mappingData, out Message));
        }

        public bool DeleteDealerSFAMapping(DealerSFAMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.DeleteDealerSFAMapping(mappingData, out Message));
        }

        public bool UpdateDealerSFAMapping(DealerSFAMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.UpdateDealerSFAMapping(mappingData, out Message));
        }
        #endregion Dealer SFA Mapping

        #region Dealer Classification Mapping
        public DealerClassificationMappingTable GetDealerClassificationMappingTable(DealerClassificationMappingSearch mappingData, out string Message)
        {
            return _IMappingProvider.GetDealerClassificationMappingTable(mappingData, out Message);
        }

        public bool CreateDealerClassificationMapping(DealerClassificationMappingTable mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.CreateDealerClassificationMapping(mappingData, out Message));
        }

        public bool DeleteDealerClassificationMapping(DealerClassificationMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.DeleteDealerClassificationMapping(mappingData, out Message));
        }

        public bool UpdateDealerClassificationMapping(DealerClassificationMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.UpdateDealerClassificationMapping(mappingData, out Message));
        }

        public bool ManageDealerClassificationMapping(DealerProductMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.ManageDealerClassificationMapping(mappingData, out Message));
        }
        #endregion Dealer Classification Mapping

        #region Product Category SFA Mapping
        public bool CreateProductCategorySFAMapping(ProductCategorySFAMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.CreateProductCategorySFAMapping(mappingData, out Message));
        }

        public bool DeleteProductCategorySFAMapping(ProductCategorySFAMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.DeleteProductCategorySFAMapping(mappingData, out Message));
        }
        #endregion Product Category SFA Mapping

        #region Employee Structure Mapping
        public bool CreateEmployeeStructureMapping(EmployeeStructureMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.CreateEmployeeStructureMapping(mappingData, out Message));
        }

        public bool DeleteEmployeeStructureMapping(EmployeeStructureMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.DeleteEmployeeStructureMapping(mappingData, out Message));
        }

        public IEnumerable<SFAFormData> GetSFAForStructureMapping(SFAForStructureMappingInput objSearchData)
        {
            return _IMappingProvider.GetSFAForStructureMapping(objSearchData);
        }
        #endregion Employee Structure Mapping

        #region User Branch Channel Product Category Mapping
        public bool ManageUserBranchChannelPCMapping(UserBranchChannelPCMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.ManageUserBranchChannelPCMapping(mappingData, out Message));
        }

        public UserBranchChannelPCMapping GetUserBranchChannelPCMapping(Int64 MappingUserId)
        {
            return _IMappingProvider.GetUserBranchChannelPCMapping(MappingUserId);
        }
        #endregion User Branch Channel Product Category Mapping

        #region Incentive Target Category Mapping
        public bool ManageIncentiveTargetCategoryMapping(IncentiveTargetCategoryMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.ManageIncentiveTargetCategoryMapping(mappingData, out Message));
        }

        public IncentiveTargetCategoryMapping GetIncentiveTargetCategoryMapping(Int64 IncentiveCatId)
        {
            return _IMappingProvider.GetIncentiveTargetCategoryMapping(IncentiveCatId);
        }
        #endregion Incentive Target Category Mapping

        #region Assign Incentive Category to SFA
        public IncentiveCategorySFAMapping GetSFAForIncentiveCategorySFAMapping(Int64 RDIId)
        {
            return _IMappingProvider.GetSFAForIncentiveCategorySFAMapping(RDIId);
        }

        public IncentiveCategorySFAMapping ManageIncentiveCategorySFAMapping(IncentiveCategorySFAMapping mappingData, out string Message)
        {
            return _IMappingProvider.ManageIncentiveCategorySFAMapping(mappingData, out Message);
        }
        #endregion Assign Incentive Category to SFA

        #region Product Definition Under Target Category
        public bool ManageProductDefinitionUnderTargetCategory(ProdDefUnderTargetCatGridOutput mappingData, out string Message)
        {
            return _IMappingProvider.ManageProductDefinitionUnderTargetCategory(mappingData, out Message);
        }
        public ProdDefUnderTargetCatGridOutput GetProductsByCategorySubCategory(ProdDefUnderTargetCat mappingData, out string Message)
        {
            return _IMappingProvider.GetProductsByCategorySubCategory(mappingData, out Message);
        }

        public ProdDefUnderTargetCatGridOutput GetProductsByCategorySubCategoryforallMat(ProdDefUnderTargetCatforAllMat mappingData, out string Message)
        {
            return _IMappingProvider.GetProductsByCategorySubCategoryforallMat(mappingData, out Message);
        }
        #endregion Product Definition Under Target Category

        #region Assign Target To SFA API
        public DataTable UploadTargetToSFAByMonth(AssignTargetToSFAUpload targetData)
        {
            return _IMappingProvider.UploadTargetToSFAByMonth(targetData);
        }

        public AssignTargetToSFAGet GetBranchByUserId(Int64 UserId)
        {
            return _IMappingProvider.GetBranchByUserId(UserId);
        }
        #endregion Assign Target To SFA API

        #region Navigation Overloads
        public bool CreateIncentiveCategorySFAMapping(NavigationIncentiveCategorySFAMapping mappingData, out string Message)
        {
            return Convert.ToBoolean(_IMappingProvider.CreateIncentiveCategorySFAMapping(mappingData, out Message));
        }
        #endregion Navigation Overloads

        #region Sales PIC Outlet Mapping
        public bool ManageSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData, out string Message)
        {
            return _IMappingProvider.ManageSalesPICOutletMapping(objFormData, out Message);
        }

        public ManageSalesPICOutletMappingForm GetSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData, out string Message)
        {
            return _IMappingProvider.GetSalesPICOutletMapping(objFormData, out Message);
        }

        public bool DeleteSalesPICOutletMapping(DeleteSalesPICOutletMappingForm objFormData, out string Message)
        {
            return _IMappingProvider.DeleteSalesPICOutletMapping(objFormData, out Message);
        }
        #endregion Sales PIC Outlet Mapping

        #region Assign Festival Target To SFA 
        public List<AssignFestivalTargetGrid> GetFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput)
        {
            return _IMappingProvider.GetFestivalTargetToSFABySchemeId(objInput);
        }

        public List<AssignFestivalTargetGrid> ShowFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput)
        {
            return _IMappingProvider.ShowFestivalTargetToSFABySchemeId(objInput);
        }

        public DataTable UploadFestivalTargetToSFAByScheme(AssignFestivalTargetUpload objInput)
        {
            return _IMappingProvider.UploadFestivalTargetToSFAByScheme(objInput);
        }
        #endregion Assign Festival Target To 

        #region QSR Quantity Report Nikita kawade 9/9/2024
        public DataTable UploadReportQSRQuantity(QSRQuantityUpload targetData)
        {
            return _IMappingProvider.UploadReportQSRQuantity(targetData);
        }


# endregion QSR Quantity Report
    }
}
