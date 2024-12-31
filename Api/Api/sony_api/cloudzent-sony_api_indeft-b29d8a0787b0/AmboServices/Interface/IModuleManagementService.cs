using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboUtilities;
using AmboLibrary.Modules;
using System.Data;

namespace AmboServices.Interface
{
    public interface IModuleManagementService
    {
        #region Dealer Master Code Update Service Methods
        Envelope<List<string>> GetDealerMasterCodeList();

        Envelope<List<Dealerdetails>> GetDealerByMasterCode(string masterCode);

        Envelope<bool> UpdateDealerMasterCode(DealerMasterCodeUpdate dealerMasterCodeData);

        Envelope<bool> ValidateDealerMasterCode(string masterCode);
        #endregion Dealer Master Code Update Service Methods

        #region Asset Issue to SFA
        Envelope<DataTable> IssueAssestToSFA(AssetAssignmentToSFA InputParam);

        Envelope<IEnumerable<AssetIssuedToSFAGrid>> GetAssetIssuedToSFA(AssetIssuedToSFAGet InputParam);

        Envelope<IEnumerable<AssetsDropDownData>> GetAssetsDropDown(AssetIssuedToSFAGet InputParam);
        #endregion

        #region Asset Assignment To RDI Service Methods
        Envelope<bool> UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate assetAssignmentToRDI);

        Envelope<AssetAssignmentToRDIUpdate> GetAssetAssignmentToRDIById(Int64? Id);

        Envelope<DataTable> UploadAssetAssignmentToRDI(AssetAssignmentToRDIUpload InputParam);
        #endregion Asset Assignment To RDI Service Methods

        #region Asset Collection From SFA Service Methods
        Envelope<DataTable> UploadAssetCollectionFromSFA(AssetCollectionFromSFA InputParam);

        Envelope<IEnumerable<AssetCollectionFromSFAData>> GetAssetCollectionFormatFromSFA(AssetCollectionFromSFAGet Input);

        #endregion Asset Collection From SFA Service Methods
    }
}
