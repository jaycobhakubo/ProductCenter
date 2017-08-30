// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{

    /// <summary>
    /// Represents a Product Type returned from the server.
    /// </summary>
    internal struct ProductTypeListItem 
    {
        public int ProductTypeId;
        public string ProductType;
    }

    /// <summary>
    /// Represents a Get Product Types message.
    /// </summary>
    internal class GetProductTypesMessage : ServerMessage 
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public List<ProductTypeListItem> ProductTypes { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetProductTypesMessage 
        /// </summary>
        public GetProductTypesMessage()
        {
            m_id = 18069; // MessageId
            ProductTypes = new List<ProductTypeListItem>();
        }
        #endregion

        #region Member Methods
        public static ProductTypeListItem[] GetArray()
        {
            var msg = new GetProductTypesMessage();
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetProductTypesMessage: " + ex.Message);
            }
            return msg.ProductTypes.ToArray();
        }

        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }

        /// <summary>
        /// Parses the response received from the server.
        /// </summary>
        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            // Create the streams we will be reading from.
            var responseStream = new MemoryStream(m_responsePayload);
            var responseReader = new BinaryReader(responseStream, Encoding.Unicode);

            // Check the response length.
            if (responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("Get Product Types");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count of Product Types.
                var productTypeCount = responseReader.ReadUInt16();

                // Clear the Product Type array.
                ProductTypes.Clear();

                // Get all the Product Types
                for (ushort x = 0; x < productTypeCount; x++)
                {
                    var productType = new ProductTypeListItem {ProductTypeId = responseReader.ReadInt32()};

                    // Product Type Name
                    var stringLen = responseReader.ReadUInt16();
                    productType.ProductType = new string(responseReader.ReadChars(stringLen));

                    ProductTypes.Add(productType);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Product Types", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Product Types", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
