using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Configuration
{
    class BarberShopConfiguration : EntityTypeConfiguration<BarberShop>
    {
        public BarberShopConfiguration()
        {
            this.HasKey(b => b.Id);
            this.Property(b => b.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
