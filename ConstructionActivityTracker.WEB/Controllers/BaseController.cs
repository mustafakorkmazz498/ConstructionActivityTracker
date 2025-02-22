using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.WEB.Controllers;

public class BaseController : Controller
{
    protected readonly string ApiBaseUrl;
    protected readonly string UiBaseUrl;

    public BaseController(IConfiguration configuration)
    {
        ApiBaseUrl = configuration["ApiSettings:BaseUrl"];
        UiBaseUrl = configuration["UiSettings:BaseUrl"];
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var currentController = filterContext.RouteData.Values["controller"]?.ToString();
        var currentAction = filterContext.RouteData.Values["action"]?.ToString();

        ViewBag.ApiBaseUrl = ApiBaseUrl;
        ViewBag.UiBaseUrl = UiBaseUrl;
        ViewBag.CurrentController = currentController;
        ViewBag.CurrentAction = currentAction;

        //var allowedActions = new[] { "Login" };
        //if (allowedActions.Contains(currentAction, StringComparer.OrdinalIgnoreCase))
        //{
        //    base.OnActionExecuting(filterContext);
        //    return;
        //}

        //var token = filterContext.HttpContext.Request.Cookies["token"];

        //if (string.IsNullOrEmpty(token))
        //{
        //    filterContext.Result = new RedirectToActionResult("Unauthorized", "Error", null);
        //    return;
        //}

        base.OnActionExecuting(filterContext);
    }
}
