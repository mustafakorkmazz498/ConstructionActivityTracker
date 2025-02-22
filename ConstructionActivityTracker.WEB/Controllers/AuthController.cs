using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.WEB.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IConfiguration configuration) : base(configuration)
        {
        }
		public IActionResult User()
		{
			return View();
		}
		public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
