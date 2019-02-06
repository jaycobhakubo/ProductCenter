using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ScanCodeEditForm : GTI.Modules.Shared.EliteGradientForm
    {
        protected List<string> m_scanCodes = new List<string>();

        public ScanCodeEditForm()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!m_txtNewScanCode.Focused && keyData != Keys.Tab && keyData != Keys.Up && keyData != Keys.Down && keyData != Keys.PageUp && keyData != Keys.PageDown && keyData != Keys.Home && keyData != Keys.End)
            {
                m_txtNewScanCode.Focus();

                KeysConverter kc = new KeysConverter();
                string tmp = kc.ConvertToString(keyData);

                if (tmp.Length == 1)
                {
                    if (tmp[0] >= 'A' && tmp[0] <= 'Z')
                    {
                        if ((keyData & Keys.Shift) != Keys.Shift)
                            tmp = tmp.ToLower();
                    }

                    m_txtNewScanCode.Text = tmp;
                    m_txtNewScanCode.SelectionStart = 100;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void m_btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void m_btnRemove_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void m_btnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        #region Member Properties

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemIs
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets the Item ID.
        /// </summary>
        public int ItemID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scan codes in the list box when adding an item.
        /// </summary>
        public List<string> ScanCodes
        {
            get
            {
                return m_scanCodes;
            }

            set
            {
                m_scanCodes.Clear();

                foreach (string code in value)
                    m_scanCodes.Add(code);
            }
        }

        #endregion

        private void ScanCodeEditForm_Shown(object sender, EventArgs e)
        {
            m_btnClose.Focus();

            if (ItemID != 0) //we need to get any existing scan codes
            {
                GetScanCodesMessage getCodes = new GetScanCodesMessage(ItemIs == ItemType.Package, ItemID);

                Cursor = Cursors.WaitCursor;

                try
                {
                    getCodes.Send();

                    foreach (string code in getCodes.ScanCodes)
                        m_lbScanCodes.Items.Add(code);

                    if (getCodes.ScanCodes.Count > 0)
                        m_lbScanCodes.Focus();
                }
                catch (Exception ex)
                {
                    MessageForm.Show(ex.Message + "\n\n" + ex.InnerException != null && ex.InnerException.Message != null ? "\n\n" + ex.InnerException.Message : "");
                }

                Cursor = Cursors.Default;
            }
            else //use whatever we were passed
            {
                foreach (string code in m_scanCodes)
                    m_lbScanCodes.Items.Add(code);
            }
        }

        private void m_btnClose_Click(object sender, EventArgs e)
        {
            if (ItemID == 0) //return with the list of scan codes 
            {
                m_scanCodes.Clear();

                foreach (string code in m_lbScanCodes.Items)
                    m_scanCodes.Add(code);
            }
        }

        private void m_btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(m_txtNewScanCode.Text) && !m_lbScanCodes.Items.Contains(m_txtNewScanCode.Text))
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    if (ItemID == 0) //just work with the list box
                    {
                        m_lbScanCodes.Items.Add(m_txtNewScanCode.Text);
                        m_txtNewScanCode.Clear();
                    }
                    else //work with live data
                    {
                        AddRemoveFindScanCodeMessage work = new AddRemoveFindScanCodeMessage(Operation.Add, ItemIs, ItemID, m_txtNewScanCode.Text);

                        work.Send();

                        if (work.WasSuccessful)
                        {
                            m_lbScanCodes.Items.Add(m_txtNewScanCode.Text);
                            m_txtNewScanCode.Clear();
                        }
                        else
                        {
                            MessageForm.Show(string.Format("Scan code is being used by:\n\n{0}-  {1}", work.ItemUsingScanCodeType == ItemType.Package? "Package" : "Product", work.ItemUsingScanCodeName));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show(ex.Message + "\n\n" + ex.InnerException != null && ex.InnerException.Message != null ? "\n\n" + ex.InnerException.Message : "");
                }

                Cursor = Cursors.Default;
            }
        }

        private void m_btnRemove_Click(object sender, EventArgs e)
        {
            if (m_lbScanCodes.SelectedItem != null) //we have something to remove
            {
                Cursor = Cursors.WaitCursor;

                string code = (string)m_lbScanCodes.SelectedItem;

                try
                {
                    if(ItemID != 0) //work with live data
                    {
                        AddRemoveFindScanCodeMessage work = new AddRemoveFindScanCodeMessage( Operation.Remove, ItemIs, ItemID, code);

                        work.Send();
                    }

                    m_lbScanCodes.Items.Remove(m_lbScanCodes.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageForm.Show(ex.Message + ex.InnerException != null && ex.InnerException.Message != null?"\n\n" + ex.InnerException.Message:"");
                }

                Cursor = Cursors.Default;
            }
        }
    }
}
