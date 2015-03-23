using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DialogServiceLibrary.Service;
using SCWorldEdit.WindowViewModelMapping;
using ServiceLocator;
using ServiceLocator.WindowViewModelMapping;

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

            Locator.RegisterSingleton<IWindowViewModelMappings, ScWorldEditMapping>();
            Locator.RegisterSingleton<IDialogService, DialogService>();

            //Locator.RegisterSingleton<IPartyTrackerBusinessRules, PartyTrackerBusinessRules>();

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
