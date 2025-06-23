using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.ConferenceCenters;

public class IndexModel(ApplicationDbContext context) : PageModel
{
    public IList<ConferenceCenter> ConferenceCenter { get;set; } = default!;

    public async Task OnGetAsync()
    {
        ConferenceCenter = await context.ConferenceCenters.ToListAsync();
    }
}