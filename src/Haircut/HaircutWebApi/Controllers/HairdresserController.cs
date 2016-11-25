using Haircut.Database.Contract;
using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HaircutWebApi.Controllers
{
    public class HairdresserController : ApiController
    {
        private IHairdresserRepository _hairdresserRepository;

        public HairdresserController(IHairdresserRepository hairdresserRepository)
        {
            _hairdresserRepository = hairdresserRepository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok( _hairdresserRepository.GetAll() );
        }
    }
}
