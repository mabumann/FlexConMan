using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexConMan_Monolith.Models;

public class Session
{
    [Key]
    public string SessionId { get; set; } = default!;

    [Required]
    public DateTime? Datetime { get; set; }

    [Required]
    public string? RoomId { get; set; }

    [Required]
    public int? ConferenceId { get; set; }

    [Required]
    public string? PreId { get; set; }

    //[Required]
    public int? ContingentId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public Room? Room { get; set; }
    [ForeignKey(nameof(ConferenceId))]
    public Conference? Conference { get; set; }
    [ForeignKey(nameof(PreId))]
    public Presentation? Presentation { get; set; }
    [ForeignKey(nameof(ContingentId))]
    public Contingent? Contingent { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}