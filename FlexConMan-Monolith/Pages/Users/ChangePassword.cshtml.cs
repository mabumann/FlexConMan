using System.ComponentModel.DataAnnotations;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlexConMan_Monolith.Pages.Users;

public class ChangePasswordModel(UserManager<User> userManager) : PageModel
{
    [BindProperty]
    public string UserId { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = default!;
        
    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = default!;

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        UserId = id;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await userManager.FindByIdAsync(UserId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        return RedirectToPage("./Details", new {id = UserId});
    }
}