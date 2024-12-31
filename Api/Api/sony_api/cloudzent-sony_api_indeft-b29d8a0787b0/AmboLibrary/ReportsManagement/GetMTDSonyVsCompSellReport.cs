using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class MTDSonyVsCompSellReport
    {
        public string C_Month { get; set; }
        public string L_Month { get; set; }
        public string L_2_Month { get; set; }
        public List<CompetitonData> Data { get; set; }
        public MTDSonyVsCompSellReport()
        {
            Data = new List<CompetitonData>();
        }
    }

    public class Model
    {
        public string ModelName { get; set; }
        public Int64 C_Month_qty { get; set; }
        public Int64 L_Month_Qty { get; set; }
        public Int64 L_2_Month_qty { get; set; }
    }
    public class CompetitonData
    {
        public string Company { get; set; }
        public List<Model> Model { get; set; }
        public CompetitonData()
        {
            Model = new List<Model>();
        }
    }
    public class ModelData
    {
        public string Company { get; set; }
        public string ModelName { get; set; }
        public Int64 C_Month_qty { get; set; }
        public Int64 L_Month_Qty { get; set; }
        public Int64 L_2_Month_qty { get; set; }
    }

    public class MTDSonyVsCompSellInput
    {
        public Int64 DealerId { get; set; }
        public Int64 ProductCatId { get; set; }
        public List<Int64> ProductSubCatIdList { get; set; }
        public MTDSonyVsCompSellInput()
        {
            ProductSubCatIdList = new List<Int64>();
        }
    }
}
