using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConstructionActivityTracker.WEB.Helpers;

public static class HtmlHelpers
{
    public static string IsActive(this IHtmlHelper html, string controller, string action, string cssClass = "active")
    {
        var routeData = html.ViewContext.RouteData;

        var currentController = routeData.Values["controller"]?.ToString();
        var currentAction = routeData.Values["action"]?.ToString();

        return (string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(action, currentAction, StringComparison.OrdinalIgnoreCase))
            ? cssClass
            : string.Empty;
    }

    public static string IsAccordionActive(this IHtmlHelper html, string[] controllers, string cssClass = "nav-item-open")
    {
        var routeData = html.ViewContext.RouteData;

        var currentController = routeData.Values["controller"]?.ToString();

        return controllers.Any(controller => string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase))
            ? cssClass
            : string.Empty;
    }

    public static string GetAccordionStyle(this IHtmlHelper html, string[] controllers)
    {
        var routeData = html.ViewContext.RouteData;

        var currentController = routeData.Values["controller"]?.ToString();

        return controllers.Any(controller => string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase))
            ? "display: block;"
            : "display: none;";
    }
    public static string GetPageTitle(this IHtmlHelper html)
    {
        var routeData = html.ViewContext.RouteData;

        var currentController = routeData.Values["controller"]?.ToString();
        var currentAction = routeData.Values["action"]?.ToString();

        return $"{currentAction}";
    }
}

