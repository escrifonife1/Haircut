using Haircut.Core.Contract;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Services
{
    public abstract class BaseService : IErrorMessages
    {
        string baseUrl = "http://tiagopascal-001-site1.gtempurl.com/HaircutApi/api/";
        protected RestClient _client;
        private string _errorMessage;

        public BaseService()
        {
            _client = new RestClient(baseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);            
        }

        public async Task<T> FromRequestBody<T>(T data, string resource, Method method)
        {
            var request = new RestRequest($"{resource}/", method);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            var loginJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", loginJson, ParameterType.RequestBody);

            var responseData = await _client.ExecuteTaskAsync<T>(request);
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
            _errorMessage = responseData.ErrorMessage;

            if (responseData.StatusCode != System.Net.HttpStatusCode.OK)
            {
                dynamic contentObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData.Content);

                _errorMessage = contentObject?.Message;
            }
        }

        public string ErrorMessage()
        {
            return _errorMessage;
        }

        public void SetErrorMessage(string message)
        {
            _errorMessage = message;
        }
    }
}
