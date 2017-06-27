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

        //Details
        //[Authorize]
        //public ActionResult Details()
        //{
        //    ViewBag.Message = "Details page.";
        //    var model = new DetailsViewModel();
        //    model.Address1 = company.Address1;
        //    model.Address2 = company.Address2;
        //    model.City = company.City;
        //    model.State = company.State;
        //    model.PostalCode = company.PostalCode;
        //    model.PhoneNumber = company.PhoneNumber;
        //    return View(model);
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult Details(DetailsViewModel model)
        //{
        //    ViewBag.Message = "Details page.";

        //    var company = this.UserCompany;

        //    company.Name = model.CompanyName;
        //    company.Address1 = model.Address1;
        //    company.Address2 = model.Address2;
        //    company.City = model.City;
        //    company.State = model.State;
        //    company.PostalCode = model.PostalCode;
        //    company.PhoneNumber = model.PhoneNumber;

        //    var service = new CompanyService();
        //    service.Save(company);
        //    model.SuccessMessage = "Success - Profile saved.";
        //    return RedirectToAction("Index", "Manage");
        //}

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Require the user to have a confirmed email before they can log on.
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    ViewBag.errorMessage = "You must have a confirmed email to log on.";
                    return View("Error");
                }
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {

                case SignInStatus.Success:
                    //TODO: if user is seller, redirect to offer/index
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        if (UserManager.IsInRole(user.Id, "Buyer"))
                        {
                            return RedirectToAction("Index", "Offer");
                        }
                    }


                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Deal/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Deal/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
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
            int nullCount = 0;
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

     

        // GET: /Deal/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Deal/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Deal/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Deal/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Deal/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Deal/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
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


    }

}