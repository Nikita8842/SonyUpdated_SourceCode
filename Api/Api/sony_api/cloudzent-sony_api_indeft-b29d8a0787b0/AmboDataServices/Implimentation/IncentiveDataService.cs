using AmboDataServices.Interface;
using AmboLibrary.IncentiveManagement;
using AmboLibrary.WebReports;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Implimentation
{
    public class IncentiveDataService : IIncentiveDataService
    {
        private readonly IIncentiveProvider _IIncentiveProvider;

        public IncentiveDataService(IIncentiveProvider IIncentiveProvider)
        {
            _IIncentiveProvider = IIncentiveProvider;
        }

        public bool DeleteBaseIncentiveDefinition(DeleteBaseIncentiveForm objFormData, out string Message)
        {
            return _IIncentiveProvider.DeleteBaseIncentiveDefinition(objFormData, out Message);
        }

        public CreateBaseIncentiveForm GetBaseIncentiveDefinitionByTargetCategoryId(GetBaseIncentive objFormData, out string Message)
        {
            return _IIncentiveProvider.GetBaseIncentiveDefinitionByTargetCategoryId(objFormData, out Message);
        }

        public PerPieceIncentiveValues GetPerPieceIncentiveExcelFile(DownloadPerPieceIncentiveExcel objDownloadData, out string Message)
        {
            return _IIncentiveProvider.GetPerPieceIncentiveExcelFile(objDownloadData, out Message);
        }        

        public bool ManageBaseIncentiveDefinition(CreateBaseIncentiveForm objFormData, out string Message)
        {
            return _IIncentiveProvider.ManageBaseIncentiveDefinition(objFormData, out Message);
        }

        public bool ManagePerPieceIncentiveDefinition(CreatePerPieceIncentive objFormData, out string Message)
        {
            return _IIncentiveProvider.ManagePerPieceIncentiveDefinition(objFormData, out Message);
        }

        public CreatePerPieceIncentive GetPerPieceIncentiveDefinitionBySchemeId(GetPerPieceIncentiveValues objFormData, out string Message)
        {
            return _IIncentiveProvider.GetPerPieceIncentiveDefinitionBySchemeId(objFormData, out Message);
        }

        public IEnumerable<PerPieceIncentiveSchemeGet> GetPerPieceIncentiveSchemeByProductId(PerPieceIncentiveSchemeByProductId objFormData, out string Message)
        {
            return _IIncentiveProvider.GetPerPieceIncentiveSchemeByProductId(objFormData, out Message);
        }

        public bool ManagePerPieceMaterialMapping(PerPieceIncentiveCreate mappingData, out string Message)
        {
            return Convert.ToBoolean(_IIncentiveProvider.ManagePerPieceMaterialMapping(mappingData, out Message));
        }

        public bool DeletePerPieceIncentiveDefinition(DeletePerPieceIncentive objFormData, out string Message)
        {
            return _IIncentiveProvider.DeletePerPieceIncentiveDefinition(objFormData, out Message);
        }

        #region Festival Incentive
        public FestivalIncentiveDefinitionValues GetFestivalIncentiveDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData, out string Message)
        {
            return _IIncentiveProvider.GetFestivalIncentiveDefinitionExcelFile(objDownloadData, out Message);
        }
        public FestivalIncentiveDefinitionValues GetFestivalIncentiveSlabDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData, out string Message)
        {
            return _IIncentiveProvider.GetFestivalIncentiveSlabDefinitionExcelFile(objDownloadData, out Message);
        }
        public bool ManageFestivalIncentiveDefinition(CreateFestivalIncentive objFormData, out string Message)
        {
            return _IIncentiveProvider.ManageFestivalIncentiveDefinition(objFormData, out Message);
        }
        public bool ManageFestivalIncentiveSlabDefinition(CreateFestivalIncentiveSlab objFormData, out string Message)
        {
            return _IIncentiveProvider.ManageFestivalIncentiveSlabDefinition(objFormData, out Message);
        }
        public CreateFestivalIncentive GetFestivalIncentiveDefinitionBySchemeId(GetFestivalIncentiveValues objFormData, out string Message)
        {
            return _IIncentiveProvider.GetFestivalIncentiveDefinitionBySchemeId(objFormData, out Message);
        }
        public CreateFestivalIncentiveSlab GetFestivalIncentiveSlabDefinitionBySchemeId(GetFestivalIncentiveValues objFormData, out string Message)
        {
            return _IIncentiveProvider.GetFestivalIncentiveSlabDefinitionBySchemeId(objFormData, out Message);
        }
        public bool DeleteFestivalIncentiveDefinition(DeleteFestivalIncentive objFormData, out string Message)
        {
            return _IIncentiveProvider.DeleteFestivalIncentiveDefinition(objFormData, out Message);
        }
        #endregion Festival Incentive

        #region IncentiveReport
        public BasePerPieceIncentiveDisplayReportList GetBasePerPieceIncentiveReport(BasePerPieceIncentiveReportInputParam Input)
        {
            return _IIncentiveProvider.GetBasePerPieceIncentiveReport(Input);
        }
        public BasePerPieceIncentiveDetailReportList GetBasePerPieceIncentiveDetailReport(BasePerPieceIncentiveReportInputParam Input)
        {
            return _IIncentiveProvider.GetBasePerPieceIncentiveDetailReport(Input);
        }


        public FestivalIncentiveDisplayReportList GetFestivalIncentiveReport(FestivalIncentiveReportInputParam Input)
        {
            return _IIncentiveProvider.GetFestivalIncentiveReport(Input);
        }

        public FestivalIncentiveDetailReportList GetFestivalIncentiveDetailReport(FestivalIncentiveReportInputParam Input)
        {
            return _IIncentiveProvider.GetFestivalIncentiveDetailReport(Input);
        }


        public FestivalSellThruTrackerList GetFestivalSellThruTracker(FestivalSellThruTrackerInputParam Input)
        {
            return _IIncentiveProvider.GetFestivalSellThruTracker(Input);
        }

        public FestivalNameDetailList GetFestivalNameDetails()
        {
            return _IIncentiveProvider.GetFestivalNameDetails();
        }
        #endregion

        #region IncentiveReportQSR nikita 9/3/2024
        public BasePerPieceIncentiveDisplayReportList GetBasePerPieceIncentiveReportQSR(BasePerPieceIncentiveReportInputParamQSR Input)
        {
            return _IIncentiveProvider.GetBasePerPieceIncentiveReportQSR(Input);
        }
        #endregion
    }
}
