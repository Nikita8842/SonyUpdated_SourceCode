using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace AmboUtilities.Helper
{
    public class GlobalExceptionLogger: ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext ex)
        {
            string folderPath = ConfigurationManager.AppSettings["TextLogPath"];
            if (!(Directory.Exists(folderPath)))
            {
                Directory.CreateDirectory(folderPath);
            }
            FileStream objFileStrome = new FileStream(folderPath + "\\Errlog.txt", FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStrome);
            objStreamWriter.WriteLine("Message: " + ex.Exception.Message);
            objStreamWriter.WriteLine("Inner Exception" + ex.Exception.InnerException);
            objStreamWriter.WriteLine("StackTrace: " + ex.Exception.StackTrace);
            objStreamWriter.WriteLine("Date/Time: " + DateTime.Now.ToString());
            objStreamWriter.WriteLine("============================================");
            objStreamWriter.Flush();
            objStreamWriter.Close();
            objFileStrome.Close();
        }
    }
}
