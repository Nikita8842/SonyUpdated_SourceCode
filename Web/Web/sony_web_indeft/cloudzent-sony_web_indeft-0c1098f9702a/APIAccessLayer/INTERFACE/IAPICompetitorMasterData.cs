using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPICompetitorMasterData
    {
        Envelope<bool> InsertCompetitorMaster(CompetitorMaster InputParam);
        Envelope<bool> UpdateCompetitorMaster(CompetitorMaster InputParam);
        Envelope<bool> DeleteCompetitorMaster(CompetitorMaster InputParam);

        Envelope<bool> InsertCompetitorProduct(CompetitorProductData InputParam);
        Envelope<bool> UpdateCompetitorProduct(CompetitorProductData InputParam);
        Envelope<bool> DeleteCompetitorProduct(CompetitorProductData InputParam);

        Envelope<bool> InsertCompetitorModel(CompetitorModelMaster InputParam);
        Envelope<bool> UpdateCompetitorModel(CompetitorModelMaster InputParam);
        Envelope<bool> DeleteCompetitorModel(CompetitorModelMaster InputParam);
        CompetitorModelList GetCompetitorModelById(CompetitorDataInput competitorModelId);

        IEnumerable<CompetitorMaster> GetCompetitors();
        IEnumerable<CompetitorModelMaster> GetAllCompetitorModels();
    }
}
