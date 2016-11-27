﻿using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Contract
{
    public interface IBarbershoperService : IBaseService
    {
        Task<List<BarberShop>> Barbershopers();
    }
}
