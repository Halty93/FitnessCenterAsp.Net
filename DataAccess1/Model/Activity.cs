using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using DataAccess.Interface;


namespace DataAccess.Model
{
    public class Activity : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Název aktivity je povinný údaj")]
        public virtual string Name { get; set; }

        [AllowHtml]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = "Cena je povinný údaj")]
        [Range(1.00, 1000.00, ErrorMessage = "Price must be between 1.00 and 1000.00")]
        public virtual decimal Price { get; set; }

        [Required(ErrorMessage = "Obtížnost je povinný údaj")]
        [Range(1, 10, ErrorMessage = "Obtížnost může být ohodnocena číslicemi od 1 do 10.")]
        public virtual int Difficultness { get; set; }

        public virtual string SmallImageName { get; set; }

        public virtual string BigImageName { get; set; }

        public virtual FitnessUser Author { get; set; }

    }
}
