using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental
{
    /// <summary>
    /// Car Rental Exercise
    /// Some input validations were ommited for brevity.
    /// </summary>
    class Program
    {
        private static Store Store { get; set; }
        private static Customer Customer { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the car rental store! Please type your name to begin:");
            var customerName = Console.ReadLine();

            Customer = new Customer(customerName);
            Store = new Store();

            var keyboardInput = string.Empty;

            while (keyboardInput != "0")
            {
                Console.WriteLine("============ CAR RENTAL STORE ============");
                Console.WriteLine("Please select an option from the menu:");
                Console.WriteLine("1 - See all cars");
                Console.WriteLine("2 - Rent a car");
                Console.WriteLine("3 - Return a car");
                Console.WriteLine("0 - Exit");

                keyboardInput = Console.ReadLine();

                if (keyboardInput == "1")
                {
                    var cars = Store.GetAllCars();

                    Console.WriteLine("Please select the car to see the details:");

                    PrintCars(cars);

                    var carInput = GetIntFromUserInput() - 1;
                    var car = cars.ElementAtOrDefault(carInput);

                    PrintCarDetails(car);
                }
                else if (keyboardInput == "2")
                {
                    var cars = Store.GetAvailableCars();

                    if (Store.IsCustomerCurrentlyRentingACar(Customer))
                    {
                        Console.WriteLine("You cannot rent more cars until you return your currently rented car");
                        continue;
                    }

                    Console.WriteLine("Please select the car you would like to rent:");

                    PrintCars(cars);

                    var carInput = GetIntFromUserInput()-1;

                    Console.WriteLine("Please type the start date to rent the car (mm/dd/yyyy)");

                    var from = Convert.ToDateTime(Console.ReadLine());

                    Console.WriteLine("Please type the end date to rent the car (mm/dd/yyyy)");

                    var to = Convert.ToDateTime(Console.ReadLine());

                    if (!Store.IsValidPeriodForRent(from, to))
                    {
                        Console.WriteLine("The period to rent a car needs to be between 1 and 30 days.");
                        continue;
                    }

                    var car = cars.ElementAtOrDefault(carInput);

                    PrintCarDetails(car, false, from, to);

                    Console.WriteLine("Would you like to proceed?");
                    Console.WriteLine("1 - Yes");
                    Console.WriteLine("2 - No");

                    keyboardInput = Console.ReadLine();

                    if (keyboardInput == "1")
                    {
                        Store.RentCar(Customer, car, from, to);
                    }
                }
                else if (keyboardInput == "3")
                {
                    var car = Store.GetRentedCarByCustomer(Customer);

                    if (car == null)
                    {
                        Console.WriteLine("You haven't rented any cars");
                    }
                    else
                    {
                        PrintCarDetails(car, false, car.RentInformation.RentedFrom, car.RentInformation.RentedTo);

                        Console.WriteLine("Would you like to return the car?");
                        Console.WriteLine("1 - Yes");
                        Console.WriteLine("2 - No");

                        keyboardInput = Console.ReadLine();

                        if (keyboardInput == "1")
                        {
                            Console.WriteLine("If you would like to share your experience with us please type it now.");

                            var comments = Console.ReadLine();
                            
                            Store.ReturnCar(car, Customer, comments);
                        }
                    }
                }
            }
        }

        //Nullable parameters in the method with default values
        private static void PrintCarDetails(Car car, bool showComments = true, DateTime? from = null, DateTime? to = null)
        {
            Console.WriteLine($"=== Car Details: ===");
            Console.WriteLine($"Make: {car.Make}");
            Console.WriteLine($"Model: {car.Model}");
            Console.WriteLine($"Year: {car.Year}");
            Console.WriteLine($"Rate per day: ${car.RatePerDay}");
            
            if (from.HasValue && to.HasValue)
            {
                Console.WriteLine($"Total price for the period: ${car.CalculateRent(from.Value, to.Value)}");
            }

            if (showComments)
            {
                Console.WriteLine("Comments: ");

                foreach (var comment in car.GetComments())
                {
                    Console.WriteLine($"From {comment.CreatedBy.Name} at {comment.CreatedAt.ToString("MM/dd/yyyy")}: {comment.Text}");
                }
            }
        }

        private static void PrintCars(IReadOnlyList<Car> cars)
        {
            for (var i = 0; i < cars.Count(); i++)
            {
                Console.WriteLine($"{i + 1} - {cars[i].Make} {cars[i].Model} - {(cars[i].IsAvailable ? "AVAILABLE" : "NOT AVAILABLE")}");
            }
        }

        private static int GetIntFromUserInput()
        {
            var userInput = Console.ReadLine();
            var output = 0;

            while (!int.TryParse(userInput, out output))
            {
                Console.WriteLine("Invalid input. Please try again with a number");

                userInput = Console.ReadLine();
            }

            return output;
        }
    }
}
