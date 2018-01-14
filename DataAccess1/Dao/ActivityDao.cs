using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class ActivityDao : DaoBase<Activity>
    {
        public ActivityDao() : base()
        {           
        }

        public bool ActivityExist(string name)
        {
            return session.CreateCriteria<Activity>()
                .Add(Restrictions.Eq("Name", name))
                .UniqueResult<Activity>() == null ? false : true;
        }
    }
}
