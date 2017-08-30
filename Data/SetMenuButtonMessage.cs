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
    /// Represents a Set Menu Button Message.
    /// </summary>
    internal class SetMenuButtonMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Member variables
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SetMenuButtonMessage class.
        /// </summary>
        public SetMenuButtonMessage()
            : this(0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SetMenuButtonMessage class.
        /// </summary>
        public SetMenuButtonMessage(int menuId, MenuButtonList[] menuButtonList)
        {
            m_id = 18090; // Set Menu Button Data
            MenuId = menuId;
            MenuButtonList = menuButtonList;
        }
        #endregion

        #region Member Methods
        public static void Save(int menuId, MenuButtonList[] menuButtonList)
        {
            var msg = new SetMenuButtonMessage(menuId, menuButtonList);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("SetMenuButtonMessage: " + ex.Message);
            }
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Menu Id
            requestWriter.Write(MenuId);

            // Button Count
            var itemCount = (ushort)MenuButtonList.Length;
            requestWriter.Write(itemCount);

            // Menu Buttons List
            //foreach (PackageProductList packageProductList in m_packageProductList)
            foreach (var menuButtonList in MenuButtonList)
            {
                // Package Id
                requestWriter.Write(menuButtonList.PackageId);

                // Function Id
                requestWriter.Write(menuButtonList.FunctionId);

                // Discount Id
                requestWriter.Write(menuButtonList.DiscountId);

                // Page Number
                requestWriter.Write(menuButtonList.PageNumber);

                // Key Number
                requestWriter.Write(menuButtonList.KeyNum);

                // Key Text
                requestWriter.Write((ushort)menuButtonList.KeyText.Length);
                requestWriter.Write(menuButtonList.KeyText.ToCharArray());

                // Key Color
                requestWriter.Write(menuButtonList.KeyColor);

                // Key Locked
                requestWriter.Write(menuButtonList.KeyLocked);

                // Player Required
                requestWriter.Write(menuButtonList.PlayerRequired);

                // Remove Button
                requestWriter.Write(menuButtonList.RemoveButton);

                // Button Graphic Id
                requestWriter.Write(menuButtonList.ButtonGraphicId);

                // Menu Button Devices US4756
                requestWriter.Write(menuButtonList.ValidDevices == null ? (ushort)0 : (ushort)menuButtonList.ValidDevices.Count); // if null, just write zero.
                if (menuButtonList.ValidDevices != null)
                {
                    foreach (var device in menuButtonList.ValidDevices)
                        requestWriter.Write((int)device.Id);
                }
            }

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Sets the Menu Id.
        /// </summary>
        public int MenuId { protected get; set; }

        /// <summary>
        /// Sets the Menu Button List.
        /// </summary>
        public MenuButtonList[] MenuButtonList { protected get; set; }
        #endregion
    }
}
