using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.WEB.Controllers;

public class OperationClaimController : BaseController
{
    public OperationClaimController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult OperationClaim()
    {
        return View();
    }
}