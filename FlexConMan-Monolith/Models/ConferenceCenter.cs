using System.ComponentModel.DataAnnotations;

namespace FlexConMan_Monolith.Models;

public class ConferenceCenter
{
    [Key]
    public int CcId { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string? City { get; set; }

    [Required]
    public string? Email { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<Conference> Conferences { get; set; } = new List<Conference>();
}