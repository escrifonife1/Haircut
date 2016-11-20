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
        [HttpGet]
        public Login Logar([FromBody] Login login)
        {
            return login;
        }
    }
}