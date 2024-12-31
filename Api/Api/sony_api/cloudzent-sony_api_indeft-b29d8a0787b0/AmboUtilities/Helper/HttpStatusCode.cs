using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboUtilities.Helper
{
    /// <summary>
    /// Class contain HttpStatusCode
    /// consume by AmboDBService
    /// </summary>
    //class HttpStatusCode
    //{
    //    //public static explicit operator HttpStatusCode(NotAcceptable v)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}

    public enum Acceptable
    {
        // OK = 200, we are not going to use 200 anywhere in the code. 
        //200 is default status code fired by the API
        Created = 201,
        Accepted = 202,
        Found = 302        
    }

    public enum NotAcceptable
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        NotModified = 304
    }
}
