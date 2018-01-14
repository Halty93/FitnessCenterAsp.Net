using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class FitnessUser : MembershipUser, IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Jméno uživatele je povinný údaj")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Příjmení uživatele je povinný údaj")]
        public virtual string Surname { get; set; }

        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Rodné číslo piště bez znaků")]
        public virtual string BirthNumber { get; set; }

        [Required(ErrorMessage = "Login je povinný údaj")]
        public virtual string Login { get; set; }

        [Required(ErrorMessage = "Heslo uživatele je povinný údaj")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hesla se neshodují. ")]
        public virtual string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Datum narození je povinný údaj")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public virtual DateTime Birthdate { get; set; }

        [DataType(DataType.PhoneNumber)]
        public virtual string PhoneNumber { get; set; }

        [Required(ErrorMessage = "EmailAddress je povinný údaj")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]
        public virtual string EmailAddress { get; set; }

        public virtual string SmallImageName { get; set; }

        public virtual string BigImageName { get; set; }

        public virtual string LicenceList { get; set; }

        public virtual Address Address { get; set; }

        public virtual Role Role { get; set; }
    }
}
