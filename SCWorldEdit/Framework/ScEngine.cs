using System.IO;
using ICSharpCode.SharpZipLib;

//TODO: World should be Image 768x768 (256 * 3)

namespace SCWorldEdit.Framework
{

	public class ScEngine
	{
        public ScWorld World { get; set; }

        public ScEngine()
        {
            World = new ScWorld();
        }

		public void LoadWorld(string argFileName)
		{
			//Create Temp directory
			DirectoryInfo localDirectory = Directory.CreateDirectory("ScWorldEdit");
			string newFileName = localDirectory.FullName + "\\Temp.zip";

			File.Copy(argFileName, newFileName, true);

			//Open the zip file.
			//Save the three files to the Temp dir
			ZipWrapper.ExtractZipFile(newFileName, localDirectory.FullName);
			
			//Open the files.
            World.Load(localDirectory.FullName);

			//return localWorld;
		}
	}
}
