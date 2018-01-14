using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class UserDao : DaoBase<FitnessUser>
    {

        public FitnessUser GetByLogin(string login)
        {
            return session.CreateCriteria<FitnessUser>()
                .Add(Restrictions.Eq("Login", login))
                .UniqueResult<FitnessUser>();
        }

        public IList<FitnessUser> GetAllTrainers()
        {
            return session.CreateCriteria<FitnessUser>()
                .Add(Restrictions.Eq("Role.Id", 212))
                .List<FitnessUser>();
        }

        public FitnessUser GetByLoginAndPassword(string username, string password)
        {
            return session.CreateCriteria<FitnessUser>()
                .Add(Restrictions.Eq("Login", username))
                .Add(Restrictions.Eq("Password", password))
                .UniqueResult<FitnessUser>();
        }

        public IList<FitnessUser> GetUserPage(int count, int page)
        {
            return session.CreateCriteria<FitnessUser>()
                .AddOrder(Order.Asc("Surname"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<FitnessUser>();
        }

        public IList<FitnessUser> GetUsersByRole(int count, int page,  int id)
        {
            return session.CreateCriteria<FitnessUser>()
                .Add(Restrictions.Eq("Role.Id", id))
                .AddOrder(Order.Asc("Surname"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<FitnessUser>();
        }

        public bool LoginExist(string login)
        {
            return session.CreateCriteria<FitnessUser>()
                .Add(Restrictions.Eq("Login", login))
                .UniqueResult<FitnessUser>() == null ? false : true;
        }
    }
}
