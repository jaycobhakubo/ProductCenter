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
    /// Represents a CBB Program Game returned from the server.
    /// </summary>
    internal struct CBBProgramGameListItem
    {
        public int CBBProgramGameId;
        public string CBBProgramGameName;
    }

    /// <summary>
    /// Represents a Get CBB Program Game Message.
    /// </summary>
    internal class GetCBBProgramGameMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Member Variables
        protected List<CBBProgramGameListItem> cbbProgramGame;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetCBBProgramGameMessage 
        /// </summary>
        public GetCBBProgramGameMessage()
        {
            m_id = 6035; // MessageId
            cbbProgramGame = new List<CBBProgramGameListItem>();
        }
        #endregion

        #region Member Methods
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
                throw new MessageWrongSizeException("Get CBB Program Game");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the array.
                cbbProgramGame.Clear();

                // Get all the items.
                for (ushort x = 0; x < itemCount; x++)
                {
                    var cbbProgramGameListItem = new CBBProgramGameListItem
                                                 {CBBProgramGameId = responseReader.ReadInt32()};

                    // CBB Program Game Name
                    var stringLen = responseReader.ReadUInt16();
                    cbbProgramGameListItem.CBBProgramGameName = new string(responseReader.ReadChars(stringLen));

                    cbbProgramGame.Add(cbbProgramGameListItem);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get CBB Program Game", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get CBB Program Game", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets the CBB Program Game List.
        /// </summary>
        public CBBProgramGameListItem[] CBBProgramGameList
        {
            get
            {
                return cbbProgramGame.ToArray();
            }
        }
        #endregion
    }
}
