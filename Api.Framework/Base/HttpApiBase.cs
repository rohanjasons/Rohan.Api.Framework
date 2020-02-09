using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Framework.Base
{
    public class HttpApiBase
    {
        protected HttpResponseMessage HttpResponseMessage { get; set; }

        public HttpApiBase()
        {
        }

        /// <summary>
        /// Acquire access to Publish or Subscribe
        /// Gets the bearer token then requests the SAS token
        /// </summary>
        /// <returns>Access details for both regions</returns>
        public async Task<T> AcquireAccess<T>(string clientId, string clientSecret, string requestAccessUrl, string action)
        {
            var accessToken = await AcquireAccessToken(
                testConfig.RequestAccessAuthority,
                testConfig.RequestAccessResource,
                clientId,
                clientSecret);

            T accessResult;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                _results = await client.GetAsync($"{requestAccessUrl}/api/RequestAccess/" + action);
                accessResult = JsonConvert.DeserializeObject<T>(await _results.Content.ReadAsStringAsync());
            }
            return accessResult;
        }

        /// <summary>
        /// Send a message to the Topic in both regions
        /// </summary>
        /// <param name="accessResult">The topic access details</param>
        /// <param name="message">Message to send</param>
        /// <returns>void</returns>
        protected async Task SendMessage(PublisherRequestAccessResult accessResult, TestMessage message)
        {
            foreach (PublishRequestAccessRegionResult region in accessResult.Regions)
            {

                var builder = new ServiceBusConnectionStringBuilder(
                    region.Endpoint,
                    accessResult.TopicName,
                    region.SasToken);

                var client = new TopicClient(builder);

                await client.SendAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))));
            }
        }

        /// <summary>
        /// Receive a message from the Topic Subscription in both regions
        /// </summary>
        /// <param name="accessResult">The subscription access details</param>
        /// <returns>List of messages received from the subscription of each region</returns>
        protected async Task<IList<TestMessage>> ReceiveMessage(SubscriberRequestAccessResult accessResult)
        {
            IList<TestMessage> testMessages = new List<TestMessage>();

            foreach (SubscriberRequestAccessRegionResult region in accessResult.Regions)
            {
                var builder = new ServiceBusConnectionStringBuilder(region.Endpoint, accessResult.TopicName, region.SasToken);

                // Setup the message receiver
                var client = new MessageReceiver(
                    new ServiceBusConnection(builder),
                    $"{accessResult.TopicName}/Subscriptions/{accessResult.SubscriptionName}",
                    ReceiveMode.ReceiveAndDelete)
                {
                    OperationTimeout = TimeSpan.FromSeconds(5)
                };

                Message message = null;

                message = await client.ReceiveAsync();

                testMessages.Add(JsonConvert.DeserializeObject<TestMessage>(Encoding.UTF8.GetString(message.Body)));
            }

            return testMessages;
        }

        /// <summary>
        /// Receive a message from the Topic Subscription 
        /// </summary>
        /// <param name="accessResult">The subscription access details</param>
        /// <returns>Message received from the subscription</returns>
        protected async Task<Object> ReceiveMessage()
        {
            // Setup the message receiver
            var client = new MessageReceiver(new ServiceBusConnection(builder),
                $"{accessResult.TopicName}/Subscriptions/{alternativeAccessResult.SubscriptionName}",
                ReceiveMode.ReceiveAndDelete)
            {
                OperationTimeout = TimeSpan.FromSeconds(5)
            };



            message = await client.ReceiveAsync();

            return JsonConvert.DeserializeObject<TestMessage>(Encoding.UTF8.GetString(message.Body));
        }

        /// <summary>
        /// Deletes a subscription from the Service Bus Topic
        /// </summary>
        protected async Task DeleteSubscription(string subscriptionName, string subscriptionId, string resourceGroupName, string nameSpaceName)
        {
            var accessToken = await AcquireAccessToken(testConfig.RequestAccessAuthority, testConfig.ServiceBusResource, testConfig.RequestAccessClientId, testConfig.RequestAccessClientSecret);

            using (var client = new ServiceBusManagementClient(new TokenCredentials(accessToken)))
            {
                client.SubscriptionId = subscriptionId;

                if ((await client.Subscriptions.ListByTopicAsync(resourceGroupName, nameSpaceName, testConfig.TopicName)).Any(s => s.Name == subscriptionName))
                {
                    await client.Subscriptions.DeleteAsync(resourceGroupName, nameSpaceName, testConfig.TopicName, subscriptionName);
                }
            }
        }

        /// <summary>
        /// Acquire and AAD access token to make authenticated calls to the Resource Manager
        /// </summary>
        /// <param name="authority">Aazure AD Authority to authenticate against</param>
        /// <param name="resource">The indentifier of the Azure resource to be accessed</param>
        /// <param name="clientId">ClientId of the requesting application</param>
        /// <param name="clientSecret">ClientSecret of the requesting application</param>
        /// <returns>Bearer token</returns>
        protected async Task<string> AcquireAccessToken(string authority, string resource, string clientId, string clientSecret)
        {
            var context = new AuthenticationContext(authority);
            var result = await context.AcquireTokenAsync(resource, new ClientCredential(clientId, clientSecret));
            return result.AccessToken;
        }
    }
}
