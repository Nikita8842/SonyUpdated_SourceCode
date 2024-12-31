using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboServices.Interface;
using AmboDataServices.Interface;
using AmboUtilities;

namespace AmboServices.Implimentation
{
    public class ValidateService : IValidateService
    {
        private readonly IValidateDataService _IValdateDataService;

        public ValidateService(IValidateDataService IValidateDataService)
        {
            _IValdateDataService = IValidateDataService;
        }

        public Envelope<bool> Validate()
        {
            var IsValid = _IValdateDataService.ValidateTest();

            return (IsValid != false) ?
                new Envelope<bool> {Data=IsValid, MessageCode=201, Message="Data Validate" } :
                new Envelope<bool> {Data=IsValid, MessageCode=401, Message="Please try later" };
        }
    }
}
