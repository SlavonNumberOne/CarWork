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
