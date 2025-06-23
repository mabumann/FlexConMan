using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Sessions;

public class EditModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Session Session { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session =  await context.Sessions.FirstOrDefaultAsync(m => m.SessionId == id);
        if (session == null)
        {
            return NotFound();
        }
        Session = session;
        ViewData["ConferenceId"] = new SelectList(context.Conferences, "ConferenceId", "Name");
        ViewData["ContingentId"] = new SelectList(context.Contingents, "ContingentId", "ContingentId");
        ViewData["PreId"] = new SelectList(context.Presentations, "PreId", "PreId");
        ViewData["RoomId"] = new SelectList(context.Rooms, "RoomId", "RoomId");
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

        context.Attach(Session).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SessionExists(Session.SessionId))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool SessionExists(string id)
    {
        return context.Sessions.Any(e => e.SessionId == id);
    }
}