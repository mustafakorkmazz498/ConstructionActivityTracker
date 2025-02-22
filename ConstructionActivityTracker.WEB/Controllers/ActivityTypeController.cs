using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.WEB.Controllers;

public class ActivityTypeController : BaseController
{
    public ActivityTypeController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult ActivityType()
    {
        return View();
    }
}
