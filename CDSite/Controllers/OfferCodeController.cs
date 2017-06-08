using CDLib;
using CDLib.Domain;
using CDSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDSite.Controllers
{
    public class OfferCodeController : BaseController
    {
        // GET: OfferCode
        public ActionResult List(int offerId)
        {
            OfferCodeListViewModel model = new OfferCodeListViewModel();
            model.OfferId = offerId;
            OfferService offerService = new OfferService();
            Offer offer = offerService.GetOffer(offerId);
            model.OfferTitle = offer.Title;
            model.OfferCodeList = new List<OfferCodeViewModel>();
            //Pull data from database and display in table.
            var offerCodeList = offerService.GetAllOfferCodes(offer.Id);
            var offerList = offerService.GetAllOfferCodes(offer.Id);
            foreach (var item in offerCodeList)
            {
                OfferCodeViewModel offerCodeViewModel = new OfferCodeViewModel();
                offerCodeViewModel.Code = item.Code;
                offerCodeViewModel.OfferId = item.OfferId;
                offerCodeViewModel.Id = item.Id;
                model.OfferCodeList.Add(offerCodeViewModel);

            }
            return View(model);
        }

        //public ActionResult Create(ManageMessageId? message)
        //{
        //    var company = UserCompany;
        //    var model = new OfferViewModel { };
        //    var companyService = new CompanyService();
        //    return View(model);
        //}
        public ActionResult Create(int offerId)
        {
            var model = new OfferCodeViewModel { };

            var offerService = new OfferService();

            model.OfferId = offerId;
            model.Id = 0;

            return View(model);
        }
        //[HttpPost]
        //[Authorize]
        //public ActionResult Create(OfferCodeViewModel model)
        //{
        //    ViewBag.Message = "Create page.";

        //    var offer = new Offer();
        //    var offerService = new OfferService();

        //    model.SuccessMessage = "Success - Offer Code saved.";
        //    return View(model);
        //}

        [HttpPost]
        [Authorize]
        public ActionResult Edit(OfferCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var offerCode = new OfferCode();
            var offerService = new OfferService();

            offerCode.Code = model.Code;
            offerCode.OfferId = model.OfferId;
            offerCode.Id = model.Id;
            offerService.SaveOfferCode(offerCode);

            model.SuccessMessage = "Success - Offer Code saved.";
            return RedirectToAction("List", new { offerId = model.OfferId });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            // ViewBag.Message = "Edit page.";

            var offerService = new OfferService();

            var offerCode = offerService.GetOfferCode(id);
            var model = new OfferCodeViewModel();

            // if offercode not null, then get offer. if offer not null, then verify offer.company is user's company
            if (offerCode != null)
            {
                Offer offer = new Offer();
                offer = offerService.GetOffer(offerCode.OfferId);
                var company = UserCompany;
                if (offer.CompanyId != company.Id)
                {
                    offerCode = null;
                }
            }
            if (offerCode == null)
            {
                //offer ain't there.
                model.ErrorMessage = "Offer Code not found.";
            }

            else
            {
                model.Code = offerCode.Code;
                model.OfferId = offerCode.OfferId;
                model.Id = offerCode.Id;
            }

            return View(model);
        }
    }
}