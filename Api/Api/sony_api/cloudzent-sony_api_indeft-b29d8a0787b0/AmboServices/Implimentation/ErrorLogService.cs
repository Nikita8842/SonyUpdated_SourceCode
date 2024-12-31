using AmboServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.GlobalAccessible;
using AmboDataServices.Interface;

namespace AmboServices.Implimentation
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogDataService _IErrorLogDataService;

        public ErrorLogService(IErrorLogDataService IErrorLogDataService)
        {
            _IErrorLogDataService = IErrorLogDataService;
        }

        public void CreateErrorLogWeb(ErrorDetailsInput ErrorDetailsInput)
        {
            _IErrorLogDataService.CreateErrorLogWeb(ErrorDetailsInput);
        }
    }
}
