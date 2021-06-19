using InertiaChess.Logic.Services;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace InertiaChess.Presentation.ViewModels
{
    public class GameLogViewModel : BindableBase
    {
        private readonly ILoggingService loggingService;

        public GameLogViewModel(ILoggingService loggingService)
        {
            this.loggingService = loggingService;

            this.loggingService.OnLogUpdated += this.OnLogMessageReceived;
        }

        public ObservableCollection<string> MessageLog { get; } = new ObservableCollection<string>();

        private void OnLogMessageReceived(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.MessageLog.Add(message);
            });
            
        }
    }
}
