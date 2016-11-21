using Haircut.Core.Contract;
using Haircut.Model.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Services
{
    public class LoginService : BaseService, ILoginService
    {
        public async Task<Login> Log(Login login)
        {
            var request = new RestRequest("login/", Method.POST);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "1"); // replaces matching token in request.Resource
            //request.AddBody(login);
            request.AddHeader("Accept", "application/json");
            //request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.Parameters.Clear();
            var loginJson = Newtonsoft.Json.JsonConvert.SerializeObject(login);
            request.AddParameter("application/json", loginJson, ParameterType.RequestBody);

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            IRestResponse response = _client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var userResponse = await _client.ExecuteTaskAsync<Login>(request);
            var user = userResponse.Data;
            return user;
            //var name = user;

            //// easy async support
            //_client.ExecuteAsync(request, response => {
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = _client.ExecuteAsync<Person>(request, response => {
            //    Console.WriteLine(response.Data.Name);
            //});

            //// abort the request on demand
            //asyncHandle.Abort();            
        }

        public Login Log1(Login login)
        {
            var request = new RestRequest("login/{id}", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("id", "1"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            IRestResponse response = _client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var userResponse = _client.Execute<Login>(request);
            var user = userResponse.Data;
            return user;
            //var name = user;

            //// easy async support
            //_client.ExecuteAsync(request, response => {
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = _client.ExecuteAsync<Person>(request, response => {
            //    Console.WriteLine(response.Data.Name);
            //});

            //// abort the request on demand
            //asyncHandle.Abort();            
        }
    }
}
