using Haircut.Core.Contract;
using Haircut.Model.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Services
{
    public class SchedulesService : BaseService, ISchedulesService
    {
        public async Task<List<Schedule>> Availables(DateTime from, int loginId)
        {
            var request = new RestRequest("schedule/{from}", Method.GET);
            //var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(login);
            request.AddParameter("from", from.ToString()); // replaces matching token in request.Resource
            request.AddParameter("loginId", loginId); // replaces matching token in request.Resource

            var userResponse = await _client.ExecuteTaskAsync<List<Schedule>>(request);
            return userResponse.Data;

            /*
            var disponiveis = new List<string>();
            var horarioInicial = DateTime.Today.AddHours(8);

            for (int i = 0; i < 23; i++)
            {
                disponiveis.Add(horarioInicial.ToString("dd/MM/yyyy hh:mm"));
                horarioInicial = horarioInicial.AddMinutes(30);
            }

            return Task.FromResult(disponiveis);*/
        }

        public async Task Schedule(Schedule schedule)
        {
            await PutFromRequestBody(schedule, "schedule");
        }

        public async Task<Schedule> ScheduleByLogin(Login login)
        {
            var request = new RestRequest("schedule/{loginId}", Method.GET);            
            request.AddUrlSegment("loginId", login.Id.ToString()); // replaces matching token in request.Resource
                        
            var userResponse = await _client.ExecuteTaskAsync<Schedule>(request);
            return userResponse.Data;            
        }

        
    }
}
