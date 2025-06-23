using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlexConMan_Monolith.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexConMan_Monolith.Pages.Sessions;

public class CreateModel(ApplicationDbContext context) : PageModel
{
    public IActionResult OnGet()
    { 
        ViewData["ConferenceId"] = new SelectList(context.Conferences, "ConferenceId", "Name");
        ViewData["PreId"] = new SelectList(context.Presentations, "PreId", "Title");
        ViewData["RoomId"] = new SelectList(context.Rooms, "RoomId", "Name");
        return Page();
    }

    [BindProperty]
    public Session Session { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Fetch the Room entity
        var room = await context.Rooms
            .FirstOrDefaultAsync(r => r.RoomId == Session.RoomId);

        if (room == null || room.NumberOfSeats == null)
        {
            ModelState.AddModelError(string.Empty, "Room not found or number of seats not set.");
            return Page();
        }

        // Create a new Contingent
        var contingent = new Contingent
        {
            RemainingBoxofficeQuota = room.NumberOfSeats.Value,
            RemainingOnlineQuota = room.NumberOfSeats.Value
        };

        // Add and save the contingent to get its ID
        context.Contingents.Add(contingent);
        await context.SaveChangesAsync();

        // Assign the new contingent to the session
        Session.ContingentId = contingent.ContingentId;

        // Add and save the session
        context.Sessions.Add(Session);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}