namespace Smartwyre.DeveloperTest.Services
{
    public interface ILogger
    {
        void Error(string message);
        void Info(string message);
        void Log(string message);
        void Warning(string message);
    }
}