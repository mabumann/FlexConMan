using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Sessions;

public class IndexModel(ApplicationDbContext context) : PageModel
{
    public IList<Session> Session { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Session = await context.Sessions
            .Include(s => s.Conference)
            .Include(s => s.Contingent)
            .Include(s => s.Presentation)
            .Include(s => s.Room).ToListAsync();
    }
}