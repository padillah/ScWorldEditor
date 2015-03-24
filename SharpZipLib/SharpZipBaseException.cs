using System;
using System.Runtime.Serialization;

#if !NETCF_1_0 && !NETCF_2_0

#endif

namespace ZipLib
{
	/// <summary>
	/// SharpZipBaseException is the base exception class for the SharpZipLibrary.
	/// All library exceptions are derived from this.
	/// </summary>
	/// <remarks>NOTE: Not all exceptions thrown will be derived from this class.
	/// A variety of other exceptions are possible for example <see cref="ArgumentNullException"></see></remarks>
#if !NETCF_1_0 && !NETCF_2_0
	[Serializable]
#endif
	public class SharpZipBaseException : ApplicationException
	{
#if !NETCF_1_0 && !NETCF_2_0
		/// <summary>
		/// Deserialization constructor 
		/// </summary>
		/// <param name="argInfo"><see cref="System.Runtime.Serialization.SerializationInfo"/> for this constructor</param>
		/// <param name="argContext"><see cref="StreamingContext"/> for this constructor</param>
		protected SharpZipBaseException(SerializationInfo argInfo, StreamingContext argContext )
			: base( argInfo, argContext )
		{
		}
#endif
		
		/// <summary>
		/// Initializes a new instance of the SharpZipBaseException class.
		/// </summary>
		public SharpZipBaseException()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the SharpZipBaseException class with a specified error message.
		/// </summary>
		/// <param name="argMessage">A message describing the exception.</param>
		public SharpZipBaseException(string argMessage)
			: base(argMessage)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SharpZipBaseException class with a specified
		/// error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="argMessage">A message describing the exception.</param>
		/// <param name="argInnerException">The inner exception</param>
		public SharpZipBaseException(string argMessage, Exception argInnerException)
			: base(argMessage, argInnerException)
		{
		}
	}
}
