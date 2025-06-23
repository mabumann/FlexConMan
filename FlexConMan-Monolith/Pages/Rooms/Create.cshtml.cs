using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Rooms;

public class CreateModel(ApplicationDbContext context) : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["CcId"] = new SelectList(context.ConferenceCenters, "CcId", "City", Room.CcId);
        return Page();
    }

    [BindProperty]
    public Room Room { get; set; } = new();

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["CcId"] = new SelectList(context.ConferenceCenters, "CcId", "City", Room.CcId);

            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                foreach (var error in state.Errors)
                {
                    Console.WriteLine($"{key}: {error.ErrorMessage}");
                }
            }
            return Page();
        }

        context.Rooms.Add(Room);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}