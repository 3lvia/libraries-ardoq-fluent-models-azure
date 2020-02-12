using System;

namespace ArdoqFluentModels.Azure
{
    internal class ConsoleLogger : ILogger
    {
        public void Flush()
        {
        }

        public void LogDebug(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString()} - Debug - {message}");
        }

        public void LogError(string message)
        {
            Console.Error.WriteLine($"{DateTime.Now.ToString()} - Debug - {message}");
        }

        public void LogException(Exception ex)
        {
            Console.Error.WriteLine($"{DateTime.Now.ToString()} - Debug - {ex.ToString()}");
        }

        public void LogMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString()} - Debug - {message}");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString()} - Debug - {message}");
        }
    }
}
