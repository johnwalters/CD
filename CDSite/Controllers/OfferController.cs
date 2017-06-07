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
    public class OfferController : BaseController
    {
        
        // GET: Offer
        public ActionResult Index()
        {
            OfferListViewModel model = new OfferListViewModel();
            model.OfferList = new List<OfferViewModel>();
            //Pull data from database and display in table.
            CompanyService companyService = new CompanyService();
            OfferService offerService = new OfferService();

            var company = base.UserCompany;
            var offerList = offerService.GetAllOffers(company.Id);
            foreach (var item in offerList)
            {
                OfferViewModel offerViewModel = new OfferViewModel();
                offerViewModel.Title = item.Title;
                offerViewModel.Description = item.Description;
                offerViewModel.Url = item.Url;
                offerViewModel.Category = item.Category;
                model.OfferList.Add(offerViewModel);
                offerViewModel.Id = item.Id;
            }
            
            return View(model);
        }
        
        //[HttpPost]
        //[Authorize]
        //public ActionResult Index(IndexViewModel model)
        //{
        //    ViewBag.Message = "Offers page.";
        //}
        public ActionResult Create(ManageMessageId? message)
        {
            var company = UserCompany;

            var model = new OfferViewModel { };
            
            var companyService = new CompanyService();
            

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(OfferViewModel model)
        {
            ViewBag.Message = "Create page.";

            var company = UserCompany;
            var companyService = new CompanyService();
           
            model.SuccessMessage = "Success - Profile saved.";
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(OfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var company = UserCompany;
            var offer = new Offer();
            var offerService = new OfferService();

            offer.Title = model.Title;
            offer.Description = model.Description;
            offer.Url = model.Url;
            offer.Category = model.Category;
            offer.CompanyId = company.Id;
            offer.Id = model.Id;

            offerService.SaveOffer(offer);
            
            model.SuccessMessage = "Success - Offer saved.";
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            // ViewBag.Message = "Edit page.";

            var company = UserCompany;
            var offerService = new OfferService();

            var offer = offerService.GetOffer(id);
            
            var model = new OfferViewModel();
            if(offer != null && offer.CompanyId != UserCompany.Id)
            {
                offer = null;
            }
            if (offer == null)
            {
                //offer ain't there.
                model.ErrorMessage = "Offer not found.";
            }
            
            else
            {
                model.Title = offer.Title;
                model.Description = offer.Description;
                model.Url = offer.Url;
                model.Category = offer.Category;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(OfferViewModel model)
        {
            OfferService service = new OfferService();
            service.DeleteOffer(model.Id);
            return RedirectToAction("Index");
        }
    }
}