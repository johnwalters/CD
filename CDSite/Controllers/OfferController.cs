using CDLib;
using CDLib.Domain;
using CDSite.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static CDSite.Controllers.ManageController;

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
        public ActionResult Create(ManageMessageId? message)
        {
            var userId = User.Identity.GetUserId();

            var model = new OfferViewModel { };

            var service = new OfferService();
            var companyService = new CompanyService();
            var company = companyService.GetByUserId(userId);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(OfferViewModel model)
        {
            ViewBag.Message = "Create page.";

            var userId = User.Identity.GetUserId();
            var service = new OfferService();
            var companyService = new CompanyService();
            var company = companyService.GetByUserId(userId);


            //offer.Title = model.Title;
            //offer.Description = model.Description;
            //offer.Url = model.Url;


            // service.Save(offer);
            model.SuccessMessage = "Success - Profile saved.";
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(OfferViewModel model)
        {
            // ViewBag.Message = "Edit page.";

            var userId = User.Identity.GetUserId();
            var service = new CompanyService();
            var company = service.GetByUserId(userId);
            var offer = new Offer();
            var offerService = new OfferService();

            offer.Title = model.Title;
            offer.Description = model.Description;
            offer.Url = model.Url;
            offer.CompanyId = company.Id;

            offerService.Save(offer);
            
            model.SuccessMessage = "Success - Offer saved.";
            return RedirectToAction("Index");
        }
    }
}