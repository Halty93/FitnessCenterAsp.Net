using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
    }
}
