using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogServiceLibrary.Service;
using DialogServiceLibrary.Service.FrameworkDialogs.OpenFile;
using SCWorldEdit.Assets;
using ServiceLocator;

namespace SCWorldEdit
{
	public class MainViewModel
	{
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

			}

		}

	}
}
