using Survey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Utils
{
    public class BaseController : Controller
    {
        // GET: Base
        public SurveyEntities5 db = new SurveyEntities5();
        public string UserName { get; set; }
        public string NameSurname { get; set; }
        public string CategoryName { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Session["UserName"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/SignIn");
            }
            else
            {
                UserName = Session["UserName"].ToString();
                NameSurname = Session["NameSurname"].ToString();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}