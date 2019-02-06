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
        private CompAwardType m_AwardTypeID;
        private int m_compID;
        private int m_playerID;
        private int m_groupID;

        private SetCompAwardedToPlayer() : base()
        {
            m_id = 18213;
        }
        
        public enum CompAwardType
        {
            Player = 1,
            Group = 2
        }

        public static void SetCompAwardToPlayer(int compID, int playerID)
        {
            var m = new SetCompAwardedToPlayer()
            {
                m_AwardTypeID = CompAwardType.Player,
                m_compID = compID,
                m_playerID = playerID,
            };

            try
            {
                m.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("Set coupon to a player: " + ex.Message);
            }
        }

        public static void SetCompAwardToGroup(int compID, int groupId)
        {
            var m = new SetCompAwardedToPlayer()
            {
                m_AwardTypeID = CompAwardType.Group,
                m_compID = compID,
                m_groupID = groupId
            };

            try
            {
                m.Send();
            }
            catch(ServerCommException ex)
            {
                throw new Exception("Set coupon to a group: " + ex.Message);
            }
        }
    
        protected override void PackRequest()
        {
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            switch(m_AwardTypeID)
            {

                case CompAwardType.Player:
                    requestWriter.Write((byte)m_AwardTypeID);
                    requestWriter.Write(m_compID);
                    requestWriter.Write(m_playerID);
                    break;
                case CompAwardType.Group:
                    requestWriter.Write((byte)m_AwardTypeID);
                    requestWriter.Write(m_compID);
                    requestWriter.Write(m_groupID);
                    break;
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
