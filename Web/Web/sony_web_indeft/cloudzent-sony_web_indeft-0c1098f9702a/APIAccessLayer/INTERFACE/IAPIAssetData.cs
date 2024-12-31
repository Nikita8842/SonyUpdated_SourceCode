using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIAssetData
    {
        Envelope<bool> CreateAsset(AssetMaster objFormData);

        Envelope<bool> UpdateAsset(AssetMaster objFormData);

        Envelope<bool> DeleteAsset(AssetMaster objFormData);
    }
}
