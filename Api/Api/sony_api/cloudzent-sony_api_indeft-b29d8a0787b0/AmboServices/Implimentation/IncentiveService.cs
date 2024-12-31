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
using AmboLibrary.WebReports;

namespace AmboServices.Implimentation
{
    public class IncentiveService : IIncentiveService
    {
        private readonly IIncentiveDataService _IIncentiveDataService;

        public IncentiveService(IIncentiveDataService IIncentiveDataService)
        {
            _IIncentiveDataService = IIncentiveDataService;
        }

        public Envelope<bool> DeleteBaseIncentiveDefinition(DeleteBaseIncentiveForm objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.DeleteBaseIncentiveDefinition(objFormData, out Message);
            return output ? new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)Acceptable.Created } :
                new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)NotAcceptable.NotAcceptable };
        }

        public Envelope<CreateBaseIncentiveForm> GetBaseIncentiveDefinitionByTargetCategoryId(GetBaseIncentive objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.GetBaseIncentiveDefinitionByTargetCategoryId(objFormData, out Message);
            return (output == null || output.objDefinitionData == null || output.objDefinitionData.Count == 0) ? 
                new Envelope<CreateBaseIncentiveForm> { Data = null, Message = Message, MessageCode = (int)NotAcceptable.NotFound} :
                new Envelope<CreateBaseIncentiveForm> { Data = output, Message = Message, MessageCode = (int)Acceptable.Found};
        }

        public Envelope<PerPieceIncentiveValues> GetPerPieceIncentiveExcelFile(DownloadPerPieceIncentiveExcel objDownloadData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.GetPerPieceIncentiveExcelFile(objDownloadData, out Message);
            return output.ExcelData.Rows.Count > 0 ? new Envelope<PerPieceIncentiveValues> { Data = output, Message = Message, MessageCode = (int)Acceptable.Created } :
                new Envelope<PerPieceIncentiveValues> { Data = output, Message = Message, MessageCode = (int)NotAcceptable.NotAcceptable };
        }
        
        public Envelope<bool> ManageBaseIncentiveDefinition(CreateBaseIncentiveForm objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.ManageBaseIncentiveDefinition(objFormData, out Message);
            return output ? new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)Acceptable.Created } :
                new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)NotAcceptable.NotAcceptable };
        }

        public Envelope<bool> ManagePerPieceIncentiveDefinition(CreatePerPieceIncentive objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.ManagePerPieceIncentiveDefinition(objFormData, out Message);
            return output ? new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)Acceptable.Created } :
                new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)NotAcceptable.NotAcceptable };
        }

        public Envelope<CreatePerPieceIncentive> GetPerPieceIncentiveDefinitionBySchemeId(GetPerPieceIncentiveValues objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.GetPerPieceIncentiveDefinitionBySchemeId(objFormData, out Message);
            return (output == null || output.SchemeName == null || output.SchemeName == "") ?
                new Envelope<CreatePerPieceIncentive> { Data = null, Message = Message, MessageCode = (int)NotAcceptable.NotFound } :
                new Envelope<CreatePerPieceIncentive> { Data = output, Message = Message, MessageCode = (int)Acceptable.Found };
        }

        public Envelope<IEnumerable<PerPieceIncentiveSchemeGet>> GetPerPieceIncentiveSchemeByProductId(PerPieceIncentiveSchemeByProductId objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.GetPerPieceIncentiveSchemeByProductId(objFormData, out Message);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<PerPieceIncentiveSchemeGet>> { Data = null, Message = Message, MessageCode = (int)NotAcceptable.NotFound } :
                new Envelope<IEnumerable<PerPieceIncentiveSchemeGet>> { Data = output, Message = Message, MessageCode = (int)Acceptable.Found };
        }

        public Envelope<bool> ManagePerPieceMaterialMapping(PerPieceIncentiveCreate mappingData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.ManagePerPieceMaterialMapping(mappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeletePerPieceIncentiveDefinition(DeletePerPieceIncentive objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.DeletePerPieceIncentiveDefinition(objFormData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        #region Festival Incentive
        public Envelope<FestivalIncentiveDefinitionValues> GetFestivalIncentiveDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.GetFestivalIncentiveDefinitionExcelFile(objDownloadData, out Message);
            return (output.ExcelData == null) ?
                new Envelope<FestivalIncentiveDefinitionValues> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message }:
                new Envelope<FestivalIncentiveDefinitionValues> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<FestivalIncentiveDefinitionValues> GetFestivalIncentiveSlabDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.GetFestivalIncentiveSlabDefinitionExcelFile(objDownloadData, out Message);
            return (output.ExcelData == null) ?
                new Envelope<FestivalIncentiveDefinitionValues> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<FestivalIncentiveDefinitionValues> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<bool> ManageFestivalIncentiveDefinition(CreateFestivalIncentive objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.ManageFestivalIncentiveDefinition(objFormData, out Message);
            return output ? new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)Acceptable.Created } :
                new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)NotAcceptable.NotAcceptable };
        }

        public Envelope<bool> ManageFestivalIncentiveSlabDefinition(CreateFestivalIncentiveSlab objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.ManageFestivalIncentiveSlabDefinition(objFormData, out Message);
            return output ? new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)Acceptable.Created } :
                new Envelope<bool> { Data = output, Message = Message, MessageCode = (int)NotAcceptable.NotAcceptable };
        }

        public Envelope<CreateFestivalIncentive> GetFestivalIncentiveDefinitionBySchemeId(GetFestivalIncentiveValues objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.GetFestivalIncentiveDefinitionBySchemeId(objFormData, out Message);
            return (output == null || output.SchemeName == null || output.SchemeName == "") ?
                new Envelope<CreateFestivalIncentive> { Data = null, Message = Message, MessageCode = (int)NotAcceptable.NotFound } :
                new Envelope<CreateFestivalIncentive> { Data = output, Message = Message, MessageCode = (int)Acceptable.Found };
        }

        public Envelope<CreateFestivalIncentiveSlab> GetFestivalIncentiveSlabDefinitionBySchemeId(GetFestivalIncentiveValues objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.GetFestivalIncentiveSlabDefinitionBySchemeId(objFormData, out Message);
            return (output == null || output.SchemeName == null || output.SchemeName == "") ?
                new Envelope<CreateFestivalIncentiveSlab> { Data = null, Message = Message, MessageCode = (int)NotAcceptable.NotFound } :
                new Envelope<CreateFestivalIncentiveSlab> { Data = output, Message = Message, MessageCode = (int)Acceptable.Found };
        }

        public Envelope<bool> DeleteFestivalIncentiveDefinition(DeleteFestivalIncentive objFormData)
        {
            var Message = string.Empty;
            var output = _IIncentiveDataService.DeleteFestivalIncentiveDefinition(objFormData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Festival Incentive

        #region IncentiveReport
        public Envelope<BasePerPieceIncentiveDisplayReportList> GetBasePerPieceIncentiveReport(BasePerPieceIncentiveReportInputParam Input)
        {
            var output = _IIncentiveDataService.GetBasePerPieceIncentiveReport(Input);
            return (output == null || output.BasePerPieceIncentiveData.Count() == 0) ?
                new Envelope<BasePerPieceIncentiveDisplayReportList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<BasePerPieceIncentiveDisplayReportList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Base Per Piece Incentive data fetched successfully." };
        }
        public Envelope<BasePerPieceIncentiveDetailReportList> GetBasePerPieceIncentiveDetailReport(BasePerPieceIncentiveReportInputParam Input)
        {
            var output = _IIncentiveDataService.GetBasePerPieceIncentiveDetailReport(Input);
            return (output == null || output.BasePerPieceIncentiveDetailData.Count() == 0) ?
                new Envelope<BasePerPieceIncentiveDetailReportList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<BasePerPieceIncentiveDetailReportList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Base Per Piece Incentive detail data fetched successfully." };
        }

        public Envelope<FestivalIncentiveDisplayReportList> GetFestivalIncentiveReport(FestivalIncentiveReportInputParam Input)
        {
            var output = _IIncentiveDataService.GetFestivalIncentiveReport(Input);
            return (output == null || output.FestivalIncentiveData.Count() == 0) ?
                new Envelope<FestivalIncentiveDisplayReportList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<FestivalIncentiveDisplayReportList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Festival Incentive data fetched successfully." };
        }
        public Envelope<FestivalIncentiveDetailReportList> GetFestivalIncentiveDetailReport(FestivalIncentiveReportInputParam Input)
        {
            var output = _IIncentiveDataService.GetFestivalIncentiveDetailReport(Input);
            return (output == null || output.FestivalIncentiveDetailData.Count() == 0) ?
                new Envelope<FestivalIncentiveDetailReportList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<FestivalIncentiveDetailReportList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Festival Incentive detail data fetched successfully." };
        }

        public Envelope<FestivalSellThruTrackerList> GetFestivalSellThruTracker(FestivalSellThruTrackerInputParam Input)
        {
            var output = _IIncentiveDataService.GetFestivalSellThruTracker(Input);
            return (output == null || output.FestivalSellThruTrackerData.Count() == 0) ?
                new Envelope<FestivalSellThruTrackerList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<FestivalSellThruTrackerList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Festival Sell Thru Tracker fetched successfully." };
        }

        public Envelope<FestivalNameDetailList> GetFestivalNameDetails()
        {
            var output = _IIncentiveDataService.GetFestivalNameDetails();
            return (output == null || output.FestivalNameDetailData.Count() == 0) ?
                new Envelope<FestivalNameDetailList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<FestivalNameDetailList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Festival Name details fetched successfully." };
        }
        #endregion

        #region IncentiveReportQSR Nikita 9/03/2024
        public Envelope<BasePerPieceIncentiveDisplayReportList> GetBasePerPieceIncentiveReportQSR(BasePerPieceIncentiveReportInputParamQSR Input)
        {
            var output = _IIncentiveDataService.GetBasePerPieceIncentiveReportQSR(Input);
            return (output == null || output.BasePerPieceIncentiveData.Count() == 0) ?
            new Envelope<BasePerPieceIncentiveDisplayReportList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
            new Envelope<BasePerPieceIncentiveDisplayReportList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Base Per Piece Incentive data fetched successfully." };
        }
        #endregion
    }
}
