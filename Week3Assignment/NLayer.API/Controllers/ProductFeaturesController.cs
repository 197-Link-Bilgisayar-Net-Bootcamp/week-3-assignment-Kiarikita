using Microsoft.AspNetCore.Mvc;

namespace NLayer.API.Controllers
{
    public class ProductFeaturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
