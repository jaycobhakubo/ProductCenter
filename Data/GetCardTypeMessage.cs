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
    /// Represents a Card Type returned from the server.
    /// </summary>
    internal struct CardTypeListItem
    {
        public int CardTypeId;
        public string CardTypeName;
    }

    /// <summary>
    /// Represents a Get Card Type message.
    /// </summary>
    internal class GetCardTypeMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Member Properties
        public List<CardTypeListItem> CardTypeList { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetCardTypeMessage 
        /// </summary>
        public GetCardTypeMessage()
        {
            m_id = 6003; // MessageId
            CardTypeList = new List<CardTypeListItem>();
        }
        #endregion

        #region Member Methods
        public static CardTypeListItem[] GetArray()
        {
            var msg = new GetCardTypeMessage();
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetCardTypeMessage: " + ex.Message);
            }
            return msg.CardTypeList.ToArray();
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
                throw new MessageWrongSizeException("Get Card Type");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the array.
                CardTypeList.Clear();

                // Get all items.
                for (ushort x = 0; x < itemCount; x++)
                {
                    var cardType = new CardTypeListItem {CardTypeId = responseReader.ReadInt32()};

                    // Card Type Description
                    var stringLen = responseReader.ReadUInt16();
                    cardType.CardTypeName = new string(responseReader.ReadChars(stringLen));

                    CardTypeList.Add(cardType);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Card Type", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Card Type", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
