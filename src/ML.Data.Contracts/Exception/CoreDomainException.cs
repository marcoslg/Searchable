namespace ML.Data.Contracts.Exception
{
    public class CoreDomainException : System.Exception
    {
        public CoreDomainException(string message) : base(message)
        { }

        public CoreDomainException(string message, System.Exception innerException) :
            base(message, innerException)
        { }
    }
}
