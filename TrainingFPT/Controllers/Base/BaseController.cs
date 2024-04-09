using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TrainingFPT.Migrations;

namespace TrainingFPT.Controllers.Base
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var getController = context.ActionDescriptor.RouteValues["Controller"].ToLower();
            var roleId = HttpContext.Session.GetString("SessionRoleId");
            if (getController != "Login")
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUsername")) && !string.IsNullOrEmpty(roleId))
                {
                    var getAction = context.ActionDescriptor.RouteValues["action"];
                    if (int.Parse(roleId) == 1) // admin
                    {
                        if (getController == "category" || (getController == "category" && getAction == "Add") || (getController == "category" && getAction == "Update"))
                        {
                            context.Result = new RedirectResult("/Home/Unauthorised");
                        }
                        else if (getController == "course" || (getController == "course" && getAction == "Add") || (getController == "course" && getAction == "Update"))
                        {
                            context.Result = new RedirectResult("/Home/Unauthorised");
                        }
                        else if (getController == "topic" || (getController == "topic" && getAction == "Add") || (getController == "topic" || getAction == "Update"))
                        {
                            context.Result = new RedirectResult("/Home/Unauthorised");
                        }
                        else
                        {
                            base.OnActionExecuted(context);
                        }
                    }
                    else if (int.Parse(roleId) == 2) //Manage Training Staff
                    {
                     /*   if (getController == "users" && getAction == "TraineeAdd")
                        {
                            context.Result = new RedirectResult("/Home/Unauthorised");
                        }
                        else*/
                        if (getController == "users" && getAction == "Index" || (getController == "users" && getAction == "Add") || (getController == "users" && getAction == "Update"))
                        {
                            context.Result = new RedirectResult("/Home/Unauthorised");
                        }
                        else
                        {
                            base.OnActionExecuted(context);
                        }
                    }
                    else if (int.Parse(roleId) == 3)
                    {
                        if((getController == "course" && getAction == "Index") || getController == "home")
                        {
                            base.OnActionExecuted(context);
                        }
                        else
                        {
                            context.Result = new RedirectResult("/Home/Unauthorised");
                        }
                    }
                }
                else
                {
                    context.Result = new RedirectResult("/Login/Index");
                }
            }
            else
            {
                base.OnActionExecuted(context);
            }
        }
    }
}
