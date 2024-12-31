using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboDataServices.Interface;
using AmboLibrary.Modules;
using AmboProvider.Interface;

namespace AmboDataServices.Implimentation
{
    public class ModuleManagementDataService : IModuleManagementDataService
    {
        private readonly IModuleManagementProvider _IModuleManagementProvider;

        public ModuleManagementDataService(IModuleManagementProvider IModuleManagementProvider)
        {
            _IModuleManagementProvider = IModuleManagementProvider;
        }

        #region Dealer Master Code Update API
        public List<string> GetDealerMasterCodeList()
        {
            return _IModuleManagementProvider.GetDealerMasterCodeList();
        }

        public List<Dealerdetails> GetDealerByMasterCode(string masterCode)
        {
            return _IModuleManagementProvider.GetDealerByMasterCode(masterCode);
        }

        public bool UpdateDealerMasterCode(DealerMasterCodeUpdate dealerMasterCodeData, out string Message)
        {
            return Convert.ToBoolean(_IModuleManagementProvider.UpdateDealerMasterCode(dealerMasterCodeData, out Message));
        }

        public bool ValidateDealerMasterCode(string masterCode, out string Message)
        {
            return Convert.ToBoolean(_IModuleManagementProvider.ValidateDealerMasterCode(masterCode, out Message));
        }
        #endregion Dealer Master Code Update API

        #region Asset Issue to SFA
        public DataTable IssueAssestToSFA(AssetAssignmentToSFA InputParam)
        {
            return _IModuleManagementProvider.IssueAssestToSFA(InputParam);
        }

        public IEnumerable<AssetIssuedToSFAGrid> GetAssetIssuedToSFA(AssetIssuedToSFAGet InputParam)
        {
            return _IModuleManagementProvider.GetAssetIssuedToSFA(InputParam);
        }

        public IEnumerable<AssetsDropDownData> GetAssetsDropDown(AssetIssuedToSFAGet InputParam)
        {
            return _IModuleManagementProvider.GetAssetsDropDown(InputParam);
        }
        #endregion

        #region Asset Assignment To RDI API
        public AssetAssignmentToRDIUpdate GetAssetAssignmentToRDIById(Int64? Id)
        {
            return _IModuleManagementProvider.GetAssetAssignmentToRDIById(Id);
        }

        public bool UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate assetAssignmentToRDI, out string Message)
        {
            return Convert.ToBoolean(_IModuleManagementProvider.UpdateAssetAssignmentToRDI(assetAssignmentToRDI, out Message));
        }

        public DataTable UploadAssetAssignmentToRDI(AssetAssignmentToRDIUpload InputParam)
        {
            return _IModuleManagementProvider.UploadAssetAssignmentToRDI(InputParam);
        }

        #endregion Asset Assignment To RDI API

        #region Asset Collection From SFA API

        public DataTable UploadAssetCollectionFromSFA(AssetCollectionFromSFA InputParam)
        {
            return _IModuleManagementProvider.UploadAssetCollectionFromSFA(InputParam);
        }

        public IEnumerable<AssetCollectionFromSFAData> GetAssetCollectionFormatFromSFA(AssetCollectionFromSFAGet InputParam)
        {
            return _IModuleManagementProvider.GetAssetCollectionFormatFromSFA(InputParam);
        }

        #endregion Asset Collection From SFA API
    }
}
