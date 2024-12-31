using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.Abstract;
using System.Data;
using AMBOModels.ABSTRACT;
using System.ComponentModel.DataAnnotations;

namespace AMBOModels.Modules
{
    public class AssetAssignmentToRDI : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64? Id { get; set; }

    }

    public class AssetAssignmentToRDIUpdate : AssetAssignmentToRDI
    {
        public int IssuedQty { get; set; }
    }

    public class AssetAssignmentToRDIGet
    {
        public string Reference { get; set; }
    }

    public class AssetAssignmentToRDIUpload : DataTableAbstract
    {
        public override DataTable dtAsset { get; set; }
    }

    public class AssetAssignmentToRDIGrid : AssetAssignmentToRDIUpdate
    {
        public Int64 MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public string IssuedDate { get; set; }
        public Int64 RDIId { get; set; }
        public string RDIName { get; set; }
        public string RDICode { get; set; }
        public string Place { get; set; }
        public string Reason { get; set; }
    }

    public class AssetAssignmentToRDUploadOutput
    {
        public string Reference { get; set; }
        public string MaterialCode { get; set; }
        public string ProductName { get; set; }
        public string IssuedQty { get; set; }
        public string IssuedDate { get; set; }
        public string RDIName { get; set; }
        public string Place { get; set; }
        public string ReturnDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
