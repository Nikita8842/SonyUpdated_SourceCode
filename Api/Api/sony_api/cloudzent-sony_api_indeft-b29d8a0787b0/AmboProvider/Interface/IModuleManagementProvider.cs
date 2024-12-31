using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Modules;
using System.Data;

namespace AmboProvider.Interface
{
    public interface IModuleManagementProvider
    {
        #region Dealer Master Code Update API
        List<string> GetDealerMasterCodeList();

        int UpdateDealerMasterCode(DealerMasterCodeUpdate dealerMasterCodeData, out string Message);

        List<Dealerdetails> GetDealerByMasterCode(string masterCode);

        int ValidateDealerMasterCode(string masterCode, out string Message);
        #endregion Dealer Master Code Update API

        #region Asset Issue to SFA
        DataTable IssueAssestToSFA(AssetAssignmentToSFA InputParam);

        IEnumerable<AssetIssuedToSFAGrid> GetAssetIssuedToSFA(AssetIssuedToSFAGet InputParam);

        IEnumerable<AssetsDropDownData> GetAssetsDropDown(AssetIssuedToSFAGet InputParam);
        #endregion

        #region Asset Assignment To RDI API
        int UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate assetAssignmentToRDI, out string Message);

        AssetAssignmentToRDIUpdate GetAssetAssignmentToRDIById(Int64? Id);

        DataTable UploadAssetAssignmentToRDI(AssetAssignmentToRDIUpload InputParam);
        #endregion Asset Assignment To RDI API

        #region Asset Collection From SFA API
        DataTable UploadAssetCollectionFromSFA(AssetCollectionFromSFA InputParam);

        IEnumerable<AssetCollectionFromSFAData> GetAssetCollectionFormatFromSFA(AssetCollectionFromSFAGet Input);
        #endregion Asset Collection From SFA API
    }
}
