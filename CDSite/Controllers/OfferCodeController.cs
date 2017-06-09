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
            model.OfferCodeList = new List<OfferCodeViewModel>();
            OfferService offerService = new OfferService();
            Offer offer = offerService.GetOffer(offerId);
            
            if(offer == null || !offerIsOwnedByUserCompany(offerId))
            {
                model.ErrorMessage = " Offer not found.";
                return View(model);
            }
            model.OfferTitle = offer.Title;
            
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
            
            if(!offerIsOwnedByUserCompany(offerId))
            {
                return RedirectToAction("List", new { offerId });
            }
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
            var offerService = new OfferService();
            if (model.Id != 0)//not creating a new offerCode, but rather editing
            {
                var offerCode = offerService.GetOfferCode(model.Id);
                if (offerCode != null)
                {
                    if (!offerIsOwnedByUserCompany(offerCode.OfferId))
                    {
                        return RedirectToAction("List", new { offerId = offerCode.OfferId });
                    }
                }
                else//if offercode == null
                {
                    return RedirectToAction("List", new { offerId = model.OfferId });
                }
                offerCode.Code = model.Code;
                offerCode.OfferId = model.OfferId;
                offerCode.Id = model.Id;
                offerService.SaveOfferCode(offerCode);

                model.SuccessMessage = "Success - Offer Code saved.";
            }
            else //creating new offer code
            {
                if(!offerIsOwnedByUserCompany(model.OfferId))
                {
                    return RedirectToAction("List", new { offerId = model.OfferId });
                }
            }
            return RedirectToAction("List", new { offerId = model.OfferId });

        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.Message = "Edit page.";

            var offerService = new OfferService();

            var offerCode = offerService.GetOfferCode(id);
            var model = new OfferCodeViewModel();

            
            // if offercode not null, then get offer. if offer not null, then verify offer.company is user's company
            if(offerCode == null)
            {
                return RedirectToAction("List", new { offerId = 0 });
            }
            if(!offerCodeIsOwnedByUserCompany(offerCode))
            {
                return RedirectToAction("List", new { offerId = offerCode.OfferId });
            }
            
                model.Code = offerCode.Code;
                model.OfferId = offerCode.OfferId;
                model.Id = offerCode.Id;
            

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(OfferCodeViewModel model)
        {
            if (!offerIsOwnedByUserCompany(model.OfferId))
            {
                return RedirectToAction("List", new { offerId = model.OfferId });
            }
                OfferService service = new OfferService();
                service.DeleteOfferCode(model.Id);
                return RedirectToAction("List", new { offerId = model.OfferId });
        }

        public bool offerCodeIsOwnedByUserCompany(OfferCode offerCode)
        {            OfferService offerService = new OfferService();
            if (offerCode != null)
            {
                Offer offer = new Offer();
                offer = offerService.GetOffer(offerCode.OfferId);
                var company = UserCompany;
                if (offer != null && offer.CompanyId != company.Id)
                {
                    offerCode = null;
                }
                if (offer == null)
                {
                    //offer ain't there.
                    return false;
                }
                //if we got this far, should be valid.
                return true;
            }
            //if offer code is null
            else { return false; }
        }
        //overloaded for offerId
        public bool offerIsOwnedByUserCompany(int offerId)
        {
            OfferService offerService = new OfferService();
            var offer = offerService.GetOffer(offerId);
            if (offer != null)
            {
                var company = UserCompany;
                if (offer.CompanyId != company.Id)
                {
                    return false;
                }
                //if we got this far, should be valid.
                return true;
            }
            //if offer code is null
            else { return false; }
        }
    }
}