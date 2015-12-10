using DialogServiceLibrary.Service;
using SCWorldEdit.Framework;
using ServiceLocator;
using System.ComponentModel;
using System.Windows;

namespace SCWorldEdit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel _mainViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Locator.RegisterSingleton<IDialogService, DialogService>();
            Locator.RegisterSingleton<IMaterialHandler, MaterialHandler>();
            
            _mainViewModel = new MainViewModel();
            MainView view = new MainView();

            view.Closing += view_Closing;
            view.DataContext = _mainViewModel;

            view.Show();

        }

        private void view_Closing(object argSender, CancelEventArgs argEventArgs)
        {
            if (_mainViewModel.ClosingCommand != null)
            {
                if (_mainViewModel.ClosingCommand.CanExecute())
                {
                    _mainViewModel.ClosingCommand.Execute();
                    Current.Shutdown();
                }
                else
                {
                    argEventArgs.Cancel = true;
                }
            }
        }

    }
}
