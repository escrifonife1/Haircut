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
        private ILoginRepository _loginRepository;
        private IHairdresserRepository _hairdresserRepository;

        public ScheduleController(IScheduleRepository scheduleRepository, ILoginRepository loginRepository, IHairdresserRepository hairdresserRepository)
        {
            _loginRepository = loginRepository;
            _scheduleRepository = scheduleRepository;
            _hairdresserRepository = hairdresserRepository;
        }

        [HttpGet]
        public IHttpActionResult Get(int loginId)
        {
            return Ok( _scheduleRepository.GetByLoginId(loginId) );
        }

        [HttpGet]
        public IHttpActionResult Get(DateTime from, int loginId)
        {
            return Ok(_scheduleRepository.GetFromDate(from, loginId));
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Schedule schedule)
        {
            if (schedule.Available == 0)
            {
                var sh = _scheduleRepository.GetById(schedule.Id);
                if (sh.Available == 0)
                {
                    return BadRequest("Esse horário não esta mais disponível!");
                }
                sh.Available = schedule.Available;
                schedule = sh;             
            }
            else
            {
                var login = _loginRepository.GetByUserName("admin");
                schedule.Login = login;
                schedule.LoginId = login.Id;
            }

            _scheduleRepository.Update(schedule);
            _scheduleRepository.Save();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            //var login = _loginRepository.GetById(7);
            
            var lastSchedule = _scheduleRepository.GetFromDate(DateTime.Today.AddHours(8), _loginRepository.GetByUserName("Admin").Id).OrderBy(s => s.Date).LastOrDefault();

            var horarioInicial = lastSchedule?.Date.AddDays(1) ?? DateTime.Today.AddHours(8);
            
            Schedule schedule;
            for (int i = 0; i < 23; i++)
            {
                schedule = new Schedule()
                {
                    Available = 1,
                    Date = horarioInicial,
                    Login = _loginRepository.GetByUserName("Admin"),
                    Hairdresser = _hairdresserRepository.GetById(1),
                };
                                
                horarioInicial = horarioInicial.AddMinutes(30);
                _scheduleRepository.Add(schedule);
                _scheduleRepository.Save();
            }
            
            return Ok();
        }
    }
}
