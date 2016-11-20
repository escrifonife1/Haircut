using Haircut.Database.Contract;
using Haircut.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Repository
{
    public class LoginRepository : BaseRepository<Login>, ILoginRepository
    {
        public Login GetById(int id)
        {
            var login = _context.Login.Where(l => l.Id == id).FirstOrDefault();

            return login;
        }
    }
}
