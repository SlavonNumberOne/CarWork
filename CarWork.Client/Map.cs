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

        private Map(){}

        public static Map GetInstance()
        {
            if (instance == null)
                instance = new Map();
            return instance;
        }
    }
}
