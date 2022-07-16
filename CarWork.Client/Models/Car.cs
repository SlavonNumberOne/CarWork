using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWork.Client.Enums;

namespace CarWork.Client.Models
{
    public class Car
    {
        public string Id { get; set; }
        public ModelCar Model { get; set; }
        public TypeCar Type { get; set; }
        public decimal TankCapacity { get; set; }
        public decimal FuelCapacity { get; set; }
        public Engine Engine { get; set; }
        public decimal Consuption { get; set; }
        public int Location { get; set; }
        public TypeFuel TypeFuel { get; set; }
       // public OperationStatus OperationStatus { get; private set; }
        public decimal CanisterVolume { get; set; }

        public Car(ModelCar model, TypeCar type, decimal tankCapacity, decimal fuelCapacity, Engine engine, decimal consuption, int location, TypeFuel typefuel, decimal canisterVolume)
        {
            Id = Guid.NewGuid().ToString(); 
            Model = model;
            Type = type;
            TankCapacity = tankCapacity;
            FuelCapacity = fuelCapacity;
            Engine = engine;
            Consuption = consuption;
            Location = location;
            TypeFuel = typefuel;
            CanisterVolume = canisterVolume;
        }

        public bool Move(DirectionType direction)
        {
            if (FuelCapacity > 0.3m)
            {
                if (direction == DirectionType.Left)
                {

                    Location--;
                }
                else
                {
                    Location++;
                }
                var fuelForMoving = Consuption / (100000 / Map.GetInstance().MetersPerSlot);
                FuelCapacity = FuelCapacity - fuelForMoving;
                return true;
            }
            return false;
        }
    }
}
