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
    /// This is a request message from the client to the GTIS app. The purpose of this message is to be able to set the level color data for a cardset.
    /// </summary>
    internal class SetCardsetLevelDataMessage : ServerMessage
    {
        #region Properties
        protected const int MinResponseMessageLength = 4;
        public GetCardsetLevelDataMessage.CardColorLevel CardColorLevel { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SetCardsetLevelDataMessage
        /// </summary>
        /// <param name="cardColorLevelInfo">the information to save. If the color name is empty, then unlinks the level from any colors</param>
        public SetCardsetLevelDataMessage(GetCardsetLevelDataMessage.CardColorLevel cardColorLevelInfo)
        {
            m_id = 6077; // MessageId
            CardColorLevel = cardColorLevelInfo;

            m_strMessageName = "Set Cardset Color Level Data";
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Returns the list of cardset color levels
        /// </summary>
        /// <param name="cardLevelId">the information to save. If the color name is empty, then unlinks the level from any colors</param>
        /// <returns></returns>
        public static void SetCardsetLevelData(GetCardsetLevelDataMessage.CardColorLevel cardColorLevelInfo)
        {
            var msg = new SetCardsetLevelDataMessage(cardColorLevelInfo);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("Set Cardset Color Level Data: " + ex.ToString());
            }
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
            requestWriter.Write(CardColorLevel.LevelId);

            WriteString(requestWriter, CardColorLevel.ColorName);

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
                throw new MessageWrongSizeException("Set Cardset Color Level Data");

            // Try to unpack the data.
            try
            {
                // Seek past return code. This is read in by the base.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Nothing else to do here
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set Cardset Color Level Data", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Set Cardset Color Level Data", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion
    }
}
