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
    /// Represents a Card Media returned from the server.
    /// </summary>
    internal struct CardMediaListItem
    {
        public int CardMediaId;
        public string CardMediaName;
    }

    /// <summary>
    /// Represents a Get Card media message.
    /// </summary>
    internal class GetCardMediaMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Member Properties
        public int CardMediaId { protected get; set; }
        public List<CardMediaListItem> CardMediaList { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetCardMediaMesage 
        /// </summary>
        public GetCardMediaMessage()
            :this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetGameCategoryMessage 
        /// </summary>
        public GetCardMediaMessage(int cardMediaId)
        {
            m_id = 6034; // MessageId
            CardMediaId = cardMediaId;
            CardMediaList = new List<CardMediaListItem>();
        }
        #endregion

        #region Member Methods

        public static CardMediaListItem[] GetArray(int cardMediaId)
        {
            var msg = new GetCardMediaMessage { CardMediaId = cardMediaId };
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetCardMediaMessage: " + ex.Message);
            }
            return msg.CardMediaList.ToArray();
        }
        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Card Media Id
            requestWriter.Write(CardMediaId);

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
                throw new MessageWrongSizeException("Get Card Media");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the array.
                CardMediaList.Clear();

                // Get all the items.
                for (ushort x = 0; x < itemCount; x++)
                {
                    var cardMediaListItem = new CardMediaListItem {CardMediaId = responseReader.ReadInt32()};

                    // Card Media Name
                    var stringLen = responseReader.ReadUInt16();
                    cardMediaListItem.CardMediaName = new string(responseReader.ReadChars(stringLen));

                    CardMediaList.Add(cardMediaListItem);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Card Media", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Card Media", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
