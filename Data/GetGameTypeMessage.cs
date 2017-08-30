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
    /// Represents a Game Type returned from the server.
    /// </summary>
    internal struct GameTypeListItem
    {
        public int GameTypeId;
        public string GameTypeName;
    }


    internal class GetGameTypeMessage : ServerMessage
    {
        protected const int MinResponseMessageLength = 6;
        private readonly List<int> removeList;
        public List<GameTypeListItem> GameTypeList { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetProductTypesMessage 
        /// </summary>
        public GetGameTypeMessage()
        {
            m_id = 6026; // MessageId
            GameTypeList = new List<GameTypeListItem>();
            removeList = new List<int>
                         {
                             (int)GameType.TwoOn,
                             (int)GameType.TwelveOn,
                             (int)GameType.FifteenOn,
                             (int)GameType.EighteenOn,
                             // FIX : TA6539
                             //(int)GameType.NinetyNumberBingo, 
                             // END : TA6539
                             (int)GameType.EightyNumberBingo,
                             (int)GameType.EightyNumberCash
                         };
        }
        #endregion

        #region Member Methods
        public static GameTypeListItem[] GetArray()
        {
            var msg = new GetGameTypeMessage();
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetGameTypeMessage: " + ex.Message);
            }
            return msg.GameTypeList.ToArray();
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
                throw new MessageWrongSizeException("Get Game Types");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count of Game Types.
                var itemCount = responseReader.ReadUInt16();

                // Clear the Game Type array.
                GameTypeList.Clear();

                // Get all items.
                for (ushort x = 0; x < itemCount; x++)
                {
                    var gameType = new GameTypeListItem {GameTypeId = responseReader.ReadInt32()};

                    // Game Type Name
                    var stringLen = responseReader.ReadUInt16();
                    gameType.GameTypeName = new string(responseReader.ReadChars(stringLen));

                    if (!removeList.Contains(gameType.GameTypeId))
                        GameTypeList.Add(gameType);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Game Types", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Game Types", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}