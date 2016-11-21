using Haircut.Core.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Core.Services
{
    public class HorariosService : BaseService, IHorariosService
    {
        public Task<List<string>> Disponiveis()
        {
            var disponiveis = new List<string>();
            var horarioInicial = DateTime.Today.AddHours(8);

            for (int i = 0; i < 23; i++)
            {
                disponiveis.Add(horarioInicial.ToString("dd/MM/yyyy hh:mm"));
                horarioInicial = horarioInicial.AddMinutes(30);
            }

            return Task.FromResult(disponiveis);
        }        
    }
}
