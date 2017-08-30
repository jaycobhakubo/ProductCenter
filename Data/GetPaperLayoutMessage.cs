//RALLY TA 5744
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
    /// Represents a GetPaperLayoutMessage.
    /// </summary>
    internal class GetPaperLayoutMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public List<PaperLayout> PaperLayouts { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetPaperLayoutMessage 
        /// </summary>
        public GetPaperLayoutMessage()
        {
            m_id = 18184; // MessageId
            PaperLayouts = new List<PaperLayout>();
        }
        #endregion

        #region Member Methods
        public static List<PaperLayout> GetList()
        {
            var msg = new GetPaperLayoutMessage();
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetPaperLayoutMessage: " + ex.Message);
            }
            return msg.PaperLayouts;
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
                throw new MessageWrongSizeException("Get Paper Layouts");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count of Paper Layouts.
                ushort paperLayoutCount = responseReader.ReadUInt16();

                // Clear the Product Type array.
                PaperLayouts.Clear();

                // Get all the Product Types
                for (ushort x = 0; x < paperLayoutCount; x++)
                {
                    PaperLayout layout = new PaperLayout { PaperLayoutId = responseReader.ReadInt32() };

                    // Product Type Name
                    ushort stringLen = responseReader.ReadUInt16();
                    layout.PaperLayoutName = new string(responseReader.ReadChars(stringLen));

                    PaperLayouts.Add(layout);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Paper Layouts", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Paper Layouts", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
//END RALLY TA 5744
