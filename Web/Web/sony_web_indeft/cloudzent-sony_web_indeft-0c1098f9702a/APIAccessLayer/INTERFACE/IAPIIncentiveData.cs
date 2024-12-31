using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.IncentiveManagement;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using AMBOModels.Reports;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIIncentiveData
    {
        #region Base Incentive
        Envelope<bool> ManageBaseIncentiveDefinition(CreateBaseIncentiveForm objFormData);
        Envelope<bool> DeleteBaseIncentiveDefinition(DeleteBaseIncentiveForm objFormData);
        Envelope<CreateBaseIncentiveForm> GetBaseIncentiveDefinitionByTargetCategoryId(GetBaseIncentive objFormData);
        #endregion Base Incentive

        #region Per Piece Incentive
        Envelope<PerPieceIncentiveValues> GetPerPieceIncentiveExcelFile(DownloadPerPieceIncentiveExcel objDownloadData);
        Envelope<bool> ManagePerPieceIncentiveDefinition(CreatePerPieceIncentive objFormData);
        Envelope<List<PerPieceIncentiveSchemeGet>> GetPerPieceIncentiveSchemeByProductId(PerPieceIncentiveSchemeByProductId objFormData);
        Envelope<bool> ManagePerPieceMaterialMapping(PerPieceIncentiveCreate mappingData);
        Envelope<CreatePerPieceIncentive> GetPerPieceIncentiveDefinitionBySchemeId(GetPerPieceIncentiveValues objFormData);
        Envelope<MaterialCodeGet> GetMaterialIdByMaterialCode(MaterialCodeGet objFormData);
        Envelope<bool> DeletePerPieceIncentiveDefinition(DeletePerPieceIncentive objFormData);
        #endregion Per Piece Incentive

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
        Envelope<BasePerPieceIncentiveDisplayReportList> GetBasePerPieceIncentiveReport(BasePerPieceIncentiveReportInputParam objFormData);
        Envelope<BasePerPieceIncentiveDetailReportList> GetBasePerPieceIncentiveDetailReport(BasePerPieceIncentiveReportInputParam objFormData);

        Envelope<FestivalIncentiveDisplayReportList> GetFestivalIncentiveReport(FestivalIncentiveReportInputParam objFormData);
        Envelope<FestivalIncentiveDetailReportList> GetFestivalIncentiveDetailReport(FestivalIncentiveReportInputParam objFormData);
        #endregion

        #region IncentiveQSR nikita 9/3/2024
        Envelope<BasePerPieceIncentiveDisplayReportList> GetBasePerPieceIncentiveReportQSR(BasePerPieceIncentiveReportInputParamQSR objFormData);
        #endregion
    }
}
