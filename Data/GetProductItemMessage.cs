// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.
//
// US2826 Adding support for barcoded paper

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    // START RALLY TA 5744
    public struct PaperLayout
    {
        public int PaperLayoutId { get; set; }
        public string PaperLayoutName { get; set; }
    }
    //END RALLY TA 5744
    /// <summary>
    /// Represents a Product Type returned from the server.
    /// </summary>
    public struct ProductItemList
    {
        public int ProductItemId;
        public int ProductTypeId;
        public int SalesSourceId;
        public int ProductGroupId;
        public bool IsActive;
        public string ProductItemName;
        public string ProductTypeName;
        public string ProductSalesSourceName;
        public string ProductGroupName;
        //START RALLY TA 5744
        public int PaperLayoutId { get; set; }
        public string PaperLayoutName { get; set; }
        public List<Accrual> AccuralList { get; set; } //RALLY US 1796
        //END RALLY TA 5744
        public int PaperLayoutCount { get; set; }

        // US2826
        public bool BarcodedPaper { get; set; }

        //US4059 Adding perm file
        public int PermFileId { get; set; }
        public bool Validate { get; set; }
    }

    /// <summary>
    /// Represents a Get Product Item message.
    /// </summary>
    internal class GetProductItemMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        #region Member Properties
        public int OperatorId { get; set; }
        public List<ProductItemList> ProductItems { get; protected set; }
        public ProductItemList[] ProductArray
        {
            get { return ProductItems.ToArray(); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetProductItemMessage class.
        /// </summary>
        public GetProductItemMessage()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetProductItemMessage class.
        /// </summary>
        /// <param name="operatorId"></param>
        public GetProductItemMessage(int operatorId)
        {
            m_id = 18075;
            OperatorId = operatorId;
            ProductItems = new List<ProductItemList>();
        }
        #endregion
        
        #region Member Methods
        public static List<ProductItemList> GetProductItems(int operatorId)
        {
            var msg = new GetProductItemMessage(operatorId);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetProductItemMessage: " + ex.Message);
            }
            return msg.ProductItems;
        }

        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // FIX : Per Dan 6/22 email (operator removed)
            // Operator Id
            //requestWriter.Write(OperatorId);

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
                throw new MessageWrongSizeException("Get Product Item");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count of Product Types.
                var productItemCount = responseReader.ReadUInt16();

                // Clear the Product Item array.
                ProductItems.Clear();

                // Get all the Product Items
                for (ushort x = 0; x < productItemCount; x++)
                {
                    var productItem = new ProductItemList
                                      {
                                          ProductItemId = responseReader.ReadInt32(),
                                          ProductTypeId = responseReader.ReadInt32(),
                                          SalesSourceId = responseReader.ReadInt32(),
                                          IsActive = responseReader.ReadBoolean()
                                      };

                    // Product Item Name
                    var stringLen = responseReader.ReadUInt16();
                    productItem.ProductItemName = new string (responseReader.ReadChars(stringLen));

                    // Product Type Name
                    stringLen = responseReader.ReadUInt16();
                    productItem.ProductTypeName = new string(responseReader.ReadChars(stringLen));

                    // Product Sales Source Name
                    stringLen = responseReader.ReadUInt16();
                    productItem.ProductSalesSourceName = new string(responseReader.ReadChars(stringLen));

                    // Product Group Id
                    productItem.ProductGroupId = responseReader.ReadInt32();

                    // Product Group Name
                    stringLen = responseReader.ReadUInt16();
                    productItem.ProductGroupName = new string(responseReader.ReadChars(stringLen));

                    //START RALLY TA 5744
                    // Paper Layout Id
                    productItem.PaperLayoutId = responseReader.ReadInt32();

                    // Paper Layout Name
                    stringLen = responseReader.ReadUInt16();
                    productItem.PaperLayoutName = new string(responseReader.ReadChars(stringLen));

                    // Paper Layout Count
                    productItem.PaperLayoutCount = responseReader.ReadInt32();
                    //END RALLY TA 5744

                    //START RALLY US1796
                    List<Accrual> accuralList = new List<Accrual>();
                    ushort accuralCount = responseReader.ReadUInt16();
                    for (ushort i = 0; i < accuralCount; i++)
                    {
                        Accrual accural = new Accrual();
                        accural.Id = responseReader.ReadInt32();
                        stringLen = responseReader.ReadUInt16();
                        accural.Name = new string(responseReader.ReadChars(stringLen));
                        accuralList.Add(accural);
                    }
                    productItem.AccuralList = accuralList;

                    // US2826
                    productItem.BarcodedPaper = responseReader.ReadBoolean();

                    //US4059 Perm File
                    productItem.PermFileId = responseReader.ReadInt32();

                    productItem.Validate = responseReader.ReadBoolean();

                    //END RALLY US1796
                    ProductItems.Add(productItem);

                   
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Product Item", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Product Item", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion
    }
}
