using AMBOModels.Global;
using AMBOModels.MasterMaintenance;

namespace APIAccessLayer.INTERFACE
{
    public interface ISFALevelMaster
    {
        ErrorModel CreateSFALevel(SFALevelMasterData Data);
        ErrorModel DeleteSFALevel(SFALevelInput Data);
        SFALevel GetSFALevelById(SFALevelInput Data);
        ErrorModel UpdateSFALevel(SFALevelMasterData Data);
        ErrorModel CreateSFASubLevel(SFASubLevelMaster Data);
        ErrorModel DeleteSFASubLevel(SFASubLevelInput Data);
        SFASubLevel GetSFASubLevelMasterDataById(SFASubLevelInput Data);
        ErrorModel UpdateSFASubLevel(SFASubLevelMaster Data);
    }
}
