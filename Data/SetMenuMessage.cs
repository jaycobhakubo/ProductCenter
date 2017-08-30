// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.IO;
using System.Text;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    /// <summary>
    /// Represents a Set Menu message.
    /// </summary>
    internal class SetMenuMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion Constants and Data Types

        public int MenuId { get; set; }
        public int OperatorId { get; set; }
        public string MenuName { get; set; }
        public int MenuTypeId { get; set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SetMenuMessage class.
        /// </summary>
        public SetMenuMessage()
            : this(0, 0, "", 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SetMenuMessage class.
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="operatorId"></param>
        /// <param name="menuName"></param>
        /// <param name="menuTypeId"></param>
        public SetMenuMessage(int menuId, int operatorId, string menuName, int menuTypeId)
        {
            m_id = 18089; // Set Menu Data
            MenuId = menuId;
            OperatorId = operatorId;
            MenuName = menuName;
            MenuTypeId = menuTypeId;
        }
        #endregion Constructors

        #region Member Methods
        public static int Save(int menuId, int operatorId, string menuName, int menuTypeId)
        {
            var msg = new SetMenuMessage(menuId, operatorId, menuName, menuTypeId);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("SetMenuMessage: " + ex.Message);
            }
            return msg.MenuId;
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Menu Id
            requestWriter.Write(MenuId);

            // Operator Id
            requestWriter.Write(OperatorId);

            // Menu Name
            requestWriter.Write((ushort)MenuName.Length);
            requestWriter.Write(MenuName.ToCharArray());

            // Menu Type Id
            requestWriter.Write(MenuTypeId);

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }

        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            // Create the streams we will be reading from.
            var responseStream = new MemoryStream(m_responsePayload);
            var responseReader = new BinaryReader(responseStream, Encoding.Unicode);

            // Check the response length.
            if (responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("Set Menu Data");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the Menu Id
                MenuId = responseReader.ReadInt32();
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set Menu Data", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Set Menu Data", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion Member Methods
    }
}
