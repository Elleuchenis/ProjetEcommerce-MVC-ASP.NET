﻿using Azure;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using ProjetEcommerceApplication.ViewModels;

namespace ProjetEcommerceApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager ;
            this.userManager = userManager;
        }
        [HttpGet
]

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)

            {
                IdentityRole role = new IdentityRole { Name = model.RoleName };
                IdentityResult result = await roleManager.CreateAsync(role);
                if(result.Succeeded)

                {
                    return RedirectToAction("Catalogue", "Article");

                }
                foreach(IdentityError error in result.Errors)

                {
                    ModelState.AddModelError

                    (string.Empty, error.Description);

                }
            }
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
       
public async Task<IActionResult> EditRole

(string id)

        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);
            if
            (role == null
            )

            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";

                return View
                ("NotFound");
            
}
            var model = new EditRoleViewModel
            {
                Id = role.Id
            ,

                RoleName = role.Name
            };
            // Retrieve all the Users
            foreach (var user in userManager.Users.ToList())
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if
                (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add

                    (user.UserName);

                }
            }
            return View(model);
        }



        [HttpPost]
        public async Task

<IActionResult> EditRole

(EditRoleViewModel model)

        {
            var role = await roleManager.FindByIdAsync

            (model.Id);

            if
            (role == null
            )

            {
                ViewBag.ErrorMessage = $"Role with Id ={model.Id} cannot be found";

                return View
                (
                "NotFound");
            
}
            else
            {
                role.Name = model.RoleName
                ;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync
                (role);

                if
                (result.Succeeded
                )

                {
                    return RedirectToAction
                    (
                    "ListRoles");
                
}
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError

                    ("", error.Description);

                }
                return View(model);
            }
        }





















        [HttpPost]
        private async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListRoles");
            }

        }
            public async Task<IActionResult> EditUsersInRole(string roleId)
            {
                ViewBag.roleId = roleId;
                var role = await roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                    return View("NotFound");
                }
                var model = new List<UserRoleViewModel>();
                foreach (var user in userManager.Users.ToList())
                {
                    var userRoleViewModel = new UserRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRoleViewModel.IsSelected = true;
                    }
                    else
                    {
                        userRoleViewModel.IsSelected = false;
                    }
                    model.Add(userRoleViewModel);
                }
                return View(model);
            }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });

                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
        [AllowAnonymous]

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
    }

