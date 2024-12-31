using AmboLibrary.IncentiveManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Interface
{
    public interface IDeviationDataService
    {
        DeviationUploadByRDIExcel GetDeviationUploadByRDIExcel(DeviationUploadByRDISearch objSearchData, out string Message);

        DeviationUploadByRDIExcel GetDeviationUploadByRDIReasons(DeviationUploadByRDISearch objSearchData, out string Message);

        DeviationUploadByRDIExcel ManageDeviationUploadByRDI(DeviationUploadByRDIExcel objData, out string Message);

        DeviationUploadByRDIExcel ManageDeviationUploadByRDI_Festival(DeviationUploadByRDIExcel objData, out string Message);

        bool ManageDeviationUploadByRDI_SaveReasons(DeviationUploadByRDISaveReasons objData, out string Message);

        bool ManageDeviationUploadByRDI_ApproveReasons(DeviationUploadByRDISaveReasons objData, out string Message);

        DeviationApprovalExcel GetDeviationApprovalExcel(DeviationApprovalSearch objSearchData, out string Message);

        bool UploadDeviationApprovalExcel(DeviationApprovalExcel objData, out string Message);

        DeviationFinalUploadExcel GetDeviationFinalUploadExcel(DeviationFinalUploadSearch objSearchData, out string Message);

        bool UploadDeviationFinalUploadExcel(DeviationFinalUploadExcel objData, out string Message);
    }
}
