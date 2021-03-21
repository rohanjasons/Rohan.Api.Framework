using System.Collections.Generic;
using RestSharp;

namespace Api.Framework.Managers
{
    /// <summary>
    /// The methods in this class can be used to add Query parameter to the container to be sent to the API
    /// </summary>
    public static class QueryParameterManager
    {
        /// <summary>
        /// Method for adding a query parameter to the container to be sent to the API. This can be used for adding a header, query parameter or request body to the container
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="name"> The name of the parameter </param>
        /// <param name="value"> The value of the parameter </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestQueryParameter(this RestRequest restRequest, string name, string value)
        {
            return restRequest.AddQueryParameter(name, value);
        }

        /// <summary>
        /// This method receive multiple Query parameter to add to the container to be sent to the API
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="queryParameters"> The list of parameters </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestQueryParameter(this RestRequest restRequest, Dictionary<string, string> queryParameters)
        {
            foreach (var (key, value) in queryParameters)
            {
                restRequest.AddQueryParameter(key, value);
            }
            return restRequest;
        }
    }
}