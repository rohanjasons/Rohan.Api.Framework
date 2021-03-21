using RestSharp;

namespace Api.Framework.Base
{
    /// <summary>
    /// This is the base class that all API tests will inherit from.
    /// </summary>
    public abstract class ApiBase
    {
        /// <summary>
        ///  Client to translate RestRequests into HTTP requests and process response results
        /// </summary>
        protected static RestClient RestClient;

        /// <summary>
        /// Container for the data sent to the API
        /// </summary>
        protected static RestRequest RestRequest;
        
        /// <summary>
        /// Interface for the container for the data sent to the API
        /// Contains the Endpoint, Headers, Body etc
        /// </summary>
        protected static IRestRequest IRestRequest;

        /// <summary>
        /// Container for the data sent back from the API
        /// Contains the response status code, response content etc
        /// </summary>
        protected static IRestResponse RestResponse;

        /// <summary>
        /// Constructor used to initialise the API base
        /// </summary>
        /// <param name="restClient"> Client to translate RestRequests into HTTP requests and process response results </param>
        /// <param name="restRequest"> Container for the data sent to the API </param>
        /// <param name="iRestRequest"> Interface for the container for the data sent to the API </param>
        /// <param name="restResponse"> Container for the data sent back from the API </param>
        protected ApiBase(RestClient restClient, RestRequest restRequest, IRestRequest iRestRequest, IRestResponse restResponse)
        {
            RestClient = restClient;
            RestRequest = restRequest;
            RestResponse = restResponse;
            IRestRequest = iRestRequest;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        protected ApiBase() { }
    }
}