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

        public Point3D CameraPosition { get; set; }

        public Vector3D CameraLook { get; set; }

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

            //TODO: Move the camera to the ScWorld class
            CameraPosition = new Point3D(3, 2, -4);
            CameraLook = new Vector3D(-2.5, -1.5, 3.5);

            ViewportCamera = new PerspectiveCamera(
                CameraPosition,
                CameraLook,  //Need to lookat 0.5, 0.5, -0.5 to see the center of the cube.
                new Vector3D(0, 1, 0), 45);

            DirectionalLight localLight = new DirectionalLight(Colors.White, new Vector3D(0, 0, 1));

            LocalModelGroup = new Model3DGroup();

            LocalModelGroup.Children.Add(localLight);

            ScBlock localBlock = new ScBlock(new Point3D(0, 0, 0));

            LocalModelGroup.Children.Add(localBlock.BlockModel);

        }

        private void FileOpen()
        {
            IOpenFileDialog localOpenDialog = new OpenFileDialogViewModel();
            IDialogService localDialogService = Locator.Resolve<IDialogService>();

            bool fileResult = localDialogService.ShowOpenFileDialog(this, localOpenDialog);

            if (fileResult)
            {
                IScEngine localEngine = Locator.Resolve<ScEngine>();

                CurrentWorld = localEngine.LoadWorld(localOpenDialog.FileName);
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
