using Haircut.Core.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haircut.Model.Models;

namespace Haircut.Core.Services
{
    public class HairdresserService : BaseService, IHairdresserService
    {
        public async Task<List<Hairdresser>> Hairdressers(BarberShop barbershop)
        {
            return await Get<int, List<Hairdresser>>(barbershop.Id, "hairdresser", "barbershopId");
        }
    }
}
