using Newtonsoft.Json;
using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// The methods in this class can be used to add parameters to the container to be sent to the API
    /// </summary>
    public static class ParameterManager
    {
        /// <summary>
        /// Method for adding a parameter to the container to be sent to the API. This can be used for adding a header, query parameter or request body to the container
        /// This allows a JSON object to be added to the container
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="name"> The name of the parameter </param>
        /// <param name="value"> The value of the parameter </param>
        /// <param name="parameterType"> Types of parameter that can be added to a request </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestParameter(RestRequest restRequest, string name, object value, ParameterType parameterType)
        {
            var requestJson = JsonConvert.SerializeObject(value);
            return restRequest.AddParameter(name, requestJson, parameterType);
        }

        /// <summary>
        /// Method for adding a parameter to the container to be sent to the API. This can be used for adding a header, query parameter or request body to the container
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="name"> The name of the parameter </param>
        /// <param name="value"> The value of the parameter </param>
        /// <param name="parameterType"> Types of parameter that can be added to a request </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestParameter(RestRequest restRequest, string name, string value, ParameterType parameterType)
        {
            return restRequest.AddParameter(name, value, parameterType);
        }

        /// <summary>
        /// Method for adding a query parameter to the container to be sent to the API. This can be used for adding a header, query parameter or request body to the container
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="name"> The name of the parameter </param>
        /// <param name="value"> The value of the parameter </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestQueryParameter(RestRequest restRequest, string name, string value)
        {
            return restRequest.AddQueryParameter(name, value);
        }
    }
}