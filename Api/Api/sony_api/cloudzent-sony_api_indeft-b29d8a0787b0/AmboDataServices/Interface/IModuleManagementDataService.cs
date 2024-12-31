using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Modules;
using System.Data;

namespace AmboDataServices.Interface
{
    public interface IModuleManagementDataService
    {
        #region Dealer Master Code Update Data Service Methods
        List<string> GetDealerMasterCodeList();

        List<Dealerdetails> GetDealerByMasterCode(string masterCode);

        bool UpdateDealerMasterCode(DealerMasterCodeUpdate dealerMasterCodeData, out string Message);

        bool ValidateDealerMasterCode(string masterCode, out string Message);
        #endregion Dealer Master Code Update Data Service Methods

        #region Asset Issue to SFA
        DataTable IssueAssestToSFA(AssetAssignmentToSFA InputParam);

        IEnumerable<AssetIssuedToSFAGrid> GetAssetIssuedToSFA(AssetIssuedToSFAGet InputParam);

        IEnumerable<AssetsDropDownData> GetAssetsDropDown(AssetIssuedToSFAGet InputParam);
        #endregion

        #region Asset Assignment To RDI Data Service Methods
        bool UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate assetAssignmentToRDI, out string Message);

        AssetAssignmentToRDIUpdate GetAssetAssignmentToRDIById(Int64? Id);

        DataTable UploadAssetAssignmentToRDI(AssetAssignmentToRDIUpload InputParam);
        #endregion Asset Assignment To RDI Data Service Methods

        #region Asset Collection From SFA Data Service Methods
        DataTable UploadAssetCollectionFromSFA(AssetCollectionFromSFA InputParam);
        IEnumerable<AssetCollectionFromSFAData> GetAssetCollectionFormatFromSFA(AssetCollectionFromSFAGet Input);

        #endregion Asset Collection From SFA Data Service Methods
    }
}
