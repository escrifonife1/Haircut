using Haircut.Database.Contract;
using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace HaircutWebApi.Controllers
{
    public class LoginController : ApiController
    {
        private ILoginRepository _loginRepository;
        
           
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Login login;

            try
            {
                login = _loginRepository.GetById(id);
            }
            catch(Exception ex)
            {
                login = new Login()
                {
                    Id = 99,
                    UserName = ex.ToString(),
                    Password = "Error"
                };
            }

            return Ok(login);

        }
    }
}