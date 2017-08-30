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
    /// Represents a Set Package Product Message
    /// </summary>
    internal class SetPackageProductMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public int PackageId { protected get; set; }
        public int OperatorId { protected get; set; }
        public PackageProduct[] PackageProductList { protected get; set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SetPackageProductMessage class.
        /// </summary>
        public SetPackageProductMessage()
            : this(0, 0, null)
        {
        }

        public SetPackageProductMessage(int packageId, int operatorId, PackageProduct[] packageProductList)
        {
            m_id = 18082; // Set Package Product Data
            PackageId = packageId;
            OperatorId = operatorId;
            PackageProductList = packageProductList;
        }
        #endregion

        #region Member Methods

        public static void SetPackageProduct(int packageId, int operatorId, PackageProduct[] packageProductList)
        {
            var msg = new SetPackageProductMessage(packageId, operatorId, packageProductList);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("SetPackageProductMessage: " + ex.Message);
            }
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Package Id
            requestWriter.Write(PackageId);

            // Product Count
            var itemCount = (ushort)PackageProductList.Length;
            requestWriter.Write(itemCount);

            // Package Product List
            foreach (var packageProductList in PackageProductList)
            {
                // Product Id
                requestWriter.Write(packageProductList.ProductId);

                // Game Type Id
                requestWriter.Write(packageProductList.GameTypeId);

                // Card Level Id
                requestWriter.Write(packageProductList.CardLevelId);

                // Card Media Id
                requestWriter.Write(packageProductList.CardMediaId);

                // Card Type Id
                requestWriter.Write(packageProductList.CardTypeId);

                // Game Caegory Id
                requestWriter.Write(packageProductList.GameCategoryId);

                // Is Taxed
                requestWriter.Write(packageProductList.IsTaxed);

                // Quantity
                requestWriter.Write(packageProductList.Quantity);

                // Card Count
                requestWriter.Write(packageProductList.CardCount);

                // Price
                WriteString(requestWriter, packageProductList.Price);

                // Points Per Quantity
                WriteString(requestWriter, packageProductList.PointsPerQuantity);

                // Points Per Dollar
                WriteString(requestWriter, packageProductList.PointsPerDollar);

                // Points To Redeem
                WriteString(requestWriter, packageProductList.PointsToRedeem);

                // Numbers Required (CBB)
                requestWriter.Write(packageProductList.NumbersRequired);

                // Alt Price
                WriteString(requestWriter, packageProductList.AltPrice);

                // Is Qualifying
                requestWriter.Write((bool)packageProductList.CountsTowardsQualifyingSpend);
            }

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();            
        }
        #endregion

    }
}
