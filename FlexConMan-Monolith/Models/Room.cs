using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexConMan_Monolith.Models;

public class Room
{
    [Key]
    public string RoomId { get; set; } = default!;

    [Required]
    public int? CcId { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public int? NumberOfSeats { get; set; }

    [ForeignKey(nameof(CcId))]
    public ConferenceCenter? ConferenceCenter { get; set; }

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}