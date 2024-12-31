using AmboDataServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.IncentiveManagement;
using AmboProvider.Interface;

namespace AmboDataServices.Implimentation
{
    public class DeviationDataService : IDeviationDataService
    {
        private readonly IDeviationProvider _IDeviationProvider;

        public DeviationDataService(IDeviationProvider IDeviationProvider)
        {
            _IDeviationProvider = IDeviationProvider;
        }

        public DeviationUploadByRDIExcel GetDeviationUploadByRDIExcel(DeviationUploadByRDISearch objSearchData, out string Message)
        {
            return _IDeviationProvider.GetDeviationUploadByRDIExcel(objSearchData, out Message);
        }

        public DeviationUploadByRDIExcel GetDeviationUploadByRDIReasons(DeviationUploadByRDISearch objSearchData, out string Message)
        {
            return _IDeviationProvider.GetDeviationUploadByRDIReasons(objSearchData, out Message);
        }

        public DeviationUploadByRDIExcel ManageDeviationUploadByRDI(DeviationUploadByRDIExcel objData, out string Message)
        {
            return _IDeviationProvider.ManageDeviationUploadByRDI(objData, out Message);
        }

        public DeviationUploadByRDIExcel ManageDeviationUploadByRDI_Festival(DeviationUploadByRDIExcel objData, out string Message)
        {
            return _IDeviationProvider.ManageDeviationUploadByRDI_Festival(objData, out Message);
        }

        public bool ManageDeviationUploadByRDI_SaveReasons(DeviationUploadByRDISaveReasons objData, out string Message)
        {
            return _IDeviationProvider.ManageDeviationUploadByRDI_SaveReasons(objData, out Message);
        }

        public bool ManageDeviationUploadByRDI_ApproveReasons(DeviationUploadByRDISaveReasons objData, out string Message)
        {
            return _IDeviationProvider.ManageDeviationUploadByRDI_ApproveReasons(objData, out Message);
        }

        public DeviationApprovalExcel GetDeviationApprovalExcel(DeviationApprovalSearch objSearchData, out string Message)
        {
            return _IDeviationProvider.GetDeviationApprovalExcel(objSearchData, out Message);
        }

        public bool UploadDeviationApprovalExcel(DeviationApprovalExcel objData, out string Message)
        {
            return _IDeviationProvider.UploadDeviationApprovalExcel(objData, out Message);
        }

        public DeviationFinalUploadExcel GetDeviationFinalUploadExcel(DeviationFinalUploadSearch objSearchData, out string Message)
        {
            return _IDeviationProvider.GetDeviationFinalUploadExcel(objSearchData, out Message);
        }

        public bool UploadDeviationFinalUploadExcel(DeviationFinalUploadExcel objData, out string Message)
        {
            return _IDeviationProvider.UploadDeviationFinalUploadExcel(objData, out Message);
        }
    }
}
