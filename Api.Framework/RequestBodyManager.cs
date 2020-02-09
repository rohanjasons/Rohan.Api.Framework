using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static class RequestBodyManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestRequest"></param>
        /// <param name="requestObject"></param>
        /// <returns></returns>
        public static IRestRequest AddJsonRequestToBody(RestRequest RestRequest, object requestObject)
        {
            return RestRequest.AddJsonBody(requestObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestRequest"></param>
        /// <param name="requestObject"></param>
        /// <returns></returns>
        public static IRestRequest AddXmlRequestToBody(RestRequest RestRequest, object requestObject)
        {
            return RestRequest.AddXmlBody(requestObject);
        }
    }
}
