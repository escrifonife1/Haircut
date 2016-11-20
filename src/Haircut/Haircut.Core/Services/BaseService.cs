using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Services
{
    public abstract class BaseService
    {
        string baseUrl = "http://tiagopascal-001-site1.gtempurl.com/HaircutApi/api/";
        protected RestClient _client;

        public BaseService()
        {
            _client = new RestClient(baseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            
        }
    }
}
