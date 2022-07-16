using CarWork.Client.Enums;
using System;

namespace CarWork.Client.Models
{
    /// <summary>
    /// Закз на заправку машины
    /// </summary>
    public class RefuelOrder
    {
        public string Id { get; set; }
        /// <summary>
        ///Водитель которому нужно отвезти топливо
        /// </summary>
        public Driver DriverClient { get; set; } 

        /// <summary>
        /// Исполнитель этого закза
        /// </summary>
        public TankerDriver Excecutor { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public StatusOrder StatusOrder { get; set; }

        /// <summary>
        /// цена за заказ
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// тип топлива для заказа
        /// </summary>
        public TypeFuel TypeFuel { get; set; }

        /// <summary>
        /// Скольео надо топлива для машины
        /// </summary>
        public decimal NeedCapacity { get; set; }
        
        public RefuelOrder(string id, Driver driverClient, TankerDriver excecutor, StatusOrder statusOrder,  decimal price, TypeFuel typeFuel, decimal needCapacity)
        {
            Id = Guid.NewGuid().ToString();
            DriverClient = driverClient;
            Excecutor = excecutor;
            StatusOrder = statusOrder;
            Price = price;
            TypeFuel = typeFuel;
            NeedCapacity = needCapacity;
        }
        public RefuelOrder()
        {

        }
    }
}
