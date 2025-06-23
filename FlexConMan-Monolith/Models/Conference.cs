using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexConMan_Monolith.Models;

public class Conference
{
    [Key]
    public int ConferenceId { get; set; }

    [Required]
    public int? CcId { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public DateTime? DateFrom { get; set; }

    [Required]
    public DateTime? DateTo { get; set; }

    [ForeignKey(nameof(CcId))]
    public ConferenceCenter? ConferenceCenter { get; set; }

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}