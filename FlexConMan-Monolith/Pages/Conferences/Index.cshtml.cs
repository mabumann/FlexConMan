using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Conferences;

public class IndexModel(ApplicationDbContext context) : PageModel
{
    public IList<Conference> Conference { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Conference = await context.Conferences
            .Include(c => c.ConferenceCenter).ToListAsync();
    }
}