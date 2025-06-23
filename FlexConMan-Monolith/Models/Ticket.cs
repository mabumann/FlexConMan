using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexConMan_Monolith.Models;

public class Ticket
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public string CustomerId { get; set; }

    [Required]
    public string? SessionId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public User User { get; set; } = default!;
    [ForeignKey(nameof(SessionId))]
    public Session? Session { get; set; }
}