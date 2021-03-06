﻿using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Contract
{
    public interface ILoginRepository : IBaseRepository<Login>
    {        
        Login GetByLogin(Login login);
        Login GetByUserName(string userName);
    }
}
