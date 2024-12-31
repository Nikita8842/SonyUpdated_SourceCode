using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboProvider.Interface;
using AmboDataServices.Interface;

namespace AmboDataServices.Implimentation
{
   public class ValidateDataService : IValidateDataService
    {
        private readonly IValidateTest _Validatetest;
        public ValidateDataService(IValidateTest ValidateTest)
        {
            _Validatetest = ValidateTest;
        }

        public bool ValidateTest()
        {
           return Convert.ToBoolean(_Validatetest.IValidateTest());
        }
    }
}
