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
    /// Represents a Delete Product Item Message.
    /// </summary>
    internal class DelProductItemMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public int OperatorId { protected get; set; }
        public int ProductItemId { protected get; set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DelProductItemMessage class.
        /// </summary>
        public DelProductItemMessage()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelProductItemMessage class.
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="productItemId"></param>
        public DelProductItemMessage(int operatorId, int productItemId)
        {
            m_id = 18077; // Delete Product Item.
            OperatorId = operatorId;
            ProductItemId = productItemId;
        }
        #endregion

        #region Member Methods
        public static void Delete(int operatorId, int productItemId)
        {
            var msg = new DelProductItemMessage(operatorId, productItemId);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("DelProductItemMessage: " + ex.Message);
            }
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Operator Id
            requestWriter.Write(OperatorId);

            // Product Item Id
            requestWriter.Write(ProductItemId);

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }
        #endregion
    }
}
