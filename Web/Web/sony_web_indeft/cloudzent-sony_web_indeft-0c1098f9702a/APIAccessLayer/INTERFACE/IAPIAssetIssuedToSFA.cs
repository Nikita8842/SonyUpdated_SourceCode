using AMBOModels.Modules;
using APIAccessLayer.Helper;
using System.Collections.Generic;
using System.Data;

namespace APIAccessLayer.INTERFACE
{
  public interface IAPIAssetIssuedToSFA
    {
        Envelope<DataTable> IssueAssetToSFA(AssetAssignmentToSFA InputParam);

        Envelope<IEnumerable<AssetIssuedToSFAGrid>> GetAssetIssuedToSFA(AssetIssuedToSFAGet InputParam);

        Envelope<IEnumerable<AssetsDropDownData>> GetAssetsDropDown(AssetIssuedToSFAGet InputParam);
    }
}
