using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static class RequestManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestClient"></param>
        /// <param name="RestResponse"></param>
        /// <param name="restRequest"></param>
        /// <returns></returns>
        public static IRestResponse SendRequestAndGetResponse(RestClient RestClient, IRestResponse RestResponse, RestRequest restRequest)
        {
            return RestResponse = RestClient.Execute(restRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestClient"></param>
        /// <param name="request"></param>
        public static void SendRequest(RestClient RestClient, RestRequest request)
        {
            RestClient.Execute(request);
        }
    }
}
