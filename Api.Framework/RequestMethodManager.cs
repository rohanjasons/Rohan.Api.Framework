using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static class RequestMethodManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RestRequest"></param>
        /// <param name="method"></param>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        public static RestRequest SetRequestMethod(RestRequest RestRequest, Method method, DataFormat dataFormat)
        {
            RestRequest = new RestRequest(method)
            {
                RequestFormat = dataFormat
            };

            return RestRequest;
        }
    }
}
