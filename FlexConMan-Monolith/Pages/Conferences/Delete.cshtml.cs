using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Authorization;

namespace FlexConMan_Monolith.Pages.Conferences;

[Authorize(Roles = "ADMIN")]
public class DeleteModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Conference Conference { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var conference = await context.Conferences.FirstOrDefaultAsync(m => m.ConferenceId == id);

        if (conference == null)
        {
            return NotFound();
        }

        Conference = conference;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var conference = await context.Conferences.FindAsync(id);
        if (conference != null)
        {
            Conference = conference;
            context.Conferences.Remove(Conference);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}