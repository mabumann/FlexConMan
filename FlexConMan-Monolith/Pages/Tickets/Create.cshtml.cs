using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Tickets;

public class CreateModel(ApplicationDbContext context) : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["SessionId"] = new SelectList(context.Sessions, "SessionId", "SessionId");
        ViewData["CustomerId"] = new SelectList(context.Users, "UserId", "Email");
        return Page();
    }

    [BindProperty]
    public Ticket Ticket { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Tickets.Add(Ticket);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}