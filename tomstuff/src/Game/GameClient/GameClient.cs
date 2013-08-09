using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using Shared.ViewModels;

namespace GameClient
{
    public class GameClient
    {
        public string AuthToken { get; set; }
        public string Endpoint { get; set; }

        public GameClient(string authToken, string endpoint = null)
        {
            AuthToken = authToken + "vpsH1H";
            Endpoint = endpoint ?? "http://icfpc2013.cloudapp.net/";
        }

        public IEnumerable<Problem> GetProblems()
        {
            var client = new RestClient(Endpoint);
            var request = new RestRequest("myproblems?auth={auth}");
            request.Method = Method.POST;

            request.AddParameter("auth", AuthToken, ParameterType.UrlSegment);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Service call returned invalid response");

            return JsonConvert.DeserializeObject<Problem[]>(response.Content);
            ;
        }
    }
}
