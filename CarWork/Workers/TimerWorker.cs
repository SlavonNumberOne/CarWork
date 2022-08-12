using CarWork.Client.Enums;
using CarWork.Client.Models;
using CarWork.Client.Utils;
using CarWork.Hubs;
using CarWork.RequestModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace CarWork.Client
{
    public class TimerWorker : IHostedService, IDisposable
    {
        private static object _locker = new object();
        private static Timer _timer;
        private readonly IHubContext<CarWorkHub> _carWorkHub;
        
        public TimerWorker(IHubContext<CarWorkHub> carWorkHub)
        {
            _timer = new Timer(200);
            _timer.Elapsed += StepStart;
            _carWorkHub = carWorkHub;

            //initialize for test
            var map = Map.GetInstance();
            map.Refuielings.Add(new Refuieling("ANP", 102, 51m, 1000m, TypeFuel.Oil));
            map.Refuielings.Add(new Refuieling("Avis", 10, 56m, 1000m, TypeFuel.Dizel));
            map.Refuielings.Add(new Refuieling("WOG", 157, 58m, 1000m, TypeFuel.Oil));


            var engine = new Engine(TypeFuel.Oil, 1.8m);
            var car = new Car(ModelCar.Nisan, TypeCar.Pickup, 45m, 5m, engine, 10.2m, 67, TypeFuel.Oil, 0);
            var driver = new TaxiDriver("Oleg", car, 2m, 23, SexType.Male, 5000m);

            var engine1 = new Engine(TypeFuel.Dizel, 2.8m);
            var car1 = new Car(ModelCar.Toyota, TypeCar.Minivan, 50m, 50m, engine1, 9.6m, 20, TypeFuel.Dizel, 20);
            var driver1 = new TaxiDriver("Bob", car1, 6m, 50, SexType.Male, 5000m);

            var engine2 = new Engine(TypeFuel.Dizel, 2.8m);
            var car2 = new Car(ModelCar.Toyota, TypeCar.Minivan, 50m, 50m, engine2, 9.6m, 130, TypeFuel.Dizel, 20);
            var driver2 = new TaxiDriver("Joh", car2, 6m, 50, SexType.Male, 5000m);

            map.Cars.Add(car);
            map.Cars.Add(car1);
            map.Cars.Add(car2);

            map.WorkDrivers.Add(driver);
            map.WorkDrivers.Add(driver1);
            map.WorkDrivers.Add(driver2);


            map.DeliveryOrders.Add(GenerateOrder.Generate());
            map.DeliveryOrders.Add(GenerateOrder.Generate());

           
        }

        private async void StepStart(object sender, ElapsedEventArgs e)
        {
            lock (_locker)
            {
             
                //Console.WriteLine("Start");
                //IWorkDriver driver = new Driver();
                foreach (var driver in Map.GetInstance().WorkDrivers)
                {

                    driver.ExecuteOperation();
                }
                
                //if (Map.DeliveryOrders.Where(o => o.StatusOrder != StatusOrder.Achieved).Count() < Map.WorkDrivers.Count)
                //{
                //    Map.DeliveryOrders.Add(GenerateOrder.Generate());
                //}
                //Console.WriteLine("End");
            }
            var message = new MapObjectsInfoMessage()
            {
                Refuielings = Map.GetInstance().Refuielings,
                DeliveryOrders = Map.GetInstance().DeliveryOrders,
                Drivers = Map.GetInstance().WorkDrivers
            }; 
            await _carWorkHub.Clients.All.SendAsync("ListOfCarsWasUpdated", message);

        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(System.Threading.CancellationToken cancellationToken)
        {
            _timer.Start();
            return Task.CompletedTask;

        }

        public Task StopAsync(System.Threading.CancellationToken cancellationToken)
        {
            _timer.Stop();
            return Task.CompletedTask;
        }
    }
}
