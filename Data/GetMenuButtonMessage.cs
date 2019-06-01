// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    /// <summary>
    /// Represents a Menu Button item returned from the server.
    /// </summary>
    internal struct MenuButtonList
    {
        public int PackageId;
        public int FunctionId;
        public int DiscountId;
        public byte PageNumber;
        public byte KeyNum;
        public string KeyText;
        public int KeyColor;
        public bool KeyLocked;
        public bool PlayerRequired;
        public bool Upsell;
        public string ReceiptText;
        public int DiscountTypeId;
        public decimal DiscountAmount;
        public decimal DiscountPointsPerDollar;
        public byte RemoveButton;
        public int ButtonGraphicId;
        public bool DefaultValidation;
        public bool RequiresAuthorization;

        /// <summary>
        /// The device list this button is valid for
        /// </summary>
        public List<Device> ValidDevices;
    }

    internal class GetMenuButtonMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion Constants and Data Types

        #region Member Variables
        public int MenuId { get; set; }
        public ushort MenuPage { get; set; }
        public List<MenuButtonList> MenuButtonItems { get; set; }
        public int TotalPages { get; set; }
        #endregion Member Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetMenuButtonMessage class.
        /// </summary>
        public GetMenuButtonMessage()
            : this(0,0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetMenuButtonMessage class.
        /// </summary>
        public GetMenuButtonMessage(int menuId, ushort menuPage)
        {
            TotalPages = 0;
            m_id = 18103;
            MenuId = menuId;
            MenuPage = menuPage;
            MenuButtonItems = new List<MenuButtonList>();
        }
        #endregion Constructors

        #region Member Methods
        public static List<MenuButtonList> GetButtons(int menuId, ushort menuPage, out int totalPages)
        {
            var msg = new GetMenuButtonMessage(menuId, menuPage);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetMenuButtonMessage: " + ex.Message);
            }
            totalPages = msg.TotalPages;
            return msg.MenuButtonItems;
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Menu Id
            requestWriter.Write(MenuId);

            // Menu Page
            requestWriter.Write(MenuPage);

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
                throw new MessageWrongSizeException("Get Menu Button");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the button count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the item array.
                MenuButtonItems.Clear();

                // Get all the Items
                for (ushort x = 0; x < itemCount; x++)
                {
                    var menuItem = new MenuButtonList
                                   {
                                       PackageId = responseReader.ReadInt32(),
                                       FunctionId = responseReader.ReadInt32(),
                                       DiscountId = responseReader.ReadInt32(),
                                       PageNumber = responseReader.ReadByte()
                                   };

                    // Set the total number of pages
                    if (menuItem.PageNumber > TotalPages)
                        TotalPages = menuItem.PageNumber;

                    // Key Num
                    menuItem.KeyNum = responseReader.ReadByte();

                    // Key Text
                    var stringLen = responseReader.ReadUInt16();
                    menuItem.KeyText = new string(responseReader.ReadChars(stringLen));

                    // Key Color
                    menuItem.KeyColor = responseReader.ReadInt32();

                    // Key Locked
                    menuItem.KeyLocked = responseReader.ReadBoolean();

                    // Player Required
                    menuItem.PlayerRequired = responseReader.ReadBoolean();

                    // Upsell
                    menuItem.Upsell = responseReader.ReadBoolean();

                    // Receipt Text
                    stringLen = responseReader.ReadUInt16();
                    menuItem.ReceiptText = new string(responseReader.ReadChars(stringLen));

                    // Discount Type Id
                    menuItem.DiscountTypeId = responseReader.ReadInt32();

                    // Discount Amount
                    menuItem.DiscountAmount = 0M;
                    stringLen = responseReader.ReadUInt16();
                    var tempDec = new string(responseReader.ReadChars(stringLen));

                    if (!string.IsNullOrEmpty(tempDec))
                        menuItem.DiscountAmount = decimal.Parse(tempDec, CultureInfo.InvariantCulture);

                    // Discount Points Per Dollar.
                    menuItem.DiscountPointsPerDollar = 0M;
                    stringLen = responseReader.ReadUInt16();
                    tempDec = new string(responseReader.ReadChars(stringLen));

                    if (!string.IsNullOrEmpty(tempDec))
                        menuItem.DiscountPointsPerDollar = decimal.Parse(tempDec, CultureInfo.InvariantCulture);

                    // Button Graphic Id (was Key Color)
                    menuItem.ButtonGraphicId = responseReader.ReadInt32();

                    // Use as default validation
                    menuItem.DefaultValidation = responseReader.ReadBoolean();

                    menuItem.RequiresAuthorization = responseReader.ReadBoolean();

                    // Remove Button
                    menuItem.RemoveButton = 0;

                    // Menu Button Devices US4756
                    menuItem.ValidDevices = new List<Device>();
                    ushort deviceCount = responseReader.ReadUInt16();
                    for (int i = 0; i < deviceCount; i++)
                    {
                        menuItem.ValidDevices.Add(Device.FromId(responseReader.ReadInt32()));
                    }

                    MenuButtonItems.Add(menuItem);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Menu Button", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Menu Button", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion Member Methods
    }
}
