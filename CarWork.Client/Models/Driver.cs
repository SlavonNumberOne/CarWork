using System;
using System.Linq;
using CarWork.Client.Enums;

namespace CarWork.Client.Models
{
    public class Driver
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Car Car { get; set; }
        public decimal DrivingExperiense { get; set; }
        public int Age { get; set; }
        public SexType Sex { get; set; }
        public decimal Money { get; set; }
        public Operation Operation { get; set; }
        public bool WaitingForFuel { get; set; } = false;

        public Driver(string name, Car car, decimal drivingExperiense, int age, SexType sex, decimal money)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Car = car;
            DrivingExperiense = drivingExperiense;
            Age = age;
            Sex = sex;
            
            Money = money;
            Operation = new Operation() {Status = OperationStatus.None};
        }

        protected void MoveToRefuiling() //Перейти к заправке
        {
            if(Operation.Refuieling.Location == Car.Location)
            {
                var needToPay = Operation.Refuieling.Refuel(Car);
                Money = Money - needToPay;
                Operation.Refuieling.PayFuel(needToPay);
                ClearOperation();
                return;
            }

            if (!Car.Move(Operation.Direction))
            {
                SendRefuelOrder();
            }
        }

        protected bool GetIsNeedToRefuiled()//нужно ли запрвмиться
        {
            var fuelCapasitiPercent = Car.FuelCapacity / (Car.TankCapacity / 100); 
            if(fuelCapasitiPercent >= 50)
            {
                return false;
            }
            
            if(fuelCapasitiPercent <= 10)
            {
                return true;
            }

            Random random = new Random();
            var disiredValue = random.Next(11, 39);

            return fuelCapasitiPercent < disiredValue;
        }

        protected Refuieling FindRefuiling()//НайтиЗаправка
        {
            return FindRefuiling(Car.TypeFuel);
        }

        protected Refuieling FindRefuiling(TypeFuel typeFuel)//НайтиЗаправка
        {
            var addressCar = Car.Location;
            var minDistance = Map.GetInstance().SlotCount;
           
            Refuieling nearRefuiling = null;

            var typeRefuilings = Map.GetInstance().Refuielings.Where(r => r.TypeFuel == typeFuel);

            foreach (var refuieling in typeRefuilings)
            {
                var distance = Math.Abs(refuieling.Location - addressCar);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    nearRefuiling = refuieling;
                }
            }
            return nearRefuiling;
        }

        protected void SendRefuelOrder()
        {
            if (!WaitingForFuel) {
                Map.GetInstance().RefuelOrders.Add(new RefuelOrder
                {
                    DriverClient = this,
                    StatusOrder = StatusOrder.Created,
                    TypeFuel = Car.TypeFuel,
                    NeedCapacity = Car.TankCapacity / 2
                });
                WaitingForFuel = true;
            }
        }

        protected void ClearOperation()
        {
            Operation.Status = OperationStatus.None;
            Operation.RefuelOrder = null;
            Operation.Refuieling = null;
            Operation.Order = null;
        }
    }
}
 