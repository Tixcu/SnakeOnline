using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SnakeGame
{
    public sealed class SnakeOnlineClient // Https Requests
    {
        const string Baseurl = "http://safeboard.northeurope.cloudapp.azure.com";
        private readonly RestClient _httpClient;

        public SnakeOnlineClient(string uri = Baseurl)
        {
            _httpClient = new RestClient(uri);
        }

        public async Task<string> GetNameAsync(string token)
        {
            var request = new RestRequest($"api/Player/name");
            request.AddQueryParameter("token", token);
            var response = await _httpClient.ExecuteGetTaskAsync<NameResponse>(request);
            if (response.ContentLength == 0)
            {
                return "Unauthorized";
            } else
            {
                return response.Data.Name;
            }
        }
        public async Task<GameboardResponse> GetGameboardAsync()
        {
            var request = new RestRequest($"api/Player/gameboard");
            var response = await _httpClient.ExecuteGetTaskAsync<GameboardResponse>(request);
            return response.Data;
        }

        public async Task SendDirection(string idirection, string itoken)
        {
            var request = new RestRequest($"api/Player/direction", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var temp = new TokenAndDirection
            {
                direction = idirection,
                token = itoken
            };
            var json = request.JsonSerializer.Serialize(temp);
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            var response = await _httpClient.ExecutePostTaskAsync(request);

        }
    }

    public sealed class TokenAndDirection
    {
        public string direction { get; set; }
        public string token { get; set; }
    }

    public sealed class NameResponse
    {
        public string Name { get; set; }
    }

    public sealed class GameboardResponse //Deserialization for Gameboard Get request
    {
        public bool IsStarted { get; set; } 
        public bool IsPaused { get; set; } 
        public int RoundNumber { get; set; } 
        public int TurnNumber { get; set; } 
        public int TurnTimeMilliseconds { get; set; } 
        public int TimeUntilNextTurnMilliseconds { get; set; } 
        public BoardSize GameboardSize { get; set; } 
        public int MaxFood { get; set; } 
        public List<PlayerState> Players { get; set; }
        public List<Point> Food { get; set; }
        public List<Wall> Walls { get; set; }
    }
    public sealed class BoardSize 
    {
        public int Width { get; set; }
        public int Heigth { get; set; }
    }
    public sealed class PlayerState
    {
        public string Name { get; set; }
        public bool IsSpawnProtected { get; set; }
        public List<Point> Snake { get; set; }

    }
    public sealed class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public sealed class Wall
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
