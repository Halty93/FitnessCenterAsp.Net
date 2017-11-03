using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Reservation : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Čas rezervace je povinný údaj")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public virtual DateTime ReservationTime { get; set; }
        
        public virtual Term Term { get; set; }

        public virtual User User { get; set; }
    }
}
