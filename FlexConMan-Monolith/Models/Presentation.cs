using System.ComponentModel.DataAnnotations;

namespace FlexConMan_Monolith.Models;

public class Presentation
{
    [Key]
    public string PreId { get; set; } = default!;

    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string? Speaker { get; set; }

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}