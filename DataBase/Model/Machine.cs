using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataBase.Interface;

namespace DataBase.Model
{
    public class Machine : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Název stroje je povinný údaj")]
        public virtual string Name { get; set; }

        public virtual User Repairman { get; set; }

        [Required(ErrorMessage = "Datum poslední revize je povinný údaj")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public virtual DateTime LastCheck { get; set; }

        [Required(ErrorMessage = "Datum následující revize je povinný údaj")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public virtual DateTime NextCheck { get; set; }

        [Required(ErrorMessage = "Stav stroje je povinný údaj")]
        //nějaké ošetření stavů(poškozený, v pořádku, opravuje se)
        public virtual string Status { get; set; }

        [AllowHtml]
        public virtual string Description { get; set; }

        public virtual User FaultUser { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public virtual DateTime FaultDate { get; set; }

        public virtual string Fault { get; set; }

        public virtual string SmallImageName { get; set; }

        public virtual string BigImageName { get; set; }
    }
}
