﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Trainer : User
    {
        public List<string> LicenceList { get; set; }
    }
}
