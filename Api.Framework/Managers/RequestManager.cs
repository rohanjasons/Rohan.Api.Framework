using System;
using RestSharp;

namespace Api.Framework.Managers
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
        /// <param name="iRestRequest"> Container for data that is sent to API </param>
        /// <returns> Response from API </returns>
        public static IRestResponse SendRequestAndGetResponse(this RestClient restClient, IRestRequest iRestRequest)
        {
            try
            {
                return restClient.Execute(iRestRequest);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"When attempting to execute the request the following error occurred: {exception}");
                throw;
            }
        }
    }
}