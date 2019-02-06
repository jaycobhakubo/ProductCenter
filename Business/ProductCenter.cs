// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.UI;
using GTI.Modules.ProductCenter.Properties;

namespace GTI.Modules.ProductCenter.Business
{
    /// <summary>
    /// Represents the Product Center application.
    /// </summary>
    internal sealed class ProductCenter
    {
        #region Constants and Data Types
        private const string LogPrefix = "ProductCenter - ";
        #endregion

        #region Declarations
        private ProductCenterSettings mobjSetting;
        private bool loggingEnabled;
        private readonly object logSync = new object();
        private int machineId;
        private int operatorId;
        private SplashScreen loadingForm;
        private ProductCenterMdiForm mainMenuForm;
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets whether the ProductCenter was initialized.
        /// </summary>
        public bool IsInitialized { get; private set; }
        ///// <summary>
        ///// Gets whether we use pre printed packs
        ///// </summary>
        //public bool UsePrePrintedPacks { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the ProductCenter class.
        /// </summary>
        /// <param name="module">The module which is running this 
        /// object.</param>
        public ProductCenter(ProductCenterModule module)
        {
            IsInitialized = false;
            if(module == null)
                throw new ArgumentNullException("module");
        }

        #endregion

        #region Member Methods


        /// <summary>
        /// Initializes all the ProductCenter's Data.
        /// </summary>
        public void Initialize(bool showLoadingForm)
        {
            // Check to see if we are already initialized.
            if (IsInitialized)
                return;

            ModuleComm modComm;

            // Get the system related id's
            try
            {
                modComm = new ModuleComm();

                modComm.GetDeviceId();
                machineId = modComm.GetMachineId();
                operatorId = modComm.GetOperatorId();
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format(Resources.GetDeviceInfoFailed, e.Message), Resources.ProductCenterName);
                return;
            }
            // Create and show the loading form.
            loadingForm = new SplashScreen
                            {
                                Version = GetVersionAndCopyright(true),
                                ApplicationName = Resources.ProductCenterName,
                                Cursor = Cursors.WaitCursor
                            };

            if (showLoadingForm)
                loadingForm.Show();

            // Get the workstation's settings from the server.
            loadingForm.Status = Resources.LoadingWorkstationInfo;
            Application.DoEvents();

            // Create a settings object with the default values.
            mobjSetting = new ProductCenterSettings { DisplayMode = new NormalDisplayMode() };
            try
            {
                GetSettings();
            }
            catch (Exception e)
            {
                MessageForm.Show(string.Format(Resources.GetSettingsFailed, e.Message),Resources.GetSettingsTitle, MessageFormTypes.OK, 0);//RALLY DE 6657
                return;
            }

            // Check to see if we want to log everything.
            try
            {
                if (mobjSetting.EnableLogging)
                {
                    Logger.EnableFileLog(mobjSetting.LoggingLevel, mobjSetting.FileLogRecycleDays);
                    Logger.StartLogger(Logger.StandardPrefix);
                    loggingEnabled = true;
                    Log(string.Format("Initializing Product Center ({0})...", GetVersionAndCopyright(false)), LoggerLevel.Information);
                }
            }
            catch (Exception e)
            {
                MessageForm.Show(string.Format(Resources.LogFailed, e.Message), Resources.LogFailedTitle, MessageFormTypes.OK, 0);//RALLY DE 6657
                return;
            }

            // Check to see if we only want to display in English.
            if (mobjSetting.ForceEnglish)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                Log("Forcing English.", LoggerLevel.Configuration);
            }

            loadingForm.Status = Resources.StartingProductCenter;
            Application.DoEvents();

            // Create main menu.
            mainMenuForm = new ProductCenterMdiForm(mobjSetting);

            loadingForm.Cursor = Cursors.Default;

            Application.DoEvents();
            IsInitialized = true;

            // Close the SplashScreen loadng form.
            loadingForm.CloseForm();

