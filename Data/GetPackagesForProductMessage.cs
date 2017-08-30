using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    public class PackageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GetPackagesForProductMessage : ServerMessage
    {
        protected const int MinResponseMessageLength = 6;
        public int ProductId { protected get; set; }
        public List<PackageInfo> Packages { get; protected set; }

        public GetPackagesForProductMessage()
        {
            m_id = 18164;
            Packages = new List<PackageInfo>();
        }

        public static List<PackageInfo> GetList(int productId)
        {
            GetPackagesForProductMessage msg = new GetPackagesForProductMessage { ProductId = productId };
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetPackagesForProductMessage: " + ex.Message);
            }
            return msg.Packages;
        }

        protected override void PackRequest()
        {
            try
            {
                // Create the streams we will be writing to.
                var requestStream = new MemoryStream();
                var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

                // ProductId
                requestWriter.Write(ProductId);

                // Set the bytes to be sent.
                m_requestPayload = requestStream.ToArray();

                // Close the streams.
                requestWriter.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("GetPackagesForProductMessage.PackRequest()...Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Parses the response received from the server.
        /// </summary>
        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            // Create the streams we will be reading from.
            var responseStream = new MemoryStream(m_responsePayload);
            var responseReader = new BinaryReader(responseStream, Encoding.Unicode);

            // Check the response length.
            if (responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("GetPackagesForProduct");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the array.
                Packages.Clear();

                // Get all the items.
                for (ushort x = 0; x < itemCount; x++)
                {
                    PackageInfo packageInfo = new PackageInfo { Id = responseReader.ReadInt32() };

                    // Package Name
                    var stringLen = responseReader.ReadUInt16();
                    packageInfo.Name = new string(responseReader.ReadChars(stringLen));

                    Packages.Add(packageInfo);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("GetPackagesForProduct", e);
            }
            catch (Exception e)
            {
                throw new ServerException("GetPackagesForProduct", e);
            }

            // Close the streams.
            responseReader.Close();
        }

    }
}
