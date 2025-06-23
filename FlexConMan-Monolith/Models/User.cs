using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FlexConMan_Monolith.Models;

public class User : IdentityUser
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Firstname { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}