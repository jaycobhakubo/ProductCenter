// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.IO;
using System.Text;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    /// <summary>
    /// Represents a Delete Package Item Message.
    /// </summary>
    internal class DelPackageItemMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        /// <summary>
        /// Sets the Package Id.
        /// </summary>
        private int PackageId { get; set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DelPackageItemMessage class.
        /// </summary>
        public DelPackageItemMessage()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelPackageItemMessage class.
        /// </summary>
        /// <param name="packageId"></param>
        public DelPackageItemMessage(int packageId)
        {
            m_id = 18080; // Delete Package Item.
            PackageId = packageId;
        }
        #endregion

        #region Member Methods
        public static void DeletePackage(int packageId)
        {
            var msg = new DelPackageItemMessage(packageId);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("DelPackageItemMessage: " + ex.Message);
            }
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Package Id
            requestWriter.Write(PackageId);

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }

        // FIX: DE8818
        protected override void UnpackResponse()
        {
            try
            {
                base.UnpackResponse();
            }
            catch(Exception)
            {
                throw new ServerException(ServerReturnCode, ServerExceptionTranslator.GetServerErrorMessage(ServerReturnCode));
            }
        }
        #endregion

    }
}
