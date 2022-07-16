using CarWork.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarWork.Controllers
{
    public class UIController : Controller
    {
        
        private readonly IHubContext<CarWorkHub> _carWorkHub;

        public UIController(IHubContext<CarWorkHub> carWorkHub)
        {
            _carWorkHub = carWorkHub;
        }
        public IActionResult RoadLife()
        {
            return View();
        }
    }
}
