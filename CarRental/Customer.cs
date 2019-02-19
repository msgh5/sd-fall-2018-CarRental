using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public class Customer
    {
        public string Name { get; private set; }

        public Customer(string name)
        {
            Name = name;
        }
    }
}
