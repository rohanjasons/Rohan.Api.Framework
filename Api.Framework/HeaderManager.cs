using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// This class is to be used to add request headers to the container to be sent to the API
    /// </summary>
    public static class HeaderManager
    {
        /// <summary>
        /// This method adds a request header to the container to be sent to the API
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="name"> The name of the parameter </param>
        /// <param name="value"> The value of the parameter </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestHeader(RestRequest restRequest, string name, string value)
        {
            return restRequest.AddHeader(name, value);
        }
    }
}