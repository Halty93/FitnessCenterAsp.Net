using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Term
    {
        public int Id { get; set; }
        public DateTime StartTerm { get; set; }
        public DateTime EndeTerm { get; set; }
        public int Capacity { get; set; }
        public bool IsRepeatable { get; set; }
        //vazba na trenera
        //vazba na mistnost
        //vazba na aktivitu
    }
}
