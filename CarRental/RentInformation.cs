using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    /// <summary>
    /// Represents the current information about the customer who rented the car and the period
    /// </summary>
    public class RentInformation
    {
        public DateTime RentedFrom { get; private set; }
        public DateTime RentedTo { get; private set; }
        public Customer RentedBy { get; private set; }

        public RentInformation(Customer customer, DateTime from, DateTime to)
        {
            RentedBy = customer;
            RentedFrom = from;
            RentedTo = to;
        }
    }
}
