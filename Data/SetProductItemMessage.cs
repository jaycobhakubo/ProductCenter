// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  � 2007 GameTech
// International, Inc.
//
// US2826 Adding support for barcoded paper

using System;
using System.IO;
using System.Text;
using GTI.Modules.Shared;
using System.Collections.Generic;

namespace GTI.Modules.ProductCenter.Data
{
    /// <summary>
    /// Represents a Set Product Item message.
    /// </summary>
    internal class SetProductItemMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public int OperatorId { get; set; }
        public int ProductItemId { get; set; }
        public int ProductTypeId { get; set; }
        public int SalesSourceId { get; set; }
        public int ProductGroupId { get; set; }
        public bool IsActive { get; set; }
        public bool BarcodedPaper { get; set; } // US2826
        public int PermFileId { get; set; } // US4059
        public int CardColorSetId { get; set; }
        public bool Validate { get; set; }
        public string ProductItemName { get; set; }
        public int PaperLayoutId { get; set; } //RALLY TA 5744
        public List<Accrual> AccuralList { get; set; } //RALLY US1796
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SetProductItemMessage class.
        /// </summary>
        public SetProductItemMessage()
            : this(new ProductItemList()) //RALLY TA 5744 RALLY US1796 RALLY US8439
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the SetProductItemMessage class.
        /// </summary>
        public SetProductItemMessage(ProductItemList productItem) //RALLY TA 5744 RALLY US1796
        {
            m_id = 18076; // Set Product Item
            //START RALLY US1796
            ProductItemId = productItem.ProductItemId;
            ProductTypeId = productItem.ProductTypeId;
            SalesSourceId = productItem.SalesSourceId;
            ProductGroupId = productItem.ProductGroupId;
            ProductItemName = productItem.ProductItemName;
            PaperLayoutId = productItem.PaperLayoutId; //RALLY TA 5744
            AccuralList = productItem.AccuralList;//RALLY US1796
            IsActive = productItem.IsActive;
            BarcodedPaper = productItem.BarcodedPaper; //US2826
            PermFileId = productItem.PermFileId;//US4059            
            CardColorSetId = productItem.CardColorSetId;
            Validate = productItem.Validate;
            
            //END RALLY US1796
        }
        #endregion

        #region Member Methods
        public static int Save(ProductItemList productItem) //RALLY TA 5744 RALLY US1796 RALLY DE8439
        {
            //START RALLY TA 5744
            SetProductItemMessage msg = new SetProductItemMessage(productItem); // RALLY US1796 RALLY DE8439
            //END RALLY TA 5744

            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("SetProductItemMessage: " + ex.Message);
            }
            catch (ServerException ex)
            {
                throw ex;
            }
            return msg.ProductItemId;
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // FIX : Per Dan 6/22 email (operator removed)
            // Operator Id
            //requestWriter.Write(OperatorId);

            // Product Item Id
            requestWriter.Write(ProductItemId);

            // Product Type Id
            requestWriter.Write(ProductTypeId);

            // Sales Source Id
            requestWriter.Write(SalesSourceId);

            // Product Item Name
            requestWriter.Write((ushort)ProductItemName.Length);
            requestWriter.Write(ProductItemName.ToCharArray());

            // Product Group Id
            requestWriter.Write(ProductGroupId);

            //START RALLY TA 5744
            // Paper Layout Id
            requestWriter.Write(PaperLayoutId);
            //END RALLY TA 5744
            requestWriter.Write(IsActive); //RALLY DE8439

            //Set the accural list 
            //START RALLY US1796
            requestWriter.Write((ushort)AccuralList.Count);
            foreach (Accrual accural in AccuralList)
            {
                requestWriter.Write(accural.Id);
            }
            //END RALLY US1796

            //US2826
            requestWriter.Write(BarcodedPaper);

            //US4059
            requestWriter.Write(PermFileId);

            requestWriter.Write(CardColorSetId);

            requestWriter.Write(Validate);

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

           
            // Close the streams.
            requestWriter.Close();
        }

        protected override void UnpackResponse()
        {
            // Create the streams we will be reading from.
            var responseStream = new MemoryStream(m_responsePayload);
            var responseReader = new BinaryReader(responseStream, Encoding.Unicode);


            try
            {
            base.UnpackResponse();

            // Check the response length.
            if (responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("Set Product Item");

            // Try to unpack the data.
            
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the Product Item Id.
                ProductItemId = responseReader.ReadInt32();
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set Product Item", e);
            }
            catch (Exception e)
            {
                throw new ServerException(ServerReturnCode,ServerExceptionTranslator.GetServerErrorMessage(ServerReturnCode));
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion

    }
}
