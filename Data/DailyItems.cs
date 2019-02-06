using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTI.Modules.ProductCenter.Data
{
    /// US1772
    /// <summary>
    /// Represents a (single) Daily Menu Button item to be sent to the server
    /// </summary>
    public class DailyMenuButton
    {
        public int MenuButtonId
        {
            get;
            set;
        }
        public int DiscountTypeId
        {
            get;
            set;
        }
        public int PackageId
        {
            get;
            set;
        }
        public byte PageNumber
        {
            get;
            set;
        }
        public bool ChargeDeviceFee
        {
            get;
            set;
        }
        public string ReceiptText
        {
            get;
            set;
        }
        /// <summary>
        /// The location of the button on the page
        /// </summary>
        public byte KeyNum
        {
            get;
            set;
        }
        public string KeyText
        {
            get;
            set;
        }
        public int KeyColor
        {
            get;
            set;
        }
        public bool KeyLocked
        {
            get;
            set;
        }
        public bool PlayerRequired
        {
            get;
            set;
        }
        public bool DefaultValidation
        {
            get;
            set;
        }
        public bool IsValidationPackage
        {
            get;
            set;
        }
        public int FunctionId
        {
            get;
            set;
        }
        public int DiscountId
        {
            get;
            set;
        }
        public decimal DiscountAmount
        {
            get;
            set;
        }
        public decimal DiscountPointsPerDollar
        {
            get;
            set;
        }
        public int ButtonGraphicId
        {
            get;
            set;
        }
        public bool RemoveButton
        {
            get;
            set;
        }

        /// <summary>
        /// The list of product items in this menu button
        /// </summary>
        public List<DailyProductPackageItem> ProductItems
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new Daily Menu button
        /// </summary>
        /// <param name="button">a single button to make a copy of to turn it into a daily button</param>
        internal DailyMenuButton(MenuButtonList button, List<DailyProductPackageItem> products = null)
        {
            PackageId = button.PackageId;
            FunctionId = button.FunctionId;
            DiscountId = button.DiscountId;
            PageNumber = button.PageNumber;
            KeyNum = button.KeyNum;
            KeyText = button.KeyText;
            KeyColor = button.KeyColor;
            KeyLocked = button.KeyLocked;
            PlayerRequired = button.PlayerRequired;
            ReceiptText = button.ReceiptText;
            DiscountTypeId = button.DiscountTypeId;
            DiscountAmount = button.DiscountAmount;
            DiscountPointsPerDollar = button.DiscountPointsPerDollar;
            RemoveButton = button.RemoveButton != 0;
            ButtonGraphicId = button.ButtonGraphicId;
            DefaultValidation = button.DefaultValidation;

            if (products != null)
                ProductItems = products;
            else
                ProductItems = new List<DailyProductPackageItem>();
        }

        /// <summary>
        /// Default constructor to create a new Daily Menu button
        /// </summary>
        public DailyMenuButton()
        {
            ProductItems = new List<DailyProductPackageItem>();
        }
    }

    /// US1772
    /// <summary>
    /// 
    /// </summary>
    public class DailyProductPackageItem
    {
        public int DailyProductId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int SalesSourceId { get; set; }
        public string SalesSourceName { get; set; }
        public int CardCutId { get; set; }
        public string CardCutName { get; set; }
        public int GameTypeId { get; set; }
        public string GameTypeName { get; set; }
        public int CardLevelId { get; set; }
        public string CardLevelName { get; set; }
        public int CardMediaId { get; set; }
        public string CardMediaName { get; set; }
        public int CardTypeId { get; set; }
        public string CardTypeName { get; set; }
        public int ProgramCBBGameId { get; set; }
        public int ProgramGameId { get; set; }
        public string ProgramGameName { get; set; }
        public int GameCategoryId { get; set; }
        public string GameCategoryName { get; set; }
        public byte Quantity { get; set; }
        public bool IsTaxed { get; set; }
        public ushort CardCount { get; set; }
        public string Price { get; set; }
        public string AltPrice { get; set; }
        public bool CountsTowardsQualifyingSpend { get; set; }
        public bool Prepaid { get; set; }
        public string PointsPerQuantity { get; set; }
        public string PointsPerDollar { get; set; }
        public string PointsToRedeem { get; set; }
        public ushort NumbersRequired { get; set; }
        public bool IsBarcodedPaper { get; set; }
        public bool IsValidated { get; set; }
        public int CardPositionsMapId { get; set; }
        public SortedList<byte, byte> PositionStarCodes { get; set; }

        public DailyProductPackageItem()
        {
        }

        public DailyProductPackageItem(PackageProduct productItem)
        {
            ProductId = productItem.ProductId;
            ProductName = productItem.ProductName;
            ProductTypeId = productItem.ProductTypeId;
            ProductTypeName = productItem.ProductTypeName;
            SalesSourceId = productItem.SalesSourceId;
            SalesSourceName = productItem.SalesSourceName;
            CardCutId = productItem.CardCutId;
            CardCutName = productItem.CardCutName;
            GameTypeId = productItem.GameTypeId;
            GameTypeName = productItem.GameTypeName;
            CardLevelId = productItem.CardLevelId;
            CardLevelName = productItem.CardLevelName;
            CardMediaId = productItem.CardMediaId;
            CardMediaName = productItem.CardMediaName;
            CardTypeId = productItem.CardTypeId;
            CardTypeName = productItem.CardTypeName;
            ProgramCBBGameId = productItem.ProgramCBBGameId;
            ProgramGameId = productItem.ProgramGameId;
            ProgramGameName = productItem.ProgramGameName;
            GameCategoryId = productItem.GameCategoryId;
            GameCategoryName = productItem.GameCategoryName;
            Quantity = productItem.Quantity;
            IsTaxed = productItem.IsTaxed;
            CardCount = productItem.CardCount;
            Price = productItem.Price;
            AltPrice = productItem.AltPrice;
            CountsTowardsQualifyingSpend = productItem.CountsTowardsQualifyingSpend;
            Prepaid = productItem.Prepaid;
            PointsPerQuantity = productItem.PointsPerQuantity;
            PointsPerDollar = productItem.PointsPerDollar;
            PointsToRedeem = productItem.PointsToRedeem;
            NumbersRequired = productItem.NumbersRequired;
        }

        public PackageProduct ToPackageProduct()
        {
            PackageProduct retVal = new PackageProduct();
            retVal.ProductId = ProductId;
            retVal.ProductName = ProductName;
            retVal.ProductTypeId = ProductTypeId;
            retVal.ProductTypeName = ProductTypeName;
            retVal.SalesSourceId = SalesSourceId;
            retVal.SalesSourceName = SalesSourceName;
            retVal.CardCutId = CardCutId;
            retVal.CardCutName = CardCutName;
            retVal.GameTypeId = GameTypeId;
            retVal.GameTypeName = GameTypeName;
            retVal.CardLevelId = CardLevelId;
            retVal.CardLevelName = CardLevelName;
            retVal.CardMediaId = CardMediaId;
            retVal.CardMediaName = CardMediaName;
            retVal.CardTypeId = CardTypeId;
            retVal.CardTypeName = CardTypeName;
            retVal.ProgramCBBGameId = ProgramCBBGameId;
            retVal.ProgramGameId = ProgramGameId;
            retVal.ProgramGameName = ProgramGameName;
            retVal.GameCategoryId = GameCategoryId;
            retVal.GameCategoryName = GameCategoryName;
            retVal.Quantity = Quantity;
            retVal.IsTaxed = IsTaxed;
            retVal.CardCount = CardCount;
            retVal.Price = Price;
            retVal.AltPrice = AltPrice;
            retVal.CountsTowardsQualifyingSpend = CountsTowardsQualifyingSpend;
            retVal.Prepaid = Prepaid;
            retVal.PointsPerQuantity = PointsPerQuantity;
            retVal.PointsPerDollar = PointsPerDollar;
            retVal.PointsToRedeem = PointsToRedeem;
            retVal.NumbersRequired = NumbersRequired;

            return retVal;
        }
    }
}
