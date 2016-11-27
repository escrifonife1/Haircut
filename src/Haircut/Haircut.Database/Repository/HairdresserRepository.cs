using Haircut.Database.Contract;
using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Repository
{
    public class HairdresserRepository : BaseRepository<Hairdresser>, IHairdresserRepository
    {
        public List<Hairdresser> GetAll(int barbershopId)
        {
            return _context.Hairdresser.Where(h => h.BarberShopId == barbershopId).OrderBy(h => h.Name).ToList();
        }
    }
}
