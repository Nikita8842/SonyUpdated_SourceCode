using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIRegionData
    {
        Envelope<bool> CreateRegion(RegionMaster objFormData);

        Envelope<bool> UpdateRegion(RegionMaster objFormData);

        Envelope<bool> DeleteRegion(RegionMaster objFormData);
    }
}
