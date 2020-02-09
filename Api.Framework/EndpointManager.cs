using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static class EndpointManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetRequestEndpoint(RestClient RestClient, string endpoint)
        {
            return RestClient = new RestClient(endpoint);
        }
    }
}
