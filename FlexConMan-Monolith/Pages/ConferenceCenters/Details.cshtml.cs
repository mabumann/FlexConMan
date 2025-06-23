using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.ConferenceCenters;

public class DetailsModel(ApplicationDbContext context) : PageModel
{
    public ConferenceCenter ConferenceCenter { get; set; } = default!;
    public IList<Conference> Conferences { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var conferenceCenter = await context.ConferenceCenters
            .Include(cc => cc.Conferences)
            .FirstOrDefaultAsync(m => m.CcId == id);
        if (conferenceCenter == null)
        {
            return NotFound();
        }

        ConferenceCenter = conferenceCenter;
        Conferences = ConferenceCenter.Conferences?.ToList() ?? new List<Conference>();
        return Page();
    }
}