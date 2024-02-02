using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise
{
    class Flight
    {
        public int Number { get; }
        public string DepartureCity { get; }
        public string ArrivalCity { get; }
        public int Day { get; }
        private int remainingCapacity;

        public Flight(int number, string departureCity, string arrivalCity, int day)
        {
            Number = number;
            DepartureCity = departureCity;
            ArrivalCity = arrivalCity;
            Day = day;
            remainingCapacity = 20;
        }

        public bool CanAccommodateOrder()
        {
            return remainingCapacity > 0;
        }

        public void AssignOrder()
        {
            remainingCapacity--;
        }
    }
}
