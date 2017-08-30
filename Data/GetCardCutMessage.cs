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
    /// Represents a card Cut returned from the server.
    /// </summary>
    internal struct CardCutListItem
    {
        public int CardCutId;
        public string CardCutName;
    }

    /// <summary>
    /// Represents a Get Card Cut message.
    /// </summary>
    internal class GetCardCutMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Member Properties
        public int CardCutId { protected get; set; }
        public List<CardCutListItem> CardCutList { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetGameCategoryMessage 
        /// </summary>
        public GetCardCutMessage()
            :this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetGameCategoryMessage 
        /// </summary>
        public GetCardCutMessage(int cardCutId)
        {
            m_id = 6033; // MessageId
            CardCutId = cardCutId;
            CardCutList = new List<CardCutListItem>();
        }
        #endregion

        #region Member Methods

        public static CardCutListItem[] GetArray(int cardCutId)
        {
            var msg = new GetCardCutMessage {CardCutId = cardCutId};
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetCardCutMessage: " + ex.Message);
            }
            return msg.CardCutList.ToArray();
        }

        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        protected override void PackRequest()
        {
            try
            {
                // Create the streams we will be writing to.
                var requestStream = new MemoryStream();
                var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

                // Card Cut Id
                requestWriter.Write(CardCutId);

                // Set the bytes to be sent.
                m_requestPayload = requestStream.ToArray();

                // Close the streams.
                requestWriter.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("GetCardCutMessage.PackRequest()...Exception: "+ex.Message );
            }
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
                throw new MessageWrongSizeException("Get Card Cut");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the array.
                CardCutList.Clear();

                // Get all the items.
                for (ushort x = 0; x < itemCount; x++)
                {
                    var cardCutListItem = new CardCutListItem {CardCutId = responseReader.ReadInt32()};

                    // Card Cut Name
                    var stringLen = responseReader.ReadUInt16();
                    cardCutListItem.CardCutName = new string(responseReader.ReadChars(stringLen));

                    CardCutList.Add(cardCutListItem);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Card Cut", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Card Cut", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
