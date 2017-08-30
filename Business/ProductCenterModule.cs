// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.Business
{
    /// <summary>
    /// The implementation of the IGTIModule COM interface for the ProductCenter.
    /// </summary>
    [
        ComVisible(true),
        Guid("A1F3370A-B627-4082-B505-574BF7D7D011"),
        ClassInterface(ClassInterfaceType.None),
        ComSourceInterfaces(typeof(_IGTIModuleEvents)),
        ProgId("GTI.Modules.ProductCenter.ProductCenterModule")
    ]
    public sealed class ProductCenterModule : IGTIModule 
    {
        #region Constants and Data Types
        private const string ModuleName = "GameTech Edge Bingo System Product Center Module"; // Rally TA8833
        #endregion

        #region Events
        /// <summary>
        /// The signature of the 'Stopped' COM connection point handler.
        /// </summary>
        /// <param name="moduleId">The id of the stopped module.</param>
        public delegate void IGTIModuleStoppedEventHandler(int moduleId);

        /// <summary>
        /// The event that will translate to the COM connection point.
        /// </summary>
        public event IGTIModuleStoppedEventHandler Stopped;

        /// <summary>
        /// Occurs when something wants the ProductCenter to stop itself.
        /// </summary>
        internal event EventHandler StopProductCenter;

        /// <summary>
        /// Occurs when something wants the ProductCenter to come to the front of the 
        /// screen.
        /// </summary>
        internal event EventHandler BringToFront;
        #endregion

        #region Declarations
        private object m_syncRoot = new object();
        private int m_moduleId = 0;
        private static bool m_isStopped = true;
        private Thread m_productCenterThread = null;
        #endregion

        #region Member Methods
        /// <summary>
        /// Starts the module.  If the module is already started nothing
        /// happens.  This method will block if another thread is currently
        /// executing it.
        /// </summary>
        /// <param name="moduleId">The id to be given to the module.</param>
        public void StartModule(int moduleId)
        {
            lock(m_syncRoot)
            {
                // Don't start again if we are already started.
                if(!m_isStopped)
                    return;

                // Assign the id.
                m_moduleId = moduleId;

                // Create a thread to run the ProductCenter.
                m_productCenterThread = new Thread(Run);

                // Change the thread regional settings to the current OS 
                // globalization info.
                m_productCenterThread.CurrentUICulture = CultureInfo.CurrentCulture;

                // Set the thread as Single Thread Apartment (STA)
                m_productCenterThread.TrySetApartmentState(ApartmentState.STA);
                
                // Start it.
                m_productCenterThread.Start();
                
                // Mark the module as started.
                m_isStopped = false;
            }
        }

        /// <summary>
        /// Creates the ProductCenter object and blocks until the ProductCenter is told to close
        /// or the user closes the ProductCenter.
        /// </summary>
        private void Run()
        {
            ProductCenter pc = null;

            try
            {
                // Create and initialize new ProductCenter object.
                pc = new ProductCenter(this);
                pc.Initialize(true);

                // Listen for the event where something wants the ProductCenter to stop.
                StopProductCenter += new EventHandler(pc.CloseProductCenter);
                BringToFront +=new EventHandler(pc.BringToFront);

                if (pc.IsInitialized)
                {
                    pc.Start(); // Show the ProductCenter and block.
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                try
                {
                    // Shutdown the ProductCenter
                    if (pc != null)
                    {
                        pc.Shutdown();
                        pc = null;
                    }
                    OnStop();
                }
                catch
                {
                }
                lock (m_syncRoot)
                {
                    // Mark the module as stopped
                    m_isStopped = true;
                }
            }
        }
        
        /// <summary>
        /// This method blocks until the module is stopped.  If the module is 
        /// already stopped nothing happens.
        /// </summary>
        public void StopModule()
        {
            if(m_productCenterThread != null)
            {
                // Send the stop event to module's controller.
                EventHandler stopHandler = StopProductCenter;

                if(stopHandler != null)
                    stopHandler(this, new EventArgs());

                m_productCenterThread.Join();
            }
        }

        /// <summary>
        /// Signals the COM connection point that we have stopped.
        /// </summary>
        internal void OnStop()
        {           
            IGTIModuleStoppedEventHandler handler = Stopped;

            if(handler != null)
                handler(m_moduleId);
        }

        /// <summary>
        /// Returns the name of this GTI module.
        /// </summary>
        /// <returns>The module's name.</returns>
        public string QueryModuleName()
        {
            return ModuleName;
        }

        /// <summary>
        /// Tells the module to bring itself to the front of the screen.
        /// </summary>        
        public void ComeToFront()
        {
            EventHandler handler = BringToFront;

            if (handler != null)
                handler(this, new EventArgs());            
        }

        /// <summary>
        /// Returns a panel filled with ImageButtons so other applications
        /// can emulate the ProductCenter main selling form.
        /// </summary>
        /// <returns>A Panel filled with ImageButtons.</returns>
        public Panel GetDesignButtonPanel()
        {
            DisplayMode normalMode = new NormalDisplayMode();
            Panel panel = new Panel();
            panel.Name = "ProductCenter Menu Buttons Panel";

            // Create the panel.
            //SellingForm.CreateMenuButtons(null, panel, false, normalMode);

            // Clean up.
            normalMode = null;

            return panel;
        }
        #endregion
    }

}
