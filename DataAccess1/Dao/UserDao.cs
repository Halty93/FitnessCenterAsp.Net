using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess.Dao
{
    public class UserDao:DaoBase<User>
    {
        public UserDao() : base()
        {
        }
    }
}
