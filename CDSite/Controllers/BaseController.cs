using CDLib;
using CDLib.Domain;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDSite.Controllers
{
    public class BaseController : Controller
    {
        private Company _userCompany;
        protected Company UserCompany
        {
            get
            {
                if (_userCompany == null)
                {
                    var userId = User.Identity.GetUserId();
                    var service = new CompanyService();
                    var company = service.GetByUserId(userId);
                    _userCompany = company;
                }
                return _userCompany;
            }
        }
    }
}