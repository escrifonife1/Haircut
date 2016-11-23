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

            login = _loginRepository.GetByLogin(lo);

            return Ok(login);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Login lo)
        {
            Login login;

            login = _loginRepository.GetByUserName(lo.UserName);

            if(login != null)
            {
                return BadRequest("Esse usuário já esta cadastrado, por favor, informe outro nome de usuário!");
            }

            _loginRepository.Add(lo);
            _loginRepository.Save();
            login = _loginRepository.GetByLogin(lo);

            return Ok(login);
        }
    }
}