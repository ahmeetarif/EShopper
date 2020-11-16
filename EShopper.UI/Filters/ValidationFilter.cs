using Microsoft.AspNetCore.Mvc.Filters;

namespace EShopper.UI.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}