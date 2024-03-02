using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Models.Order_Aggregate
{
    public class Adress
    {
        public Adress(string firstName, string lName, string street, string city, string country)
        {
            FirstName = firstName;
            LName = lName;
            Street = street;
            City = city;
            Country = country;
        }
        public Adress()
        {
            
        }

        public string FirstName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
