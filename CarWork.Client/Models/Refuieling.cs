using CarWork.Client.Enums;
using System;

namespace CarWork.Client.Models
{
    public class Refuieling
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Location { get; set; }
        public decimal PricePerLiter { get; set; }
        public decimal Capacity { get; set; }
        public decimal Money { get; set; }
        public TypeFuel TypeFuel { get; set; }
        public Refuieling(string name, int location, decimal pricePerLiter, decimal capacity, TypeFuel typeFuel)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Location = location;
            PricePerLiter = pricePerLiter;
            Capacity = capacity;
            TypeFuel = typeFuel;
        }

        public decimal Refuel(Car car)
        {
            var needFuelCapaciti = car.TankCapacity - car.FuelCapacity;  
            car.FuelCapacity = needFuelCapaciti;
            car.TypeFuel = TypeFuel;
            Capacity = Capacity - needFuelCapaciti;
            return needFuelCapaciti * PricePerLiter;
        }
        public decimal Refuel(Canister canister, decimal needCapacity)
        {
            canister.Capacity += needCapacity;
            canister.TypeFuel = TypeFuel;
            Capacity = Capacity - needCapacity;
            return needCapacity * PricePerLiter;
        }
        public void PayFuel(decimal money)
        {
            Money = money + Money;

        }
    }
}
