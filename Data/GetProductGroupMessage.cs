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
    /// Represents a Product Group Item returned from the server.
    /// </summary>
    public class ProductGroupItem
    {
        public int ProdGroupId;
        public string ProdGroupName;
        public bool IsActive;
        public bool IsModified;
    }

    /// <summary>
    /// Represents a Get Product Groups message.
    /// </summary>
    internal class GetProductGroupMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public List<ProductGroupItem> ProductGroups { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetProductGroupMessage 
        /// </summary>
        public GetProductGroupMessage()
        {
            m_id = 18166; // MessageId
            ProductGroups = new List<ProductGroupItem>();
        }
        #endregion

        #region Member Methods
        public static List<ProductGroupItem> GetList()
        {
            var msg = new GetProductGroupMessage();
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetProductGroupMessage: " + ex.Message);
            }
            return msg.ProductGroups;
        }

        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // get all groups
            requestWriter.Write(0);

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
                throw new MessageWrongSizeException("Get Product Groups");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count of Product Types.
                ushort productGroupCount = responseReader.ReadUInt16();

                // Clear the Product Type array.
                ProductGroups.Clear();

                // Get all the Product Types
                for (ushort x = 0; x < productGroupCount; x++)
                {
                    ProductGroupItem productGroup = new ProductGroupItem { ProdGroupId = responseReader.ReadInt32() };

                    // Product Type Name
                    ushort stringLen = responseReader.ReadUInt16();
                    productGroup.ProdGroupName = new string(responseReader.ReadChars(stringLen));

                    // Is Active flag
                    productGroup.IsActive = responseReader.ReadBoolean();

                    // reset modified flag
                    productGroup.IsModified = false;

                    ProductGroups.Add(productGroup);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Product Groups", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Product Groups", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
