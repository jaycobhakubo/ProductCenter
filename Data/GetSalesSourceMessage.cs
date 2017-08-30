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
    /// Represents a Sales Source returned from the server.
    /// </summary>
    internal struct SalesSourceListItem
    {
        public int SalesSourceId;
        public string SalesSource;
    }

    
    /// <summary>
    /// Represents a Get Sales Source message.
    /// </summary>
    internal class GetSalesSourceMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public List<SalesSourceListItem> SalesSource { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetSalesSourceMessage 
        /// </summary>
        public GetSalesSourceMessage()
        {
            m_id = 18070; // MessageId
            SalesSource = new List<SalesSourceListItem>();
        }
        #endregion

        #region Member Methods
        public static SalesSourceListItem[] GetArray()
        {
            var msg = new GetSalesSourceMessage();
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetSalesSourceMessage: " + ex.Message);
            }
            return msg.SalesSource.ToArray();
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
                throw new MessageWrongSizeException("Get Sales Source");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count of Sales Source.
                var salesSourceCount = responseReader.ReadUInt16();

                // Clear the Sales Source array.
                SalesSource.Clear();

                // Get all the Sales Source
                for (ushort x = 0; x < salesSourceCount; x++)
                {
                    var salesSource = new SalesSourceListItem {SalesSourceId = responseReader.ReadInt32()};

                    // Sales Source Name
                    var stringLen = responseReader.ReadUInt16();
                    salesSource.SalesSource = new string(responseReader.ReadChars(stringLen));

                    SalesSource.Add(salesSource);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Sales Source", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Sales Source", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
