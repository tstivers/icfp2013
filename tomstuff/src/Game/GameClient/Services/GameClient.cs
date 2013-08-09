using System;
using System.Collections.Generic;
using System.Net;
using GameClient.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace GameClient.Services
{
    public enum TrainingOperators
    {
        Empty,
        Tfold,
        Fold
    }

    public interface IGameClient
    {
        IEnumerable<Problem> GetProblems();
        Problem GetTrainingProblem(int size, TrainingOperators operators);
    }

    public class GameClient : IGameClient
    {
        public string AuthToken { get; set; }
        public string Endpoint { get; set; }
        private readonly IRestClient _client;

        public GameClient(string authToken, string endpoint = null)
        {
            AuthToken = authToken + "vpsH1H";
            Endpoint = endpoint ?? "http://icfpc2013.cloudapp.net/";
            _client = new RestClient(Endpoint);
        }

        public IEnumerable<Problem> GetProblems()
        {            
            var request = new RestRequest("myproblems?auth={auth}");
            request.Method = Method.POST;

            request.AddParameter("auth", AuthToken, ParameterType.UrlSegment);

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Service call returned invalid response");

            return JsonConvert.DeserializeObject<Problem[]>(response.Content);            
        }

        public Problem GetTrainingProblem(int size, TrainingOperators operators)
        {
            var trainRequest = new TrainRequest() {size = size};

            switch (operators)
            {
                case TrainingOperators.Empty:
                    break;
                case TrainingOperators.Fold:
                    trainRequest.operators = "fold";
                    break;
                case TrainingOperators.Tfold:
                    trainRequest.operators = "tfold";
                    break;
            }
        
            var request = new RestRequest("train?auth={auth}");
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("auth", AuthToken, ParameterType.UrlSegment);
            request.AddBody(trainRequest);

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Service call returned invalid response");

            if (response.ErrorException != null)
                throw response.ErrorException;

            return JsonConvert.DeserializeObject<Problem>(response.Content);            
        }
    }
}
