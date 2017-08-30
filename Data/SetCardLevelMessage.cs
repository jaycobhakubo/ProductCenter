using System;
using System.IO;
using System.Text;
using GTI.Modules.Shared;
using GTI.Modules.Shared.Data;

namespace GTI.Modules.ProductCenter.Data
{
    /// <summary>
    /// Represents a Set Card Level Message.
    /// </summary>
    internal class SetCardLevelMessage : ServerMessage
    {
        protected const int MinResponseMessageLength = 8;
        public int CardLevelId { protected get; set; }
        public CardLevelItem CardLevel { protected get; set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SetCardLevelMessage class.
        /// </summary>
        public SetCardLevelMessage(int cardLevelId, CardLevelItem cardLevelItem)
        {
            m_id = 6032; // Set Card Level Item
            CardLevelId = cardLevelId;
            CardLevel = cardLevelItem;
        }
        #endregion

        #region Member Methods
        // FIX : DE3187 handle in use card levels
        public static bool Save(int cardLevelId, CardLevelItem cardLevelItem)
        {
            SetCardLevelMessage msg = new SetCardLevelMessage(cardLevelId, cardLevelItem);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("SetCardLevelMessage: " + ex.Message);
            }
            catch (ServerException)
            {
                return false;
            }
            return true;
        }

        /// RALLY US4547
        /// <summary>
        /// Saves a new card level item to the server
        /// </summary>
        /// <param name="cardLevelItem"></param>
        /// <returns>the ID of the card level item</returns>
        public static int SaveNew(CardLevelItem cardLevelItem)
        {
            SetCardLevelMessage msg = new SetCardLevelMessage(0, cardLevelItem);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("SetCardLevelMessage: " + ex.Message);
            }
            catch (ServerException)
            {
                return 0;
            }
            return msg.CardLevelId;
        }

        // END: DE3187
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Card Level Id
            requestWriter.Write(CardLevel.CardLevelId);

            // Card Level Color
            requestWriter.Write(CardLevel.LevelColor);

            // Card Level Multiplier
            requestWriter.Write((ushort)CardLevel.Multiplier.Length);
            requestWriter.Write(CardLevel.Multiplier.ToCharArray());

            // Card Level Name
            requestWriter.Write((ushort)CardLevel.CardLevelName.Length);
            requestWriter.Write(CardLevel.CardLevelName.ToCharArray());

            // Card Is Active flag
            requestWriter.Write(CardLevel.IsActive);

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }

        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            // Create the streams we will be reading from.
            MemoryStream responseStream = new MemoryStream(m_responsePayload);
            BinaryReader responseReader = new BinaryReader(responseStream, Encoding.Unicode);

            // Check the response length.
            if (responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("SetCardLevel");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the Menu Id
                CardLevelId = responseReader.ReadInt32();
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("SetCardLevela", e);
            }
            catch (Exception e)
            {
                throw new ServerException("SetCardLevel", e);
            }
            finally
            {
                // Close the streams.
                responseReader.Close();
            }

        }
        #endregion

    }
}
