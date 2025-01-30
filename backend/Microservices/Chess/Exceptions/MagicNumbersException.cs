namespace Chess.Exceptions
{
    public  class MagicNumbersException : Exception
    {
        public MagicNumbersException(string message) : base(message) { }
        public MagicNumbersException(string message, Exception innerException) : base(message, innerException) { }
    }
}