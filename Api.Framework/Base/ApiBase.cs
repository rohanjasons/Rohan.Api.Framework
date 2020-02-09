using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ApiBase
    {
        protected RestClient RestClient;
        protected RestRequest RestRequest;
        protected IRestRequest IRestRequest;
        protected IRestResponse RestResponse;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="restRequest"></param>
        /// <param name="restResponse"></param>
        public ApiBase(RestClient restClient, RestRequest restRequest, IRestResponse restResponse)
        {
            RestClient = restClient;
            RestRequest = restRequest;
            RestResponse = restResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        public ApiBase() { }
    }
}