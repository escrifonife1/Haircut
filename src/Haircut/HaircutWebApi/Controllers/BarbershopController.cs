using Haircut.Database.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HaircutWebApi.Controllers
{
    public class BarbershopController : ApiController
    {
        private IBarbershopRepository _barbershopRepository;

        public BarbershopController(IBarbershopRepository barbershopRepository)
        {
            _barbershopRepository = barbershopRepository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_barbershopRepository.GetAll());
        }
    }
}
