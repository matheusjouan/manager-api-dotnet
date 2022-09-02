namespace Manager.Core.Excpetions;

public class DomainExcpetion : Exception
{
    internal List<string> _errors;

    public IReadOnlyCollection<string> Errors => _errors;

    public DomainExcpetion() { }

    public DomainExcpetion(string message, List<string> errors) : base(message)
    {
        _errors = errors;
    }

    public DomainExcpetion(string message) : base(message) { }

    public DomainExcpetion(string message, Exception innerException) : base(message, innerException) { }
}

