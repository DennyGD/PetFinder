﻿namespace PetFinder.Web.Infrastructure.Filters
{
    using System.Web.Mvc;

    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.HttpContext.Request.IsAjaxRequest()))
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}
