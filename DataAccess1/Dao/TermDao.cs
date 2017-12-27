using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class TermDao:DaoBase<Term>
    {
        public TermDao() : base()
        {
        }

        public IList<Term> GetTermsByRoom(Room room)
        {
            return session.CreateCriteria<Term>()
                .Add(Restrictions.Eq("Room", room))
                .List<Term>();
        }

        public IList<Term> GetTermsByActivity(Activity activity)
        {
            return session.CreateCriteria<Term>()
                .Add(Restrictions.Eq("Activity", activity))
                .List<Term>();
        }

        public int GetActualCapacity(Term term)
        {           
            return term.Capacity - session.CreateCriteria<Reservation>()
                .Add(Restrictions.Eq("Term", term)).SetProjection(Projections.CountDistinct("User"))
                .UniqueResult<int>();
        }

        public IList<Term> GetOldTerms()
        {
            return session.CreateCriteria<Term>()
               .Add(Restrictions.Lt("EndTerm", DateTime.Now))
               .List<Term>();
        }

        public IList<Term> GetNewTerms()
        {
            return session.CreateCriteria<Term>()
               .Add(Restrictions.Ge("EndTerm", DateTime.Now))
               .List<Term>();
        }

        public IList<Term> GetTermsByTrainer(FitnessUser trainer)
        {
            return session.CreateCriteria<Term>()
                .Add(Restrictions.Eq("Trainer", trainer))
                .List<Term>();
        }

        public IList<Term> GetOldTermsByTrainer(FitnessUser trainer)
        {
            return session.CreateCriteria<Term>()
                .Add(Restrictions.Eq("Trainer", trainer))
                .Add(Restrictions.Lt("EndTerm", DateTime.Now))
                .List<Term>();
        }

        public IList<Term> GetNewTermsByTrainer(FitnessUser trainer)
        {
            return session.CreateCriteria<Term>()
                .Add(Restrictions.Eq("Trainer", trainer))
                .Add(Restrictions.Ge("EndTerm", DateTime.Now))
                .List<Term>();
        }
    }
}
