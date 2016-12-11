#if ! NET451
namespace System.Threading
{
    public class ThreadInterruptedException : Exception
    {
        public ThreadInterruptedException()
        {
        }

        public ThreadInterruptedException(string message) : base(message)
        {
        }

        public ThreadInterruptedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

#endif