#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2017 Fortunet, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GTI.Modules.Shared;
using System.Globalization;

namespace GTI.Modules.ProductCenter.Data
{
    /// <summary>
    /// Represents a message to get the daily sale menu
    /// </summary>
    public class GetDailyStaffMenusMessage : ServerMessage
    {
        #region Constants And Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Private Members

        private DateTime m_gamingDate;
        private bool m_ignoreDeviceType = true;
        private bool m_getDailyMenuButtonIDs = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the gaming date to get the menus for.
        /// </summary>
        public DateTime GamingDate
        {
            get
            {
                return m_gamingDate;
            }
            set
            {
                m_gamingDate = value;
            }
        }

        /// <summary>
        /// A map of the menus to the buttons inside them (didn't want to change the menu object)
        /// </summary>
        public Dictionary<POSMenuItem, List<DailyMenuButton>> MenuToButtons
        {
            get;
            private set;
        }
        #endregion

        public GetDailyStaffMenusMessage(DateTime gamingDate)
        {
            m_id = 18040; // Get Daily Staff Menus
            m_strMessageName = "Get Daily Staff Menus";
            m_gamingDate = gamingDate;
        }

        #region Member Methods

        /// <summary>
        /// Returns the list of daily staff menus for the sent-in gaming date
        /// </summary>
        /// <param name="gamingDate"></param>
        /// <returns></returns>
        public static Dictionary<POSMenuItem, List<DailyMenuButton>> GetDailyStaffMenus(DateTime gamingDate)
        {
            var msg = new GetDailyStaffMenusMessage(gamingDate);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("Get Daily Staff Menus Message: " + ex.Message);
            }

