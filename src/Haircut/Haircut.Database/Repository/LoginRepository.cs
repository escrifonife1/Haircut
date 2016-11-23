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
        //public Login GetById(int id)
        //{
        //    var login = _context.Login.Where(l => l.Id == id).FirstOrDefault();

        //    return login;
        //}

        public Login GetByUserName(string userName)
        {
            var lo = _context.Login.Where(l => l.UserName == userName ).FirstOrDefault();

            return lo;
        }

        public Login GetByLogin(Login login)
        {
            var lo = _context.Login.Where(l => l.UserName == login.UserName && l.Password == login.Password).FirstOrDefault();

            return lo;
        }
    }
}
