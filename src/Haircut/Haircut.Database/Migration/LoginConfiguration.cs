using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Migration
{
    public class LoginConfiguration : EntityTypeConfiguration<Login>
    {
        public LoginConfiguration()
        {
            this.HasKey(l => l.Id);
            this.Property(l => l.UserName)
                .IsRequired();
            this.Property(l => l.Password)
                .IsRequired();
        }        
    }
}
