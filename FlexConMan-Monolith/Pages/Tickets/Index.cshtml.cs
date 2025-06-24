using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;

namespace FlexConMan_Monolith.Pages.Tickets;

public class IndexModel(ApplicationDbContext context, UserManager<User> userManager)
    : PageModel
{
    public IList<Ticket> Ticket { get; set; } = default!;
    public IList<TicketViewModel> GroupedTickets { get; set; } = new List<TicketViewModel>();

    public async Task OnGetAsync()
    {


        var user = User;
        IList<Ticket> tickets;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {

            // Check if user is in the "Admin" role
            if (user.IsInRole("ADMIN"))
            {
                tickets = await context.Tickets
                    .Include(t => t.User)
                    .Include(t => t.Session)
                    .ThenInclude(s => s.Conference)
                    .Include(t => t.Session)
                    .ThenInclude(s => s.Presentation)
                    .ToListAsync();

            }
            else
            {
                // Get the current user's Id
                var userId = userManager.GetUserId(user);
                tickets = await context.Tickets
                    .Include(t => t.User)
                    .Include(t => t.Session)
                    .ThenInclude(s => s.Conference)
                    .Include(t => t.Session)
                    .ThenInclude(s => s.Presentation)
                    .Where(t => t.CustomerId == userId)
                    .ToListAsync();

            }
        }
        else
        {
            // Not authenticated, show no tickets
            tickets = new List<Ticket>();
        }

        Ticket = tickets;

        GroupedTickets = tickets
            .Where(t => t.Session != null && t.Session.Conference != null && t.Session.Presentation != null &&
                        t.User != null)
            .GroupBy(t => new
            {
                t.User.Email,
                ConferenceName = t.Session.Conference.Name,
                PresentationTitle = t.Session.Presentation.Title
            })
            .Select(g => new TicketViewModel
            {
                UserEmail = g.Key.Email,
                ConferenceName = g.Key.ConferenceName,
                PresentationTitle = g.Key.PresentationTitle,
                Count = g.Count()
            })
            .ToList();
    }
}

public class TicketViewModel
{
    public string UserEmail { get; set; }
    public string ConferenceName { get; set; }
    public string PresentationTitle { get; set; }
    public int Count { get; set; }
}