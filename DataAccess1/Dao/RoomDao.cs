using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class RoomDao : DaoBase<Room>
    {
        public RoomDao() : base()
        {
        }

        public bool RoomExist(string name)
        {
            return session.CreateCriteria<Room>()
                .Add(Restrictions.Eq("Name", name))
                .UniqueResult<Room>() == null ? false : true;
        }
    }
}
