﻿using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Contract
{
    public interface IHairdresserRepository : IBaseRepository<Hairdresser>
    {
        List<Hairdresser> GetAll(int barbershopId);
    }
}
