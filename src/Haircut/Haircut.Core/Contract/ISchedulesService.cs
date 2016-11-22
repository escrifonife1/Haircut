using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Contract
{
    public interface ISchedulesService
    {
        Task<List<string>> Disponiveis();
    }
}
