using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;

namespace FlexConMan_Monolith.Pages.Users;

public class CreateModel(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    : PageModel
{
    [BindProperty]
    public User User { get; set; } = default!;

    [BindProperty, Required, DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [BindProperty, Required]
    public string SelectedRole { get; set; } = default!;

    public List<SelectListItem> RolesOptions { get; set; } =
    [
        new() { Value = "User", Text = "User" },
        new() { Value = "Admin", Text = "Admin" }

    ];

    public IActionResult OnGet()
    {
        return Page();
    }

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await userManager.CreateAsync(User, Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        if (!await roleManager.RoleExistsAsync(SelectedRole))
        {
            var role = new IdentityRole(SelectedRole);
            await roleManager.CreateAsync(role);
        }
        return RedirectToPage("./Index");
    }
}