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
    public class ScheduleController : ApiController
    {
        private IScheduleRepository _scheduleRepository;

        public ScheduleController(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [HttpGet]
        public IHttpActionResult Get(int loginId)
        {
            return Ok( _scheduleRepository.GetByLoginId(loginId) );
        }

        [HttpGet]
        public IHttpActionResult Get(DateTime from)
        {
            return Ok(_scheduleRepository.GetFromDate(from));
        }

        [HttpPut]
        public IHttpActionResult Put(Schedule schedule)
        {
            _scheduleRepository.Add(schedule);
            _scheduleRepository.Save();

            return Ok();
        }
    }
}
