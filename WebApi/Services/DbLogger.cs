using System.Diagnostics;

namespace WebApi.Services
{
    public class DbLogger : ILoggerService
    {
        public void Write(string message)
        {
            Debug.WriteLine($"[DB Logger] - {message}");
        }
    }
}
