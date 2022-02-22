using Microsoft.AspNetCore.Mvc;

namespace DB.AirBDB.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
