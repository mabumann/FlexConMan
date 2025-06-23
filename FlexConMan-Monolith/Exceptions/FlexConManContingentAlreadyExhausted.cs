namespace FlexConMan_Monolith.Exceptions;

[Serializable]
internal class FlexConManContingentAlreadyExhausted : Exception
{
    public FlexConManContingentAlreadyExhausted()
    {
    }

    public FlexConManContingentAlreadyExhausted(string? message) : base(message)
    {
    }

    public FlexConManContingentAlreadyExhausted(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}