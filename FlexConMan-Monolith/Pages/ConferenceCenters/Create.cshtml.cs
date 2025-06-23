using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.ConferenceCenters;

public class CreateModel(ApplicationDbContext context) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public ConferenceCenter ConferenceCenter { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.ConferenceCenters.Add(ConferenceCenter);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}