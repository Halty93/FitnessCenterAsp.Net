using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Term : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Začátek je povinný údaj")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD.MM.YYYY HH:mm}")]
        public virtual DateTime StartTerm { get; set; }

        [Required(ErrorMessage = "Konec je povinný údaj")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy H:m}")]
        public virtual DateTime EndTerm { get; set; }

        [Required(ErrorMessage = "Kapacita je povinný údaj")]
        [Range(1, 100, ErrorMessage = "Kapacita musí být v rozsahu od 1 do 100.")]
        public virtual int Capacity { get; set; }

        public virtual FitnessUser Trainer { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Room Room { get; set; }

    }
}
