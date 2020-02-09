using System;
using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static class HeaderManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestRequest"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IRestRequest AddRequestHeader(RestRequest RestRequest, string name, string value)
        {
            return RestRequest.AddHeader(name, value);
        }
    }
}
