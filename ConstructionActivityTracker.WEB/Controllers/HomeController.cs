using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.WEB.Controllers;

public class HomeController : BaseController
{
    public HomeController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult Index()
    {
        ViewBag.ActivePage = "Index";
        return View();
    }
}