using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Presentations;

public class CreateModel(ApplicationDbContext context) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Presentation Presentation { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Presentations.Add(Presentation);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}