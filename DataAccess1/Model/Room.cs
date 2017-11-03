using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
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
    }
}
