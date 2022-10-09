using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AirportApiTest.Filters;

public class CodeIATAFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        Debug.WriteLine("Before Action Execution");
        var queries = filterContext.HttpContext.Request.Query.Where(q => q.Key.Contains("IATA"));
        foreach (var query in queries)
        {
            if (!IsIATACode(query.Value))
            {
                filterContext.Result =
                    new BadRequestObjectResult("Incorrect IATA code. Must be contain 3 character in upper case.");
                return;
            }

            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
                return;
            }
        }
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        Debug.WriteLine("After Action Execution");
    }

    private static bool IsIATACode(string value)
    {
        var regex = new Regex("[a-zA-Z]{3,3}");
        return value.Length == 3 && regex.IsMatch(value);
    }
}