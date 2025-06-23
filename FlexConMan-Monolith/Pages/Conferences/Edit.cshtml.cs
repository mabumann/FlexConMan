using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Authorization;

namespace FlexConMan_Monolith.Pages.Conferences;

[Authorize(Roles = "ADMIN")]
public class EditModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Conference Conference { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var conference =  await context.Conferences.FirstOrDefaultAsync(m => m.ConferenceId == id);
        if (conference == null)
        {
            return NotFound();
        }
        Conference = conference;
        ViewData["CcId"] = new SelectList(context.ConferenceCenters, "CcId", "City");
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

        context.Attach(Conference).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ConferenceExists(Conference.ConferenceId))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool ConferenceExists(int id)
    {
        return context.Conferences.Any(e => e.ConferenceId == id);
    }
}