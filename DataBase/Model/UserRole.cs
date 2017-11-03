using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Interface;

namespace DataBase.Model
{
    public class UserRole:IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Název role je povinný údaj")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Popis role je povinný údaj")]
        public virtual string Description { get; set; }
    }
}
