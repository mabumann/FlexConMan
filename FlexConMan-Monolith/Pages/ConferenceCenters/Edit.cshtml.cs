using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.ConferenceCenters;

public class EditModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public ConferenceCenter ConferenceCenter { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var conferenceCenter =  await context.ConferenceCenters.FirstOrDefaultAsync(m => m.CcId == id);
        if (conferenceCenter == null)
        {
            return NotFound();
        }
        ConferenceCenter = conferenceCenter;
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

        context.Attach(ConferenceCenter).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ConferenceCenterExists(ConferenceCenter.CcId))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool ConferenceCenterExists(int id)
    {
        return context.ConferenceCenters.Any(e => e.CcId == id);
    }
}