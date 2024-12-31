using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace AmboUtilities.Helper
{
    /// <summary>
    /// Class is responsible to Custom Exception &
    /// Return Custom Message Instead of detail of Error Message to the Client
    /// Team, ValueFirst (Gurugram, India)
    /// </summary>
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext Context)
        {
            if (Context.Exception != null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent(Context.Exception.StackTrace+'_'+Context.Exception.Message),
                    ReasonPhrase = "Exception"
                });
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An Error has Occurred, Please Contact the Administrator."),
                ReasonPhrase = "Critical Exception"
            });
        }
    }
}
