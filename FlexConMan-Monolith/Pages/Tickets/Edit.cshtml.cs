using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Tickets;

public class EditModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Ticket Ticket { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket =  await context.Tickets.FirstOrDefaultAsync(m => m.OrderId == id);
        if (ticket == null)
        {
            return NotFound();
        }
        Ticket = ticket;
        ViewData["SessionId"] = new SelectList(context.Sessions, "SessionId", "SessionId");
        ViewData["CustomerId"] = new SelectList(context.Users, "UserId", "Email");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Attach(Ticket).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TicketExists(Ticket.OrderId))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool TicketExists(int id)
    {
        return context.Tickets.Any(e => e.OrderId == id);
    }
}