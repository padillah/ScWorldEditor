using System;
using ZipLib.Core;

namespace ZipLib.Zip
{
    /// <summary>
    /// FastZipEvents supports all events applicable to <see cref="FastZip">FastZip</see> operations.
    /// </summary>
// ReSharper disable ClassNeverInstantiated.Global
    public class FastZipEvents
// ReSharper restore ClassNeverInstantiated.Global
    {
        /// <summary>
        /// Delegate to invoke when processing directories.
        /// </summary>
        public ProcessDirectoryHandler ProcessDirectory;
		
        /// <summary>
        /// Delegate to invoke when processing files.
        /// </summary>
        public ProcessFileHandler ProcessFile;

        /// <summary>
        /// Delegate to invoke during processing of files.
        /// </summary>
        public ProgressHandler Progress;

        /// <summary>
        /// Delegate to invoke when processing for a file has been completed.
        /// </summary>
        public CompletedFileHandler CompletedFile;
		
        /// <summary>
        /// Delegate to invoke when processing directory failures.
        /// </summary>
        public DirectoryFailureHandler DirectoryFailure;
		
        /// <summary>
        /// Delegate to invoke when processing file failures.
        /// </summary>
        public FileFailureHandler FileFailure;
		
        /// <summary>
        /// Raise the <see cref="DirectoryFailure">directory failure</see> event.
        /// </summary>
        /// <param name="argDirectory">The directory causing the failure.</param>
        /// <param name="argEvtArgs">The exception for this event.</param>
        /// <returns>A boolean indicating if execution should continue or not.</returns>
        public bool OnDirectoryFailure(string argDirectory, Exception argEvtArgs)
        {
            bool result = false;
            DirectoryFailureHandler handler = DirectoryFailure;

            if ( handler != null ) {
                ScanFailureEventArgs args = new ScanFailureEventArgs(argDirectory, argEvtArgs);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }
		
        /// <summary>
        /// Fires the <see cref="FileFailure"> file failure handler delegate</see>.
        /// </summary>
        /// <param name="argFile">The file causing the failure.</param>
        /// <param name="argEvtArgs">The exception for this failure.</param>
        /// <returns>A boolean indicating if execution should continue or not.</returns>
        public bool OnFileFailure(string argFile, Exception argEvtArgs)
        {
            FileFailureHandler handler = FileFailure;
            bool result = (handler != null);

            if ( result ) {
                ScanFailureEventArgs args = new ScanFailureEventArgs(argFile, argEvtArgs);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }
		
        /// <summary>
        /// Fires the <see cref="ProcessFile">ProcessFile delegate</see>.
        /// </summary>
        /// <param name="argFile">The file being processed.</param>
        /// <returns>A boolean indicating if execution should continue or not.</returns>
        public bool OnProcessFile(string argFile)
        {
            bool result = true;
            ProcessFileHandler handler = ProcessFile;

            if ( handler != null ) {
                ScanEventArgs args = new ScanEventArgs(argFile);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }

        /// <summary>
        /// Fires the <see cref="CompletedFile"/> delegate
        /// </summary>
        /// <param name="argFile">The file whose processing has been completed.</param>
        /// <returns>A boolean indicating if execution should continue or not.</returns>
        public bool OnCompletedFile(string argFile)
        {
            bool result = true;
            CompletedFileHandler handler = CompletedFile;
            if ( handler != null ) {
                ScanEventArgs args = new ScanEventArgs(argFile);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }
		
        /// <summary>
        /// Fires the <see cref="ProcessDirectory">process directory</see> delegate.
        /// </summary>
        /// <param name="argDirectory">The directory being processed.</param>
        /// <param name="argHasMatchingFiles">Flag indicating if the directory has matching files as determined by the current filter.</param>
        /// <returns>A <see cref="bool"/> of true if the operation should continue; false otherwise.</returns>
        public bool OnProcessDirectory(string argDirectory, bool argHasMatchingFiles)
        {
            bool result = true;
            ProcessDirectoryHandler handler = ProcessDirectory;
            if ( handler != null ) {
                DirectoryEventArgs args = new DirectoryEventArgs(argDirectory, argHasMatchingFiles);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }

        /// <summary>
        /// The minimum timespan between <see cref="Progress"/> events.
        /// </summary>
        /// <value>The minimum period of time between <see cref="Progress"/> events.</value>
        /// <seealso cref="Progress"/>
        /// <remarks>The default interval is three seconds.</remarks>
        public TimeSpan ProgressInterval
        {
            get { return _progressInterval; }
            set { _progressInterval = value; }
        }

        #region Instance Fields
        TimeSpan _progressInterval = TimeSpan.FromSeconds(3);
        #endregion
    }
}