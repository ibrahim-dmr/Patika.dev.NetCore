using System.Diagnostics;

namespace WebApi.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Debug.WriteLine($"[Console Logger] - {message}");
        }

    }
}
