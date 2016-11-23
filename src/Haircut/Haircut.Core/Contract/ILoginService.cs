using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Contract
{
    public interface ILoginService
    {
        Task<Login> Log(Login login);
        Task<Login> Register(Login login);
        bool IsValideForRegister(Login login);
        string ErrorMessage();
    }
}
