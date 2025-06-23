using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;

namespace FlexConMan_Monolith.Pages.Conferences;

public class DetailsModel(ApplicationDbContext context, UserManager<User> userManager)
    : PageModel
{
    public Conference Conference { get; set; } = default!;

    public int GetUserTicketCount(string sessionId)
    {
        var userId = userManager.GetUserId(User);
        return context.Tickets.Count(t => t.SessionId == sessionId && t.CustomerId == userId);
    }

    public async Task<IActionResult> OnPostBuyAsync(string sessionId, int ticketCount)
    {
        var userId = userManager.GetUserId(User);
        if (context.Tickets.Any(t => t.SessionId == sessionId && t.CustomerId == userId))
        {
            return RedirectToPage();
        }

        var session = await context.Sessions.Include(s => s.Contingent)
            .FirstOrDefaultAsync(s => s.SessionId == sessionId);
        if (session == null || session.Contingent == null)
        {
            return NotFound();
        }

        if (session.Contingent.RemainingOnlineQuota < ticketCount)
        {
            ModelState.AddModelError(string.Empty, "Not enough tickets available.");
            return Page();
        }

        session.Contingent.ReduceContingent(Contingent.ContingentType.ONLINE, ticketCount);

        for (var i = 0; i < ticketCount; i++)
        {
            context.Tickets.Add(new Ticket { CustomerId = userId, SessionId = sessionId });
        }
        await context.SaveChangesAsync();

        return RedirectToPage("/Tickets/Index");
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Conference = await context.Conferences
            .Include(c => c.ConferenceCenter)
            .Include(c => c.Sessions) // Include session
            .ThenInclude(s => s.Room) // Optionally include related Room
            .Include(c => c.Sessions)
            .ThenInclude(s => s.Presentation) // Optionally include related Presentation
            .Include(c => c.Sessions)
            .ThenInclude(s => s.Contingent) // Optionally include related Contingent    
            .FirstOrDefaultAsync(m => m.ConferenceId == id);

        if (Conference == null)
        {
            return NotFound();
        }

        return Page();
    }
}