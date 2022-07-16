using CarWork.Client.Enums;
using System;

namespace CarWork.Client.Models
{
    public class Engine
    {
        public string Id { get; set; }
        public TypeFuel TypeFuel { get; set; }
        public decimal Volume { get; set; }
        public Engine(TypeFuel typeFuel, decimal volume)
        {
            Id = Guid.NewGuid().ToString();
            TypeFuel = typeFuel;
            Volume = volume;

        }
    }
}
