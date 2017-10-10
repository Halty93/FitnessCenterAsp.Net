using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficultness { get; set; }
        //pridat trenera jako zakladatele kurzu
    }
}
