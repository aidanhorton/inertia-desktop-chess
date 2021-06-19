using System.Threading.Tasks;

namespace InertiaChess.Logic.Services
{
    public class LoggingService : ILoggingService
    {
        public event LogHandler OnLogUpdated;

        public void Log(string message)
        {
            this.OnLogUpdated?.Invoke(message);
        }
    }
}
