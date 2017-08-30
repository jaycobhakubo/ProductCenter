using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data.TempSQL;
using System.Globalization;//CultureInfo


namespace GTI.Modules.ProductCenter.Data
{
    class SetDeleteCompMessage : ServerMessage
    {
        
       private int m_compID;//0 new comp
        private int m_Status;

        public SetDeleteCompMessage(int compID)
        {
            m_id = 18215;
            m_compID = compID;
        }


        public static int RunMessage(int compID)
        {
            SetDeleteCompMessage msg = new SetDeleteCompMessage(compID);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("Set delete comp definitions: " + ex.Message);
            }

            return msg.m_Status;
        }

        protected override void PackRequest()
        {
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            requestWriter.Write(m_compID); 
         
            m_requestPayload = requestStream.ToArray();
            requestWriter.Close();

        }

        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            MemoryStream responseStream = new MemoryStream(m_responsePayload);
            BinaryReader responseReader = new BinaryReader(responseStream, Encoding.Unicode);


            try
            {
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                if (ReturnCode == 0)
                {
                    m_Status = responseReader.ReadInt32(); 
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set delete comp definitions: ", e);
            }
            catch (Exception e)
            {
                throw new ServerException("set delete comp definitions: ", e);
            } 

            responseReader.Close();


        }


    }
}
