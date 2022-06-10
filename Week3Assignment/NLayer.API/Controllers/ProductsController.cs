using Microsoft.AspNetCore.Mvc;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}
