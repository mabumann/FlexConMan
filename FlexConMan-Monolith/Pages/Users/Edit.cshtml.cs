using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FlexConMan_Monolith.Pages.Users;

[Authorize]
public class EditModel(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : PageModel
{
    [BindProperty]
    public User EditUser { get; set; } = default!;

    [BindProperty]
    public string SelectedRole { get; set; } = string.Empty;
        
    public List<SelectListItem> Roles { get; set; } = [];

    public bool CanEditRole { get; set; }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user =  await userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        EditUser = user;

        var currentUser = await userManager.GetUserAsync(User);
        var isAdmin = await userManager.IsInRoleAsync(currentUser, "ADMIN");
        var isEditingSelf = currentUser.Id == EditUser.Id;

        CanEditRole = isAdmin && !isEditingSelf;

        if (CanEditRole)
        {
            Roles = roleManager.Roles
                .Select(role => new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name
                }).ToList();
            var userRoles = await userManager.GetRolesAsync(EditUser);
            SelectedRole = userRoles.FirstOrDefault() ?? string.Empty;
        }

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var userToUpdate = await userManager.FindByIdAsync(EditUser.Id);
        if (userToUpdate == null)
        {
            return NotFound();
        }

        userToUpdate.Name = EditUser.Name;
        userToUpdate.Firstname = EditUser.Firstname;
        userToUpdate.Email = EditUser.Email;
        userToUpdate.UserName = EditUser.UserName;

        var result = await userManager.UpdateAsync(userToUpdate);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        var currentUser = await userManager.GetUserAsync(User);
        var isAdmin = await userManager.IsInRoleAsync(currentUser, "ADMIN");
        var isEditingSelf = currentUser.Id == EditUser.Id;

        if (isAdmin && !isEditingSelf && !string.IsNullOrEmpty(SelectedRole))
        {
            var currentRoles = await userManager.GetRolesAsync(userToUpdate);
            if (!currentRoles.Contains(SelectedRole))
            {
                await userManager.RemoveFromRolesAsync(userToUpdate, currentRoles);
                await userManager.AddToRoleAsync(userToUpdate, SelectedRole);
            }
        }

        return RedirectToPage("./Index");
    }
}