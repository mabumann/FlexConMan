using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Authorization;

namespace FlexConMan_Monolith.Pages.Conferences;

[Authorize(Roles = "ADMIN")]
public class CreateModel(ApplicationDbContext context) : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["CcId"] = new SelectList(context.ConferenceCenters, "CcId", "City");
        return Page();
    }

    [BindProperty]
    public Conference Conference { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Conferences.Add(Conference);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}