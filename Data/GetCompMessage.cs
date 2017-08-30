using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GTI.Modules.Shared;
using System.Globalization;
using GTI.Modules.ProductCenter.Data.TempSQL;

//US4852: Product Center > Coupons: Require spend

namespace GTI.Modules.ProductCenter.Data
{

   public class GetCompMessage : ServerMessage
    {
       private int m_compID { get; set; }
       private List<PlayerComp> m_coupons;

       public GetCompMessage(int compID)
       {
           m_id = 18212;
           m_compID = compID;
       }

       /// <summary>
       /// Returns the list of coupons in the system
       /// </summary>
       /// <param name="compID">the ID of the coupon to look up. If zero, returns all coupons</param>
       /// <returns></returns>
       public static List<PlayerComp> RunMessage(int compID = 0)
       {
           GetCompMessage msg = new GetCompMessage(compID);
           try
           {
               msg.Send();
           }
           catch (ServerCommException ex)
           {
               throw new Exception("Get comp message: " + ex.Message); 
           }

           return msg.m_coupons;
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

               m_coupons = new List<PlayerComp>();
               
               if (ReturnCode == (int)GTIServerReturnCode.Success)//no error
               {
                   ushort CountComp = responseReader.ReadUInt16();
                   for (ushort x = 0; x < CountComp; x++)
                   {
                       PlayerComp compdata = new PlayerComp();
                     
                       // Coupon ID
                       compdata.Id = responseReader.ReadInt32();
                       // Name
                       compdata.Name = ReadString(responseReader);
                       // Start Date
                       compdata.StartDate = DateTime.Parse(ReadString(responseReader), CultureInfo.InvariantCulture);
                       // End Date
                       compdata.EndDate = DateTime.Parse(ReadString(responseReader), CultureInfo.InvariantCulture);
                       // Value
                       compdata.Value = decimal.Parse(ReadString(responseReader), CultureInfo.InvariantCulture);
                       //Max Usage
                       compdata.CouponMaxUsage = responseReader.ReadUInt16();
                       // Last Date awarded.
                       string tempDate = ReadString(responseReader);
                       if(!String.IsNullOrWhiteSpace(tempDate))
                           compdata.LastAwardedDate = DateTime.Parse(tempDate, CultureInfo.InvariantCulture);
                       //Short name
                       compdata.ShortName = ReadString(responseReader);
                       // Comp Type Id
                       compdata.CouponType = (PlayerComp.CouponTypes)responseReader.ReadInt32();
                       // Award Type
                       compdata.AwardType = responseReader.ReadBoolean() ? PlayerComp.AwardTypes.Auto : PlayerComp.AwardTypes.Manual;
                       // Unlock Spend
                       compdata.UnlockSpend = ReadDecimal(responseReader) ?? 0;
                       // Unlock Session Count
                       compdata.UnlockSessionCount = responseReader.ReadInt32();
                       //Minimum spend to qualify //US4852
                       compdata.MinimumSpendToQualify = decimal.Parse(ReadString(responseReader), CultureInfo.InvariantCulture);
                       //count of restricted products //US4852
                       var count = responseReader.ReadInt16();
                       for (var i = 0; i < count; i++)
                       {
                           //restricted product ID //US4852
                           compdata.RestrictedProductIds.Add(responseReader.ReadInt32());
                       }
                       // Count of Qualifying Packages US4941
                       count = responseReader.ReadInt16();
                       for (var i = 0; i < count; i++)
                       {
                           //Qualifying Packages ID
                           compdata.EarnedPackageIDs.Add(responseReader.ReadInt32());
                       }
                       // Count of Package Restrictions US4932
                       count = responseReader.ReadInt16();
                       for (var i = 0; i < count; i++)
                       {
                           //Package Restriction ID
                           compdata.RestrictedPackageIds.Add(responseReader.ReadInt32());
                       }
                       compdata.IgnoreValidationsForIgnoredPackages = responseReader.ReadBoolean();

                       m_coupons.Add(compdata);
                   }
               }
           }
           catch (EndOfStreamException e)
           {
               throw new MessageWrongSizeException("Get Coupons", e);
           }
           catch (Exception e)
           {
               throw new ServerException("Get Coupons", e);
           }

           // Close the streams.
           responseReader.Close();
       }
    }
}
