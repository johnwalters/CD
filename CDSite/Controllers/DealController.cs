using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CDSite.Models;
using CDLib;
using CDLib.Domain;
using System.Collections.Generic;

namespace CDSite.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class DealController : BaseController
    {
        // GET: Deal


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public DealController()
        {

        }

        public DealController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }



        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

   

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register(string offerToken)
        {
            var model = new DealRegisterViewModel();
            var offerService = new OfferService();
            Offer offer = offerService.GetOfferByToken(offerToken);
            model.IsCodeAvailable = isCodeAvailable(offer.Id);
            model.Title = offer.Title;
            model.Description = offer.Description;
            model.OfferToken = offerToken;
            return View(model);
        }
        public bool isCodeAvailable(int offerId)
        {
            var offerService = new OfferService();
            Offer offer = offerService.GetOffer(offerId);
            //List<OfferCode> offerCodeList = new List<OfferCode>();
            var offerCodeList = offerService.GetAllOfferCodes(offer.Id);
            return offerCodeList.Any(oc => String.IsNullOrEmpty(oc.ClaimingUser));
            //foreach (OfferCode item in offerCodeList)
            //{
            //    offerCodeList.Add(item);
            //    if (item.ClaimingUser == null)
            //    {
            //        nullCount++;
            //    }
            //}
            //if (nullCount > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public string CodeForUser(int offerId, string userId)
        {
            var offerService = new OfferService();
            Offer offer = offerService.GetOffer(offerId);
            
            var offerCodeList = offerService.GetAllOfferCodes(offer.Id);
            var offerCode =  offerCodeList.Where(oc => oc.ClaimingUser == userId).FirstOrDefault();
            if (offerCode != null)
            {
                return offerCode.Code;
            }
            else
            {
                return null;
            }
          
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(DealRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //password = 8^)U/G/D49HKT{2R
                var offerService = new OfferService();
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                var result = await UserManager.CreateAsync(user, "kf6Ua?a<2DhfZ<,t");
                if (result.Succeeded)
                {
                    //Line below commented out to prevent log in until the user is confirmed.
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link

                    //Create company with user ID


                    //Add user role of buyer
                    await UserManager.AddToRoleAsync(user.Id, "Buyer");


                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account", model.OfferToken);

                    // Uncomment to debug locally 
                    // TempData["ViewBagLink"] = callbackUrl;


                    ViewBag.Message = "Please check your email to receive your code.";

#if DEBUG
                    ViewBag.Message += callbackUrl;
#endif
                    return View("Info");
                    //return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        [AllowAnonymous]
        public async Task<ActionResult> RegisterJson(DealRegisterViewModel model)
        {
            var registerResult = new DealRegisterResult();
            if (ModelState.IsValid)
            {
                //password = kf6Ua?a<2DhfZ<,t
                var offerService = new OfferService();
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                var result = await UserManager.CreateAsync(user, "kf6Ua?a<2DhfZ<,t");
                if (result.Succeeded)
                {
                    //Line below commented out to prevent log in until the user is confirmed.
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link

                    //Create company with user ID


                    //Add user role of buyer
                    await UserManager.AddToRoleAsync(user.Id, "Buyer");


                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account", model.OfferToken);
                    registerResult.IsSuccessful = true;
                    registerResult.SuccessMessage = "Please check your email to receive your code.";

#if DEBUG
                    registerResult.SuccessMessage += callbackUrl;
#endif
                    // Uncomment to debug locally 
                    // TempData["ViewBagLink"] = callbackUrl;

                    var jsonResult = Json(registerResult);
                    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return jsonResult;
                    //return RedirectToAction("Index", "Home");

                }
                else
                {
                    AddErrors(result, registerResult);
                    registerResult.IsSuccessful = false;
                    var jsonResult = Json(registerResult);
                    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    
                    return jsonResult;
                }
            }
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                        registerResult.ErrorMessages.Add(error.ErrorMessage);
                }
            }
            var errorResult = Json(registerResult);
            errorResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return errorResult;
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string offerToken, string code)
        {
            if (userId == null || offerToken == null)
            {
                return View("Error");
            }
            OfferService offerService = new OfferService();
            var model = new DealRegisterViewModel();
            var offer = offerService.GetOfferByToken(offerToken);
            var existingOfferCode = CodeForUser(offer.Id, userId);
            if (existingOfferCode == null)//if user does not have code yet, claim next
            {
                var offerCode = offerService.ClaimNextCode(offer.Id, userId);
                model.OfferCode = offerCode;
                model.Title = offer.Title;
                model.Description = offer.Description;
                model.Url = offer.Url;
            }
            else//otherwise use the code that user already has
            {
                model.OfferCode = existingOfferCode;
                model.Title = offer.Title;
                model.Description = offer.Description;
                model.Url = offer.Url;
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail", model);
            }
            else
            {
                return View("Error");
            }
        }
        
        
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmailJson(string userId, string offerToken, string code)
        {
            var confirmEmailResult = new DealConfirmEmailResult();
            if (userId == null || offerToken == null)
            {
                var errors = Json(confirmEmailResult);
                if (userId == null)
                {
                    confirmEmailResult.IsSuccessful = false;
                    confirmEmailResult.ErrorMessages.Add("userId invalid");
                    errors = Json(confirmEmailResult);
                }
                if (offerToken == null)
                {
                    confirmEmailResult.IsSuccessful = false;
                    confirmEmailResult.ErrorMessages.Add("offerToken invalid");
                    errors = Json(confirmEmailResult);
                }
                errors.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return errors;
            }
            OfferService offerService = new OfferService();
            var model = new DealRegisterViewModel();
            var offer = offerService.GetOfferByToken(offerToken);
            //TODO: If offer is null we need to return unsuccessful Message: "Offer not found"
            var existingOfferCode = CodeForUser(offer.Id, userId);
            if (existingOfferCode == null)//if user does not have code yet, claim next
            {
                var offerCode = offerService.ClaimNextCode(offer.Id, userId);
                model.OfferCode = offerCode;
                model.Title = offer.Title;
                model.Description = offer.Description;
                model.Url = offer.Url;
            }
            else//otherwise use the code that user already has
            {
                model.OfferCode = existingOfferCode;
                model.Title = offer.Title;
                model.Description = offer.Description;
                model.Url = offer.Url;
            }
            //TODO: Handle if userId is bad.
            try
            {
                var result = await UserManager.ConfirmEmailAsync(userId, code);
                if (result.Succeeded)
                {
                    confirmEmailResult.IsSuccessful = true;
                    confirmEmailResult.Code = model.OfferCode;
                    confirmEmailResult.Description = model.Description;
                    confirmEmailResult.Url = model.Url;
                    var success = Json(confirmEmailResult);
                    success.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return success;
                }
                else
                {
                    confirmEmailResult.IsSuccessful = false;
                    confirmEmailResult.Code = null;
                    //TODO: change error message assignment to foreach
                    confirmEmailResult.ErrorMessages[0] = "Unhandled error occurred.";
                    var errors = Json(confirmEmailResult);
                    errors.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return errors;
                }
            }
            catch(System.InvalidOperationException ex)
            {
                confirmEmailResult.IsSuccessful = false;
                confirmEmailResult.ErrorMessages.Add(ex.Message);
                var errors = Json(confirmEmailResult);
                errors.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return errors;
            }
            catch (Exception ex)
            {
                confirmEmailResult.IsSuccessful = false;
                confirmEmailResult.ErrorMessages.Add("Unknown Error");
                var errors = Json(confirmEmailResult);
                errors.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return errors;
            }

        }



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void AddErrors(IdentityResult identityResult, DealRegisterResult dealRegisterResult)
        {
            foreach (var error in identityResult.Errors)
            {
                dealRegisterResult.ErrorMessages.Add(error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject, string offerToken)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Deal",
               new { userId = userID, code = code, offerToken = offerToken }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(userID, subject,
               "Please receive your code by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return callbackUrl;
        }
#endregion


        public class DealRegisterResult
        {
            public bool IsSuccessful { get; set; }
            public string SuccessMessage { get; set; }
            public List<string> ErrorMessages { get; set; }

            public DealRegisterResult()
            {
                ErrorMessages = new List<string>();
            }
            
        }

        public class DealConfirmEmailResult
        {
            public bool IsSuccessful { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public List<string> ErrorMessages = new List<string>();
        }

    }

}