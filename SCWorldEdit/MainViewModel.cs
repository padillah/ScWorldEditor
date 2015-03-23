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
				//TODO: Open the zip file.
				//TODO: Save the three files in a subfolder
				//TODO: Open the "Chunks.dat" file.
				//TODO: Fill ScWorld class.
			}

		}

	}
	
	//TODO: Create an ScWorld class
	//class ScWorld
	//{
		//Take a look at ChunkHelper and see if that is any use.
	//}
	}
}
