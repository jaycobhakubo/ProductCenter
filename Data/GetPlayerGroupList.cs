using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using GTI.Modules.ProductCenter.UI;
using System.IO;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Data
{
    class GetPlayerGroupList : ServerMessage
    {
        private List<PlayerListDefinition> List_pld = new List<PlayerListDefinition>();



        public List<PlayerListDefinition> GetPlayerListDefinitionMSG()
        {
            m_id = 8038;
            try
            {
                Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("GetPlayerListDetail: " + ex.Message);
            }

            return List_pld;
        }

        protected override void PackRequest()
        {
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            requestWriter.Write(0); //Get Everything

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
                ushort ListDefinitionCount = responseReader.ReadUInt16();// 2bytes

                if (ReturnCode == 0)
                {
                    for (int i = 0; i < ListDefinitionCount; i++)
                    {

                        PlayerListDefinition pld = new PlayerListDefinition();

                        pld.DefId = responseReader.ReadInt32();
                        ushort stringLen = responseReader.ReadUInt16();
                        pld.DefinitionName = new string(responseReader.ReadChars(stringLen));

                        ushort ListDetailCount = responseReader.ReadUInt16();
                        PlayerListSetting pls = new PlayerListSetting();
                        for (int ii = 0; ii < ListDetailCount; ii++)
                        {

                            pls.SettingID = responseReader.ReadInt32();
                            stringLen = responseReader.ReadUInt16();
                            pls.SettingValue = new string(responseReader.ReadChars(stringLen));
                        }
                        List_pld.Add(pld);

                    }
                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("SetPlayerList:", e);
            }
            catch (Exception e)
            {
                throw new ServerException("SetPlayerList:", e);
            }

            responseReader.Close();


        }


    }
}
