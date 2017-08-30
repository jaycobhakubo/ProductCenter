using System;
using System.IO;
using System.Text;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    internal class SetProductGrouptMessage : ServerMessage
    {
        protected const int MinResponseMessageLength = 8;
        public ProductGroupItem ProductGroup { get; set; }

        public SetProductGrouptMessage(ProductGroupItem productGroup)
        {
            m_id = 18167;
            ProductGroup = productGroup;
        }

        public static int Save(ProductGroupItem productGroup)
        {
            SetProductGrouptMessage msg = new SetProductGrouptMessage(productGroup);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("SetProductGrouptMessage: " + ex.Message);
            }
            // FIX : DE3187 handle in use product groups
            catch (ServerException ex)
            {
                if (ex.ReturnCode == GTIServerReturnCode.InUse)
                    return (int)ex.ReturnCode;
                throw new Exception("SetProductGrouptMessage: " + ex.Message);
            }
            // END: DE3187
            return (int)GTIServerReturnCode.Success;
        }

        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // ProductGroup Id
            requestWriter.Write(ProductGroup.ProdGroupId);

            // ProductGroup Name
            requestWriter.Write((ushort)ProductGroup.ProdGroupName.Length);
            requestWriter.Write(ProductGroup.ProdGroupName.ToCharArray());

            // Is Active
            requestWriter.Write(ProductGroup.IsActive);

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
                throw new MessageWrongSizeException("Set ProductGroup Item");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the ProductGroup Id.
                ProductGroup.ProdGroupId = responseReader.ReadInt32();

                // reset the IsModified flag
                ProductGroup.IsModified = false;
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set ProductGroup Item", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Set ProductGroup Item", e);
            }

            // Close the streams.
            responseReader.Close();
        }
    }
}
