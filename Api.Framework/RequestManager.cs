using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// This class contains all the functions for sending requests to the API
    /// </summary>
    public static class RequestManager
    {
        /// <summary>
        /// Sends a request to the API and returns the response
        /// </summary>
        /// <param name="restClient"> Client to translate RestRequests into HTTP requests and process response results </param>
        /// <param name="iRestResponse"> Container for data sent back from API </param>
        /// <param name="iRestRequest"> Container for data that is sent to API </param>
        /// <returns> Response from API </returns>
        public static IRestResponse SendRequestAndGetResponse(RestClient restClient, IRestResponse iRestResponse, IRestRequest iRestRequest)
        {
            return iRestResponse = restClient.Execute(iRestRequest);
        }

        /// <summary>
        /// Sends a request to the API does not return a response
        /// </summary>
        /// <param name="restClient"> Client to translate RestRequests into HTTP requests and process response results </param>
        /// <param name="iRestRequest"> Container for data that is sent to API </param>
        public static void SendRequest(RestClient restClient, IRestRequest iRestRequest)
        {
            restClient.Execute(iRestRequest);
        }
    }
}