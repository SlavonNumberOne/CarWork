using CarWork.Client.Enums;
using System;

namespace CarWork.Client.Models
{
    public class DeliveryOrder
    {
        public string Id { get; set; }
        public Passenger Passenger { get; set; }
        public Driver Driver { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public int DeliveryLocation { get; set; }
        public decimal Price { get; set; }

        public DeliveryOrder(Passenger passenger, Driver driver, StatusOrder statusOrder, int deliveryLocation, decimal price)
        {
            Id = Guid.NewGuid().ToString();
            Passenger = passenger;
            Driver = driver;
            StatusOrder = statusOrder;
            DeliveryLocation = deliveryLocation;
            Price = price;
        }
        public DeliveryOrder()
        {

        }
    }
}