            return msg.MenuToButtons;
        }

        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            using (var requestStream = new MemoryStream())
            using (var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode))
            {
                // Gaming Date
                WriteString(requestWriter, m_gamingDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));

                //Ignore device type
                requestWriter.Write(m_ignoreDeviceType);

                //Get daily menu button IDs
                requestWriter.Write(m_getDailyMenuButtonIDs);

                // Set the bytes to be sent.
                m_requestPayload = requestStream.ToArray();

                // Close the streams.
                requestWriter.Close();
            }
        }

        /// <summary>
        /// Parses the response received from the server.
        /// </summary>
        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            // Create the streams we will be reading from.
            using (var responseStream = new MemoryStream(m_responsePayload))
            using (var reader = new BinaryReader(responseStream, Encoding.Unicode))
            {
                // Try to unpack the data.
                try
                {
                    // Seek past return code.
                    reader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                    // Parse the menu data.
                    List<POSMenuItem> menus = new List<POSMenuItem>();
                    MenuToButtons = new Dictionary<POSMenuItem, List<DailyMenuButton>>();

                    // Get the count of menus.
                    ushort menuCount = reader.ReadUInt16();

                    // Get all the menus.
                    for (ushort x = 0; x < menuCount; x++)
                    {
                        POSMenuItem menu = new POSMenuItem();
                        menu.IsDailyMenu = true;

                        // POS Menu Id
                        menu.MenuId = reader.ReadInt32();

                        // Menu Name
                        menu.MenuName = ReadString(reader);

                        // Session Number
                        reader.ReadInt16();

                        // Session Played Id
                        reader.ReadInt32();

                        // Program Name
                        ReadString(reader);

                        //IsMaxValidationEnabled
                        reader.ReadBoolean();

                        //IsAutoDiscountsEnabled
                        reader.ReadBoolean();

                        //IsDeviceFeesEnabled
                        reader.ReadBoolean();

                        //PointsMultiplier
                        reader.ReadInt32();

                        //US5287:session Max card limit
                        reader.ReadInt32();

                        if (!menus.Any(i => i.MenuId == menu.MenuId)) // we only need one of each here
                        {
                            MenuToButtons.Add(menu, new List<DailyMenuButton>());
                            menus.Add(menu);
                        }
                    }

                    // Rally TA1045
                    // Get the list of buttons.
                    menuCount = reader.ReadUInt16();

                    // Read all the buttons for a particular menu.
                    for (ushort x = 0; x < menuCount; x++)
                    {
                        // POS Menu Id
                        int menuId = reader.ReadInt32();
                        List<POSMenuItem> menuItems = menus.FindAll(i => i.MenuId == menuId);
                        long menuDefStart = reader.BaseStream.Position;
                        bool haveDefaultValidationPackage = false;

                        foreach (POSMenuItem item in menuItems)
                        {
                            haveDefaultValidationPackage = false;

                            reader.BaseStream.Position = menuDefStart;

                            // Get the count of buttons.
                            ushort buttonCount = reader.ReadUInt16();

                            // Get all the buttons.
                            for (ushort y = 0; y < buttonCount; y++)
                            {
                                DailyMenuButton button = new DailyMenuButton();

                                if (m_getDailyMenuButtonIDs)
                                    button.MenuButtonId = reader.ReadInt32();

                                // Package Id
                                button.PackageId = reader.ReadInt32();

                                // Function Id
                                button.FunctionId = reader.ReadInt32();

                                // Discount Id
                                button.DiscountId = reader.ReadInt32();

                                // Page Number
                                button.PageNumber = reader.ReadByte();

                                // Key Num
                                button.KeyNum = reader.ReadByte();

                                // Key Text
                                button.KeyText = ReadString(reader);

                                // Key Color
                                button.KeyColor = reader.ReadInt32();

                                // Key Locked
                                button.KeyLocked = reader.ReadBoolean();

                                // Player Required
                                button.PlayerRequired = reader.ReadBoolean();

                                // Graphic Id
                                button.ButtonGraphicId = reader.ReadInt32();

                                //Use as default validation package for menu
                                bool defaultValidationPackage = reader.ReadBoolean();
                                bool isValidationPackage = false;

                                // Discount Type Id
                                int discountTypeId = reader.ReadInt32();

                                // Discount Amount
                                decimal tempDec;
                                if (Decimal.TryParse(ReadString(reader), out tempDec))
                                    button.DiscountAmount = tempDec;

                                // Discount Points Per Dollar
                                if (Decimal.TryParse(ReadString(reader), out tempDec))
                                    button.DiscountPointsPerDollar = tempDec;

                                if (button.PackageId != 0)
                                {
                                    // Charge Device Fee
                                    button.ChargeDeviceFee = reader.ReadBoolean();

                                    // Package Receipt Text
                                    button.ReceiptText = ReadString(reader);

                                    //Override Validation
                                    reader.ReadBoolean();

                                    //Validation Quantity
                                    reader.ReadInt32();

                                    //Requires Validation
                                    reader.ReadBoolean();

                                    // Get the products associated to this package.
                                    ushort productCount = reader.ReadUInt16();

                                    // Get all the products.
                                    for (ushort z = 0; z < productCount; z++)
                                    {
                                        DailyProductPackageItem product = new DailyProductPackageItem();

                                        // Product Type
                                        product.ProductTypeId = reader.ReadInt32();

                                        if (product.ProductTypeId == (int)ProductType.Validation)
                                        {
                                            button.IsValidationPackage = true;
                                            isValidationPackage = true;
                                        }

                                        // Daily Product Id
                                        product.DailyProductId = reader.ReadInt32();

                                        // Card Media
                                        product.CardMediaId = reader.ReadInt32();

                                        // Card Type
                                        product.CardTypeId = reader.ReadInt32();

                                        // Game Type
                                        product.GameTypeId = reader.ReadInt32();

                                        // Game Category Id
                                        product.GameCategoryId = reader.ReadInt32();

                                        // Card Level Id
                                        product.CardLevelId = reader.ReadInt32();

                                        // Product Name
                                        product.ProductName = ReadString(reader);

                                        // Is Taxed
                                        product.IsTaxed = reader.ReadBoolean();

                                        // Price
                                        product.Price = ReadString(reader);

                                        // Quantity
                                        product.Quantity = reader.ReadByte();

                                        // Card Count
                                        product.CardCount = reader.ReadUInt16();

                                        // Optional
                                        reader.ReadBoolean();

                                        // Numbers Required
                                        product.NumbersRequired = reader.ReadUInt16();

                                        // Points Per Dollar
                                        product.PointsPerDollar = ReadString(reader);

                                        // Points Per Product
                                        product.PointsPerQuantity = ReadString(reader);

                                        // Points To Redeem
                                        product.PointsToRedeem = ReadString(reader);

                                        // Skip Package Code
                                        ReadString(reader);

                                        //barcoded Paper
                                        product.IsBarcodedPaper = reader.ReadBoolean();

                                        //is validated
                                        product.IsValidated = reader.ReadBoolean();

                                        //get alternative price
                                        product.AltPrice = ReadString(reader);

                                        //get qualifying product flag
                                        product.CountsTowardsQualifyingSpend = reader.ReadBoolean();

                                        //get prepaid flag
                                        reader.ReadBoolean();

                                        // Compatible Devices
                                        reader.ReadInt32();

                                        button.ProductItems.Add(product);
                                    }
                                }

                                if (!haveDefaultValidationPackage && defaultValidationPackage && isValidationPackage)
                                {
                                    button.DefaultValidation = true;
                                    haveDefaultValidationPackage = true;
                                }

                                if(!MenuToButtons[item].Exists(i => i.MenuButtonId == button.MenuButtonId))
                                    MenuToButtons[item].Add(button);
                            }
                        }
                    }
                }
                catch (EndOfStreamException e)
                {
                    throw new MessageWrongSizeException(m_strMessageName, e);
                }
                catch (Exception e)
                {
                    throw new ServerException(m_strMessageName, e);
                }

                // Close the streams.
                reader.Close();
            }
        }
        #endregion
    }
}
