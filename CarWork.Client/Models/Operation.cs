using CarWork.Client.Enums;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace CarWork.Client.Models
{

    public class Operation
    {
        //public string Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OperationStatus Status { get; set; }
        public DeliveryOrder Order { get; set; }
        public Refuieling Refuieling { get; set; }
        public DirectionType Direction { get; set; }
        public RefuelOrder RefuelOrder { get; set; }

    }
}
