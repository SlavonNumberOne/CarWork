using CarWork.Client;
using CarWork.Client.Models;
using CarWork.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CarWork.Controllers.Api
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        
        //[HttpPost("create")]
        //public async Task<bool> CreateAsync(Car car)
        //{
        //    var mapAdd = Map.GetInstance();
        //    mapAdd.Cars.Add(car);
            
        
        //    return true;
        //}

    //// DELETE: api/5
    //[HttpDelete("{id}/delete")]
    //public void Delete(int id)
    //{
    //    Map.GetInstance().Cars.Remove(id);
    //}

}
}
