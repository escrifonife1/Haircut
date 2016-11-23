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
        private string _errorMessage;

        public bool IsValideForRegister(Login login)
        {
            _errorMessage = string.Empty;

            if (string.IsNullOrEmpty(login.Name))
            {
                _errorMessage = "Informe o nome";
                return false;
            }

            if (string.IsNullOrEmpty(login.UserName))
            {
                _errorMessage = "Informe o usuário";
                return false;
            }

            if (string.IsNullOrEmpty(login.Phone))
            {
                _errorMessage = "Informe o telefone";
                return false;
            }

            if (string.IsNullOrEmpty(login.Password))
            {
                _errorMessage = "Informe a senha";
                return false;
            }

            return true;
        }

        public string ErrorMessage()
        {
            return _errorMessage;
        }

        public async Task<Login> Log(Login login)
        {
            var request = new RestRequest("login/", Method.PUT);
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
            /*IRestResponse response = await _client.ExecuteTaskAsync(request);
            var content = response.Content;*/ // raw content as string

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

        public async Task<T> PostFromRequestBody<T>(T data, string resource)
        {
            var request = new RestRequest($"{resource}/", Method.POST);
            request.AddHeader("Accept", "application/json");            
            request.Parameters.Clear();
            var loginJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", loginJson, ParameterType.RequestBody);
            
            /*IRestResponse response = await _client.ExecuteTaskAsync(request);
            var content = response.Content; */
            var responseData = await _client.ExecuteTaskAsync<T>(request);
			_errorMessage = responseData.ErrorMessage;

            if(responseData.StatusCode != System.Net.HttpStatusCode.OK)
            {                
                dynamic contentObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData.Content) ;
             
                _errorMessage = contentObject?.Message;
            }

            return responseData.Data;            
        }

        

        public async Task<Login> Register(Login login)
        {
            var lo = await PostFromRequestBody<Login>(login, "login");
            return lo;
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
            /*IRestResponse response = _client.Execute(request);
            var content = response.Content;*/ // raw content as string

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
