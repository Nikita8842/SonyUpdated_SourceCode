using AMBOModels.Global;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIShiftData
    {
        ErrorModel CreateShiftTiming(ShiftMaster objFormData);

        ErrorModel UpdateShiftTiming(ShiftMaster objFormData);

        ErrorModel DeleteShift(ShiftMaster objFormData);

        ShiftMaster GetShiftById(ShiftMaster Data);
    }
}
