using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DialogServiceLibrary.Service;
using DialogServiceLibrary.Service.FrameworkDialogs.OpenFile;
using SCWorldEdit.Annotations;
using SCWorldEdit.Assets;
using SCWorldEdit.Rules;
using ServiceLocator;

namespace SCWorldEdit
{
	public class MainViewModel:INotifyPropertyChanged
	{
		public ScWorld CurrentWorld { get; set; }

		public RelayCommand ClosingCommand { get; set; }
		public RelayCommand FileOpenCommand { get; set; }

		public Action CloseAction { get; set; }

		public MainViewModel()
		{
			ClosingCommand = new RelayCommand(CloseAction);
			FileOpenCommand = new RelayCommand(FileOpen);
		}

		private void FileOpen()
		{
			IOpenFileDialog localOpenDialog = new OpenFileDialogViewModel();
			IDialogService localDialogService = Locator.Resolve<IDialogService>();

			bool fileResult = localDialogService.ShowOpenFileDialog(this, localOpenDialog);

			if (fileResult)
			{
				IScRulesEngine localRules = Locator.Resolve<IScRulesEngine>();

				CurrentWorld = localRules.LoadWorld(localOpenDialog.FileName);
			}

			OnPropertyChanged("CurrentWorld");

		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string argPropertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(argPropertyName));
		}
	}

	//TODO: Create an ScWorld class
	//class ScWorld
	//{
	//Take a look at ChunkHelper and see if that is any use.
	//}

}
