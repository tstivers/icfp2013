using System;
using System.Threading;
using RestSharp;

namespace GameClient.Extensions
{
    public static class RestClientExtensions
    {
        public static IRestResponse<T> GameExecute<T>(this IRestClient client, IRestRequest request) where T : new()
        {
            IRestResponse<T> response;

            do
            {
                response = client.Execute<T>(request);
                if ((int) response.StatusCode == 429)
                    Thread.Sleep(TimeSpan.FromSeconds(20));
            } while ((int) response.StatusCode == 429);

            return response;
        }
    }
}
