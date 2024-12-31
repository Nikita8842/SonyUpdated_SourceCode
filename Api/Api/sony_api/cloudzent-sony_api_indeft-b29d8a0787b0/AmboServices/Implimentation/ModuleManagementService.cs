using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Modules;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using AmboDataServices.Interface;
using System.Data;

namespace AmboServices.Implimentation
{
    public class ModuleManagementService : IModuleManagementService
    {
        private readonly IModuleManagementDataService _IModuleManagementDataService;

        public ModuleManagementService(IModuleManagementDataService IModuleManagementDataService)
        {
            _IModuleManagementDataService = IModuleManagementDataService;
        }

        #region Dealer Master Code Update API
        public Envelope<List<string>> GetDealerMasterCodeList()
        {
            var output = _IModuleManagementDataService.GetDealerMasterCodeList();
            return (output == null) ?
                new Envelope<List<string>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<string>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All dealer's master code data fetched successfully." };
        }

        public Envelope<List<Dealerdetails>> GetDealerByMasterCode(string masterCode)
        {
            var output = _IModuleManagementDataService.GetDealerByMasterCode(masterCode);
            return (output == null) ?
                new Envelope<List<Dealerdetails>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<Dealerdetails>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All dealer's data fetched successfully." };
        }

        public Envelope<bool> UpdateDealerMasterCode(DealerMasterCodeUpdate dealerMasterCodeData)
        {
            var Message = string.Empty;
            var output = _IModuleManagementDataService.UpdateDealerMasterCode(dealerMasterCodeData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }

        public Envelope<bool> ValidateDealerMasterCode(string masterCode)
        {
            var Message = string.Empty;
            var output = _IModuleManagementDataService.ValidateDealerMasterCode(masterCode, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }
        #endregion Dealer Master Code Update API

        #region Asset Issue to SFA
        public Envelope<DataTable> IssueAssestToSFA(AssetAssignmentToSFA InputParam)
        {
            var Output =  _IModuleManagementDataService.IssueAssestToSFA(InputParam);
            return (Output==null)?
                new Envelope<DataTable> { Data = Output, MessageCode = (int)Acceptable.Found, Message = "No data return" } :
                new Envelope<DataTable> { Data = Output, MessageCode = (int)NotAcceptable.NotFound, Message = "Data Partially Upload" };
        }

        public Envelope<IEnumerable<AssetIssuedToSFAGrid>> GetAssetIssuedToSFA(AssetIssuedToSFAGet InputParam)
        {
            var output = _IModuleManagementDataService.GetAssetIssuedToSFA(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<AssetIssuedToSFAGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<AssetIssuedToSFAGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Data fetched successfully." };
        }

        public Envelope<IEnumerable<AssetsDropDownData>> GetAssetsDropDown(AssetIssuedToSFAGet InputParam)
        {
            var output = _IModuleManagementDataService.GetAssetsDropDown(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<AssetsDropDownData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<AssetsDropDownData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Data fetched successfully." };
        }
        #endregion

        #region Asset Assignment To RDI API
        public Envelope<AssetAssignmentToRDIUpdate> GetAssetAssignmentToRDIById(Int64? Id)
        {
            var Message = string.Empty;
            var output = _IModuleManagementDataService.GetAssetAssignmentToRDIById(Id);
            return (output == null) ?
                new Envelope<AssetAssignmentToRDIUpdate> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<AssetAssignmentToRDIUpdate> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }

        public Envelope<bool> UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate assetAssignmentToRDI)
        {
            var Message = string.Empty;
            var output = _IModuleManagementDataService.UpdateAssetAssignmentToRDI(assetAssignmentToRDI, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }

        public Envelope<DataTable> UploadAssetAssignmentToRDI(AssetAssignmentToRDIUpload InputParam)
        {
            var Message = string.Empty;
            var output = _IModuleManagementDataService.UploadAssetAssignmentToRDI(InputParam);
            return (output != null && output.Rows.Count > 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Created, Message = "Partial/Full data updated/inserted successfully." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = "Error occured while uploading data." };
        }

        #endregion Asset Assignment To RDI API

        #region Asset Collection From SFA API

        public Envelope<DataTable> UploadAssetCollectionFromSFA(AssetCollectionFromSFA InputParam)
        {
            var output = _IModuleManagementDataService.UploadAssetCollectionFromSFA(InputParam);
            return (output != null && output.Rows.Count > 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Created, Message = "Full/partial data updated/inserted successfully." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "Error in inserting data" };
        }

        public Envelope<IEnumerable<AssetCollectionFromSFAData>> GetAssetCollectionFormatFromSFA(AssetCollectionFromSFAGet InputParam)
        {
            var output = _IModuleManagementDataService.GetAssetCollectionFormatFromSFA(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<AssetCollectionFromSFAData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<AssetCollectionFromSFAData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Asset Collection data fetched successfully." };
        }


        #endregion Asset Collection From SFA API
    }
}
