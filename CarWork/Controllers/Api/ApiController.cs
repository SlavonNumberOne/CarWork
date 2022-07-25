using CarWork.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarWork.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IHubContext<CarWorkHub> _carWorkHub;
        public ApiController(IHubContext<CarWorkHub> carWorkHub)
        {
            _carWorkHub = carWorkHub;
        }

        [HttpPost("create")]
        public 
    }
}
