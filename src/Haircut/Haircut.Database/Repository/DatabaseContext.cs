using Haircut.Database.Configuration;
using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("Data Source=sql5031.smarterasp.net;Initial Catalog=DB_A10E59_tiagopascal;Persist Security Info=True;User ID=DB_A10E59_tiagopascal_admin;Password=DB1crmgps")
        {

        }

        public DbSet<Login> Login { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Hairdresser> Hairdresser { get; set; }
        public DbSet<BarberShop> BarberShop { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<string>()
                .Configure(s => s.HasMaxLength(50));

            modelBuilder.Configurations.Add(new LoginConfiguration());
            modelBuilder.Configurations.Add(new ScheduleConfiguration());
            modelBuilder.Configurations.Add(new HairdresserConfiguration());
            modelBuilder.Configurations.Add(new BarberShopConfiguration());
        }

    }
}
