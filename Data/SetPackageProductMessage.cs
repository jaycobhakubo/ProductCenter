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
            catch(ServerCommException ex)
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
            foreach(var packageProduct in PackageProductList)
            {
                // Product Id
                requestWriter.Write(packageProduct.ProductId);

                // Game Type Id
                requestWriter.Write(packageProduct.GameTypeId);

                // Card Level Id
                requestWriter.Write(packageProduct.CardLevelId);

                // Card Media Id
                requestWriter.Write(packageProduct.CardMediaId);

                // Card Type Id
                requestWriter.Write(packageProduct.CardTypeId);

                // Game Caegory Id
                requestWriter.Write(packageProduct.GameCategoryId);

                // Is Taxed
                requestWriter.Write(packageProduct.IsTaxed);

                // Quantity
                requestWriter.Write(packageProduct.Quantity);

                // Card Count
                requestWriter.Write(packageProduct.CardCount);

                // Price
                WriteString(requestWriter, packageProduct.Price);

                // Points Per Quantity
                WriteString(requestWriter, packageProduct.PointsPerQuantity);

                // Points Per Dollar
                WriteString(requestWriter, packageProduct.PointsPerDollar);

                // Points To Redeem
                WriteString(requestWriter, packageProduct.PointsToRedeem);

                // Numbers Required (CBB)
                requestWriter.Write(packageProduct.NumbersRequired);

                // Alt Price
                WriteString(requestWriter, packageProduct.AltPrice);

                // Is Qualifying
                requestWriter.Write((bool)packageProduct.CountsTowardsQualifyingSpend);

                // Prepaid
                requestWriter.Write((bool)packageProduct.Prepaid);

                if(packageProduct.CardTypeId == (int)CardType.Star)
                {
                    if(packageProduct.CardPositionsMapId == 0)
                        requestWriter.Write((int)1);
                    else
                        requestWriter.Write(packageProduct.CardPositionsMapId);

                    if(packageProduct.PositionStarCodes == null || packageProduct.PositionStarCodes.Count == 0)
                    {
                        requestWriter.Write((byte)1);
                        requestWriter.Write((byte)0);
                        requestWriter.Write((byte)1);
                    }
                    else
                    {
                        requestWriter.Write((byte)packageProduct.PositionStarCodes.Count);
                        foreach(var kvp in packageProduct.PositionStarCodes)
                        {
                            requestWriter.Write(kvp.Key);
                            requestWriter.Write(kvp.Value);
                        }
                    }
                }
            }

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }
        #endregion

    }
}
