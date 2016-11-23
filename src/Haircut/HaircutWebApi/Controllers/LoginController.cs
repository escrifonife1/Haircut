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

        [HttpPut]
        public IHttpActionResult Put([FromBody] Login lo)
        {
            Login login;

            try
            {
                login = _loginRepository.GetByLogin(lo);
            }
            catch(Exception ex)
            {
                login = new Login()
                {
                    Id = 99999,
                    UserName = ex.ToString(),
                    Password = "Error"
                };
            }

            return Ok(login);

        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Login lo)
        {
            Login login;

            try
            {
                _loginRepository.Add(lo);
                login = _loginRepository.GetByLogin(lo);
            }
            catch (Exception ex)
            {
                login = new Login()
                {
                    Id = 99999,
                    UserName = ex.ToString(),
                    Password = "Error"
                };
            }

            return Ok(login);

        }
    }
}