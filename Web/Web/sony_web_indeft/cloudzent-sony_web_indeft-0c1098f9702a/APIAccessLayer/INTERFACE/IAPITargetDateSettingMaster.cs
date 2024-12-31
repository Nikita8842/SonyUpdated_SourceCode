using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIAccessLayer.Helper;
using AMBOModels.MasterMaintenance;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPITargetDateSettingMaster
    {
        Envelope<bool> UpdateTargetDateSettingMaster(TargetDateSettingMaster objFormData);

        Envelope<bool> CreateTargetDateSettingMaster(TargetDateSettingMaster objFormData);

        TargetDateSettingMaster GetTargetDateSettingMasterById(TargetDateSettingMaster InputParam);
    }
}
