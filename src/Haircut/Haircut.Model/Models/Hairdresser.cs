using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Model.Models
{
    public class Hairdresser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BarberShopId { get; set; }
        public virtual BarberShop BarberShop { get; set; }
    }
}
