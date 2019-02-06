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
    /// Represents a POS Menu item returned from the server.
    /// </summary>
    public struct POSMenuItem
    {
        public int MenuId;
        public string MenuName;
        public int MenuTypeId;
        /// US1772
        /// <summary>
        /// Whether or not this menu is a daily menu
        /// </summary>
        public bool IsDailyMenu;

        public override bool Equals(object obj)
        {
            if (obj is POSMenuItem)
            {
                POSMenuItem other = (POSMenuItem)obj;
                return other.MenuId == this.MenuId;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return MenuId.GetHashCode();
        }
    }

    /// <summary>
    /// Represents a Page Number for a specific Menu Id.
    /// </summary>
    internal struct PageItem
    {
        public POSMenuItem Menu;
        public byte MenuPage;
    }

    internal class GetPOSMenuMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        /// <summary>
        /// Gets or Sets the Operator Id
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// Gets the list of Menu Items retrieved from the server.
        /// </summary>
        public List<POSMenuItem> MenuItems { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetPOSMenuMessage class.
        /// </summary>
        public GetPOSMenuMessage()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetPOSMenuMessage class.
        /// </summary>
        /// <param name="operatorId"></param>
        public GetPOSMenuMessage(int operatorId)
        {
            m_id = 18007;
            OperatorId = operatorId;
            MenuItems = new List<POSMenuItem>();
        }
        #endregion

        #region Member Methods
        public static List<POSMenuItem> GetMenuList(int packageId)
        {
            var msg = new GetPOSMenuMessage(packageId);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetPOSMenuMessage: " + ex.Message);
            }
            return msg.MenuItems;
        }
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

        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            // Create the streams we will be reading from.
            var responseStream = new MemoryStream(m_responsePayload);
            var responseReader = new BinaryReader(responseStream, Encoding.Unicode);

            // Check the response length.
            if (responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("Get Menu Item");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the item count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the Package Item array.
                MenuItems.Clear();

                // Get all the Items
                for (ushort x = 0; x < itemCount; x++)
                {
                    var menuItem = new POSMenuItem {MenuId = responseReader.ReadInt32()};

                    // Menu Name
                    var stringLen = responseReader.ReadUInt16();
                    menuItem.MenuName = new string(responseReader.ReadChars(stringLen));

                    // Menu Type Id
                    menuItem.MenuTypeId = responseReader.ReadInt32();

                    MenuItems.Add(menuItem);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Menu Item", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Menu Item", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
