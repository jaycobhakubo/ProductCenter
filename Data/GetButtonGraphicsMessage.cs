using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    /// <summary>
    /// Represents a card Cut returned from the server.
    /// </summary>
    internal struct ButtonGraphic
    {
        public int ButtonGraphicId;
        public string ButtonGraphicDescription;
    }

    internal class GetButtonGraphicsMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public int ButtonGraphicId { protected get; set; }
        public List<ButtonGraphic> ButtonGraphics { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetGameCategoryMessage 
        /// </summary>
        public GetButtonGraphicsMessage()
            :this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetGameCategoryMessage 
        /// </summary>
        public GetButtonGraphicsMessage(int graphicId)
        {
            m_id = 18132; // MessageId
            ButtonGraphicId = graphicId;
            ButtonGraphics = new List<ButtonGraphic>();
        }
        #endregion

        #region Member Methods
        public static List<ButtonGraphic> GetButtonGraphics(int graphicId)
        {
            var msg = new GetButtonGraphicsMessage(graphicId);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetButtonGraphicsMessage: " + ex.Message);
            }
            return msg.ButtonGraphics;
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

                // Button Graphic Id
                requestWriter.Write(ButtonGraphicId);

                // Set the bytes to be sent.
                m_requestPayload = requestStream.ToArray();

                // Close the streams.
                requestWriter.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("GetButtonGraphicsMessage.PackRequest()...Exception: " + ex.Message);
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
                throw new MessageWrongSizeException("Get Button Graphics");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the array.
                ButtonGraphics.Clear();

                // Get all the items.
                for (ushort x = 0; x < itemCount; x++)
                {
                    var graphic = new ButtonGraphic {ButtonGraphicId = responseReader.ReadInt32()};
                    var stringLen = responseReader.ReadUInt16();
                    graphic.ButtonGraphicDescription = new string(responseReader.ReadChars(stringLen));
                    ButtonGraphics.Add(graphic);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Button Graphics", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Button Graphics", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion
    }
}
