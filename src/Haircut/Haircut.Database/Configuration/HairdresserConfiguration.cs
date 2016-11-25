using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Configuration
{
    public class HairdresserConfiguration : EntityTypeConfiguration<Hairdresser>
    {
        public HairdresserConfiguration()
        {
            this.HasKey(h => h.Id);
            this.Property(b => b.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
