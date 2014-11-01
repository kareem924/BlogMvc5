using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMvc5.Models;
using BlogMvc5.Models.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMvc5.Controllers
{
    public class UsersController : Controller
    {
          IUnitOfWork _uow;
        public UsersController()
        {
            _uow = new UnitOfWork();
        }
        public UsersController(IUnitOfWork uof) // Fake 
        {
            _uow = uof;
        }
        //
        // GET: /Users/
        public ActionResult Index()
        {
            var allUsers = _uow.Users.List().Select(user => new UserRoleViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = user.Roles.Select(role => new RoleViewModel
                {
                    RoleName = role.Role.Name
                }).ToList()
            });
            return View(allUsers);
        }
        public ActionResult Edit(string Id)
        {
            var selectedUser = _uow.Users.List(user => user.Id == Id).Select(user => new Models.UserRoleViewModel
            {
                UserName = user.UserName,
                Roles = user.Roles.Select(role => new Models.RoleViewModel
                {
                    RoleId = role.RoleId,
                    RoleName = role.Role.Name
                }).ToList()
            }).FirstOrDefault();

            ViewBag.roleName = new SelectList(_uow.Roles.List(), "Name", "Name");

            ViewBag.UserRoles = new SelectList(selectedUser.Roles, "RoleName", "RoleName");
          
            return View("edit", selectedUser);
        }
        public ActionResult AddToRole(string Id, string roleName)
        {
            var result = this.AddUserToRole(Id, roleName);
            if (result.Succeeded)
            {
                ViewBag.Message = "User has been added to role successfully!.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return this.Edit(Id);
        }

        public ActionResult RemoveFromRole(string Id, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new BlogDbContext()));

            var result = userManager.RemoveFromRole(Id, roleName);
            if (result.Succeeded)
            {
                ViewBag.Message = "User has been removed from role successfully!.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return this.Edit(Id);
        }
        private IdentityResult AddUserToRole(string Id, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new BlogDbContext()));
            IdentityResult result;
            if (!userManager.IsInRole(Id, roleName))
            {
                result = userManager.AddToRole(Id, roleName);
            }
            else
            {
                string message = string.Format("User is already existed in {0} Role", roleName);
                result = new IdentityResult(message);
            }
            return result;
        }
	}
}