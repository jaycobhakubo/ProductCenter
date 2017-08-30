﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    class GetPlayerByMagCardMessage : ServerMessage
    {
        private string m_magcardnumber;
        private PlayerName flname = new PlayerName();
        //private string m_lname;

        public GetPlayerByMagCardMessage(string magcardnumber)
        {
            m_id = 8012;
            m_magcardnumber = magcardnumber;
        }


        public static PlayerName RunMessage(string magcardnumber)
        {
            GetPlayerByMagCardMessage msg = new GetPlayerByMagCardMessage(magcardnumber);
            try
            {
                msg.Send();
            }
            catch(ServerCommException ex)
            {
                throw new Exception("Get player by mag card: " + ex.Message); 
            }

            return msg.flname;


        }


        protected override void PackRequest()
        {
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);
        
            requestWriter.Write((ushort)m_magcardnumber.Length);
            requestWriter.Write(m_magcardnumber.ToCharArray());

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
                    PlayerName data = new PlayerName();

                    data.PlayerID =  responseReader.ReadInt32(); //PlayerID
                    responseReader.ReadBoolean();//Pin Required

                    // Third party player card PIN error
                    responseReader.ReadBoolean();

                    // Third party player points correct
                    responseReader.ReadBoolean();

                    // Third party interface down
                    responseReader.ReadBoolean();
                    
                    ushort stringLen = responseReader.ReadUInt16();
                    data.Fname = new string(responseReader.ReadChars(stringLen));

                    stringLen = responseReader.ReadUInt16();//MName len
                    responseReader.ReadChars(stringLen);//Mname

                    stringLen = responseReader.ReadUInt16();
                    data.Lname = new string(responseReader.ReadChars(stringLen));

                    stringLen = responseReader.ReadUInt16();//Gender len
                    responseReader.ReadChars(stringLen);//Gender

                    flname = data;
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set comp definitions: ", e);
            }
            catch (Exception e)
            {
                throw new ServerException("set comp definitions: ", e);
            } 

            responseReader.Close();


        }


    }





    public struct PlayerName
    {
        public int PlayerID;
        public string Fname;
        public string Lname;
    }
}