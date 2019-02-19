using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    //Represents a comment from a customer
    public class Comment
    {
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Customer CreatedBy { get; private set; }

        public Comment(string text, Customer createdBy)
        {
            Text = text;
            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
        }
    }
}
