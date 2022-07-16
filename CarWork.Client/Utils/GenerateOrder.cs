using CarWork.Client.Models;
using System;

namespace CarWork.Client.Utils
{
    public class GenerateOrder
    {
        public static DeliveryOrder Generate()
        {
            var passenger = GeneratePassenger.Generate();
            var deliveryLocation = DeliveryLocationGenerate();
             
            return new DeliveryOrder()
            {
                Id = Guid.NewGuid().ToString(),
                Passenger = passenger,
                //Car = car;
                StatusOrder = Enums.StatusOrder.Created,
                DeliveryLocation = deliveryLocation,
                Price = PriceGenerate(passenger.Location, deliveryLocation)
            };
        }

        public static int DeliveryLocationGenerate()
        {
            Random random = new Random();
            var deliveryLocation = random.Next(0, Map.GetInstance().SlotCount);
            return deliveryLocation;
        }

        public static decimal PriceGenerate(int clientLocation, int deliveryLocation)
        {
            Random random = new Random();
            var km = random.Next(8,11);
            var distans = Math.Abs(clientLocation - deliveryLocation);
            var price = km * distans;
            return price;
        }
    }
}
