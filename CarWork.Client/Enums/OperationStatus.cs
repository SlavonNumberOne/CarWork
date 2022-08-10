using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace CarWork.Client.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperationStatus
    {
        /// <summary>
        /// операции нет, нужно искать операцию
        /// </summary>
        None,
        /// <summary>
        /// Сейчас он едет на заправку заправлять машину
        /// </summary>
        Refiel,
        /// <summary>
        /// Везет клиента
        /// </summary>
        ExecuteOrder,
        /// <summary>
        /// Едет к клиенту
        /// </summary>
        MoveToOrder,
        /// <summary>
        /// поехать на заправку для тго что бы запрвить другую машинку
        /// </summary>
        MoveForNeedFuel,
        /// <summary>
        /// ехать к машине что бы ее заправить
        /// </summary>
        MoveToRefuelCar

    }
}
