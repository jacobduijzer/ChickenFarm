using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChickenFarm.FarmService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}