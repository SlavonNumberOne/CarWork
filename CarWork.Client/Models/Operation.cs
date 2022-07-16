using CarWork.Client.Enums;

namespace CarWork.Client.Models
{
    public class Operation
    {
        //public string Id { get; set; }
        public OperationStatus Status { get; set; }
        public DeliveryOrder Order { get; set; }
        public Refuieling Refuieling { get; set; }
        public DirectionType Direction { get; set; }
        public RefuelOrder RefuelOrder { get; set; }

    }
}
