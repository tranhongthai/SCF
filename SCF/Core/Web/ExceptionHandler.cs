﻿using System.Web;
using System.Web.Mvc;
using Peyton.Core.Common;
using Peyton.Core;

namespace System.Web.Mvc
{
    public class HandleExceptionAttribute : HandleErrorAttribute
    {

        public HandleExceptionAttribute()
        {
        }

        public override void OnException(ExceptionContext filterContext)
        {
            //if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    return;
            //}

            //if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            //{
            //    return;
            //}

            //if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            //{
            //    return;
            //}

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var response = new JsonResponse<bool>();
                response.Errors.Add(filterContext.Exception.Message, "Unknown");
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = response
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };

                filterContext.HttpContext.Response.StatusCode = 500;
            }

            // log the error using log4net.
            Logger.LogError(filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
