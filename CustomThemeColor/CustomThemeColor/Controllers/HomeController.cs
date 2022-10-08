using CustomThemeColor.ActionFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomThemeColor.Controllers
{
    [CustomActionFilter]
    public class HomeController : Controller
    {
        public static string BgColor {
            get { return "#808080"; } 
        }
        public static string BorderColor
        {
            get{ return "#757575";}
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.BgColor = BgColor;
            ViewBag.BorderColor = BorderColor;
            // Access the controller, parameters, querystring, etc. from the filterContext
            base.OnActionExecuting(filterContext);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}