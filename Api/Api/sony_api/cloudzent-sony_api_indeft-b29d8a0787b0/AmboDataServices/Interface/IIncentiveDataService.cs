using AmboLibrary.IncentiveManagement;
using AmboLibrary.WebReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Interface
{
    public interface IIncentiveDataService
    {
        bool ManageBaseIncentiveDefinition(CreateBaseIncentiveForm objFormData, out string Message);
        bool DeleteBaseIncentiveDefinition(DeleteBaseIncentiveForm objFormData, out string Message);
        CreateBaseIncentiveForm GetBaseIncentiveDefinitionByTargetCategoryId(GetBaseIncentive objFormData, out string Message);
        PerPieceIncentiveValues GetPerPieceIncentiveExcelFile(DownloadPerPieceIncentiveExcel objDownloadData, out string Message);
        bool ManagePerPieceIncentiveDefinition(CreatePerPieceIncentive objFormData, out string Message);
        CreatePerPieceIncentive GetPerPieceIncentiveDefinitionBySchemeId(GetPerPieceIncentiveValues objFormData, out string Message);
        IEnumerable<PerPieceIncentiveSchemeGet> GetPerPieceIncentiveSchemeByProductId(PerPieceIncentiveSchemeByProductId objFormData, out string Message);
        bool ManagePerPieceMaterialMapping(PerPieceIncentiveCreate mappingData, out string Message);
        bool DeletePerPieceIncentiveDefinition(DeletePerPieceIncentive objFormData, out string Message);

        #region Festival Incentive
        FestivalIncentiveDefinitionValues GetFestivalIncentiveDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData, out string Message);
        FestivalIncentiveDefinitionValues GetFestivalIncentiveSlabDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData, out string Message);
        bool ManageFestivalIncentiveDefinition(CreateFestivalIncentive objFormData, out string Message);
        bool ManageFestivalIncentiveSlabDefinition(CreateFestivalIncentiveSlab objFormData, out string Message);
        CreateFestivalIncentive GetFestivalIncentiveDefinitionBySchemeId(GetFestivalIncentiveValues objFormData, out string Message);
        CreateFestivalIncentiveSlab GetFestivalIncentiveSlabDefinitionBySchemeId(GetFestivalIncentiveValues objFormData, out string Message);
        bool DeleteFestivalIncentiveDefinition(DeleteFestivalIncentive objFormData, out string Message);
        #endregion Festival Incentive

        #region IncentiveReport
        BasePerPieceIncentiveDisplayReportList GetBasePerPieceIncentiveReport(BasePerPieceIncentiveReportInputParam Input);
        BasePerPieceIncentiveDetailReportList GetBasePerPieceIncentiveDetailReport(BasePerPieceIncentiveReportInputParam Input);

        FestivalIncentiveDisplayReportList GetFestivalIncentiveReport(FestivalIncentiveReportInputParam Input);
        FestivalIncentiveDetailReportList GetFestivalIncentiveDetailReport(FestivalIncentiveReportInputParam Input);

        FestivalSellThruTrackerList GetFestivalSellThruTracker(FestivalSellThruTrackerInputParam Input);
        FestivalNameDetailList GetFestivalNameDetails();
        #endregion

        #region IncentiveQSR
        BasePerPieceIncentiveDisplayReportList GetBasePerPieceIncentiveReportQSR(BasePerPieceIncentiveReportInputParamQSR Input);
        #endregion
    }
}
