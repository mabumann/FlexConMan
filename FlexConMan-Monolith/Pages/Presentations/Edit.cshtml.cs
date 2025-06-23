using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Presentations;

public class EditModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Presentation Presentation { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var presentation =  await context.Presentations.FirstOrDefaultAsync(m => m.PreId == id);
        if (presentation == null)
        {
            return NotFound();
        }
        Presentation = presentation;
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

        context.Attach(Presentation).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PresentationExists(Presentation.PreId))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool PresentationExists(string id)
    {
        return context.Presentations.Any(e => e.PreId == id);
    }
}