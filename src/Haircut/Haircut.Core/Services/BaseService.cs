using Haircut.Core.Contract;
using RestSharp;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Haircut.Core.Services
{
    public abstract class BaseService : IBaseService
    {
        string baseUrl = "http://tiagopascal-001-site1.gtempurl.com/HaircutApi/api/";
        protected RestClient _client;
        private string _errorMessage;

        public BaseService()
        {
            _client = new RestClient(baseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);            
        }

        public async Task<TOut> Get<TIn, TOut>(TIn data, string resource, string paramName)
        {
            var request = new RestRequest($"{resource}/{paramName}", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter($"{paramName}", data.ToString()); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            /*IRestResponse response = _client.Execute(request);
            var content = response.Content;*/ // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var responseData = await  _client.Execute<TOut>(request);
            AddErrorMessageIfNeeded(responseData);
            return responseData.Data;
        }

        public async Task<TOut> Get<TOut>(string resource)
        {
            var request = new RestRequest($"{resource}/", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            
            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            /*IRestResponse response = _client.Execute(request);
            var content = response.Content;*/ // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var responseData = await _client.Execute<TOut>(request);
            AddErrorMessageIfNeeded(responseData);
            return responseData.Data;            
        }

        public async Task<T> FromRequestBody<T>(T data, string resource, Method method)
        {
            var request = new RestRequest($"{resource}/", method);
            request.AddHeader("Accept", "application/json");
			request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.Parameters.Clear();
            var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", dataJson, ParameterType.RequestBody);

            var responseData = await ExecuteTaskAsync<T>(request);
            AddErrorMessageIfNeeded(responseData);

            return responseData.Data;
        }

        public async Task<T> PostFromRequestBody<T>(T data, string resource)
        {
            return await FromRequestBody(data, resource, Method.POST);
        }       

        public async Task<T> PutFromRequestBody<T>(T data, string resource)
        {
            return await FromRequestBody(data, resource, Method.PUT);
        }

        private void AddErrorMessageIfNeeded<T>(IRestResponse<T> responseData)
        {
            //_errorMessage = responseData.ErrorMessage;

            if (responseData.StatusCode != System.Net.HttpStatusCode.OK)
            {
                dynamic contentObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData.Content);

                _errorMessage = contentObject?.Message;
            }

				
        }

        public async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            var response = await _client.Execute<T>(request, token);
            AddErrorMessageIfNeeded(response);
            return response;
        }

        public async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            var response = await _client.Execute<T>(request);
            AddErrorMessageIfNeeded(response);
            return response;
        }

        public string ErrorMessage()
        {
            return _errorMessage;
        }

        public bool HasMessageError()
        {
            return !string.IsNullOrWhiteSpace(ErrorMessage());
        }

        public void SetErrorMessage(string message)
        {
            _errorMessage = message;
        }
    }
}
