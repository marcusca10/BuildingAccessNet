using Marcusca10.Samples.BuildingAccessNet.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Marcusca10.Samples.BuildingAccessNet.Web.Controllers
{
    [Authorize]
    public class TenantController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public TenantController()
        { }

        public TenantController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        #region Tenant Management
        // GET: Tenant
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tenant/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tenant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tenant/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tenant/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tenant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tenant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tenant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Tenant user management
        // GET: Tenant/ManageUser
        public ActionResult ManageUser()
        {
            List<ManageUserViewModel> model = new List<ManageUserViewModel>();

            foreach (ApplicationUser user in UserManager.Users)
            {
                model.Add(new ManageUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.UserName
                });
            }

            // DataReader already opened, need to add tenants separately
            foreach (ManageUserViewModel user in model)
            {
                user.Tenant = GetUserTenant(user.Id);
            }

            return View(model);
        }

        // GET: Tenant/NewUser
        public ActionResult NewUser()
        {
            return View();
        }

        // POST: Tenant/NewUser
        [HttpPost]
        public ActionResult NewUser(ManageUserViewModel model)
        {
            try
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = UserManager.Create(user);
                if (result.Succeeded)
                {
                    // Add tenant claim
                    UserManager.AddClaim(user.Id, new Claim(ClaimNames.tenant.ToString(), model.Tenant));

                    return RedirectToAction("ManageUser");
                }
                AddErrors(result.Errors);
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
            }
            return View(model);
        }

        // GET: Tenant/EditUser/5
        public ActionResult EditUser(string id)
        {
            ApplicationUser user = UserManager.FindById(id.ToString());
            ApplicationUser me = UserManager.FindById(User.Identity.GetUserId());

            ManageUserViewModel model = new ManageUserViewModel()
            {
                Id = id.ToString(),
                Email = user.Email,
                Name = user.UserName,
                Tenant = GetUserTenant(id)
        };

            return View(model);
        }

        // POST: Tenant/EditUser/5
        [HttpPost]
        public ActionResult EditUser(Guid id, ManageUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // TODO: Add update logic here
                ApplicationUser user = UserManager.FindById(id.ToString());
                ApplicationUser me = UserManager.FindById(User.Identity.GetUserId());

                user.Email = model.Email;
                user.UserName = model.Name;

                SetTenantClaim(user.Id, model.Tenant);

                return RedirectToAction("ManageUser");
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
            }
            return View(model);
        }

        // GET: Tenant/DeleteUser/5
        public ActionResult DeleteUser(string id)
        {
            ApplicationUser user = UserManager.Users.FirstOrDefault(item => item.Id == id.ToString());
            ManageUserViewModel model = new ManageUserViewModel()
            {
                Id = id,
                Email = user.Email,
                Name = user.UserName,
                Tenant =  GetUserTenant(id)
            };

            return View(model);
        }

        // POST: Tenant/DeleteUser/5
        [HttpPost]
        public ActionResult DeleteUser(Guid id, ManageUserViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                UserManager.Delete(UserManager.Users.FirstOrDefault(item => item.Id == id.ToString()));
                
                return RedirectToAction("ManageUser");
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
            }
            return View(model);
        }
        #endregion

        #region Helper

        private void AddErrors(string error)
        {
            AddErrors(new List<string>() { error });            
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        string GetUserTenant(string id)
        {
            var tenantClaim = UserManager.GetClaims(id).Where(item => item.Type == ClaimNames.tenant.ToString());

            if (tenantClaim.Count() > 0)
                return tenantClaim.First().Value;
            else
                return "00000000-0000-0000-0000-0000000000000";
        }

        void SetTenantClaim(string id, string tenant)
        {
            var claims = UserManager.GetClaims(id);

            Claim claim = claims.FirstOrDefault(item => item.Type == ClaimNames.tenant.ToString());
            if (claim != null)
            {
                if (claim.Value != tenant)
                {
                    // Remove outdated tenant claim
                    UserManager.RemoveClaim(id, claim);

                    // Add new tenant claim
                    UserManager.AddClaim(id, new Claim(ClaimNames.tenant.ToString(), tenant));
                }
            }
            else throw new Exception("The tenant value is invalid.");
        }

        #endregion

    }
}
