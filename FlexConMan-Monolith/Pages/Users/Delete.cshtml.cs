using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;

namespace FlexConMan_Monolith.Pages.Users;

public class DeleteModel(UserManager<User> userManager) : PageModel
{
    public User User { get; set; } = default!;
    public IList<string> Roles { get; set; } = new List<string>();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();
        User = user;
        Roles = await userManager.GetRolesAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await userManager.FindByIdAsync(id);
        if (user != null)
        {
            var result = await userManager.DeleteAsync(User);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                User = user;
                Roles = await userManager.GetRolesAsync(user);
                return Page();
            }
        }

        return RedirectToPage("./Index");
    }
}