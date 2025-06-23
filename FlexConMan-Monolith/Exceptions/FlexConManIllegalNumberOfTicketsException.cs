namespace FlexConMan_Monolith.Exceptions;

[Serializable]
internal class FlexConManIllegalNumberOfTicketsException : Exception
{
    public FlexConManIllegalNumberOfTicketsException()
    {
    }

    public FlexConManIllegalNumberOfTicketsException(string? message) : base(message)
    {
    }

    public FlexConManIllegalNumberOfTicketsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}