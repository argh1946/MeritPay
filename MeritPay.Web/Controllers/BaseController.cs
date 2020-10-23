using System.Linq;
using MeritPay.Core.Common;
using MeritPay.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MeritPay.Web.Controllers
{
    public class NoCache : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            filterContext.HttpContext.Response.Headers["Expires"] = "-1";
            filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";

            base.OnResultExecuting(filterContext);
        }
    }
    [NoCache]
    public class BaseController : Controller
    {

        
        public ActionResult ExitProgram()
        { 
            //Faranam.Utils.Log.LogAction(TextMessages.Message("10202"));
            CommonHelper.CurrentUser.Logout();
            return new RedirectResult(AppSettings.SSOUrl + "/login?ReturnUrl=" + AppSettings.Url, false);
        }
    }
   
    public class NeedPermissionAttribute : ActionFilterAttribute
    {
        UserPermission _permission;
        string[] _ids;
        public NeedPermissionAttribute(UserPermission permission, params string[] ids)
        {
            _permission = permission;
            _ids = ids;

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (
                CommonHelper.CurrentUserHasPermission(_permission)
                && (
                    _ids.Length == 0 ||
                    _ids.Any(x => x == CommonHelper.CurrentUser.UserName)
                )
            )
                base.OnActionExecuting(filterContext);
            else
            {
                filterContext.Result = new RedirectResult(AppSettings.SSOUrl+"/login?ReturnUrl="+AppSettings.Url, false);
            }
        }
    }
}