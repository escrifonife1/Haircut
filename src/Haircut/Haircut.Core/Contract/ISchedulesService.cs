using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Contract
{
    public interface ISchedulesService : IErrorMessages
    {
        Task<List<Schedule>> Availables(DateTime from, int loginId);
        Task<Schedule> ScheduleByLogin(Login login);
        Task Schedule(Schedule schedule);
    }
}
