using AMBOModels.Abstract;
using AMBOModels.GlobalAccessible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class CompetitorModelMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 CompetitorProductCatID { get; set; }
        public Int64 SonyProductSubCategory { get; set; }
        public string ModelName { get; set; }
        public bool Discontinued { get; set; }
        public bool Status { get; set; }
        public Int64 SonyMaterialId { get; set; }
        public bool IsActive { get; set; }
        
        
        public Int64 SizeValueId { get; set; }
        public Int64 SegmentValueId { get; set; }
        public Int64 InternetValueId { get; set; }
        public Int64 TvTypeValueId { get; set; }
        public Int64 Id3DValue { get; set; }
        public Int64 ResolutioValueId { get; set; }
    }
    public class CompetitorModelList : CompetitorModelMaster
    {
        public string CompetitorCode { get; set; }
        public string CompetitorProductCategory { get; set; }
        public string SonyProductCategory { get; set; }
        public string SonyProductSubCategoryName { get; set; }
        public string SonyModel { get; set; }
        public Int64 SonyProductCategoryId { get; set; }
        public Int64 CompetitorId { get; set; }
        public string CompetitorModelStatus { get; set; }
    }

    public class CompetitorData
    {
        public Int64 SProductCatId { get; set; }
        public String SProductCatName { get; set; }
        public List<CompetitorProducts> SProductSubCatList { get; set; }
        public CompetitorData()
        {
            SProductSubCatList = new List<CompetitorProducts>();
        }
    }

    public class CompetitorDataInput
    {
        public Int64 CProductCatId { get; set; }
    }
}
