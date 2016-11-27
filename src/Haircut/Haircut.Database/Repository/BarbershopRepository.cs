using Haircut.Database.Contract;
using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Repository
{
    public class BarbershopRepository : BaseRepository<BarberShop>, IBarbershopRepository
    {
        public List<BarberShop> GetAll()
        {
            return _context.BarberShop.OrderBy(b => b.Name).ToList();
        }
    }
}
