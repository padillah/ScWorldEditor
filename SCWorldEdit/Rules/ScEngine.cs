using System.IO;
using ICSharpCode.SharpZipLib;

//TODO: World should be Image 768x768 (256 * 3)

namespace SCWorldEdit.Rules
{
    public interface IScEngine
	{
		ScWorld LoadWorld(string argFileName);
	}

	public class ScEngine : IScEngine
	{
		public ScWorld LoadWorld(string argFileName)
		{
			//Create Temp directory
			DirectoryInfo localDirectory = Directory.CreateDirectory("ScWorldEdit");
			string newFileName = localDirectory.FullName + "\\Temp.zip";

			File.Copy(argFileName, newFileName, true);

			//Open the zip file.
			//Save the three files to the Temp dir
			ZipWrapper.ExtractZipFile(newFileName, localDirectory.FullName);
			
			//Open the "Chunks.dat" file.
			string chunkFileName = localDirectory.FullName + "\\Chunks.dat";
			ScWorld localWorld = new ScWorld(chunkFileName);


			return localWorld;
		}
	}
}
