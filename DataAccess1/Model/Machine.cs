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
    public class Machine : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Název stroje je povinný údaj")]
        public virtual string Name { get; set; }

        public virtual FitnessUser Repairman { get; set; }

        [Required(ErrorMessage = "Datum poslední revize je povinný údaj")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public virtual DateTime LastCheck { get;set; }

        [Required(ErrorMessage = "Datum následující revize je povinný údaj")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public virtual DateTime NextCheck { get; set; }

        [Required(ErrorMessage = "Stav stroje je povinný údaj")]
        //nějaké ošetření stavů(poškozený, v pořádku, opravuje se)
        public virtual string Status { get; set; }

        [AllowHtml]
        public virtual string Description { get; set; }

        public virtual FitnessUser FaultUser { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public virtual DateTime FaultDate { get; set; }

        [AllowHtml]
        public virtual string Fault { get; set; }

        public virtual string SmallImageName { get; set; }

        public virtual string BigImageName { get; set; }

        public static IList<string> StatsList
        {
            get
            {
                IList<string> stats = new List<string>();
                stats.Add("V pořádku");
                stats.Add("Poškozený");
                return stats;
            }
        }
    }
}
