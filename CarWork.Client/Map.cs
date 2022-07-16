using System;
using System.Collections.Generic;
using System.Linq;
using CarWork.Client.Enums;
using CarWork.Client.Interface;
using CarWork.Client.Models;
using CarWork.Client.Utils;

namespace CarWork.Client
{
    public  class Map
    {
        private static Map instance;
        public int SlotCount { get; set; } = 200;
        public int MetersPerSlot { get; set; } = 500;
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Passenger> Clients { get; set; } = new List<Passenger>();
        public List<IWorkDriver> WorkDrivers { get; set; } = new List<IWorkDriver>();
        public List<Refuieling> Refuielings { get; set; } = new List<Refuieling>();
        public List<DeliveryOrder> DeliveryOrders { get; set; } = new List<DeliveryOrder>();
        public List<RefuelOrder> RefuelOrders { get; set; } = new List<RefuelOrder>();

        private Map()
        {
            Refuielings.Add(new Refuieling("ANP", 102, 51m, 1000m, TypeFuel.Oil));
            Refuielings.Add(new Refuieling("Avis", 10, 56m, 1000m, TypeFuel.Dizel));
            Refuielings.Add(new Refuieling("WOG", 157, 58m, 1000m, TypeFuel.Oil));

            var engine = new Engine(TypeFuel.Oil, 1.8m);
            var car = new Car(ModelCar.Nisan, TypeCar.Pickup, 45m, 5m, engine, 10.2m, 67, TypeFuel.Oil, 0);
            var driver = new TaxiDriver("Oleg", car, 2m, 23, SexType.Male, 5000m);

            Cars.Add(car);
            WorkDrivers.Add(driver);

            DeliveryOrders.Add(GenerateOrder.Generate());
            DeliveryOrders.Add(new DeliveryOrder());

        }

        public static Map GetInstance()
        {
            if (instance == null)
                instance = new Map();
            return instance;
        }


    }
}
