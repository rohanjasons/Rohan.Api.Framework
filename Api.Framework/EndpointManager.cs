using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// This class is for the purpose of setting an endpoint to be used within a request
    /// </summary>
    public static class EndpointManager
    {
        /// <summary>
        /// Method to set the endpoint that would be used within a request
        /// </summary>
        /// <param name="restClient"> Client to translate RestRequests into HTTP requests and process response results </param>
        /// <param name="endpoint"> Endpoint required for request </param>
        /// <returns> Returns a restClient with an endpoint </returns>
        public static RestClient SetRequestEndpoint(RestClient restClient, string endpoint)
        {
            return restClient = new RestClient(endpoint);
        }
    }
}
