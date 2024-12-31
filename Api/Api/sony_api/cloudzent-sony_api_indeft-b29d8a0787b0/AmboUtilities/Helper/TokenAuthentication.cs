using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace AmboUtilities.Helper
{
    /// <summary>
    /// Token validation get value(Encrypt) from the request Header
    /// Team ValueFirst(Gurugram,India)
    /// </summary>

    public class TokenAuthentication : DelegatingHandler
    {
        public string key { get; set; }
        public string value { get; set; }

        const string Origin = "Origin";
        const string AccessControlRequestMethod = "Access-Control-Request-Method";
        const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        public TokenAuthentication(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
        
        /// <summary>
        /// Get Header value & Validate it with default one
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>return based on Header value.If matched then pass control to respective controller otherwise return back to client</returns>

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {


            bool isCorsRequest = request.Headers.Contains(Origin);
            bool isPreflightRequest = request.Method == HttpMethod.Options;
            if (isCorsRequest)
            {
                if (isPreflightRequest)
                {
                    return Task.Factory.StartNew<HttpResponseMessage>(() =>
                    {
                        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                        response.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());

                        string accessControlRequestMethod = request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();
                        if (accessControlRequestMethod != null)
                        {
                            response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
                        }

                        string requestedHeaders = string.Join(", ", request.Headers.GetValues(AccessControlRequestHeaders));
                        if (!string.IsNullOrEmpty(requestedHeaders))
                        {
                            response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);
                        }

                        return response;
                    }, cancellationToken);
                }
                else
                {
                    var credentials = validatekey(request);
                    if (credentials != null && credentials[0].Trim() == key &&
                        credentials[1].Trim() == value)
                    {
                        return base.SendAsync(request, cancellationToken);
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }

                }
            }
            else
            {
                var credentials = validatekey(request);
                if (credentials != null && credentials[0].Trim() == key &&
                    credentials[1].Trim() == value)
                {
                    return base.SendAsync(request, cancellationToken);
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
                // return base.SendAsync(request, cancellationToken);
            }

        }


        /// <summary>
        /// This method get the header value
        /// decrypt it and match with the values 
        /// </summary>
        /// <param name="message"></param>
        /// <returns>return header values</returns>
        private string[] validatekey(HttpRequestMessage message)
        {
            IEnumerable<string> Messagevalue = null;

            if (message.Headers == null) return null;

            if (message.Headers.TryGetValues("Authorization", out Messagevalue))
            {
                var MessageKeyValue = Messagevalue.First().Split(':');
                var credential = Messagevalue.First().Split(':');

                var credentials = Encoding.ASCII.GetString(
                     Convert.FromBase64String(MessageKeyValue[0])) + ':' + Encoding.ASCII.GetString(
                     Convert.FromBase64String(MessageKeyValue[1]));

                credential = credentials.Split(':');
                return credential.Length == 2 ? credential : null;
            }
            return null;
        }
    }


    public class UploadMultipartMediaTypeFormatter : MediaTypeFormatter
    {
        private static Type _supportedType = typeof(byte[]);
        public UploadMultipartMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            // return type == _supportedType;
            return true;
        }

    }
}
