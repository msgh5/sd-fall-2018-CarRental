using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental
{
    /// <summary>
    /// Represents the car rental store.
    /// </summary>
    public class Store
    {
        private List<Car> Cars { get; set; }

        public Store()
        {
            Cars = new List<Car>();
            GenerateCars();
        }

        /// <summary>
        /// Gets all cars from the system.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Car> GetAllCars()
        {
            return Cars;
        }

        /// <summary>
        /// Gets all cars that are not currently rented.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Car> GetAvailableCars()
        {
            return Cars.Where(p => p.IsAvailable).ToList();
        }

        /// <summary>
        /// Rents a car for the specified customer on the specified period.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="car"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void RentCar(Customer customer, Car car, DateTime from, DateTime to)
        {
            if (!IsValidPeriodForRent(from, to))
            {
                throw new Exception("The period to rent a car needs to be between 1 and 30 days.");
            }

            var rentInformation = new RentInformation(customer, from, to);

            car.RentInformation = rentInformation;
        }

        /// <summary>
        /// Validates if the period is valid to rent a car.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public bool IsValidPeriodForRent(DateTime from, DateTime to)
        {
            var totalDays = to.Date.Subtract(from.Date).Days + 1;

            return totalDays > 0 && totalDays < 30;
        }

        /// <summary>
        /// Checks if the customer is currently renting a car
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool IsCustomerCurrentlyRentingACar(Customer customer)
        {
            return Cars.Any(p => p.RentInformation != null && p.RentInformation.RentedBy == customer);
        }

        /// <summary>
        /// Returns the currently rented car for a customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public Car GetRentedCarByCustomer(Customer customer)
        {
            return Cars.FirstOrDefault(p => p.RentInformation != null && p.RentInformation.RentedBy == customer);
        }

        /// <summary>
        /// Returns the car
        /// </summary>
        /// <param name="car"></param>
        public void ReturnCar(Car car, Customer customer, string comments)
        {
            if (!string.IsNullOrWhiteSpace(comments))
            {
                car.AddComment(new Comment(comments, customer));
            }

            car.RentInformation = null;
        }

        /// <summary>
        /// Generates test data for the store
        /// </summary>
        private void GenerateCars()
        {
            Cars.Add(new Car(2015, "Spark", "Chevrolet", 19.99m));
            Cars.Add(new Car(2015, "Versa", "Nissan", 25.99m));
            Cars.Add(new Car(2016, "Elantra", "Hyundai", 30.99m));
            Cars.Add(new Car(2017, "Jetta", "Volkswagen", 35.99m));
            Cars.Add(new Car(2018, "Malibu", "Chevrolet", 39.99m));
        }
    }
}
