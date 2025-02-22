using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.WEB.Controllers;

public class UserOperationClaimController : Controller
{
    public IActionResult UserOperationClaim()
    {
        return View();
    }
}
