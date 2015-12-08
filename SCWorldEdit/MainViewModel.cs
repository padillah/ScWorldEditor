using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using DialogServiceLibrary.Service;
using DialogServiceLibrary.Service.FrameworkDialogs.OpenFile;
using SCWorldEdit.Annotations;
using SCWorldEdit.Assets;
using SCWorldEdit.Framework;
using ServiceLocator;

namespace SCWorldEdit
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /**/
        public Camera ViewportCamera { get; set; }

        public Model3DGroup LocalModelGroup { get; set; }

        /**/
        public ScWorld CurrentWorld { get; set; }

        public RelayCommand ClosingCommand { get; set; }
        public RelayCommand FileOpenCommand { get; set; }

        public Action CloseAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            //Int32Collection _triangleIndices;
            ClosingCommand = new RelayCommand(CloseAction);
            FileOpenCommand = new RelayCommand(FileOpen);

            CurrentWorld = new ScWorld();

            ViewportCamera = CurrentWorld.WorldCamera;

            LocalModelGroup = CurrentWorld.WorldModelGroup;
        }

        private void FileOpen()
        {
            IOpenFileDialog localOpenDialog = new OpenFileDialogViewModel();
            IDialogService localDialogService = Locator.Resolve<IDialogService>();

            bool fileResult = localDialogService.ShowOpenFileDialog(this, localOpenDialog);

            if (fileResult)
            {
                CurrentWorld.Load(localOpenDialog.FileName);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentWorld)));

        }


    }

    //TODO: Create an ScWorld class
    //class ScWorld
    //{
    //Take a look at ChunkHelper and see if that is any use.
    //}

}
