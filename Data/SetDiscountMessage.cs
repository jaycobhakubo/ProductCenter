#region Copyright
// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2016 FortuNet, Inc.
#endregion

//US4321: (US4319) Discount based on quantity

using System;
using System.IO;
using System.Text;
using GTI.Modules.Shared;
using GTI.Modules.Shared.Business;

//US4320: (US4319) Limit how many times a discount can be used.

namespace GTI.Modules.ProductCenter.Data
{
    internal class SetDiscountMessage : ServerMessage
    {
        protected const int MinResponseMessageLength = 8;
        private int DiscountId { get; set; }
        public DiscountItem DiscountItem { get; set; }

        public SetDiscountMessage(DiscountItem discountItem)
        {
            m_id = 18091;
            DiscountItem = discountItem;
            DiscountId = discountItem.DiscountId;
        }

        public static int Save(DiscountItem discountItem)
        {
            var msg = new SetDiscountMessage(discountItem);
            try
            {
                msg.Send();
            }
            catch (ServerCommException ex)
            {
                throw new Exception("SetDiscountMessage: " + ex.Message);
            }
            return msg.DiscountId;
        }

        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            var requestStream = new MemoryStream();
            var requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Discount Id
            requestWriter.Write(DiscountItem.DiscountId);

            // Discount Type Id
            requestWriter.Write((int)DiscountItem.Type);

            // Is Active
            requestWriter.Write(DiscountItem.IsActive);

            // Discount Amount
            WriteDecimal(requestWriter, DiscountItem.DiscountAmount);

            // Points Per Dollar Amount
            WriteDecimal(requestWriter, DiscountItem.PointsPerDollar);

            // Discount Name
            WriteString(requestWriter, DiscountItem.DiscountName);

            // Discount Award Type Id
            requestWriter.Write((int)DiscountItem.DiscountAwardType);
            
            //Require Player
            requestWriter.Write(DiscountItem.IsPlayerRequired);

            //Spend Level Count
            requestWriter.Write((ushort)DiscountItem.SpendLevels.Count);

            if (DiscountItem.SpendLevels != null)
            {
                foreach (var spendLevels in DiscountItem.SpendLevels)
                {
                    //Sequence
                    requestWriter.Write(spendLevels.Sequence);

                    //Min Value
                    WriteDecimal(requestWriter, spendLevels.SpendMinValue);

                    //Max Value
                    WriteDecimal(requestWriter, spendLevels.SpendMaxValue);

                    //Actual Value
                    WriteDecimal(requestWriter, spendLevels.SpendValue);
                }
            }

            //Restriction
            requestWriter.Write(DiscountItem.RestrictedProductIds == null ? (ushort)0 : (ushort)DiscountItem.RestrictedProductIds.Count);
            if (DiscountItem.RestrictedProductIds != null)
            {
                foreach (var restrictedProduct in DiscountItem.RestrictedProductIds)
                {
                    requestWriter.Write(restrictedProduct);
                }
            }

            // Start Date
            requestWriter.Write(DiscountItem.StartDate.HasValue);
            if (DiscountItem.StartDate.HasValue)
                WriteDateTime(requestWriter, DiscountItem.StartDate.Value);

            // End Date
            requestWriter.Write(DiscountItem.EndDate.HasValue);
            if (DiscountItem.EndDate.HasValue)
                WriteDateTime(requestWriter, DiscountItem.EndDate.Value);

            // Allow Partial Discounts
            requestWriter.Write(DiscountItem.AllowPartialDiscounts);

            // Maximum Discount
            WriteDecimal(requestWriter, DiscountItem.MaximumDiscount);

            // Minimum Spend
            WriteDecimal(requestWriter, DiscountItem.MinimumSpend);

            // Schedule list
            requestWriter.Write(DiscountItem.DiscountSchedule == null ? (ushort)0 : (ushort)DiscountItem.DiscountSchedule.Count);
            if (DiscountItem.DiscountSchedule != null)
            {
                foreach (var schedule in DiscountItem.DiscountSchedule)
                {
                    // Day of Week
                    if(schedule.DayOfWeek.HasValue)
                        requestWriter.Write((byte)((byte)schedule.DayOfWeek.Value+1)); // convert the DOW into a 1-base like MsSQL and have zero mean "none"
                    else
                        requestWriter.Write((byte)0);
                    // Session Number
                    if (schedule.SessionNumber.HasValue)
                        requestWriter.Write((byte)schedule.SessionNumber.Value);
                    else
                        requestWriter.Write((byte)0);
                }
            }

            //US4320
            // Maximum Use Per Session
            requestWriter.Write((byte)DiscountItem.MaximumUsePerSession);

            // US4942 Ignore Validations for ignored packages
            requestWriter.Write((bool)DiscountItem.IgnoreValidationsForIgnoredPackages);

            // US4942 Restricted Package Count
            requestWriter.Write(DiscountItem.RestrictedPackageIds == null ? (ushort)0 : (ushort)DiscountItem.RestrictedPackageIds.Count);
            if (DiscountItem.RestrictedPackageIds != null)
            {
                foreach (int packageID in DiscountItem.RestrictedPackageIds)
                {
                    // US4942 Restricted Package ID
                    requestWriter.Write((int)packageID);
                }
            }

			//US4321 Advance Type
            requestWriter.Write((int)DiscountItem.AdvancedType);

            //US4321 buy quantity
            requestWriter.Write(DiscountItem.AdvancedQuantityDiscount.BuyQuantity);

            //US4321 buy package ID
            requestWriter.Write(DiscountItem.AdvancedQuantityDiscount.BuyPackageId);

            //US4321 get quantity
            requestWriter.Write(DiscountItem.AdvancedQuantityDiscount.GetQuantity);

            //US4321 get package ID
            requestWriter.Write(DiscountItem.AdvancedQuantityDiscount.GetPackageId);

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
                throw new MessageWrongSizeException("Set Discount Item");

            // Try to unpack the data.
            try
            {
                // Seek past return code.
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                // Get the Discount Id.
                DiscountId = responseReader.ReadInt32();
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Set Discount Item", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Set Discount Item", e);
            }

            // Close the streams.
            responseReader.Close();
        }
    }
}
