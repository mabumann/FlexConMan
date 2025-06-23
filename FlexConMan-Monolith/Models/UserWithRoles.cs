namespace FlexConMan_Monolith.Models;

public class UserWithRoles
{
    public User User { get; set; } = default!;
    public IList<string> Roles { get; set; } = new List<string>();
}