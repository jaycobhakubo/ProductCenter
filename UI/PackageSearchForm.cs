using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GTI.Modules.ProductCenter.Business;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;

namespace GTI.Modules.ProductCenter.UI
{
    /// <summary>
    /// Class to implement the Package search form
    /// </summary>
    public partial class PackageSearchForm  : GradientForm
    {
        protected DisplayMode DisplayMode = new NormalDisplayMode();

        #region Package Search Properties
        /// <summary>
        /// Get or Set the Product Name
        /// </summary>
        public string PackageName
        {
            get { return m_productName.Text; }
            set { m_productName.Text = value; }
        }
       
        /// <summary>
        /// Determines if the accrual is set
        /// </summary>
        private bool IsAccrualSet
        {
            get;
            set;
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the package search form
        /// </summary>
        /// <param name="settings">the main product center settings</param>
        public PackageSearchForm(ProductCenterSettings settings)
        {
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            AcceptButton = m_accept;
            CancelButton = m_cancel;
            IsAccrualSet = settings.AccrualEnabled; 

            m_productName.Text = "";

            SetDialogHeight(IsAccrualSet);
        }

        /// <summary>
        /// Sets the last package name
        /// </summary>
        /// <param name="productName"></param>
        public void SetLastPackageName(string packageName)
        {           
            m_productName.Text = packageName;     
        }

        /// <summary>
        /// Sets the dialog height depending on if an accrual is set or not
        /// </summary>
        /// <param name="accrualSet">if the accrual is set</param>
        private void SetDialogHeight(bool accrualSet)
        {
            Height = 225; 
        }    
        #endregion

        #region Button event handlers

        /// <summary>
        /// Called when the accept button is pressed
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event arguments</param>
        private void m_accept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Called when the cancel button is pressed
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">the event arguments</param>
        private void m_cancel_Click(object sender, EventArgs e)
        { 
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}