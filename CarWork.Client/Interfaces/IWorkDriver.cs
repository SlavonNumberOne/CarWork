using CarWork.Client.Enums;
using CarWork.Client.Models;

namespace CarWork.Client.Interface
{
    public interface IWorkDriver
    {
        bool WaitingForFuel { get; set; }

        string Token { get; }

        string Name { get; set; }

        Car Car { get; set; }

        decimal DrivingExperiense { get; set; }

        int Age { get; set; }

        SexType Sex { get; set; }

        decimal Money { get; set; }

        Operation Operation { get; set; }

        void ExecuteOperation();

    }
}
