using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class MachineDao:DaoBase<Machine>
    {
        public MachineDao() : base()
        {
        }

        public IList<Machine> GetMachinePage(int count, int page)
        {
            return session.CreateCriteria<Machine>()
                .AddOrder(Order.Asc("Name"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Machine>();
        }

        public IList<Machine> GetMachinesByStatus(int count, int page, string status)
        {
            return session.CreateCriteria<Machine>()
                .Add(Restrictions.Eq("Status", status))
                .AddOrder(Order.Asc("Name"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Machine>();
        }
    }
}
