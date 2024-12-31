using AmboDataServices.Interface;
using AmboServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.IncentiveManagement;
using AmboUtilities;
using AmboUtilities.Helper;

namespace AmboServices.Implimentation
{
    public class DeviationService : IDeviationService
    {
        private readonly IDeviationDataService _IDeviationDataService;

        public DeviationService(IDeviationDataService IDeviationDataService)
        {
            _IDeviationDataService = IDeviationDataService;
        }

        public Envelope<DeviationUploadByRDIExcel> GetDeviationUploadByRDIExcel(DeviationUploadByRDISearch objSearchData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.GetDeviationUploadByRDIExcel(objSearchData, out Message);
            return (output.ExcelData == null) ?
                new Envelope<DeviationUploadByRDIExcel> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message }:
                new Envelope<DeviationUploadByRDIExcel> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<DeviationUploadByRDIExcel> GetDeviationUploadByRDIReasons(DeviationUploadByRDISearch objSearchData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.GetDeviationUploadByRDIReasons(objSearchData, out Message);
            return (output.ExcelData == null) ?
                new Envelope<DeviationUploadByRDIExcel> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<DeviationUploadByRDIExcel> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<DeviationUploadByRDIExcel> ManageDeviationUploadByRDI(DeviationUploadByRDIExcel objSearchData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.ManageDeviationUploadByRDI(objSearchData, out Message);
            return (output.ExcelData == null) ?
                new Envelope<DeviationUploadByRDIExcel> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<DeviationUploadByRDIExcel> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<DeviationUploadByRDIExcel> ManageDeviationUploadByRDI_Festival(DeviationUploadByRDIExcel objSearchData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.ManageDeviationUploadByRDI_Festival(objSearchData, out Message);
            return (output.ExcelData == null) ?
                new Envelope<DeviationUploadByRDIExcel> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<DeviationUploadByRDIExcel> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<bool> ManageDeviationUploadByRDI_SaveReasons(DeviationUploadByRDISaveReasons objData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.ManageDeviationUploadByRDI_SaveReasons(objData, out Message);
            return (!output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<bool> ManageDeviationUploadByRDI_ApproveReasons(DeviationUploadByRDISaveReasons objData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.ManageDeviationUploadByRDI_ApproveReasons(objData, out Message);
            return (!output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<DeviationApprovalExcel> GetDeviationApprovalExcel(DeviationApprovalSearch objSearchData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.GetDeviationApprovalExcel(objSearchData, out Message);
            return (output.ExcelData == null) ?
                new Envelope<DeviationApprovalExcel> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<DeviationApprovalExcel> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<bool> UploadDeviationApprovalExcel(DeviationApprovalExcel objData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.UploadDeviationApprovalExcel(objData, out Message);
            return (!output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<DeviationFinalUploadExcel> GetDeviationFinalUploadExcel(DeviationFinalUploadSearch objSearchData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.GetDeviationFinalUploadExcel(objSearchData, out Message);
            return (output.ExcelData == null) ?
                new Envelope<DeviationFinalUploadExcel> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<DeviationFinalUploadExcel> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<bool> UploadDeviationFinalUploadExcel(DeviationFinalUploadExcel objData)
        {
            string Message = string.Empty;
            var output = _IDeviationDataService.UploadDeviationFinalUploadExcel(objData, out Message);
            return (!output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }
    }
}
