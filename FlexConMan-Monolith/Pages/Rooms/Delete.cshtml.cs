using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Rooms;

public class DeleteModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Room Room { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await context.Rooms.FirstOrDefaultAsync(m => m.RoomId == id);

        if (room == null)
        {
            return NotFound();
        }

        Room = room;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await context.Rooms.FindAsync(id);
        if (room != null)
        {
            Room = room;
            context.Rooms.Remove(Room);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}