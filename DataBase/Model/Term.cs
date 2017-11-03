﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Interface;

namespace DataBase.Model
{
    public class Term : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Začátek je povinný údaj")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy H:m}")]
        public virtual DateTime StartTerm { get; set; }

        [Required(ErrorMessage = "Konec je povinný údaj")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy H:m}")]
        public virtual DateTime EndTerm { get; set; }

        [Required(ErrorMessage = "Kapacita je povinný údaj")]
        [Range(1, 100, ErrorMessage = "Kapacita musí být v rozsahu od 1 do 100.")]
        public virtual int Capacity { get; set; }

        [Required(ErrorMessage = "Opakovatelný je povinný údaj")]
        public virtual bool IsRepeatable { get; set; }

        public virtual User Trainer { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Room Room { get; set; }

    }
}