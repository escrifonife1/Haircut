using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Configuration
{
    public class LoginConfiguration : EntityTypeConfiguration<Login>
    {
        public LoginConfiguration()
        {
            this.HasKey(l => l.Id);
            this.Property(l => l.Name)
                .HasMaxLength(50)
                .IsRequired();
            this.Property(l => l.UserName)                
                .HasMaxLength(30)                
                .IsRequired();
            this.Property(l => l.Password)
                .HasMaxLength(50)
                .IsRequired();
            this.Property(l => l.Phone)
                .HasMaxLength(50)
                .IsRequired();
        }        
    }
}
