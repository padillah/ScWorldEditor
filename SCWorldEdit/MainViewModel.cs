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
using SCWorldEdit.Assets;
using SCWorldEdit.Framework;
using ServiceLocator;

namespace SCWorldEdit
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ScEngine localEngine;
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
            localEngine = new ScEngine();

            //Int32Collection _triangleIndices;
            ClosingCommand = new RelayCommand(CloseAction);
            FileOpenCommand = new RelayCommand(FileOpen);

            CurrentWorld = localEngine.World;

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
                localEngine.LoadWorld(localOpenDialog.FileName);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentWorld)));

        }


    }

}
