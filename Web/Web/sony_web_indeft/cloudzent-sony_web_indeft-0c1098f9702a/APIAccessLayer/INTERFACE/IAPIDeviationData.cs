using AMBOModels.IncentiveManagement;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIDeviationData
    {
        #region Deviation Upload By RDI
        Envelope<DeviationUploadByRDIExcel> GetDeviationUploadByRDIExcel(DeviationUploadByRDISearch objSearchData);

        Envelope<DeviationUploadByRDIExcel> GetDeviationUploadByRDIReasons(DeviationUploadByRDISearch objSearchData);

        Envelope<DeviationUploadByRDIExcel> ManageDeviationUploadByRDI(DeviationUploadByRDIExcel objData);

        Envelope<DeviationUploadByRDIExcel> ManageDeviationUploadByRDI_Festival(DeviationUploadByRDIExcel objData);

        Envelope<bool> ManageDeviationUploadByRDI_SaveReasons(DeviationUploadByRDISaveReasons objData);

        Envelope<bool> ManageDeviationUploadByRDI_ApproveReasons(DeviationUploadByRDISaveReasons objData);
        #endregion Deviation Upload By RDI

        #region Deviation Approval
        Envelope<DeviationApprovalExcel> GetDeviationApprovalExcel(DeviationApprovalSearch objSearchData);

        Envelope<DeviationApprovalExcel> UploadDeviationApprovalExcel(DeviationApprovalExcel objData);

        Envelope<DeviationApprovalExcel> UploadDeviationApprovalExcel_Festival(DeviationApprovalExcel objData);
        #endregion Deviation Approval

        #region Deviation FinalUpload
        Envelope<DeviationFinalUploadExcel> GetDeviationFinalUploadExcel(DeviationFinalUploadSearch objSearchData);

        Envelope<DeviationFinalUploadExcel> UploadDeviationFinalUploadExcel(DeviationFinalUploadExcel objData);

        Envelope<DeviationFinalUploadExcel> UploadDeviationFinalUploadExcel_Festival(DeviationFinalUploadExcel objData);
        #endregion Deviation FinalUpload
    }
}
