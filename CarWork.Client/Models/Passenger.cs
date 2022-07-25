using System;

namespace CarWork.Client.Models
{
    public class Passenger
    {
        public string Name { get; set; }
        public int Location { get; set; }
        public decimal Money { get; set; }

        public Passenger(string name, int location, decimal money)
        {
            Name = name;
            Location = location;
            Money = money;
        }

        public Passenger()
        {

        }
    }
    
}
