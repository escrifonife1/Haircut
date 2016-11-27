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
        public async Task<List<Schedule>> Availables(DateTime from, int loginId, int hairdresserId)
        {
            var request = new RestRequest("schedule/{from}", Method.GET);
            //var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(login);
            request.AddParameter("from", from.ToString()); // replaces matching token in request.Resource
            request.AddParameter("loginId", loginId); // replaces matching token in request.Resource
            request.AddParameter("hairdresserId", hairdresserId); // replaces matching token in request.Resource

            var userResponse = await ExecuteTaskAsync<List<Schedule>>(request);            
            return userResponse.Data;           
        }

        public async Task Schedule(Schedule schedule)
        {
            await PutFromRequestBody(schedule, "schedule");
        }

        public async Task<Schedule> ScheduleByLogin(Login login)
        {
            var request = new RestRequest("schedule/{loginId}", Method.GET);            
            request.AddUrlSegment("loginId", login.Id.ToString()); // replaces matching token in request.Resource
                        
            var userResponse = await ExecuteTaskAsync<Schedule>(request);            
            return userResponse.Data;            
        }        
    }
}
