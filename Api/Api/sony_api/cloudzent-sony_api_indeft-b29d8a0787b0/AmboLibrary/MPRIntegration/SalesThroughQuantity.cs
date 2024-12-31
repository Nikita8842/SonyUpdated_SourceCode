using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MPRIntegration
{
    public class SalesThroughQuantityInput
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string PrimaryCode { get; set; }
    }

    public class SalesThroughQuantityOutput
    {
        public string Month { get; set; }
        public string Branch { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string CityType { get; set; }
        public string Location { get; set; }
        public string DealerName { get; set; }
        public string ProductCategory { get; set; }
        public string Company { get; set; }
        public Int64 SalesThroughQuantity { get; set; }
        public int CounterSIze { get; set; }
        public Int64 Value { get; set; }
    }
}
