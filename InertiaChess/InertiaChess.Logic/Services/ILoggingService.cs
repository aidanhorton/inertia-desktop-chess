namespace InertiaChess.Logic.Services
{
    public delegate void LogHandler(string message);

    public interface ILoggingService
    {
        event LogHandler OnLogUpdated;

        void Log(string message);
    }
}
