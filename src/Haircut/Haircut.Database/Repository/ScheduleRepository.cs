using Haircut.Database.Contract;
using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Repository
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public Schedule GetByLoginId(int loginId)
        {
            return _context.Schedule.FirstOrDefault(s => s.Available == loginId);
        }

        public List<Schedule> GetFromDate(DateTime from, int loginId)
        {
            return _context.Schedule.Where(s => s.Date >= from && (s.Available == 1 || s.Login.Id == loginId) ).ToList();
        }
    }
}
