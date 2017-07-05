using CDSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CDSite.Controllers
{
    public class OffsiteController : Controller
    {
        // GET: Offsite
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(OffsiteRegisterViewModel model)
        {

            return View(model);
        }
        public ActionResult ConfirmEmail()
        {
            return View();
        }
    }
}