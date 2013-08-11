using System;
using System.Net;
using System.Threading;
using GameClient.Exceptions;
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
                    Thread.Sleep(TimeSpan.FromSeconds(1));
            } while ((int) response.StatusCode == 429);

            if ((int)response.StatusCode == 410)
                throw new ProblemExpiredException();

            if ((int) response.StatusCode == 412)
                throw new AlreadySolvedException();

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Service call returned invalid response");

            if (response.ErrorException != null)
                throw response.ErrorException;

            return response;
        }
    }
}
