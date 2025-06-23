using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlexConMan_Monolith.Pages.Users;

[Authorize(Roles = "ADMIN")]
public class ManageRolesModel(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    : PageModel
{
    public IList<User> Users { get; set; } = new List<User>();
    public IList<string> Roles { get; set; } = new List<string>();

    [BindProperty]
    public string SelectedUserId { get; set; } = string.Empty;

    [BindProperty]
    public string SelectedRole { get; set; } = string.Empty;

    public async Task OnGetAsync()
    {
        Users = userManager.Users.ToList();
        Roles = roleManager.Roles.Select(r => r.Name).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await userManager.FindByIdAsync(SelectedUserId);
        if (user != null && !string.IsNullOrEmpty(SelectedRole))
        {
            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);
            await userManager.AddToRoleAsync(user, SelectedRole);
        }
        return RedirectToPage();
    }
}