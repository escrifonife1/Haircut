using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Configuration
{
    public class ScheduleConfiguration : EntityTypeConfiguration<Schedule>
    {
        public ScheduleConfiguration()
        {
            this.HasKey(s => s.Id);
            this.Property(s => s.Date)
                .IsRequired();
            this.Property(s => s.Available)
                .IsRequired();
            this.Property(s => s.Available)
                .IsRequired();
        }
    }
}
