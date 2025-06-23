using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlexConMan_Monolith.Models;

namespace FlexConMan_Monolith.Pages.Presentations;

public class IndexModel(ApplicationDbContext context) : PageModel
{
    public IList<Presentation> Presentation { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Presentation = await context.Presentations.ToListAsync();
    }
}