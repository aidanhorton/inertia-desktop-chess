using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace InertiaChess.Presentation.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private WindowState windowState;

        public ShellViewModel()
        {
            this.MinimizeCommand = new DelegateCommand(this.MinimizeApplication);
            this.CloseCommand = new DelegateCommand(() => Application.Current.Shutdown());
        }

        public ICommand MinimizeCommand { get; }
        public ICommand CloseCommand { get; }

        public WindowState WindowState
        {
            get => windowState;
            set => this.SetProperty(ref windowState, value);
        }

        private void MinimizeApplication()
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
