using CarWork.Client.Enums;
using CarWork.Client.Interface;
using System;
using System.Linq;

namespace CarWork.Client.Models
{
    public class TankerDriver: Driver, IWorkDriver
    {
        public string Token { get; } = "T";

        public Canister Canister { get; set; }
        
        public TankerDriver(string name, Car car, decimal drivingExperiense, int age, SexType sex, decimal money)
                   : base(name, car, drivingExperiense, age, sex, money)
        {
            Canister = new Canister();
        }

        public void ExecuteOperation()
        {
            switch (Operation.Status)
            {
                case OperationStatus.None:
                    FindOperation();
                    break;
                case OperationStatus.Refiel:
                    MoveToRefuiling();
                    break;
                case OperationStatus.MoveForNeedFuel:
                    MoveForNeedFuel();
                    break;
                case OperationStatus.MoveToRefuelCar:
                    ExecuteFuelCar();
                    break;
            }
        }

        private RefuelOrder FindRefuelOrder() //поиск машин без топлива
        {
            var availableOrders = Map.GetInstance().RefuelOrders.Where(o => o.StatusOrder == StatusOrder.Created);
            if (!availableOrders.Any())
            {
                return null;
            }
            var addressCar = Car.Location;
            var minDistance = Map.GetInstance().SlotCount;
            RefuelOrder refuelOrder = null;

            foreach (var order in availableOrders)
            {
                var distance = Math.Abs(order.DriverClient.Car.Location - addressCar);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    refuelOrder = order;
                }
            }
            return refuelOrder;
        }

        private void FindOperation()
        {
            if (GetIsNeedToRefuiled())
            {
                var refuining = FindRefuiling();
                Operation.Status = OperationStatus.Refiel;
                Operation.Refuieling = refuining;
                Operation.Direction = DirectionType.Left;

                if (Car.Location < refuining.Location)
                {
                    Operation.Direction = DirectionType.Right;
                }
                return;
            }

            var carRefuel = FindRefuelOrder();
            if (carRefuel != null)
            {
                Operation.Status = OperationStatus.MoveForNeedFuel;
                Operation.RefuelOrder = carRefuel;
                var refuining = FindRefuiling(carRefuel.TypeFuel);
                Operation.RefuelOrder.StatusOrder = StatusOrder.Accepted;
                Operation.Refuieling = refuining;

                Operation.Direction = DirectionType.Left;

                if (Car.Location < refuining.Location)
                {
                    Operation.Direction = DirectionType.Right;
                }
                return;
            }
        }

        private void MoveForNeedFuel() //перейти к запраке
        {
            if (Operation.Refuieling.Location == Car.Location)
            {
               
                var needToPay = Operation.Refuieling.Refuel(Canister, Operation.RefuelOrder.NeedCapacity);
                Money = Money - needToPay;
                Operation.Refuieling.PayFuel(needToPay);
                Operation.Status = OperationStatus.MoveToRefuelCar;
                Operation.Refuieling = null;
                Operation.RefuelOrder.Price += needToPay;

                Operation.Direction = DirectionType.Left;

                if (Car.Location < Operation.RefuelOrder.DriverClient.Car.Location)
                {
                    Operation.Direction = DirectionType.Right;
                }
                return;
            }
            Car.Move(Operation.Direction);
            Operation.RefuelOrder.Price += 10;

            
        }

        private void ExecuteFuelCar()//выполнитьь заправку
        {
            if (Operation.RefuelOrder.DriverClient.Car.Location == Car.Location)
            {
                Operation.RefuelOrder.DriverClient.Car.FuelCapacity += Canister.Capacity;
                Canister.Capacity = 0;
                Operation.RefuelOrder.DriverClient.Money -= Operation.RefuelOrder.Price;
                Money += Operation.RefuelOrder.Price;
                Operation.RefuelOrder.StatusOrder = StatusOrder.Achieved;
                Operation.RefuelOrder.DriverClient.WaitingForFuel = false;
                ClearOperation();
                return;
            }
            Car.Move(Operation.Direction);
            Operation.RefuelOrder.Price += 10;
        }
    }
   
}
