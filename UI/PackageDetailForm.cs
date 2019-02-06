using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Forms;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.Shared;
using System.Collections.Generic;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class PackageDetailForm : GradientForm
    {
        protected DisplayMode DisplayMode = new NormalDisplayMode();
        protected int m_packageID = 0;
        protected List<string> m_scanCodes = new List<string>();

        #region Member Properties
        /// <summary>
        /// Gets or sets the package ID (used for scan codes).
        /// </summary>
        public int PackageID
        {
            get
            {
                return m_packageID;
            }

            set
            {
                m_packageID = value;
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
        /// Get or Set the Package Name
        /// </summary>
        public string PackageName { get { return txtPackageName.Text; } set { txtPackageName.Text = value; } }

        /// <summary>
        /// Get or Set the Receipt Text.
        /// </summary>
        public string ReceiptText { get { return txtReceipt.Text; } set { txtReceipt.Text = value; } }
        public bool ChargeDeviceFee { get { return chkChargeDeviceFee.Checked; } set { chkChargeDeviceFee.Checked = value; } }

        /// <summary>
        /// Gets or sets if the package requires validation mode at POS to purchase.
        /// </summary>
        public bool RequiresValidation
        {
            get
            {
                return RequiresValidationCheckbox.Checked;
            }

            set
            {
                RequiresValidationCheckbox.Checked = value;
            }
        }

        public bool OverrideValidation { get { return OverrideValidationCalculationCheckbox.Checked; } set { OverrideValidationCalculationCheckbox.Checked = value; } }
        
        public int ValidationQuantity 
        {
            get
            {
                int returnValue;
                int.TryParse(ValidationQuantityTextbox.Text, out returnValue);
                return returnValue;
            }
            set
            {
                ValidationQuantityTextbox.Text = value.ToString();
            } 
        }
        private bool isFilter;
        public bool IsFilter
        {
            get { return isFilter; }
            set
            {
                isFilter = value;
                if (value)
                {
                    Text = Resources.PackageSearch;
                    lblPackageName.Visible = true;
                    txtPackageName.Visible = true;
                    txtPackageName.Text = Resources.All;

                    lblReceipt.Visible = false;
                    txtReceipt.Visible = false;

                    btnAccept.Text = Resources.Go;
                    btnAccept.Visible = true;
                    btnAccept.Enabled = true;

                    btnCancel.Visible = true;
                }
            }
        }
        #endregion

        #region Constructor_Destructor
        public PackageDetailForm()
        {
            IsFilter = false;
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            AcceptButton = btnAccept;
            CancelButton = btnCancel;
            EnableButtons();
            EnableValidationOverride(false);
        }

        private void EnableButtons()
        {
            if (!IsFilter)
            {
                btnAccept.Enabled = txtPackageName.Text != ""
                                   && txtReceipt.Text != "";
            }
        }
        #endregion

        private void PackageDetailForm_Shown(object sender, EventArgs e)
        {
            UpdateScanCode();
        }

        private void AcceptClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool doReceiptChanges;
        private void OnPackageNameChanged(object sender, EventArgs e)
        {
            if (txtReceipt.Text == "" || doReceiptChanges)
            {
                doReceiptChanges = true;
                txtReceipt.Text = txtPackageName.Text;
            }
            EnableButtons();
        }

        private void OnReceiptTextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void txtReceipt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            doReceiptChanges = false;
        }

        private void chkChargeDeviceFee_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void OverrideValidationCalculationCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            EnableValidationOverride(OverrideValidationCalculationCheckbox.Checked);
        }

        public void EnableValidationOverride(bool isEnabled)
        {
            if (isEnabled)
            {
                ValidationQuantityTextbox.Enabled = true;
                ValidationQuantityLabel.ForeColor = Color.Black;
            }
            else
            {
                ValidationQuantityTextbox.Enabled = false;
                ValidationQuantityLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void ValidationQuantityTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ScanCodeEditButton_Click(object sender, EventArgs e)
        {
            ScanCodeEditForm editForm = new ScanCodeEditForm();

            editForm.ItemIs = ItemType.Package;
            editForm.ItemID = PackageID;

            if (PackageID == 0)
                editForm.ScanCodes = m_scanCodes;

            editForm.ShowDialog(this);

            if (PackageID == 0)
            {
                m_scanCodes.Clear();

                foreach (string code in editForm.ScanCodes)
                    m_scanCodes.Add(code);
            }

            UpdateScanCode();
        }

        private void UpdateScanCode()
        {
            if (PackageID != 0) //get the scan codes
            {
                GetScanCodesMessage getCodes = new GetScanCodesMessage(true, PackageID);

                Cursor = Cursors.WaitCursor;

                try
                {
                    getCodes.Send();

                    if (getCodes.ScanCodes.Count == 1)
                        ScanCodeText.Text = getCodes.ScanCodes[0];
                    else if (getCodes.ScanCodes.Count > 1)
                        ScanCodeText.Text = "Multiple";
                    else
                        ScanCodeText.Text = "None";
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
                    ScanCodeText.Text = ScanCodes[0];
                else if (ScanCodes.Count > 1)
                    ScanCodeText.Text = "Multiple";
                else
                    ScanCodeText.Text = "None";
            }
        }
    }
}