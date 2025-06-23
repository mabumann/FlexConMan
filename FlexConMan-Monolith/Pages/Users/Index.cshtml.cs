using Microsoft.AspNetCore.Mvc.RazorPages;
using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;

namespace FlexConMan_Monolith.Pages.Users;

public class IndexModel(UserManager<User> userManager) : PageModel
{
    public List<UserWithRoles> Users { get;set; } = new();

    public async Task OnGetAsync()
    {
        var users = userManager.Users.ToList();
        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            Users.Add(new UserWithRoles
            {
                User = user,
                Roles = roles
            });
        }
    }
}