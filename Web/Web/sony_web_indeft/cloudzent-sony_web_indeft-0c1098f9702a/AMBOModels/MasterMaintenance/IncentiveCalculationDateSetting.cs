using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class IncentiveCalculationDateSettingParam
    {
        public Int64 CreatedBy { get; set; }
    }
    public class IncentiveCalculationDateSetting
    {
        public Int64 Id { get; set; }
        [Required(ErrorMessage ="Please select month")]
        public string Month { get; set; }
        public int CollectionDay { get; set; }
        public Int64 CreatedBy { get; set; }
    }
    public class IncentiveCalculationDateSettingList
    {
        public List<IncentiveCalculationDateSetting> IncentiveCalculationDateSettingData { get; set; }
        public IncentiveCalculationDateSettingList()
        {
            IncentiveCalculationDateSettingData = new List<IncentiveCalculationDateSetting>();
        }
    }
}
