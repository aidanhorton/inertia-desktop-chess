using InertiaChess.Logic.Services;
using InertiaChess.Presentation.Factories;
using InertiaChess.Presentation.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace InertiaChess.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return this.Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register types here.
            containerRegistry.Register<IPieceService, PieceService>();
            containerRegistry.Register<IBoardTileFactory, BoardTileFactory>();
            containerRegistry.Register<IFenInterpretationService, FenInterpretationService>();
            containerRegistry.Register<IMinimaxService, MinimaxService>();

            containerRegistry.RegisterInstance<ILoggingService>(new LoggingService());
        }
    }
}
