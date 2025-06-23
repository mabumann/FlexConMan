using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.ConferenceCenters;

public class DeleteModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public ConferenceCenter ConferenceCenter { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var conferenceCenter = await context.ConferenceCenters.FirstOrDefaultAsync(m => m.CcId == id);

        if (conferenceCenter == null)
        {
            return NotFound();
        }

        ConferenceCenter = conferenceCenter;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var conferenceCenter = await context.ConferenceCenters.FindAsync(id);
        if (conferenceCenter != null)
        {
            ConferenceCenter = conferenceCenter;
            context.ConferenceCenters.Remove(ConferenceCenter);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}