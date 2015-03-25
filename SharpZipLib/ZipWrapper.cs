using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace ICSharpCode.SharpZipLib
{
    public static class ZipWrapper
    {

		//public static void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
		public static void ExtractZipFile(string archiveFilenameIn, string outFolder)
		{
			ZipFile zf = null;
			try
			{
				FileStream fs = File.OpenRead(archiveFilenameIn);
				zf = new ZipFile(fs);
				zf.UseZip64 = UseZip64.Off;

				//if (!String.IsNullOrEmpty(password))
				//{
				//	zf.Password = password;     // AES encrypted entries are handled automatically
				//}

				foreach (ZipEntry zipEntry in zf)
				{
					if (!zipEntry.IsFile)
					{
						continue;           // Ignore directories
					}
					String entryFileName = zipEntry.Name;
					// to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
					// Optionally match entrynames against a selection list here to skip as desired.
					// The unpacked length is available in the zipEntry.Size property.

					byte[] buffer = new byte[4096];     // 4K is optimum
					Stream zipStream = zf.GetInputStream(zipEntry);

					// Manipulate the output filename here as desired.
					String fullZipToPath = Path.Combine(outFolder, entryFileName);
					string directoryName = Path.GetDirectoryName(fullZipToPath);
					if (directoryName.Length > 0)
						Directory.CreateDirectory(directoryName);

					// Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
					// of the file, but does not waste memory.
					// The "using" will close the stream even if an exception occurs.
					using (FileStream streamWriter = File.Create(fullZipToPath))
					{
						StreamUtils.Copy(zipStream, streamWriter, buffer);
					}
				}
			}
			finally
			{
				if (zf != null)
				{
					zf.IsStreamOwner = true; // Makes close also shut the underlying stream
					zf.Close(); // Ensure we release resources
				}
			}
		}

        public static void CreateZipFile(string argOutPathname, string argPassword, IEnumerable<string> argFileList)
        {
            //Open the stream for writing
            FileStream fsOut = File.Create(argOutPathname);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            zipStream.Password = argPassword;  // optional. Null is the same as not setting. Required if using AES.

            //Loop through the files and send them to the compressor
            foreach (string currentFilename in argFileList)
            {
                //This takes everything off the front so the zipfile only has files, not source dirs.
                int folderOffset = currentFilename.LastIndexOf('\\') + 1;
                CompressFolder(currentFilename, zipStream, folderOffset);
            }

            // Makes the Close also Close the underlying stream
            zipStream.IsStreamOwner = true; 
            zipStream.Close();
        }

        public static void CreateZipFile(string argOutPathname, string argPassword, string argFileName)
        {

            FileStream fsOut = File.Create(argOutPathname);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            zipStream.Password = argPassword;  // optional. Null is the same as not setting. Required if using AES.

            int folderOffset = argFileName.LastIndexOf('\\') + 1;
            CompressFolder(argFileName, zipStream, folderOffset);

            zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
            zipStream.Close();
        }

        public static void UpdateExistingZip(string argZipFileName, string argPassword, IEnumerable<string> argFileList)
        {
            ZipFile zipFile = new ZipFile(argZipFileName);

            // Must call BeginUpdate to start, and CommitUpdate at the end.
            zipFile.BeginUpdate();

            zipFile.Password = argPassword; // Only if a password is wanted on the new entry

            // The "Add()" method will add or overwrite as necessary.
            // When the optional entryName parameter is omitted, the entry will be named
            // with the full folder path and without the drive e.g. "temp/folder/test1.txt".

            foreach (String currentFilename in argFileList)
            {
                int folderOffset = currentFilename.LastIndexOf('\\') + 1;

                string entryName = currentFilename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction

                zipFile.Add(currentFilename, entryName);
            }

            // Both CommitUpdate and Close must be called.
            zipFile.CommitUpdate();
            zipFile.Close();
        }

        public static void UpdateExistingZip(string argZipFileName, string argPassword, string argFileName)
        {
            ZipFile zipFile = new ZipFile(argZipFileName);

            // Must call BeginUpdate to start, and CommitUpdate at the end.
            zipFile.BeginUpdate();

            zipFile.Password = argPassword; // Only if a password is wanted on the new entry

            // The "Add()" method will add or overwrite as necessary.
            // When the optional entryName parameter is omitted, the entry will be named
            // with the full folder path and without the drive e.g. "temp/folder/test1.txt".

            int folderOffset = argFileName.LastIndexOf('\\') + 1;

            string entryName = argFileName.Substring(folderOffset); // Makes the name in zip based on the folder
            entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction

            zipFile.Add(argFileName, entryName);

            // Both CommitUpdate and Close must be called.
            zipFile.CommitUpdate();
            zipFile.Close();
        }

        private static void CompressFolder(string argFileName, ZipOutputStream argZipStream, int argFolderOffset)
        {

            //Check if the string is a directory
            bool isDir = (File.GetAttributes(argFileName) & FileAttributes.Directory) == FileAttributes.Directory;
            if (isDir)
            {
                List<String> files = new List<string>(Directory.GetFiles(argFileName));
                CompressFolder(files, argZipStream, argFolderOffset);
                return;
            }

            FileInfo fi = new FileInfo(argFileName);

            //Here we create a path for a new entry,
            //but this time with the '\' in the end, its a folder
            //string sEntry = argFileName.Substring(argFolderOffset) + "\\";
            string entryName = argFileName.Substring(argFolderOffset); // Makes the name in zip based on the folder
            entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
            ZipEntry newEntry = new ZipEntry(entryName);
            newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

            // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
            // A password on the ZipOutputStream is required if using AES.
            //   newEntry.AESKeySize = 256;

            // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
            // you need to do one of the following: Specify UseZip64.Off, or set the Size.
            // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
            // but the zip will be in Zip64 format which not all utilities can understand.
            //argZipStream.UseZip64 = UseZip64.Off;
            newEntry.Size = fi.Length;

            argZipStream.PutNextEntry(newEntry);

            // Zip the file in buffered chunks
            // the "using" will close the stream even if an exception occurs
            byte[] buffer = new byte[4096];
            using (FileStream streamReader = File.OpenRead(argFileName))
            {
                StreamUtils.Copy(streamReader, argZipStream, buffer);
            }
            argZipStream.CloseEntry();

        }

        private static void CompressFolder(IEnumerable<string> argFileList, ZipOutputStream argZipStream, int argFolderOffset)
        {

            foreach (string filename in argFileList)
            {

                //Check if the string is a directory
                bool isDir = (File.GetAttributes(filename) & FileAttributes.Directory) == FileAttributes.Directory;
                if (isDir)
                {
                    List<String> files = new List<string>(Directory.GetFiles(filename));
                    CompressFolder(files, argZipStream, argFolderOffset);
                    continue;
                }

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(argFolderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                argZipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, argZipStream, buffer);
                }
                argZipStream.CloseEntry();
            }
        }

    }
}