using System.Collections.Generic;
using RestSharp;

namespace Api.Framework.Managers
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
        public static IRestRequest AddRequestHeader(this RestRequest restRequest, string name, string value)
        {
            return restRequest.AddHeader(name, value);
        }

        /// <summary>
        /// This method receive multiple headers to add to the container to be sent to the API
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="headers"> The list of headers </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestHeader(this RestRequest restRequest, Dictionary<string, string> headers)
        {
            foreach (var (key, value) in headers)
            {
                restRequest.AddHeader(key, value);
            }
            return restRequest;
        }
    }
}