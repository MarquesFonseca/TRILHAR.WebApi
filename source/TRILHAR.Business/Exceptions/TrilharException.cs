namespace TRILHAR.Business.Exceptions
{
    public class TrilharException : Exception
    {
        public TrilharException()
        {

        }

        public TrilharException(string message)
            : base(message)
        {

        }

        public TrilharException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
