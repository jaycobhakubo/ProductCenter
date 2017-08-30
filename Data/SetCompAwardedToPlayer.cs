using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTI.Modules.Shared;
using System.IO;

namespace GTI.Modules.ProductCenter.Data
{
    class SetCompAwardedToPlayer : ServerMessage
    {
        private int m_compID;
        private int m_playerID;
        private int m_maxUsage;
        private int m_AwardTypeID;
        private int m_DefID;


        public int AwardTypeID
        {
            get { return m_AwardTypeID; }
            set { m_AwardTypeID = value; }
        }


        public int DefID
        {
            get { return m_DefID; }
            set { m_DefID = value; }
        }

        public void set(int compID, int playerID, int maxUsage)
        {
            m_id = 18213;
            m_compID = compID;
            m_playerID = playerID;
            m_maxUsage = maxUsage;

            try
            {
                Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("Set coupon to a player: " + ex.Message);
            }
        }
    
        protected override void PackRequest()
        {
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            if (m_AwardTypeID == 1)//Single player or all players.
            {
                requestWriter.Write((byte)1);
                requestWriter.Write(m_compID);
                requestWriter.Write(m_playerID);
                if (m_maxUsage != 0)
                {
                    requestWriter.Write((ushort)m_maxUsage);
                }
                else
                {
                    requestWriter.Write((ushort)0);
                }

            }
            else if (m_AwardTypeID == 2)//Award to multi Player or group players.
            {
                requestWriter.Write((byte)2);
                requestWriter.Write(m_compID);
                requestWriter.Write(m_DefID);
            }

            m_requestPayload = requestStream.ToArray();
            requestWriter.Close();

        }

        //no return 

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
                    int PlayerCount = responseReader.ReadInt32(); //Will use for future reference. This is the number of players that being awarded.
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set comp to a player: ", e);
            }
            catch (Exception e)
            {
                throw new ServerException("set comp to a player: ", e);
            }

            responseReader.Close();


        }


    }
}
