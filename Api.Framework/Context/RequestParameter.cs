using RestSharp;

namespace Api.Framework.Context
{
    /// <summary>
    /// This class is used to create an object to receive the list of request parameter
    /// </summary>
    public class RequestParameter
    {
        /// <summary>
        /// this field will contain the Key of parameter to be added in request
        /// </summary>
        public string ParameterKey { get; set; }

        /// <summary>
        /// This field will contain the value of key
        /// </summary>
        public string ParameterValue { get; set; }

        /// <summary>
        /// This field will contain the parameter type to be added in request 
        /// </summary>
        public ParameterType ParameterType { get; set; }
    }
}