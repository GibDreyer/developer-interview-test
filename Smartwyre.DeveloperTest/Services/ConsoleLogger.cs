// Ignore Spelling: Smartwyre

using System;

namespace Smartwyre.DeveloperTest.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            //Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }

        public void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
