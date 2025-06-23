using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Rooms;

public class IndexModel(ApplicationDbContext context) : PageModel
{
    public IList<Room> Room { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Room = await context.Rooms
            .Include(r => r.ConferenceCenter).ToListAsync();
    }
}