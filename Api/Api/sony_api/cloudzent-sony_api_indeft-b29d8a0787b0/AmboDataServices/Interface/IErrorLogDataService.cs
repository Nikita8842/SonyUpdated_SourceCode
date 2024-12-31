using AmboLibrary.GlobalAccessible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Interface
{
    public interface IErrorLogDataService
    {
        void CreateErrorLogWeb(ErrorDetailsInput ErrorDetailsInput);
    }
}
