using AmboLibrary.GlobalAccessible;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IErrorLogService
    {
        void CreateErrorLogWeb(ErrorDetailsInput ErrorDetailsInput);
    }
}
