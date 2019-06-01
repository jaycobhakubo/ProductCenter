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
    /// Represents a Package Product List returned from the server.
    /// </summary>
    public struct PackageProduct : IEquatable<PackageProduct>
    {
        public int ProductId;
        public string ProductName;
        public int ProductTypeId;
        public string ProductTypeName;
        public int SalesSourceId;
        public string SalesSourceName;
        public int CardCutId;
        public string CardCutName;
        public int GameTypeId;
        public string GameTypeName;
        public int CardLevelId;
        public string CardLevelName;
        public int CardMediaId;
        public string CardMediaName;
        public int CardTypeId;
        public string CardTypeName;
        public int ProgramCBBGameId;
        public int ProgramGameId;
        public string ProgramGameName;
        public int GameCategoryId;
        public string GameCategoryName;
        public byte Quantity;
        public bool IsTaxed;
        public ushort CardCount;
        public string Price;
        public string AltPrice; // US4543 used for coupons
        public bool CountsTowardsQualifyingSpend; // US4587
        public bool Prepaid;
        public int CardPositionsMapId;
        public SortedList<byte, byte> m_positionStarCodes;
        public string PointsPerQuantity;
        public string PointsPerDollar;
        public string PointsToRedeem;
        public ushort NumbersRequired; //ljv added
        public List<Accrual> AccrualList { get; set; }//RALLY US1796
        public bool Equals(PackageProduct other)
        {
            var isSame = (ProductId.Equals(other.ProductId) && ProductName.Equals(other.ProductName)
                          && ProductTypeId.Equals(other.ProductTypeId) && ProductTypeName.Equals(other.ProductTypeName)
                          && SalesSourceId.Equals(other.SalesSourceId) && SalesSourceName.Equals(other.SalesSourceName)
                          && GameTypeId.Equals(other.GameTypeId) && GameTypeName.Equals(other.GameTypeName)
                          && CardLevelId.Equals(other.CardLevelId) && CardLevelName.Equals(other.CardLevelName)
                          && CardMediaId.Equals(other.CardMediaId) && CardMediaName.Equals(other.CardMediaName)
                          && CardTypeId.Equals(other.CardTypeId) && CardTypeName.Equals(other.CardTypeName)
                          && ProgramCBBGameId.Equals(other.ProgramCBBGameId)
                          && ProgramGameId.Equals(other.ProgramGameId) && ProgramGameName.Equals(other.ProgramGameName)
                          && GameCategoryId.Equals(other.GameCategoryId)
                          && GameCategoryName.Equals(other.GameCategoryName) && Quantity.Equals(other.Quantity)
                          && IsTaxed.Equals(other.IsTaxed) && CardCount.Equals(other.CardCount)
                          && PointsPerQuantity.Equals(other.PointsPerQuantity)
                          && PointsPerDollar.Equals(other.PointsPerDollar)
                          && PointsToRedeem.Equals(other.PointsToRedeem)
                          && NumbersRequired.Equals(other.NumbersRequired)
                          && CountsTowardsQualifyingSpend == other.CountsTowardsQualifyingSpend
                          && Prepaid == other.Prepaid
                          && CardPositionsMapId == other.CardPositionsMapId
                          && (m_positionStarCodes == null ? 0 : m_positionStarCodes.Count) == (other.m_positionStarCodes == null ? 0 : other.m_positionStarCodes.Count)
                          );

            if(isSame) // short-circuit if the two are already not equal
            {
                var p1 = decimal.Parse(Price ?? "0");
                var p2 = decimal.Parse(other.Price ?? "0");
                isSame &= p1.Equals(p2);

                var ap1 = decimal.Parse(AltPrice ?? "0");
                var ap2 = decimal.Parse(other.AltPrice ?? "0");
                isSame &= ap1.Equals(ap2);

                if(isSame)
                {
                    foreach(var kvp in m_positionStarCodes)
                        if(!other.m_positionStarCodes.ContainsKey(kvp.Key) || other.m_positionStarCodes[kvp.Key] != kvp.Value)
                        {
                            isSame = false;
                            break;
                        }
                }
            }
            return isSame;
        }
    }

    /// <summary>
    /// Represents a Get Package Product Message
    /// </summary>
    internal class GetPackageProductMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Member Variables
        #endregion

        #region member properties
        public int OperatorId { get; set; }
        public int PackageId { get; set; }
        public bool ChargeDeviceFee { get; set; }
        public string PackageName { get; set; }
        public string ReceiptText { get; set; }
        public string PackagePrice { get; set; }
        public List<PackageProduct> PackageProducts { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetPackageProductMessage class.
        /// </summary>
        public GetPackageProductMessage()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetPackageProductMessage class.
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="operatorId"></param>
        public GetPackageProductMessage(int packageId, int operatorId)
        {
            ChargeDeviceFee = false;
            PackageName = "";
            ReceiptText = "";
            PackagePrice = "";
            m_id = 18081;
            PackageId = packageId;
            OperatorId = operatorId;
            PackageProducts = new List<PackageProduct>();
        }
        #endregion

        #region Member Methods
        public static List<PackageProduct> GetPackageProducts(int packageId, int operatorId)
        {
            var msg = new GetPackageProductMessage(packageId, operatorId);
            try
            {
                msg.Send();
            }
            catch(ServerCommException ex)
            {
                throw new Exception("GetPackageProductMessage: " + ex.Message);
            }
            return msg.PackageProducts;
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Package Id
            requestWriter.Write(PackageId);

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
            if(responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("Get Package Product Item");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Package Id
                PackageId = responseReader.ReadInt32();

                // Is ChargeDeviceFee
                ChargeDeviceFee = responseReader.ReadBoolean();

                // Package Name
                PackageName = ReadString(responseReader) ?? string.Empty; // UI doesn't handle nulls well

                // Receipt Text
                ReceiptText = ReadString(responseReader) ?? string.Empty;

                // Package Price
                PackagePrice = ReadString(responseReader) ?? string.Empty;

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the Item array.
                PackageProducts.Clear();

                // Get all the Items
                for(ushort x = 0; x < itemCount; x++)
                {
                    var packageProduct = new PackageProduct { ProductId = responseReader.ReadInt32() };

                    // Product Name
                    packageProduct.ProductName = ReadString(responseReader) ?? string.Empty;

                    // Product Type Id
                    packageProduct.ProductTypeId = responseReader.ReadInt32();

                    // Product Type Name
                    packageProduct.ProductTypeName = ReadString(responseReader) ?? string.Empty;

                    // Sales Source Id
                    packageProduct.SalesSourceId = responseReader.ReadInt32();

                    // Sales Source Name
                    packageProduct.SalesSourceName = ReadString(responseReader) ?? string.Empty;

                    // Game Type Id
                    packageProduct.GameTypeId = responseReader.ReadInt32();

                    // Game Type Name
                    packageProduct.GameTypeName = ReadString(responseReader) ?? string.Empty;

                    // Card Level Id
                    packageProduct.CardLevelId = responseReader.ReadInt32();

                    // Card Level Name
                    packageProduct.CardLevelName = ReadString(responseReader) ?? string.Empty;

                    // Card Media Id
                    packageProduct.CardMediaId = responseReader.ReadInt32();

                    // Card Media Name
                    packageProduct.CardMediaName = ReadString(responseReader) ?? string.Empty;

                    // Card Type Id
                    packageProduct.CardTypeId = responseReader.ReadInt32();

                    // Card Type Name
                    packageProduct.CardTypeName = ReadString(responseReader) ?? string.Empty;

                    // Game Category Id
                    packageProduct.GameCategoryId = responseReader.ReadInt32();

                    // Game Category Name
                    packageProduct.GameCategoryName = ReadString(responseReader) ?? string.Empty;

                    // Quantity
                    packageProduct.Quantity = responseReader.ReadByte();

                    // Is taxed
                    packageProduct.IsTaxed = responseReader.ReadBoolean();

                    // Card Count
                    packageProduct.CardCount = responseReader.ReadUInt16();

                    // Price
                    packageProduct.Price = ReadString(responseReader) ?? string.Empty;

                    // Points Per Quantity
                    packageProduct.PointsPerQuantity = ReadString(responseReader) ?? string.Empty;

                    // Points Per Dollar
                    packageProduct.PointsPerDollar = ReadString(responseReader) ?? string.Empty;

                    // Points To Redeem
                    packageProduct.PointsToRedeem = ReadString(responseReader) ?? string.Empty;

                    // Numbers Required
                    packageProduct.NumbersRequired = responseReader.ReadUInt16();           // ljv added

                    // Alternate Price
                    packageProduct.AltPrice = ReadString(responseReader) ?? string.Empty;

                    // Is Qualifying
                    packageProduct.CountsTowardsQualifyingSpend = responseReader.ReadBoolean();

                    // Prepaid
                    packageProduct.Prepaid = responseReader.ReadBoolean();

                    packageProduct.m_positionStarCodes = new SortedList<byte, byte>();
                    if(packageProduct.CardTypeId == (int)CardType.Star)
                    {
                        packageProduct.CardPositionsMapId = responseReader.ReadInt32();
                        var starCount = responseReader.ReadByte();
                        for(int s = 0; s < starCount; ++s)
                        {
                            var positionIndex = responseReader.ReadByte();
                            var starCode = responseReader.ReadByte();
                            packageProduct.m_positionStarCodes.Add(positionIndex, starCode);
                        }
                    }

                    //START RALLY US1796
                    packageProduct.AccrualList = new List<Accrual>();
                    ushort accrualCount = responseReader.ReadUInt16();
                    for(int i = 0; i < accrualCount; i++)
                    {
                        Accrual accrual = new Accrual();
                        accrual.Id = responseReader.ReadInt32();
                        accrual.Name = ReadString(responseReader) ?? string.Empty;
                        packageProduct.AccrualList.Add(accrual);
                    }
                    //END RALLY US1796
                    PackageProducts.Add(packageProduct);
                }
            }
            catch(EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Package Product Item", e);
            }
            catch(Exception e)
            {
                throw new ServerException("Get Package Product Item", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion
    }
}
