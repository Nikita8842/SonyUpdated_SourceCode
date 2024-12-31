using AmboLibrary.IncentiveManagement;
using AmboLibrary.WebReports;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IIncentiveService
    {
        Envelope<bool> ManageBaseIncentiveDefinition(CreateBaseIncentiveForm objFormData);
        Envelope<bool> DeleteBaseIncentiveDefinition(DeleteBaseIncentiveForm objFormData);
        Envelope<CreateBaseIncentiveForm> GetBaseIncentiveDefinitionByTargetCategoryId(GetBaseIncentive objFormData);
        Envelope<PerPieceIncentiveValues> GetPerPieceIncentiveExcelFile(DownloadPerPieceIncentiveExcel objDownloadData);
        Envelope<bool> ManagePerPieceIncentiveDefinition(CreatePerPieceIncentive objFormData);
        Envelope<CreatePerPieceIncentive> GetPerPieceIncentiveDefinitionBySchemeId(GetPerPieceIncentiveValues objFormData);
        Envelope<IEnumerable<PerPieceIncentiveSchemeGet>> GetPerPieceIncentiveSchemeByProductId(PerPieceIncentiveSchemeByProductId objFormData);
        Envelope<bool> ManagePerPieceMaterialMapping(PerPieceIncentiveCreate mappingData);
        Envelope<bool> DeletePerPieceIncentiveDefinition(DeletePerPieceIncentive objFormData);

        #region Festival Incentive
        Envelope<FestivalIncentiveDefinitionValues> GetFestivalIncentiveDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData);
        Envelope<FestivalIncentiveDefinitionValues> GetFestivalIncentiveSlabDefinitionExcelFile(DownloadFestivalIncentiveDefinitionExcel objDownloadData);
        Envelope<bool> ManageFestivalIncentiveDefinition(CreateFestivalIncentive objFormData);
        Envelope<bool> ManageFestivalIncentiveSlabDefinition(CreateFestivalIncentiveSlab objFormData);
        Envelope<CreateFestivalIncentive> GetFestivalIncentiveDefinitionBySchemeId(GetFestivalIncentiveValues objFormData);
        Envelope<CreateFestivalIncentiveSlab> GetFestivalIncentiveSlabDefinitionBySchemeId(GetFestivalIncentiveValues objFormData);
        Envelope<bool> DeleteFestivalIncentiveDefinition(DeleteFestivalIncentive objFormData);
        #endregion Festival Incentive

        #region IncentiveReport
        Envelope<BasePerPieceIncentiveDisplayReportList> GetBasePerPieceIncentiveReport(BasePerPieceIncentiveReportInputParam Input);
        Envelope<BasePerPieceIncentiveDetailReportList> GetBasePerPieceIncentiveDetailReport(BasePerPieceIncentiveReportInputParam Input);

        Envelope<FestivalIncentiveDisplayReportList> GetFestivalIncentiveReport(FestivalIncentiveReportInputParam Input);
        Envelope<FestivalIncentiveDetailReportList> GetFestivalIncentiveDetailReport(FestivalIncentiveReportInputParam Input);

        Envelope<FestivalSellThruTrackerList> GetFestivalSellThruTracker(FestivalSellThruTrackerInputParam Input);
        Envelope<FestivalNameDetailList> GetFestivalNameDetails();
        #endregion

        #region IncentiveQSR Nikita 9/03/2024
        Envelope<BasePerPieceIncentiveDisplayReportList> GetBasePerPieceIncentiveReportQSR(BasePerPieceIncentiveReportInputParamQSR Input);
        #endregion
    }
}
