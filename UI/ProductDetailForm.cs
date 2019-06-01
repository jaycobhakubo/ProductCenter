// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.
//
// US2826 Adding support for barcoded paper
// US4059 Adding support for selecting perm files


using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GTI.Modules.ProductCenter.Business;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;
using CardColorSet = GameTech.Elite.Base.CardColorSet;
using System.Linq;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ProductDetailForm : GradientForm
    {
        private readonly CardColorSet m_noCardColorSetSelection = CardColorSet.GetNoSet("(None)");
        protected DisplayMode DisplayMode = new NormalDisplayMode();
        private int m_accrualsCount;
        protected int m_productID = 0;
        protected List<string> m_scanCodes = new List<string>();

        private bool IsFilter { get; set; }

        #region Product Properties

        /// <summary>
        /// Gets or sets the product ID (used for scan codes).
        /// </summary>
        public int ProductID
        {
            get
            {
                return m_productID;
            }

            set
            {
                m_productID = value;
            }
        }

        /// <summary>
        /// Gets the scan codes for the package.
        /// </summary>
        public List<string> ScanCodes
        {
            get
            {
                return m_scanCodes;
            }
        }

        /// <summary>
        /// Get or Set the Product Name
        /// </summary>
        public string ProductItemName
        {
            get { return m_productName.Text; }
            set { m_productName.Text = value; }
        }

        /// <summary>
        /// Gets or sets the is active property
        /// </summary>
        public bool IsActive
        {
            get
            {
                return m_chkActive.Checked;
            }
            set
            {
                m_chkActive.Checked = value;
            }
        }

        public int AccrualAssociationsCount
        {
            get { return m_accrualsCount; }
            set
            {
                if(m_accrualsCount != value)
                {
                    m_accrualsCount = value;
                    usedByAccrualsLbl.Visible = m_accrualsCount > 0;
                    usedByAccrualsLbl.Text = String.Format("Used by {0} active accruals.", m_accrualsCount);
                }
            }
        }

        /// <summary>
        /// Gets or sets the barcoded paper property
        /// </summary>
        public bool BarcodedPaper
        {
            get
            {
                return m_chkBarcodedPaper.Checked;
            }

            set
            {
                m_chkBarcodedPaper.Checked = value;
                EnableBarcodedPaperDependantControls(value);
            }
        }


        public bool Validate
        {
            get
            {
                return chkbxIsValidate.Checked;
            }

            set
            {
                chkbxIsValidate.Checked = value;
            }
        }


        //US4059
        /// <summary>
        /// Gets or sets the Perm File ID property
        /// </summary>
        public int PermFileId
        {
            set
            {
                //iterate and select item
                foreach (ListItem item in cboPermFile.Items)
                {
                    if (int.Parse(item.Value) == value)
                    {
                        cboPermFile.SelectedItem = item;
                        break;
                    }
                }
            }
            get
            {
                //if barcoded paper is disabled then return -1
                if (!m_chkBarcodedPaper.Checked)
                {
                    return -1;
                }

                //if nothing is selected then return -1
                if (cboPermFile.SelectedItem == null)
                {
                    return -1;
                }
                
                //return selected value
                return int.Parse(((ListItem) cboPermFile.SelectedItem).Value);
            }
        }

        public int CardColorSetId
        {
            set
            {
                CardColorSet found = null;
                //iterate and select item                
                foreach(CardColorSet set in cardColorSetCombo.Items)
                    if( set.Id == value)
                    {
                        found = set;
                        break;
                    }

                cardColorSetCombo.SelectedItem = found;
            }
            get
            {
                //if barcoded paper is disabled or nothing is selected then return -1
                if(!m_chkBarcodedPaper.Checked || cardColorSetCombo.SelectedItem == null)
                    return -1;
                
                return ((CardColorSet)cardColorSetCombo.SelectedItem).Id;
            }
        }

        /// <summary>
        /// Set the Product Type List
        /// </summary>
        public Array PopulateProductTypesList
        {
            set
            {
                // Clear the Product Types List
                cboProductTypes.Items.Clear();

                // Populate the Product Types List
                foreach (ProductTypeListItem productTypeListItem in value)
                {
                    // FIX : TA6092 Support CBB license file flag                   
                    if (!CrystalBallEnabled) //RALLY DE 6557 
                    {
                        if ((productTypeListItem.ProductTypeId > 0 && productTypeListItem.ProductTypeId < 5) || productTypeListItem.ProductTypeId == 21)
                            continue;
                    }
                    // END : TA6092 Support CBB license file flag

                    cboProductTypes.Items.Add(new ListItem(productTypeListItem.ProductType, productTypeListItem.ProductTypeId.ToString()));
                }
            }
        }

        /// <summary>
        /// Get or Set the Product Type.
        /// </summary>
        public string ProductType
        {
            get { return ((ListItem)cboProductTypes.SelectedItem).Text; }
            set
            {
                cboProductTypes.Text = value;
            }
        }

        private void ShowPaperLayoutIfBingoType(string value)
        {
            // FIX : DE4036
            bool isBingo = (value == "5" && IsPlayWithPaper);//RALLY DE 6960 Unable to assign pack number
            // END : DE4036
            labelPaperLayout.Visible = isBingo;
            cboPaperLayouts.Visible = isBingo;

            // US2826 Don't show the barcoded paper check unless it is a paper product
            if (value == "16")
            {
                m_chkBarcodedPaper.Visible = true;

                //US4059
                labelPermFile.Visible = true;
                cboPermFile.Visible = true;

                cardColorSetLbl.Visible = true;
                cardColorSetCombo.Visible = true;

                EnableBarcodedPaperDependantControls(m_chkBarcodedPaper.Checked);
            }
            else
            {
                m_chkBarcodedPaper.Visible = false;
                BarcodedPaper = false;

                //US4059
                labelPermFile.Visible = false;
                cboPermFile.Visible = false;

                cardColorSetLbl.Visible = false;
                cardColorSetCombo.Visible = false;
            }
        }

        private void CboProductTypesSelectedValueChanged(object sender, EventArgs e)
        {
            //check for null
            if (cboProductTypes.SelectedItem == null)
            {
                return;
            }

            //START RALLY DE 6960 unable to assign pack number
            string typeValue = ((ListItem)cboProductTypes.SelectedItem).Value; 
            ShowPaperLayoutIfBingoType(typeValue);
            //END RALLY DE 6960

            int tempProductTypeID;// = Convert.ToInt32(typeValue);
            int.TryParse(typeValue, out tempProductTypeID);
            if (tempProductTypeID == 5 || tempProductTypeID == 16)//If its not equal to electronic and paper then hide validate checkbox.
            {
                    chkbxIsValidate.Visible = true;
                    //panel1.Location = new System.Drawing.Point(17, 88);
            }
            else
            {
                    chkbxIsValidate.Visible = false;
                    //panel1.Location = new System.Drawing.Point(17, 55);
            }
        }

        /// <summary>
        /// Get the Product Type Id.
        /// </summary>
        public string ProductTypeId
        {
            get { return ((ListItem)cboProductTypes.SelectedItem).Value; }
        }
        #endregion

        #region Sales Source Properties

        /// <summary>
        /// Set the Sales Source List
        /// </summary>
        public Array PopulateSalesSourceList
        {
            set
            {
                // Clear the Sales Source List
                cboSalesSources.Items.Clear();

                // Populate the Sales Source List
                foreach (SalesSourceListItem salesSourceListItem in value)
                {
                    //START RALLY US1863
                    if(salesSourceListItem.SalesSourceId == 2 ||
                        salesSourceListItem.SalesSourceId == 1 && IsInventoryCenterEnabled)                       
                    {
                        cboSalesSources.Items.Add(new ListItem(salesSourceListItem.SalesSource, salesSourceListItem.SalesSourceId.ToString()));
                    }
                    //END RALLY US1863
                }
            }
        }

        /// <summary>
        /// Get or Set the Sales Source.
        /// </summary>
        public string SalesSource
        {
            get { return ((ListItem)cboSalesSources.SelectedItem).Text; }
            set { cboSalesSources.Text = value; }
        }

        /// <summary>
        /// Get the Sales Source Id.
        /// </summary>
        public string SalesSourceId
        {
            get { return ((ListItem)cboSalesSources.SelectedItem).Value; }
        }
        #endregion

        #region Product Group Properties
        /// <summary>
        /// Set the Product Group List
        /// </summary>
        public List<ProductGroupItem> PopulateProductGroupList
        {
            set
            {
                // Clear the Product Group List
                cboProductGroups.Items.Clear();
                cboProductGroups.Items.Add(new ListItem(Resources.ProductGroupNone, 0.ToString()));
                cboProductGroups.SelectedIndex = 0; //RALLY DE6740 default product group should be none
                // Populate the Product Group List
                foreach (ProductGroupItem productGroup in value)
                {
                    if (productGroup.IsActive)
                        cboProductGroups.Items.Add(new ListItem(productGroup.ProdGroupName, productGroup.ProdGroupId.ToString()));
                }
            }
        }
        
        /// <summary>
        /// Get or Set the Product Group.
        /// </summary>
        public string ProductGroupName
        {
            get { return ((ListItem)cboProductGroups.SelectedItem).Text; }
            set { cboProductGroups.Text = string.IsNullOrEmpty(value) ? Resources.ProductGroupNone : value; }
        }

        /// <summary>
        /// Get the Product Group Id.
        /// </summary>
        public string ProductGroupId
        {
            get
            {
                // FIX: DE2697
                return cboProductGroups.SelectedIndex == -1 ? "0" : ((ListItem)cboProductGroups.SelectedItem).Value;
                // END: DE2697
            }
        }
        #endregion

        // FIX TA5873
        #region PaperLayout Properties
        public List<PaperLayout> PopulatePaperLayout
        {
            set
            {
                cboPaperLayouts.Items.Clear();
                
                // Populate the Product Item List.
                foreach (PaperLayout layout in value)
                {
                    cboPaperLayouts.Items.Add(new ListItem(layout.PaperLayoutName, layout.PaperLayoutId.ToString()));
                }

            }
        }

        /// <summary>
        /// Get or Set the Paper Layout name.
        /// </summary>
        public string PaperlayoutName
        {
            get
            {
                return cboPaperLayouts.SelectedItem == null ? string.Empty : ((ListItem)cboPaperLayouts.SelectedItem).Text;
            }
            set { cboPaperLayouts.Text = string.IsNullOrEmpty(value) ? Resources.ProductGroupNone : value; }
        }

        /// <summary>
        /// Get the Paper Layout Id.
        /// </summary>
        public string PaperLayoutId
        {
            get
            {
                return cboPaperLayouts.SelectedIndex == -1 ? "0" : ((ListItem)cboPaperLayouts.SelectedItem).Value;
            }
        }

        // FIX : DE4036
        private bool IsPlayWithPaper
        {
            get;
            set;
        }
        // END : DE4036

        //START RALLY US1863
        private bool IsInventoryCenterEnabled
        {
            get;
            set;
        }
        //END RALLY US1863
        // FIX TA7890
        private bool CrystalBallEnabled { get; set; }
        // END TA7890

        #endregion

        //US4059
        #region Perm File Properties
        public List<KeyValuePair<int, string>> PopulatePermFilesList
        {
            set
            {
                // Clear the Product Group List
                cboPermFile.Items.Clear();

                // Populate the Product Group List
                foreach (KeyValuePair<int, string> permFile in value)
                {
                    cboPermFile.Items.Add(new ListItem(permFile.Value, permFile.Key.ToString()));
                }
            }
        }

        public List<CardColorSet> PopulateCardColorSetList
        {
            set
            {
                cardColorSetCombo.Items.Clear();
                cardColorSetCombo.DisplayMember = "Name";
                cardColorSetCombo.ValueMember = "Id";
                var noneSet = m_noCardColorSetSelection;
                cardColorSetCombo.Items.Add(noneSet);
                cardColorSetCombo.Items.AddRange(value.OrderBy((s) => s.Name).ToArray());
            }
        }

        #endregion

        #region Constructors
        // FIX : DE4036
        public ProductDetailForm(bool isFilter, ProductCenterSettings settings)
        {
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            // Create and assign the form's idle event
            Application.Idle += OnIdle;
            AcceptButton = m_accept;
            CancelButton = m_cancel;
            IsPlayWithPaper = settings.PlayWithPaper;
            IsInventoryCenterEnabled = settings.InventoryCenterEnabled; //RALLY US1863
            // FIX TA7890
            CrystalBallEnabled = settings.CrystalBallEnabled;
            // END TA7890

            IsFilter = isFilter;
            if (isFilter)
            {
                SetFilterForm();
            }
            else
            {
                labelProductName.Visible = true;
                m_productName.Visible = true;
                m_productName.Text = "";

                labelProductType.Visible = true;
                cboProductTypes.Visible = true;

                labelProductGroup.Visible = true;
                cboProductGroups.Visible = true;

                m_accept.Text = "Accept";
                m_accept.Visible = true;
                m_accept.Enabled = false;

                m_cancel.Visible = true;

                //US2826 don't show the barcoded paper setting if paper is not selected
                if (PaperLayoutId != "16")
                {
                    m_chkBarcodedPaper.Visible = false;
                    m_chkBarcodedPaper.Checked = false;
                }

                if (settings.EnableValidation == true)
                {
                    chkbxIsValidate.Visible = true;
                    //panel1.Location = new System.Drawing.Point(17, 88);
                }
                else
                {
                    chkbxIsValidate.Visible = false;
                    //panel1.Location = new System.Drawing.Point(17, 55);

                }
            }
        }

        public void HideValidateCheckBox()
        {
            chkbxIsValidate.Visible = false;
            //panel1.Location = new System.Drawing.Point(17, 55);

        }
        
        // END : DE4036
        #endregion

        #region Helper routines
        private void OnIdle(object sender, EventArgs e)
        {
            if (!IsFilter)
            {
                //When form is in idle state will execute this.
                //Enable or Disable controls here.
                bool enableAccept = !string.IsNullOrEmpty(m_productName.Text) &&
                                    cboProductTypes.SelectedIndex != -1 &&
                                    cboProductGroups.SelectedIndex != -1;
                                    

                if (cboPaperLayouts.Visible)
                {
                    enableAccept = enableAccept && cboPaperLayouts.SelectedIndex != -1;
                }
                if (cboSalesSources.Visible)
                {
                    enableAccept = enableAccept && cboSalesSources.SelectedIndex != -1;
                }
                m_accept.Enabled = enableAccept;    
            }
            else
            {
                m_accept.Enabled = m_productName.Text != string.Empty;
            }
        }
        private void SetFilterForm()
        {
            // set width of dialog to hide the Paper Layout listbox.
            labelProductName.Visible = true;
            m_productName.Visible = true;
            m_productName.Text = string.Empty;

            labelProductType.Visible = false;
            cboProductTypes.Visible = false;

            labelSalesSource.Visible = false;
            cboSalesSources.Visible = false;

            labelProductGroup.Visible = false;
            cboProductGroups.Visible = false;

            
            labelPaperLayout.Visible = false;
            cboPaperLayouts.Visible = false;

            m_accept.Text = "Go";
            m_accept.Visible = true;
            m_accept.Enabled = false;

            m_cancel.Visible = true;

            Height = 160;
        }

        //US4059
        /// <summary>
        /// Enable/Disable controls dependent on product being barcoded paper.
        /// </summary>
        private void EnableBarcodedPaperDependantControls(bool value)
        {
            if (value)
            {
                labelPermFile.Enabled = true;
                cboPermFile.Enabled =  true;

                cardColorSetLbl.Enabled = true;
                cardColorSetCombo.Enabled = true;

                if (cboPermFile.SelectedItem == null && cboPermFile.Items.Count > 0)
                    cboPermFile.SelectedItem = cboPermFile.Items[0]; 

                if(cardColorSetCombo.SelectedItem == null && cardColorSetCombo.Items.Count > 0)
                    cardColorSetCombo.SelectedItem = cardColorSetCombo.Items[0];
            }
            else
            {
                labelPermFile.Enabled = false;
                cboPermFile.Enabled = false;
                cardColorSetLbl.Enabled = false;
                cardColorSetCombo.Enabled = false;
            }
        }

        /// Rally US4532 added tooltip so that the full name can be seen
        /// <summary>
        /// Actions that occur when the mouse hovers over the perm file combo box. 
        ///   Displays the full text of the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPermFile_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(cboPermFile.Text, cboPermFile);
        }

        private void cardColorSetCombo_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(cardColorSetCombo.Text, cardColorSetCombo);
        }

        private void UpdateScanCode()
        {
            if (ProductID != 0) //get the scan codes
            {
                GetScanCodesMessage getCodes = new GetScanCodesMessage(false, ProductID);

                Cursor = Cursors.WaitCursor;

                try
                {
                    getCodes.Send();

                    if (getCodes.ScanCodes.Count == 1)
                        labelScanCodesText.Text = getCodes.ScanCodes[0];
                    else if (getCodes.ScanCodes.Count > 1)
                        labelScanCodesText.Text = "Multiple";
                    else
                        labelScanCodesText.Text = "None";
                }
                catch (Exception ex)
                {
                    MessageForm.Show(ex.Message);
                }

                Cursor = Cursors.Default;
            }
            else
            {
                if (ScanCodes.Count == 1)
                    labelScanCodesText.Text = ScanCodes[0];
                else if (ScanCodes.Count > 1)
                    labelScanCodesText.Text = "Multiple";
                else
                    labelScanCodesText.Text = "None";
            }
        }

        #endregion
        // END TA5873

        #region Button event handlers
        private void m_accept_Click(object sender, EventArgs e)
        {
           
            // FIX: DE2697
            Application.Idle -= OnIdle;
            // END: DE2697
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_cancel_Click(object sender, EventArgs e)
        {
            // FIX: DE2697
            Application.Idle -= OnIdle;
            // END: DE2697
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //US4059
        /// <summary>
        /// Barcoded paper checked event.
        /// </summary>
        private void m_chkBarcodedPaper_CheckedChanged(object sender, EventArgs e)
        {
            EnableBarcodedPaperDependantControls(m_chkBarcodedPaper.Checked);
        }

        private void btnEditScanCodes_Click(object sender, EventArgs e)
        {
            ScanCodeEditForm editForm = new ScanCodeEditForm();

            editForm.ItemIs = ItemType.Product;
            editForm.ItemID = ProductID;

            if (ProductID == 0)
                editForm.ScanCodes = m_scanCodes;

            editForm.ShowDialog(this);

            if (ProductID == 0)
            {
                m_scanCodes.Clear();

                foreach (string code in editForm.ScanCodes)
                    m_scanCodes.Add(code);
            }

            UpdateScanCode();
        }

        private void ProductDetailForm_Shown(object sender, EventArgs e)
        {
            UpdateScanCode();
        }

        #endregion
    }
}