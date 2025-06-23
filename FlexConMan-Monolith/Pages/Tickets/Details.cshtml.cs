using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Tickets;

public class DetailsModel(ApplicationDbContext context) : PageModel
{
    public Ticket Ticket { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await context.Tickets.FirstOrDefaultAsync(m => m.OrderId == id);
        if (ticket == null)
        {
            return NotFound();
        }

        Ticket = ticket;
        return Page();
    }
}