            Log("Product Center initialized!", LoggerLevel.Debug);
        }

        /// <summary>
        /// Returns a string with the version and copyright information of 
        /// the Product Center.
        /// </summary>
        /// <param name="justVersion">true if just the version is to be 
        /// returned; otherwise false.</param>
        /// <returns>A string with the version and optionally the copyright 
        /// information.</returns>
        private static string GetVersionAndCopyright(bool justVersion)
        {
            // Get version.
            var version = Assembly.GetExecutingAssembly().GetName().Version.Major +
                "." + Assembly.GetExecutingAssembly().GetName().Version.Minor +
                "." + Assembly.GetExecutingAssembly().GetName().Version.Build +
                "." + Assembly.GetExecutingAssembly().GetName().Version.Revision;

            // Get copyright.
            if (!justVersion)
            {
                var copyright = string.Empty;

                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                if (attributes.Length > 0)
                    copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

                return version + " - " + copyright;
            }
            return version;
        }
                
        /// <summary>
        /// Writes a message to the Product Center's log.
        /// </summary>
        /// <param name="message">The message to write to the log.</param>
        /// <param name="level">The level of the message.</param>
        internal static void Log(string message, LoggerLevel level)
        {
            try
            {
                var frame = new StackFrame(1, true);
                var fileName = frame.GetFileName();
                var lineNumber = frame.GetFileLineNumber();
                message = LogPrefix + message;

                switch (level)
                {
                    case LoggerLevel.Severe:
                        Logger.LogSevere(message, fileName, lineNumber);
                        break;

                    case LoggerLevel.Warning:
                        Logger.LogWarning(message, fileName, lineNumber);
                        break;

                    default:
                        Logger.LogInfo(message, fileName, lineNumber);
                        break;

                    case LoggerLevel.Configuration:
                        Logger.LogConfig(message, fileName, lineNumber);
                        break;

                    case LoggerLevel.Debug:
                        Logger.LogDebug(message, fileName, lineNumber);
                        break;

                    case LoggerLevel.Message:
                        Logger.LogMessage(message, fileName, lineNumber);
                        break;

                    case LoggerLevel.SQL:
                        Logger.LogSql(message, fileName, lineNumber);
                        break;
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Shows the main ProductCenter form modally.
        /// </summary>
        public void Start()
        {
            if (IsInitialized && mainMenuForm != null)
            {
                Log("Starting Product Center.", LoggerLevel.Information);
                loadingForm.Close();
                Application.Run(mainMenuForm);
            }
        }

        /// <summary>
        /// Tells the ProductCenter to close the main form.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">An EventArgs object that contains the 
        /// event data.</param>
        public void CloseProductCenter(object sender, EventArgs e)
        {
            if (mainMenuForm != null)
                mainMenuForm.Close();
        }

        /// <summary>
        /// Tells the ProductCenter to bring the main form to the front.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">An EventArgs object that contains the 
        /// event data.</param>
        internal void BringToFront(object sender, EventArgs e)
        {
            if (IsInitialized && mainMenuForm != null)
            {
                if (mainMenuForm.InvokeRequired)
                {
                    MethodInvoker del = ActivateMainForm;
                    mainMenuForm.Invoke(del);
                }
                else
                    ActivateMainForm();
            }
        }

        /// <summary>
        /// Activates the main form and sets its window state to Normal.
        /// </summary>
        private void ActivateMainForm()
        {
            mainMenuForm.WindowState = FormWindowState.Normal;
            mainMenuForm.Activate();
        }

        /// <summary>
        /// Based on the exception passed in, this method will translate
        /// the error message to localized text and rethrow the exception as
        /// a ProductCenterException.  If the exception type is not recognized, 
        /// then the exception is rethrown as is.
        /// </summary>
        /// <param name="ex">The exception to reformat.</param>
        internal void ReformatException(Exception ex)
        {
            if (ex is MessageWrongSizeException)
                throw new ProductCenterException(string.Format(Resources.MessagePayloadWrongSize, ex.Message), ex);
            if (ex is ServerCommException)
                throw new ProductCenterException(Resources.ServerCommFailed, ex);
            if (ex is ServerException && ex.InnerException != null)
                throw new ProductCenterException(string.Format(Resources.InvalidMessageResponse, ex.Message), ex.InnerException);
            if (ex is ServerException)
            {
                var errorCode = (int)((ServerException)ex).ReturnCode;
                throw new ProductCenterException(string.Format(Resources.ServerErrorCode, errorCode), ex);
            }
            throw ex;
        }

        /// <summary>
        /// Gets the settings from the server.
        /// </summary>
        private void GetSettings()
        {
            // FIX  TA7890
            //////////////////////////////////////////////
            // Send message for license file settings.
            GetLicenseFileSettingsMessage licenseSettingMsg = new GetLicenseFileSettingsMessage(true);

            try
            {
                licenseSettingMsg.Send();
            }
            catch (Exception e)
            {
                ReformatException(e);
            }

            // Loop through each setting and parse the value.
            foreach (LicenseSettingValue setting in licenseSettingMsg.LicenseSettings)
            {
                mobjSetting.LoadSetting(setting);
            }

            /////////////////////////////////////////////////
            // Send message for global settings.
            GetSettingsMessage settingsMsg = new GetSettingsMessage(machineId, operatorId, SettingsCategory.GlobalSystemSettings);

            try
            {
                settingsMsg.Send();
            }
            catch (Exception e)
            {
                ReformatException(e);
            }

            // Loop through each setting and parse the value.
            SettingValue[] stationSettings = settingsMsg.Settings;

            foreach (SettingValue setting in stationSettings)
            {
                mobjSetting.LoadSetting(setting);
            }

            ////////////////////////////////////////////////////
            //// Now load all the ProductCenter specific settings.
            //settingsMsg = new GetSettingsMessage(machineId, 0, SettingsCategory.AllCategories, Setting.UsePrePrintedPacks);

            //try
            //{
            //    settingsMsg.Send();
            //}
            //catch (Exception e)
            //{
            //    ReformatException(e);
            //}

            //// Loop through each setting and parse the value.
            //stationSettings = settingsMsg.Settings;

            //foreach (SettingValue setting in stationSettings)
            //{
            //    mobjSetting.LoadSetting(setting);
            //}
            // END TA7890
        }
        
        /// <summary>
        /// Cancels any pending transactions and shuts down the Product Center.
        /// </summary>
        public void Shutdown()
        {
            Log("Shutting down.", LoggerLevel.Debug);

            machineId = 0;
            operatorId = 0;

            if (mainMenuForm != null)
            {
                mainMenuForm.Dispose();
                mainMenuForm = null;
            }

            if (loadingForm != null)
            {
                loadingForm.Close();
                loadingForm.Dispose();
                loadingForm = null;
            }

            mobjSetting = null;

            Log("Shutdown complete.", LoggerLevel.Information);
            loggingEnabled = false;

            IsInitialized = false;
        }
        #endregion
    }
}