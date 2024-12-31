using AmboLibrary.GlobalAccessible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboProvider.Interface
{
    public interface IErrorLogProvider
    {
        void CreateErrorLog(string ErrorSource, string ErrorDetails, string ErrorMessage);

        void CreateErrorLogWeb(ErrorDetailsInput ErrorDetailsInput);
    }
}
