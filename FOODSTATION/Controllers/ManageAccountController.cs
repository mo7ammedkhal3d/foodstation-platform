using System;
using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using FOODSTATION.Models;
using System.Data.Entity;
using FOODSTATION.Models.ViewModels;
using System.IO;

namespace FOODSTATION.Controllers
{
    public class ManageAccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        //private ApplicationRoleManager _roleManager;
        private ApplicationDbContext db;

        public ManageAccountController()
        {
            db = new ApplicationDbContext();
        }

        public ManageAccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        public ActionResult UserDashboard()
        {
            var users = UserManager.Users.ToList().Select(u => new UserVM
            {
                Id = u.Id,  
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = string.Join(", ", UserManager.GetRolesAsync(u.Id).Result)
            }).ToList();

            ViewBag.Users = users;  

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roles = roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.Roles = roles;

            return View();
        }

        public PartialViewResult Refreash()
        {
            var users = UserManager.Users.ToList().Select(u => new UserVM
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = string.Join(", ", UserManager.GetRolesAsync(u.Id).Result)
            }).ToList();

            ViewBag.Users = users;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roles = roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.Roles = roles;

            return PartialView("_UserPartial", users);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewUser(UserVM model)
        {
            model.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id,model.Role);
                    model.RegistrationInValid = "";
                    var returnedRoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                    var returnedRole = returnedRoleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
                    ViewBag.Roles = returnedRole;
                    return PartialView("_AddUser", model);
                }
                AddErrors(result);
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roles = roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.Roles = roles;
            // If we got this far, something failed, redisplay form
            return PartialView("_AddUser", model);
        }

        public async Task<ActionResult> GetUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                var roles = await UserManager.GetRolesAsync(user.Id);
                var updatedUser = new UserVM
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,                  
                    Role = roles.Last(),
            };
                return Json(updatedUser, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        // Edit a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserVM model)
        {
            model.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.Email = model.Email;
                user.UserName = model.UserName; 
                user.PhoneNumber= model.PhoneNumber;    
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var Userroles = await UserManager.GetRolesAsync(user.Id);
                    foreach(var role in Userroles)
                    {
                        await UserManager.RemoveFromRoleAsync(user.Id, role);
                    }
                    await UserManager.AddToRoleAsync(user.Id, model.Role);
                    model.RegistrationInValid = "";
                    var returnedRoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                    var returnedRole = returnedRoleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
                    ViewBag.Roles = returnedRole;
                    return PartialView("_EditUser", model);

                }
                AddErrors(result);
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roles = roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.Roles = roles;
            // If we got this far, something failed, redisplay form
            return PartialView("_EditUser", model);
        }

        // Delete a user
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var message = "";
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                message = "لايوجد مستخدم بالمعرف المرسل الى السرفر";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            var restaurants = db.Restaurants.Where(x => x.UserId == user.Id);
            if (restaurants.Count() > 0)
            {
                message = "haveRestaurant";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            var bills = db.Bills.Where(x => x.UserId == user.Id);
            if (bills.Count() > 0)
            {
                message = "haveBills";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            AddErrors(result);
            return View("Error");
        }

        // Delete a user
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUserAndRestaurants(string id)
        {

            var user = await UserManager.FindByIdAsync(id);

            var restaurants = db.Restaurants.Where(x => x.UserId == user.Id).ToList();
            foreach (var restaurant in restaurants)
            {
                var items = db.Items.Where(x => x.RestaurantId == restaurant.Id).ToList();
                
                if (items.Count() > 0)
                {
                    foreach (var item in items)
                    {
                        string itemOldPath = Path.Combine(Server.MapPath("~/Uploads/Items/"), item.ImgUrl);
                        System.IO.File.Delete(itemOldPath);
                        db.Items.Remove(item);
                    }
                }
                string restaurantOldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), restaurant.ImgUrl);
                System.IO.File.Delete(restaurantOldPath);
                db.Restaurants.Remove(restaurant);
                db.SaveChanges();
            }

            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            AddErrors(result);
            return View("Error");
        }

        // Add a user to a role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUserToRole(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var result = await _userManager.AddToRoleAsync(user.Id, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View("Error");
        }

        // Remove a user from a role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveUserFromRole(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var result = await _userManager.RemoveFromRoleAsync(user.Id, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View("Error");
        }

        // Get a list of user roles
        public async Task<ActionResult> UserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var roles = await _userManager.GetRolesAsync(user.Id);
            //var model = new UserRolesViewModel
            //{
            //    UserId = user.Id,
            //    UserEmail = user.Email,
            //    UserRoles = roles
            //};
            return View(/*model*/);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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

                if(db != null)
                {
                    db.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}