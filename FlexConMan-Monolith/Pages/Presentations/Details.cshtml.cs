using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Presentations;

public class DetailsModel(ApplicationDbContext context) : PageModel
{
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
}