using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Model.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Available { get; set; }
        public int LoginId { get; set; }
        public virtual Login Login { get; set; }
        public int HairdresserId { get; set; }
        public virtual Hairdresser Hairdresser { get; set; }
    }
}
