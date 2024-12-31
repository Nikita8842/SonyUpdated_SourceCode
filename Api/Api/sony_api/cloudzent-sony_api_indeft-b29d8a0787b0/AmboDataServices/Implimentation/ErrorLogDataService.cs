using AmboDataServices.Interface;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.GlobalAccessible;

namespace AmboDataServices.Implimentation
{
    public class ErrorLogDataService : IErrorLogDataService
    {
        private readonly IErrorLogProvider _IErrorLogProvider;

        public ErrorLogDataService(IErrorLogProvider IErrorLogProvider)
        {
            _IErrorLogProvider = IErrorLogProvider;
        }

        public void CreateErrorLogWeb(ErrorDetailsInput ErrorDetailsInput)
        {
             _IErrorLogProvider.CreateErrorLogWeb(ErrorDetailsInput);
        }
    }
}
