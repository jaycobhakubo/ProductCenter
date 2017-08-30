// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  � 2007 GameTech
// International, Inc.

using System;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Business
{
    /// <summary>
    /// The exception that is thrown when a non-fatal ProductCenter error occurs.
    /// </summary>
    internal class ProductCenterException : ModuleException
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the ProductCenterException class.
        /// </summary>
        public ProductCenterException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProductCenterException class with 
        /// a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public ProductCenterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProductCenterException class with 
        /// a specified error message and a reference to the inner exception 
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of 
        /// the current exception. If the innerException parameter is not a 
        /// null reference, the current exception is raised in a catch block 
        /// that handles the inner exception.</param>
        public ProductCenterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        #endregion
    }
}