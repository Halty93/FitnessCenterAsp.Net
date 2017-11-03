using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Address : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Město je povinný údaj")]
        public virtual string Town { get; set; }

        public virtual string Street { get; set; }

        public virtual string StreetNumber { get; set; }

        [Required(ErrorMessage = "PSČ je povinný údaj")]
        [DataType(DataType.PostalCode)]
        public virtual string Zip { get; set; }

        [Required(ErrorMessage = "Stát je povinný údaj")]
        public virtual string Country { get; set; }
    }
}
