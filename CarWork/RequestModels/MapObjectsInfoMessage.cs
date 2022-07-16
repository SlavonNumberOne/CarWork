using CarWork.Client.Interface;
using CarWork.Client.Models;
using System.Collections.Generic;

namespace CarWork.RequestModels
{
    public class MapObjectsInfoMessage
    {
        public List<Refuieling> Refuielings { get; set; }
        public List<DeliveryOrder> DeliveryOrders { get; set; }
        public List<IWorkDriver> Drivers { get; set; }

    }
}
