using Haircut.Database.Contract;
using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Login Logar(int id)
        {
            try
            {
                return _loginRepository.GetById(id);
            }
            catch(Exception ex)
            {
                return new Login()
                {
                    Id = 99,
                    UserName = ex.ToString(),
                    Password = "Error"
                };
            }

        }
    }
}