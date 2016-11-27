using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Contract
{
    public interface IBarbershopRepository : IBaseRepository<BarberShop>
    {
        List<BarberShop> GetAll();
    }
}
