using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Sessions;

public class DetailsModel(ApplicationDbContext context) : PageModel
{
    public Session Session { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await context.Sessions
            .Include(s => s.Room)
            .Include(s => s.Conference)
            .Include(s => s.Presentation)
            .FirstOrDefaultAsync(m => m.SessionId == id);
        if (session == null)
        {
            return NotFound();
        }

        Session = session;
        return Page();
    }
}