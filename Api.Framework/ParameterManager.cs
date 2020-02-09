using Newtonsoft.Json;
using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static class ParameterManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestRequest"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        public static IRestRequest AddRequestParameter(RestRequest RestRequest, string name, object value, ParameterType parameterType)
        {
            var requestJson = JsonConvert.SerializeObject(value);
            return RestRequest.AddParameter(name, requestJson, parameterType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestRequest"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        public static IRestRequest AddRequestParameter(RestRequest RestRequest, string name, string value, ParameterType parameterType)
        {
            return RestRequest.AddParameter(name, value, parameterType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestRequest"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IRestRequest AddRequestQueryParameter(RestRequest RestRequest, string name, string value)
        {
            return RestRequest.AddQueryParameter(name, value);
        }
    }
}
