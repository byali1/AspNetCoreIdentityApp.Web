using AspNetCoreIdentityApp.Web.Areas.Admin.Models;
using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;


        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> RoleList()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleListViewModel
            {
                Id = x.Id,
                Name = x.Name!
            }).ToListAsync();

            return View(roles);
        }





        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole
            {
                Name = request.Name
            });

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }

            TempData["RoleAdded"] = true;
            return View();
        }





        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Böyle bir rol bulunamadı.");
                return View();
            }

            return View(new UpdateRoleViewModel
            {
                Id = role.Id,
                Name = role!.Name!
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleViewModel request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);

            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Böyle bir rol bulunamadı.");
                return View();
            }

            role.Name = request.Name;

            await _roleManager.UpdateAsync(role);

            TempData["RoleUpdated"] = true;

            return View();
        }



        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Böyle bir rol bulunamadı.");
                TempData["RoleNotDeleted"] = true;
                return RedirectToAction(nameof(RolesController.RoleList));
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                TempData["RoleNotDeleted"] = true;
                return RedirectToAction(nameof(RolesController.RoleList));
            }

            TempData["RoleDeleted"] = true;
            return RedirectToAction(nameof(RolesController.RoleList));
        }
    }
}
