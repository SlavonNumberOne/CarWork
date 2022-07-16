using CarWork.Client.Enums;
using CarWork.Client.Interface;
using System;
using System.Linq;

namespace CarWork.Client.Models
{
    public class TaxiDriver : Driver, IWorkDriver
    {
        public string Token { get; } = "C";

        public TaxiDriver(string name, Car car, decimal drivingExperiense, int age, SexType sex, decimal money)
            : base( name, car, drivingExperiense, age, sex, money)
        {
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
                case OperationStatus.MoveToOrder:
                    MoveToOrder();
                    break;
                case OperationStatus.ExecuteOrder:
                    ExecuteOrder();
                    break;
            }
        }

        private DeliveryOrder FindOrder()//НАйти Заказ
        {
            var availableOrders = Map.GetInstance().DeliveryOrders.Where(o => o.StatusOrder == StatusOrder.Created);
            if (!availableOrders.Any())
            {
                return null;
            }

            var addressCar = Car.Location;
            var minDistance = Map.GetInstance().SlotCount;
            DeliveryOrder deliveryOrder = null;

            foreach (var order in Map.GetInstance().DeliveryOrders)
            {
                var distance = Math.Abs(order.Passenger.Location - addressCar);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    deliveryOrder = order;
                }
            }
            return deliveryOrder;

        }

        private void MoveToOrder()//Перейти к заказу
        {
            if (Operation.Order.Passenger.Location == Car.Location)
            {
                Operation.Order.StatusOrder = StatusOrder.InProgress;
                Operation.Status = OperationStatus.ExecuteOrder;
                Operation.Direction = DirectionType.Left;

                if (Car.Location < Operation.Order.DeliveryLocation)
                {
                    Operation.Direction = DirectionType.Right;
                }
                return;
            }

            if (!Car.Move(Operation.Direction))
            {
                SendRefuelOrder();
            }
        }

        private void ExecuteOrder()//ВыполнитьЗаказ
        {
            if (Operation.Order.DeliveryLocation == Car.Location)
            {
                Operation.Order.StatusOrder = StatusOrder.Achieved;
                Map.GetInstance().DeliveryOrders.Remove(Operation.Order);
                Money = Money + Operation.Order.Price;
                ClearOperation();
                return;
            }
            Car.Move(Operation.Direction);
        }

        protected void FindOperation()//Найти операцию
        {
            if (GetIsNeedToRefuiled())
            {
                var findRefuining = FindRefuiling();
                Operation.Status = OperationStatus.Refiel;
                Operation.Refuieling = findRefuining;
                Operation.Direction = DirectionType.Left;

                if (Car.Location < findRefuining.Location)
                {
                    Operation.Direction = DirectionType.Right;
                }
                return;
            }

            var order = FindOrder();
            if (order != null)
            {
                Operation.Status = OperationStatus.MoveToOrder;
                Operation.Order = order;
                order.StatusOrder = StatusOrder.Accepted;

                Operation.Direction = DirectionType.Left;

                if (Car.Location < order.Passenger.Location)
                {
                    Operation.Direction = DirectionType.Right;
                }
                return;
            }
        }
    }
}
