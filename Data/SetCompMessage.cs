using System;
using System.Text;
using System.IO;
using GTI.Modules.Shared;
using System.Globalization;

//US4852: Product Center > Coupons: Require spend

namespace GTI.Modules.ProductCenter
{
    class SetCompMessage : ServerMessage
    {
        private const string DATE_FORMAT = "MM-dd-yyyy hh:mm tt";
        private PlayerComp m_coupon;
        private int retID;

        public SetCompMessage(PlayerComp ci)
        {
            m_id = 18211;
            m_coupon = ci;
        }

        /// <summary>
        /// Saves the sent in coupon information
        /// </summary>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static int RunMessage(PlayerComp ci)
        {
            SetCompMessage msg = new SetCompMessage(ci);
            try
            {
                msg.Send();
            }
            catch(ServerCommException ex)
            {
                throw new Exception("Set comp definitions: " + ex.Message);
            }
            //void for now
            return msg.m_coupon.Id;
        }

        protected override void PackRequest()
        {
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            //Coupon ID. 0 means new
            requestWriter.Write(m_coupon.Id);
            //Coupon Name
            WriteString(requestWriter, m_coupon.Name);
            //Start Date
            WriteString(requestWriter, m_coupon.StartDate.ToString(DATE_FORMAT, CultureInfo.InvariantCulture));
            //End Date
            WriteString(requestWriter, m_coupon.EndDate.ToString(DATE_FORMAT, CultureInfo.InvariantCulture));
            //Value
            WriteString(requestWriter, m_coupon.Value.ToString("N2", CultureInfo.InvariantCulture));
            //Max Usage
            requestWriter.Write((short)(m_coupon.CouponMaxUsage ?? -1));
            //Comp Last Awarded Date 
            WriteString(requestWriter, m_coupon.LastAwardedDate.HasValue ? m_coupon.LastAwardedDate.Value.ToString(DATE_FORMAT, CultureInfo.InvariantCulture) : "");
            //Short Name
            WriteString(requestWriter, m_coupon.ShortName);
            //Comp Type Id 
            requestWriter.Write((int)m_coupon.CouponType);
            //Award Type
            requestWriter.Write((int)m_coupon.AwardType);

            //Unlock Spend
            WriteDecimal(requestWriter, m_coupon.UnlockSpend);
            //Unlock Session Count
            requestWriter.Write((int)m_coupon.UnlockSessionCount);

            // Birthday Window Days Before
            requestWriter.Write((int)(m_coupon.WindowAwardDaysBefore ?? -1));
            // Birthday Window Days Following
            requestWriter.Write((int)(m_coupon.WindowAwardDaysFollowing ?? -1));

            //minimum spend to qualify
            WriteString(requestWriter, m_coupon.MinimumSpendToQualify.ToString("N2", CultureInfo.InvariantCulture));//US4852
            //Count of restricted items
            requestWriter.Write(m_coupon.RestrictedProductIds == null ? (ushort)0 : (ushort)m_coupon.RestrictedProductIds.Count);//US4852
            if(m_coupon.RestrictedProductIds != null)
            {
                foreach(int productID in m_coupon.RestrictedProductIds)
                {
                    //restricted product id
                    requestWriter.Write(productID);//US4852
                }
            }
            //Count of Qualifying Packages US4941
            requestWriter.Write(m_coupon.EarnedPackageIDs == null ? (ushort)0 : (ushort)m_coupon.EarnedPackageIDs.Count);
            if(m_coupon.EarnedPackageIDs != null)
            {
                foreach(int packageID in m_coupon.EarnedPackageIDs)
                {
                    //Qualifying Package id
                    requestWriter.Write(packageID);
                }
            }
            //Count of Package Restrictions US4932
            requestWriter.Write(m_coupon.RestrictedPackageIds == null ? (ushort)0 : (ushort)m_coupon.RestrictedPackageIds.Count);
            if(m_coupon.RestrictedPackageIds != null)
            {
                foreach(int packageID in m_coupon.RestrictedPackageIds)
                {
                    //Qualifying Package id
                    requestWriter.Write(packageID);
                }
            }
            // Ignore restricted package validations
            requestWriter.Write((bool)m_coupon.IgnoreValidationsForIgnoredPackages);

            // Use Limits
            requestWriter.Write((Int16)(m_coupon.ProgramLimit ?? -1));
            requestWriter.Write((Int16)(m_coupon.DailyLimit ?? -1));
            requestWriter.Write((Int16)(m_coupon.WeeklyLimit ?? -1));
            requestWriter.Write((Int16)(m_coupon.MonthlyLimit ?? -1));
            requestWriter.Write((Int16)(m_coupon.YearlyLimit ?? -1));
            for(int i = 0; i < 7; i++)
            {
                var useLimit = m_coupon.DayOfWeekLimits[(DayOfWeek)i];
                requestWriter.Write((Int16)(useLimit ?? -1));
            }
            for(int i = 1; i <= 12; i++)
            {
                var useLimit = m_coupon.MonthOfYearLimits[i];
                requestWriter.Write((Int16)(useLimit ?? -1));
            }
            requestWriter.Write((ushort)(m_coupon.ProgramLimits.Count));
            foreach(var pl in m_coupon.ProgramLimits)
            {
                requestWriter.Write(pl.Key);
                requestWriter.Write((Int16)(pl.Value ?? -1));
            }
            requestWriter.Write((ushort)(m_coupon.SessionNumberLimits.Count));
            foreach(var l in m_coupon.SessionNumberLimits)
            {
                requestWriter.Write(l.Key);
                requestWriter.Write((Int16)(l.Value ?? -1));
            }

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

                if(ReturnCode == (int)GTIServerReturnCode.Success)
                {
                    retID = responseReader.ReadInt32();
                }
            }
            catch(EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set comp definitions: ", e);
            }
            catch(Exception e)
            {
                throw new ServerException("set comp definitions: ", e);
            }

            responseReader.Close();
        }


    }
}
