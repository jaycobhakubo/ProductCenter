using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTI.Modules.Shared;
using System.IO;

namespace GTI.Modules.ProductCenter.Data
{
    /// RALLY US4547
    /// <summary>
    /// This is a request message from the client to the GTIS app. The purpose of this message is to retrieve the information about the card set levels. 
    ///   This level data is used to set levels for the cards on paper.
    /// Note: this is a temporary solution until more time is allotted to make a better link
    /// </summary>
    internal class GetCardsetLevelDataMessage : ServerMessage
    {
        public class CardColorLevel
        {
            public int LevelId { get; set; }
            public string ColorName { get; set; }
        }

        #region Properties
        protected const int MinResponseMessageLength = 8;
        public int CardLevelId { protected get; set; }
        public List<CardColorLevel> CardLevelList { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetCardsetLevelDataMessage 
        /// </summary>
        public GetCardsetLevelDataMessage()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetCardsetLevelDataMessage
        /// </summary>
        /// <param name="cardLevelId"> 
        ///   0 – Return all levels
        ///   # - A valid level ID will return just the requested data
        /// </param>
        public GetCardsetLevelDataMessage(int cardLevelId)
        {
            m_id = 6076; // MessageId
            CardLevelId = cardLevelId;
            CardLevelList = new List<CardColorLevel>();

            m_strMessageName = "Get Cardset Color Level Data";
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Returns the list of cardset color levels
        /// </summary>
        /// <param name="cardLevelId">if non-zero, returns all cardset color levels</param>
        /// <returns></returns>
        public static List<CardColorLevel> GetCardsetLevelData(int cardLevelId = 0)
        {
            var msg = new GetCardsetLevelDataMessage { CardLevelId = cardLevelId };
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("Get Cardset Color Level Data: " + ex.ToString());
            }
            return msg.CardLevelList;
        }

        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Level Id
            requestWriter.Write(CardLevelId);

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
                throw new MessageWrongSizeException("Get Cardset Color Level Data");

            // Try to unpack the data.
            try
            {
                // Seek past return code. This is read in by the base.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Get all the items.
                for (ushort x = 0; x < itemCount; x++)
                {
                    var cardLevelListItem = new CardColorLevel
                                            {
                                                LevelId = responseReader.ReadInt32(),
                                                ColorName = ReadString(responseReader)
                                            };

                    CardLevelList.Add(cardLevelListItem);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Cardset Color Level Data", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Cardset Color Level Data", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion
    }
}
