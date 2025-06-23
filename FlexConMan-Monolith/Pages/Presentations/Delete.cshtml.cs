using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Presentations;

public class DeleteModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Presentation Presentation { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var presentation = await context.Presentations.FirstOrDefaultAsync(m => m.PreId == id);

        if (presentation == null)
        {
            return NotFound();
        }

        Presentation = presentation;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var presentation = await context.Presentations.FindAsync(id);
        if (presentation != null)
        {
            Presentation = presentation;
            context.Presentations.Remove(Presentation);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}