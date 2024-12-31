using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class MaterialMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set;}

        public Int64 Id { get; set; }
        public string MaterialCode { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Required")]
        public Int64 ProductCategoryId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Required")]
        public Int64 ProductSubCategoryId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "MOP is Required")]
        //[Range(0,int.MaxValue, ErrorMessage ="Please enter a valid MOP")]
        public string MOP { get; set; }
        public bool Status { get; set; }
        public bool Discontinued { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
        public Int64 SizeId { get; set; }
        public Int64 SegmentId { get; set; }
        public Int64 InternetId { get; set; }
        public Int64 TvTypeId { get; set; }
        public Int64 Id3D { get; set; }
        public Int64 ResolutionId { get; set; }
        public bool IsCombo { get; set; }
    }

    public class MaterialGridData : MaterialMaster
    {
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string Division { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class Size
    {
        public Int64 SizeId { get; set; }
        public string SizeName { get; set; }
    }
    public class Segment
    {
        public Int64 SegmentId { get; set; }
        public string SegmentName { get; set; }
    }
    public class D3
    {
        public Int64 D3Id { get; set; }
        public string D3Name { get; set; }
    }
    public class Internet
    {
        public Int64 InternetId { get; set; }
        public string InternetName { get; set; }
    }
    public class Resolution
    {
        public Int64 ResolutionId { get; set; }
        public string ResolutionName { get; set; }
    }
    public class TVType
    {
        public Int64 TVTypeId { get; set; }
        public string TVTypeName { get; set; }
    }
    public class MaterialCodeGet
    {
        public Int64 MaterialId { get; set; }
        public string MaterialCode { get; set; }
    }

    public class AttributeGet
    {
        public Int64 ProductCategoryId { get; set; }
    }

    public class MaterialDDInput
    {
        public Int64 ProSubCatId { get; set; }
    }

}
