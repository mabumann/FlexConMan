using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Sessions;

public class DeleteModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Session Session { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await context.Sessions.FirstOrDefaultAsync(m => m.SessionId == id);

        if (session == null)
        {
            return NotFound();
        }

        Session = session;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await context.Sessions.FindAsync(id);
        if (session != null)
        {
            Session = session;
            context.Sessions.Remove(Session);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}