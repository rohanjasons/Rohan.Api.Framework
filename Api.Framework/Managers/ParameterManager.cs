using System;
using System.Collections.Generic;
using Api.Framework.Context;
using Newtonsoft.Json;
using RestSharp;

namespace Api.Framework.Managers
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
        public static IRestRequest AddRequestParameter(this RestRequest restRequest, string name, object value, ParameterType parameterType)
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
        public static IRestRequest AddRequestParameter(this RestRequest restRequest, string name, string value, ParameterType parameterType)
        {
            return restRequest.AddParameter(name, value, parameterType);
        }


        /// <summary>
        /// This method receive multiple parameter which can be query string or header to add to the container to be sent to the API
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="parameters"> This dictionary contains multiple query string or headers </param>
        /// <param name="type"> To identify the parameter type </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestParameter(this RestRequest restRequest, Dictionary<string, string> parameters, ParameterType type)
        {
            switch (type)
            {
                case ParameterType.HttpHeader:

                    foreach (var (key, value) in parameters)
                    {
                        restRequest.AddHeader(key, value);
                    }
                    break;
                case ParameterType.QueryString:
                    foreach (var (key, value) in parameters)
                    {
                        restRequest.AddQueryParameter(key, value);
                    }
                    break;
            }
            return restRequest;
        }

        /// <summary>
        /// This method will receive the Parameter object containing key, name and parameter type to add to the container to be sent to the API
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="parameters"> This is object of Parameter which will contain key, value and type for each entry in list </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddRequestParameter(this RestRequest restRequest, IEnumerable<RequestParameter> parameters)
        {
            try
            {
                foreach (var parameter in parameters)
                {
                    restRequest.AddParameter(parameter.ParameterKey, parameter.ParameterValue, parameter.ParameterType);
                }
                return restRequest;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}