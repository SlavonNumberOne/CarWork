using CarWork.Client;
using CarWork.Client.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarWork.Controllers.Api
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<bool> CreateAsync()
        {
            var order = GenerateOrder.Generate();
            Map.GetInstance().DeliveryOrders.Add(order);

            return true;
        }
    }
}
