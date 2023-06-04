using FOODSTATION.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FOODSTATION.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationSignInManager _signInManager;
        private readonly ApplicationDbContext _context;

        public UserAuthController(ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
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


        ////
        //// POST: /Account/Login
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            model.LoginInValid = "true";

            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName,
                                                                     model.Password,
                                                                     model.RememberMe,
                                                                     shouldLockout: false);
                if (result == SignInStatus.Success)
                {
                    model.LoginInValid = "";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }

            }
            return PartialView("_UserLoginPartial", model);
        }
    }
}