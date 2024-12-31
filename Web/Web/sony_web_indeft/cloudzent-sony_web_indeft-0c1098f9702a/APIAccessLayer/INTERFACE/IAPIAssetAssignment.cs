using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.Modules;
using APIAccessLayer.Helper;
using System.Data;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIAssetAssignment
    {
        Envelope<DataTable> UploadAssetCollectionFromSFA(AssetCollectionFromSFA objFormData);

        Envelope<bool> UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate objFormData);

        AssetAssignmentToRDIUpdate GetAssetAssignmentToRDIById(AssetAssignmentToRDI InputParam);

        Envelope<DataTable> UploadAssetAssignmentToRDI(AssetAssignmentToRDIUpload objFormData);

        Envelope<IEnumerable<AssetCollectionFromSFAData>> GetAssetCollectionFormatFromSFA(AssetCollectionFromSFAGet InputParam);
    }
}
