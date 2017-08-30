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
    /// Represents a Menu Type returned from the server.
    /// </summary>
    public struct MenuTypeListItem
    {
        public int MenuTypeId;
        public string MenuTypeName;
    }
    
    /// <summary>
    /// Represents a Get Menu Types message.
    /// </summary>
    internal class GetMenuTypesMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion Constants and Data Types

        public List<MenuTypeListItem> MenuTypes { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetMenuTypesMessage.
        /// </summary>
        public GetMenuTypesMessage()
        {
            m_id = 18137;
            MenuTypes = new List<MenuTypeListItem>();
        }
        #endregion Constructors

        #region Member Methods
        public static List<MenuTypeListItem> GetList()
        {
            var msg = new GetMenuTypesMessage();
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetMenuTypesMessage: " + ex.Message);
            }
            return msg.MenuTypes;
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
                throw new MessageWrongSizeException("Get Menu Types");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count of Menu Types.
                var menuTypeCount = responseReader.ReadUInt16();

                // Clear the Menu Type array.
                MenuTypes.Clear();

                // Get all the Menu Types
                for (ushort x = 0; x < menuTypeCount; x++)
                {
                    var menuType = new MenuTypeListItem {MenuTypeId = responseReader.ReadInt32()};

                    // Menu Type Name
                    var stringLen = responseReader.ReadUInt16();
                    menuType.MenuTypeName = new string(responseReader.ReadChars(stringLen));

                    MenuTypes.Add(menuType);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Menu Types", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Menu Types", e);
            }

            responseReader.Close();
        }
        #endregion Member Methods
    }
}
