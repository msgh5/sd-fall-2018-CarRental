using System;
using System.Collections.Generic;

namespace CarRental
{
    /// <summary>
    /// Represents a car in the store
    /// </summary>
    public class Car
    {
        private const decimal PenaltyPerDay = 10;
        private const decimal PenaltyAfter30Days = 200;
        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Make { get; private set; }

        //Computed column
        public bool IsAvailable
        {
            get
            {
                return RentInformation == null;
            }
        }

        public decimal RatePerDay { get; private set; }
        public RentInformation RentInformation { get; set; }
        private List<Comment> Comments { get; set; }

        public Car(int year, string model, string make, decimal ratePerDay)
        {
            Year = year;
            Model = model;
            Make = make;
            RatePerDay = ratePerDay;
            Comments = new List<Comment>();
        }

        public decimal CalculateRent(DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new BusinessRuleException("The pickup date can't be greather than the return date");
            }

            var numberOfDays = to.Date.Subtract(from.Date).Days + 1;
            var rate = numberOfDays * RatePerDay;

            if (RentInformation != null)
            {
                var numberOfActualDays = DateTime.Now.Date.Subtract(RentInformation.RentedFrom.Date).Days + 1;

                if (numberOfActualDays != numberOfDays)
                {
                    var excedingDays = numberOfActualDays - numberOfDays;

                    rate += PenaltyPerDay * excedingDays;

                    if (excedingDays > 30)
                    {
                        rate += PenaltyAfter30Days;
                    }
                }
            }

            return rate;
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public IReadOnlyList<Comment> GetComments()
        {
            return Comments;
        }
    }
}
