using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDSite.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        // GET: Offer
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //[Authorize]
        //public ActionResult Index(IndexViewModel model)
        //{
        //    ViewBag.Message = "Offers page.";
        //}
        public ActionResult Create()
        {
            return View();
        }
    }
}