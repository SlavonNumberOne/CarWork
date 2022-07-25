using CarWork.Client.Enums;

namespace CarWork.Client.Models
{
    public class Canister
    {
        public string Id { get; set; }
        public decimal Capacity { get; set; }
        public TypeFuel TypeFuel { get; set; }
    }
}
