﻿using Marcusca10.Samples.BuildingAccessNet.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcusca10.Samples.BuildingAccessNet.Web.Controllers
{
    [Authorize]
    public class TenantController : Controller
    {
        private ApplicationUserManager _userManager;

        public TenantController()
        { }

        public TenantController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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
                    Id = new Guid(user.Id),
                    Email = user.Email,
                    Name =  user.UserName
                });
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
                // TODO: Add insert logic here

                return RedirectToAction("ManageUser");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Tenant/EditUser/5
        public ActionResult EditUser(Guid id)
        {
            ApplicationUser user = UserManager.Users.FirstOrDefault(item => item.Id == id.ToString());
            ManageUserViewModel model = new ManageUserViewModel()
            {
                Id = id,
                Email = user.Email,
                Name = user.UserName,
                Tenant = ""
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

                return RedirectToAction("ManageUser");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Tenant/DeleteUser/5
        public ActionResult DeleteUser(Guid id)
        {
            ApplicationUser user = UserManager.Users.FirstOrDefault(item => item.Id == id.ToString());
            ManageUserViewModel model = new ManageUserViewModel()
            {
                Id = id,
                Email = user.Email,
                Name = user.UserName,
                Tenant = ""
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

                return RedirectToAction("ManageUser");
            }
            catch
            {
                return View(model);
            }
        }
        #endregion
    }
}