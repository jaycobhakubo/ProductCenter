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
    /// Represents a Delete Menu message.
    /// </summary>
    internal class DelMenuMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion Constants and Data Types

        public int OperatorId { protected get; set; }
        public int MenuId { protected get; set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DelMenuMessage class.
        /// </summary>
        public DelMenuMessage()
            : this(0,0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelMenuMessage class.
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="menuId"></param>
        public DelMenuMessage(int operatorId, int menuId)
        {
            m_id = 18099; // Remove Menu Item.
            OperatorId = operatorId;
            MenuId = menuId;
        }
        #endregion Constructors

        #region Member Methods
        public static void DeleteMenu(int operatorId, int menuId)
        {
            var msg = new DelMenuMessage(operatorId, menuId);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("DelMenuMessage: " + ex.Message);
            }
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Operator Id
            requestWriter.Write(OperatorId);
            
            // Menu Id
            requestWriter.Write(MenuId);

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }
        #endregion Member Methods
    }
}
