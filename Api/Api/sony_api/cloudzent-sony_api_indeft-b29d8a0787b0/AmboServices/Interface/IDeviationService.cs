using AmboLibrary.IncentiveManagement;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IDeviationService
    {
        Envelope<DeviationUploadByRDIExcel> GetDeviationUploadByRDIExcel(DeviationUploadByRDISearch objSearchData);
        Envelope<DeviationUploadByRDIExcel> GetDeviationUploadByRDIReasons(DeviationUploadByRDISearch objSearchData);
        Envelope<DeviationUploadByRDIExcel> ManageDeviationUploadByRDI(DeviationUploadByRDIExcel objData);
        Envelope<DeviationUploadByRDIExcel> ManageDeviationUploadByRDI_Festival(DeviationUploadByRDIExcel objData);
        Envelope<bool> ManageDeviationUploadByRDI_SaveReasons(DeviationUploadByRDISaveReasons objData);
        Envelope<bool> ManageDeviationUploadByRDI_ApproveReasons(DeviationUploadByRDISaveReasons objData);
        Envelope<DeviationApprovalExcel> GetDeviationApprovalExcel(DeviationApprovalSearch objSearchData);
        Envelope<bool> UploadDeviationApprovalExcel(DeviationApprovalExcel objData);
        Envelope<DeviationFinalUploadExcel> GetDeviationFinalUploadExcel(DeviationFinalUploadSearch objSearchData);
        Envelope<bool> UploadDeviationFinalUploadExcel(DeviationFinalUploadExcel objData);
    }
}
