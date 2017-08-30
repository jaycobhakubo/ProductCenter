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
    internal struct FunctionList
    {
        public int FunctionId;
        public string FunctionName;
    }

    /// <summary>
    /// Represents a Get Function Message.
    /// </summary>
    internal class GetFunctionMessage : ServerMessage
    {
        #region Constants and Data Types
        protected const int MinResponseMessageLength = 6;
        #endregion

        public int FunctionId { get; set; }
        public List<FunctionList> FunctionItems { get; protected set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the GetFunctionMessage class.
        /// </summary>
        public GetFunctionMessage()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GetFunctionMessage class.
        /// </summary>
        /// <param name="functionId"></param>
        public GetFunctionMessage(int functionId)
        {
            m_id = 18113;
            FunctionId = functionId;
            FunctionItems = new List<FunctionList>();
        }
        #endregion

        #region Member Methods
        public static List<FunctionList> GetFunctionList(int functionId)
        {
            var msg = new GetFunctionMessage(functionId);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetFunctionMessage: " + ex.Message);
            }
            return msg.FunctionItems;
        }
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Function Id
            requestWriter.Write(FunctionId);

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
                throw new MessageWrongSizeException("Get Function");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the count.
                var itemCount = responseReader.ReadUInt16();

                // Clear the Item array.
                FunctionItems.Clear();

                // Get all the Items
                for (ushort x = 0; x < itemCount; x++)
                {
                    var function = new FunctionList {FunctionId = responseReader.ReadInt32()};

                    // Function Name
                    var stringLen = responseReader.ReadUInt16();
                    function.FunctionName = new string(responseReader.ReadChars(stringLen));

                    FunctionItems.Add(function);
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Function", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Function", e);
            }

            // Close the streams.
            responseReader.Close();
        }
        #endregion
    }
}
