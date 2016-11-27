using Haircut.Core.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haircut.Model.Models;

namespace Haircut.Core.Services
{
    public class BarbershopService : BaseService, IBarbershoperService
    {
        public async Task<List<BarberShop>> Barbershopers()
        {
            return await Get<List<BarberShop>>("barbershop");
        }
    }
}
