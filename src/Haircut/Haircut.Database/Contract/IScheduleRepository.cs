using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Contract
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        Schedule GetByLoginId(int userId);
        List<Schedule> GetFromDate(DateTime from, int loginId, int hairdresserId);
    }
}
