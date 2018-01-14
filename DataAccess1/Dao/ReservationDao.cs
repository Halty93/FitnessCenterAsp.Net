using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class ReservationDao:DaoBase<Reservation>
    {
        public ReservationDao() : base()
        {
        }

        public IList<Reservation> GetLastReservationByTerm(Term term, int count)
        {
            return session.CreateCriteria<Reservation>()
                .AddOrder(Order.Desc("ReservationTime"))
                .Add(Restrictions.Eq("Term", term))
                .SetMaxResults(count)
                .List<Reservation>();
        }

        public IList<Reservation> GetAllReservationsByTerm(Term term)
        {
            return session.CreateCriteria<Reservation>()
                .Add(Restrictions.Eq("Term", term))
                .List<Reservation>();
        }

        public IList<Reservation> GetReservationPageByUser(int count, int page, FitnessUser fitnessUser)
        {
            return session.CreateCriteria<Reservation>()
                .Add(Restrictions.Eq("User", fitnessUser))
                .AddOrder(Order.Asc("ReservationTime"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Reservation>();
        }

        public IList<Reservation> GetAllReservationsByUser(FitnessUser fitnessUser)
        {
            return session.CreateCriteria<Reservation>()
                .Add(Restrictions.Eq("User", fitnessUser))
                .List<Reservation>();
        }

        public IList<Reservation> GetReservationPage(int count, int page)
        {
            return session.CreateCriteria<Reservation>()
                .AddOrder(Order.Asc("ReservationTime"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Reservation>();
        }

        public IList<Reservation> SearchReservations(string phrase, int count, int page)
        {
            return session.CreateCriteria<Reservation>()
                .Add(Restrictions.Like("User.Name", $"%{phrase}%"))
                .Add(Restrictions.Like("User.Surname", $"%{phrase}%"))
                .Add(Restrictions.Like("Term.Activity.Name", $"%{phrase}%"))
                .AddOrder(Order.Asc("ReservationTime"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Reservation>();
        }

        public bool ReservationExist(Term term, FitnessUser user)
        {
            return session.CreateCriteria<Reservation>()
                .Add(Restrictions.Eq("User", user))
                .Add(Restrictions.Eq("Term", term))
                .UniqueResult<Reservation>() == null ? false : true;
        }
    }
}
