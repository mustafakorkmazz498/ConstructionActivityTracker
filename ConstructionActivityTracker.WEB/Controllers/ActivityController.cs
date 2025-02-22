using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.WEB.Controllers;

public class ActivityController : BaseController
{
    public ActivityController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult Activity()
    {
        return View();
    }
}
