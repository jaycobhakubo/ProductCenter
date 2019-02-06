#region Copyright
// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © FortuNet dba GameTech
// International, Inc.
#endregion

// US3692 Adding support for whole points
//US4695: Product Center: Move validations setup into Validations

using System;
using System.Globalization;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Business
{
    /// <summary>
    /// Contains all the different workstation settings for the POS module.
    /// </summary>
    public  class ProductCenterSettings
    {
        #region Member Properties
        public MSRSettings MSRSettingInfo { get; set; }

        public bool EnableAnonymousPlay { get; set; }

        /// <summary>
        /// Gets or sets the display mode to use for user interfaces.
        /// </summary>
        public DisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the database server name to use for reports.
        /// </summary>
        public string DatabaseServer { get; set; }

        /// <summary>
        /// Gets or sets the database name to use for reports.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the database user to use for reports.
        /// </summary>
        public string DatabaseUser { get; set; }

        /// <summary>
        /// Gets or sets the database password to use for reports.
        /// </summary>
        public string DatabasePassword { get; set; }

        /// <summary>
        /// Gets or sets whether to force the program to display in the 
        /// English language.
        /// </summary>
        public bool ForceEnglish { get; set; }

        /// <summary>
        /// Gets or sets whether we are using Pre-Printed card packs
        /// </summary>
        public bool UsePrePrintedPacks { get; set; }

        /// <summary>
        /// Gets or sets whether to log output to a file.
        /// </summary>
        public bool EnableLogging { get; set; }

        /// <summary>
        /// Gets or sets level of logging.
        /// </summary>
        public int LoggingLevel { get; set; }

        /// <summary>
        /// Gets or sets the number of days to keep a file log.
        /// </summary>
        public int FileLogRecycleDays { get; set; }

        //START RALLY TA 5744
        /// <summary>
        /// Gets or sets the flag indicating whether PlayWithPaper is being used.
        /// </summary>
        public bool PlayWithPaper { get; set; }
        
        public bool CrystalBallEnabled { get; set; }//RALLY TA 7890

        public bool CreditEnabled { get; set; } //RALLY DE 6809

        public bool AccrualEnabled { get; set; } //RALLY US1796

        /// <summary>
        /// Gets or sets whether or not the system allows for partial points
        /// </summary>
        public bool WholeProductPoints { get; set; } // US3692 Adding support for Whole product points

        public bool InventoryCenterEnabled { get; set; }//RALLY US1863

        //Store the systemsetting for coupon management.
        public bool AllowCouponManagement { get; set; }

        public bool EnableValidation { get; set; }

        public int CardCountValidation { get; set; }

        public int MaxValidationsPerTransaction { get; set; }

        #endregion

        #region Constructor

        public ProductCenterSettings()
        {
            EnableLogging = false;
            ForceEnglish = false;
            DatabasePassword = null;
            DatabaseUser = null;
            DatabaseName = null;
            DatabaseServer = null;
            DisplayMode = null;
            EnableAnonymousPlay = false;
            UsePrePrintedPacks = false;
            PlayWithPaper = false;
            CrystalBallEnabled = false;
            CreditEnabled = false; //RALLY DE 6809
            AccrualEnabled = false;
            WholeProductPoints = false;
            MSRSettingInfo = new MSRSettings();
        }
        #endregion]


        #region Member Methods

        // FIX : TA7890
        /// <summary>
        /// Parses a setting from the server and loads it into the 
        /// POSSettings, if valid.
        /// </summary>
        /// <param name="setting">The setting to parse.</param>
        public void LoadSetting(LicenseSettingValue setting)
        {
            try
            {
                var param = (LicenseSetting)setting.Id;

                switch (param)
                {
                    case LicenseSetting.CBBEnabled:
                        CrystalBallEnabled = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;
                    case LicenseSetting.EnableAnonymousMachineAccounts:
                        EnableAnonymousPlay = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;

                    case LicenseSetting.UsePrePrintedPacks:
                        UsePrePrintedPacks = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;

                    case LicenseSetting.PlayWithPaper:
                        PlayWithPaper = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);           
                        break;

                    //START RALLY DE 6809 change the way credit enabled is detected
                    case LicenseSetting.CreditEnabled:
                        CreditEnabled = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;
                    //END RALLY DE 6809
                    //START RALLY US1863
                    case LicenseSetting.InventoryCenterEnabled:
                        InventoryCenterEnabled = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;
                    //END RALLY US1863
                    //START RALLY US1796
                    case LicenseSetting.AccrualEnabled:
                        AccrualEnabled = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;
                    //END RALLY US1796

                    // US3692 Adding support for Whole product points
                    case LicenseSetting.ForceWholeProductPoints:
                        WholeProductPoints = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;
                }
            }
            catch (Exception)
            {
            }
        }
        // END : TA7890
        
        /// <summary>
        /// Parses a setting from the server and loads it into the 
        /// POSSettings, if valid.
        /// </summary>
        /// <param name="setting">The setting to parse.</param>
        public void LoadSetting(SettingValue setting)
        {
            try
            {
                var param = (Setting)setting.Id;

                switch (param)
                {
                    case Setting.MagneticCardFilters:
                        MSRSettingInfo.setFilters(setting.Value);
                        break;

                    case Setting.MSRReadTriggers:
                        MSRSettingInfo.setReadTriggers(setting.Value);
                        break;

                    case Setting.DatabaseServer:
                        DatabaseServer = setting.Value;
                        break;

                    case Setting.DatabaseName:
                        DatabaseName = setting.Value;
                        break;

                    case Setting.DatabaseUser:
                        DatabaseUser = setting.Value;
                        break;

                    case Setting.DatabasePassword:
                        DatabasePassword = setting.Value;
                        break;

                    case Setting.ForceEnglish:
                        ForceEnglish = Convert.ToBoolean(setting.Value);
                        break;

                    case Setting.EnableLogging:
                        EnableLogging = Convert.ToBoolean(setting.Value);
                        break;

                    case Setting.LoggingLevel:
                        LoggingLevel = Convert.ToInt32(setting.Value, CultureInfo.InvariantCulture);
                        break;

                    case Setting.LogRecycleDays:
                        FileLogRecycleDays = Convert.ToInt32(setting.Value, CultureInfo.InvariantCulture);
                        break;

                    case Setting.EnableCouponManagement:
                        AllowCouponManagement = Convert.ToBoolean(setting.Value);
                        break;

                    case Setting.EnableValidation:
                        EnableValidation = Convert.ToBoolean(setting.Value);
                        break;

                    case Setting.ProductValidationCardCount:
                        CardCountValidation = Convert.ToInt32(setting.Value, CultureInfo.InvariantCulture);
                        break;

                    case Setting.MaxValidationPerTransaction:
                        MaxValidationsPerTransaction = Convert.ToInt32(setting.Value, CultureInfo.InvariantCulture);
                        break;



                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
