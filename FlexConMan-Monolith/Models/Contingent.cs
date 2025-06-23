using System.ComponentModel.DataAnnotations;
using FlexConMan_Monolith.Exceptions;

namespace FlexConMan_Monolith.Models;


public class Contingent
{
    public enum ContingentType
    {
        ONLINE,
        BOXOFFICE
    }

    [Key]
    public int ContingentId { get; set; }

    [Required]
    public int RemainingBoxofficeQuota { get; set; }

    [Required]
    public int RemainingOnlineQuota { get; set; }

    public Contingent()
    {
        
    }

    public Contingent(int id, int totalQuote, int quoteOnline)
    {
        ContingentId = id;

        RemainingOnlineQuota = totalQuote * quoteOnline / 100;
        RemainingBoxofficeQuota = totalQuote - RemainingOnlineQuota;
    }


    public void ReduceContingent(ContingentType contingentType, int numberOfTickets)
    {
        if (numberOfTickets <= 0)
        {
            throw new FlexConManIllegalNumberOfTicketsException($"Number of tickets must be greater than zero but is {numberOfTickets}.");
        }

        switch (contingentType)
        {
            case ContingentType.ONLINE:
                RemainingOnlineQuota = ReduceThisContingent(RemainingOnlineQuota, numberOfTickets);
                break;
            case ContingentType.BOXOFFICE:
                RemainingBoxofficeQuota = ReduceThisContingent(RemainingBoxofficeQuota, numberOfTickets);
                break;
        }
    }

    private static int ReduceThisContingent(int contingent, int numberOfTickets)
    {
        var reducedContingent = contingent - numberOfTickets;
        if (reducedContingent < 0)
        {
            throw new FlexConManContingentAlreadyExhausted($"Cannot reduce contingent by {numberOfTickets} because it would be negative. Remaining contingent is {contingent}.");
        }


        return reducedContingent;
    }

    public ICollection<Session> Sessions { get; set; } = [];
}