using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise
{
    internal class Program
    {
        static void Main()
        {
            // Load flight schedule
            List<Flight> flights = LoadFlightSchedule();

            // Display loaded flight schedule
            DisplayFlightSchedule(flights);

            // Load orders from JSON file
            Dictionary<string, Order> orders = LoadOrders();

            // Schedule orders and display flight itineraries
            ScheduleAndDisplayOrders(flights, orders);
        }

        static List<Flight> LoadFlightSchedule()
        {
            List<Flight> flights = new List<Flight>
        {
            new Flight(1, "YUL", "YYZ", 1),
            new Flight(2, "YUL", "YYC", 1),
            new Flight(3, "YUL", "YVR", 1),
            new Flight(4, "YUL", "YYZ", 2),
            new Flight(5, "YUL", "YYC", 2),
            new Flight(6, "YUL", "YVR", 2)
        };

            return flights;
        }

        static void DisplayFlightSchedule(List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight: {flight.Number}, departure: {flight.DepartureCity}, arrival: {flight.ArrivalCity}, day: {flight.Day}");
            }
        }

        static Dictionary<string, Order> LoadOrders()
        {
            Dictionary<string, Order> orders = new Dictionary<string, Order>();
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            string filePath = Path.Combine(directoryPath, "orders.json");

            try
            {
                if (Directory.Exists(directoryPath) && File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    orders = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
                }
                else
                {
                    Console.WriteLine("File not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return orders;
        }


        static void ScheduleAndDisplayOrders(List<Flight> flights, Dictionary<string, Order> orders)
        {
            foreach (var order in orders)
            {
                var scheduledFlight = ScheduleOrder(order.Value, flights);

                if (scheduledFlight != null)
                {
                    Console.WriteLine($"order: {order.Key}, flightNumber: {scheduledFlight.Number}, departure: {scheduledFlight.DepartureCity}, arrival: {scheduledFlight.ArrivalCity}, day: {scheduledFlight.Day}");
                }
                else
                {
                    Console.WriteLine($"order: {order.Key}, flightNumber: not scheduled");
                }
            }
        }

        static Flight ScheduleOrder(Order order, List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                if (flight.CanAccommodateOrder())
                {
                    flight.AssignOrder();
                    return flight;
                }
            }

            return null;
        }

    }
}
