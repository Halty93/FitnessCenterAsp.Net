using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Room : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Název místnosti je povinný údaj")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Maximální kapacita místnosti je povinný údaj")]
        [Range(1, 100, ErrorMessage = "Maximální kapacita musí být v rozsahu od 1 do 100.")]
        public virtual int MaxCapacity { get; set; }

        [AllowHtml]
        public virtual string Description { get; set; }


        public static void CapacityCheck(Room r)
        {
            TermDao tDao = new TermDao();
            ReservationDao resDao = new ReservationDao();
            IList<Term> terms = tDao.GetTermsByRoom(r);

            foreach (Term t in terms)
            {
                //pokud je u nektereho terminu nastavena vetsi kapacita nez je nova maximalni kapacita mistnosti, zmen kapacitu terminu na max.kapacitu mistnosti
                if (r.MaxCapacity < t.Capacity && t.EndTerm > DateTime.Now)
                {
                    int uCount = r.MaxCapacity - (t.Capacity - tDao.GetActualCapacity(t));
                    //pokud je prihlaseno vice lidi nez nova max.kapacita mistnosti, odstran rezervace poslednich prihlasenych
                    if (uCount > 0)
                    {
                        IList<Reservation> reservations = resDao.GetLastReservationByTerm(t, uCount);
                        foreach (Reservation res in reservations)
                        {
                            resDao.Delete(res);
                        }
                    }
                    t.Capacity = r.MaxCapacity;
                    tDao.Update(t);
                }
            }
        }
    }
}
