﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameClient.Extensions;
using GameClient.ViewModels;
using log4net;
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
        ulong[] Eval(string id, string program, ulong[] inputs);
        GuessResponse Guess(string id, string program);
    }

    public class GameClient : IGameClient
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IRestClient _client;
        public string AuthToken { get; set; }
        public string Endpoint { get; set; }

        public GameClient(string authToken, string endpoint = null)
        {
            AuthToken = authToken + "vpsH1H";
            Endpoint = endpoint ?? "http://icfpc2013.cloudapp.net/";
            _client = new RestClient(Endpoint);
        }

        #region IGameClient Members

        public IEnumerable<Problem> GetProblems()
        {
            var request = new RestRequest("myproblems?auth={auth}");
            request.Method = Method.POST;

            request.AddParameter("auth", AuthToken, ParameterType.UrlSegment);

            var response = _client.GameExecute<List<Problem>>(request);

            return response.Data;
        }

        public Problem GetTrainingProblem(int size, TrainingOperators operators)
        {
            var trainRequest = new TrainRequest {size = size};

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

            var response = _client.GameExecute<Problem>(request);

            return response.Data;
        }

        public ulong[] Eval(string id, string program, ulong[] inputs)
        {
            var evalRequest = new EvalRequest
            {
                id = id,
                program = program,
                arguments = inputs.Select(x => "0x" + x.ToString("X")).ToArray()
            };

            var request = new RestRequest("eval?auth={auth}");
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("auth", AuthToken, ParameterType.UrlSegment);
            request.AddBody(evalRequest);

            var response = _client.GameExecute<EvalResponse>(request);

            var evalResponse = response.Data;

            if (evalResponse.Status != "ok")
                throw new Exception(evalResponse.Message);

            return evalResponse.Outputs.Select(x => Convert.ToUInt64(x, 16)).ToArray();
        }

        public GuessResponse Guess(string id, string program)
        {
            var guessRequest = new GuessRequest {id = id, program = program,};

            var request = new RestRequest("guess?auth={auth}");
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("auth", AuthToken, ParameterType.UrlSegment);
            request.AddBody(guessRequest);

            var response = _client.GameExecute<GuessResponse>(request);

            var guessResponse = response.Data;

            if (guessResponse.Status == "error")
                throw new Exception(guessResponse.Message);

            return guessResponse;
        }

        #endregion
    }
}
