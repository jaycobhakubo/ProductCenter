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

namespace GTI.Modules.ProductCenter.Data
{
    /// US1772
    /// <summary>
    /// The purpose of this message is to allow for the modification of the current menu data without having to send an override.
    /// </summary>
    public class UpdateDailyMenuDataMessage : ServerMessage
    {
        #region Constants And Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Public Properties

        /// <summary>
        /// The menu item to send to the server
        /// </summary>
        public POSMenuItem Menu { get; protected set; }

        /// <summary>
        /// The menu buttons assigned to the menu
        /// </summary>
        public DailyMenuButton MenuButton { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the UpdateDailyMenuDataMessage 
        /// class.
        /// </summary>
        public UpdateDailyMenuDataMessage(POSMenuItem menu, DailyMenuButton button = null)
        {
            m_id = 18236; // Update Daily Menu Data

            Menu = menu;
            if (button == null)
            {
                MenuButton = new DailyMenuButton();
                MenuButton.RemoveButton = true;
            }
            else
                MenuButton = button;
        }

        #endregion

        #region Member Methods
        public static void UpdateDailyMenuData(POSMenuItem menu, DailyMenuButton button)
        {
            var msg = new UpdateDailyMenuDataMessage(menu, button);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("Update Daily Menu Data Message: " + ex.Message);
            }
        }

        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            using (var requestStream = new MemoryStream())
            using (var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode))
            {
                // Menu Id
                requestWriter.Write(Menu.MenuId);

                // Menu Button Id
                requestWriter.Write(MenuButton.MenuButtonId);

                // Remove
                requestWriter.Write(MenuButton.RemoveButton);

                // Discount type Id
                requestWriter.Write(MenuButton.DiscountTypeId);

                // Package Id
                requestWriter.Write(MenuButton.PackageId);

                // Page Number
                requestWriter.Write(MenuButton.PageNumber);

                // Charge Device Fee
                requestWriter.Write(MenuButton.ChargeDeviceFee);

                // Receipt Text
                WriteString(requestWriter, MenuButton.ReceiptText);

                // Key Number
                requestWriter.Write(MenuButton.KeyNum);

                // Key Text
                ServerMessage.WriteString(requestWriter, MenuButton.KeyText);

                // Key Color
                requestWriter.Write(MenuButton.KeyColor);

                // Key Locked
                requestWriter.Write(MenuButton.KeyLocked);

                // Player Required
                requestWriter.Write(MenuButton.PlayerRequired);

                // Function Id
                requestWriter.Write(MenuButton.FunctionId);

                // Discount Id
                requestWriter.Write(MenuButton.DiscountId);

                // Discount Amount
                WriteString(requestWriter, MenuButton.DiscountAmount.ToString());

                // Discount Amount
                WriteString(requestWriter, MenuButton.DiscountPointsPerDollar.ToString());

                // Button Graphic Id
                requestWriter.Write(MenuButton.ButtonGraphicId);

                // Default Validation
                requestWriter.Write(MenuButton.DefaultValidation);

                // Product-Package Count
                requestWriter.Write(MenuButton.ProductItems == null ? (ushort)0 : (ushort)MenuButton.ProductItems.Count);
                foreach (var productItem in MenuButton.ProductItems)
                {
                    //Daily Package Product Id
                    requestWriter.Write(productItem.DailyProductId);

                    //Package Id
                    requestWriter.Write(MenuButton.PackageId);

                    //Product Type Id
                    requestWriter.Write(productItem.ProductTypeId);

                    //Product Item Id (for future use. IE: either send this or the Daily Package Product Id) 
                    requestWriter.Write(productItem.ProductId);

                    //Card Media Id
                    requestWriter.Write(productItem.CardMediaId);

                    //Card Type Id
                    requestWriter.Write(productItem.CardTypeId);

                    //Game Type Id
                    requestWriter.Write(productItem.GameTypeId);

                    //Game Category Id
                    requestWriter.Write(productItem.GameCategoryId);

                    //Item Name
                    WriteString(requestWriter, productItem.ProductName);

                    //Is Taxed
                    requestWriter.Write(productItem.IsTaxed);

                    //Price
                    WriteString(requestWriter, productItem.Price);

                    //Quantity
                    requestWriter.Write(productItem.Quantity);

                    //Card Count
                    requestWriter.Write(productItem.CardCount);

                    //Points Per Dollar
                    WriteString(requestWriter, productItem.PointsPerDollar);

                    //Points per Quantity
                    WriteString(requestWriter, productItem.PointsPerQuantity);

                    //Points to Redeem
                    WriteString(requestWriter, productItem.PointsToRedeem);

                    //Numbers Required
                    requestWriter.Write(productItem.NumbersRequired);

                    //Card Level Id
                    requestWriter.Write(productItem.CardLevelId);
                                        
                    //Barcoded Paper
                    requestWriter.Write(productItem.IsBarcodedPaper);

                    //Validate
                    requestWriter.Write(productItem.IsValidated);

                    //Alt Price
                    WriteString(requestWriter, productItem.AltPrice);

                    //Is Qualifying
                    requestWriter.Write(productItem.CountsTowardsQualifyingSpend);
                }


                // Set the bytes to be sent.
                m_requestPayload = requestStream.ToArray();
            }
        }

        #endregion
    }
}