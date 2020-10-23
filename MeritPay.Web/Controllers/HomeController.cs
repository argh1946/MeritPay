using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace MeritPay.Web.Controllers
{
    public class HomeController : BaseController
    {
        //[NeedPermission(Core.Common.UserPermission.FullAccess)]
        public IActionResult Index()
        {            
            return View();
        }

        [NeedPermission(Core.Common.UserPermission.BranchAccess)]
        public IActionResult Report()
        {

            return View();
        }

        [HttpPost]
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }
    }
}
