using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;
using System.Collections.Generic;
using GTI.Modules.Shared.Data;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class CardLevelDetailForm :  GradientForm
    {
        private const int NO_PAPER_HEIGHT = 270; // RALLY US4547
        protected DisplayMode DisplayMode = new NormalDisplayMode();

        #region Product Properties
        /// <summary>
        /// Get or Set the Product Name
        /// </summary>
        public string CardLevelName
        {
            get { return fieldName.Text; }
            set { fieldName.Text = value; }
        }

        public string CardLevelMultiplier
        {
            get { return fieldMultiplier.Text; }
            set { fieldMultiplier.Text = string.IsNullOrEmpty(value) ? "1" : value; }
        }

        // FIX: DE1907 TA2616 Allow Game Color instead of Level Color
        public int CardLevelColor
        {
            get
            {
                return btnColor.BackColor.ToArgb();
            }
            set
            {
                // use game color is indicated by a null (0) value for color.
                btnColor.BackColor = value == 0 ? Color.Red : Color.FromArgb(value);
                cbUseGameColor.Checked = value == 0;
            }
        }
        // END: DE1907 TA2616 Allow Game Color instead of Level Color

        public CardLevelItem OriginalItem { get; set; } // JAN- probably makes the most sense to send this in as a constructor param and set everything that way... (or an Init() method if we're worried about using the designer)

        public List<string> SelectedPaperColors
        {
            get 
            {
                return listBoxPaperColor.SelectedItems.Cast<string>().ToList();
            }
        }

        #endregion

        #region Constructors
        public CardLevelDetailForm(List<string> allPaperColors = null, List<string> selectedPaperColors = null)
        {
            InitializeComponent();

            // Create and assign the form's idle event
            AcceptButton = btnAccept;
            CancelButton = btnCancel;

            // RALLY US4547
            listBoxPaperColor.Visible = lblPaperColor.Visible = (allPaperColors != null);
            listBoxPaperColor.Items.Clear();
            if (allPaperColors != null)
            {
                allPaperColors.Sort();

                foreach (string str in allPaperColors)
                {
                    listBoxPaperColor.Items.Add(str);
                }
                if (selectedPaperColors != null)
                {
                    foreach (string str1 in selectedPaperColors)
                    {
                        listBoxPaperColor.SelectedItems.Add(str1);
                    }
                }
            }
            else
            {
                this.Height = NO_PAPER_HEIGHT;
            }
        }

        #endregion

        #region Helper routines
        /// <summary>
        /// Whether or not the save button should be enabled
        /// </summary>
        private void EnableButtons()
        {
            btnAccept.Enabled = CardLevelName != OriginalItem.CardLevelName
                                || CardLevelMultiplier != OriginalItem.Multiplier
                                || CardLevelColor != OriginalItem.LevelColor
                                || !Enumerable.Equals(SelectedPaperColors, OriginalItem.PaperCardColors);// RALLY US4547
        }
        #endregion

        #region Button event handlers
        private void m_accept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        private void ColorClick(object sender, EventArgs e)
        {
            ColorDialog frmColor = new ColorDialog { Color = btnColor.BackColor };
            if (frmColor.ShowDialog(this) == DialogResult.OK)
            {
                if (frmColor.Color.R == 255 && frmColor.Color.G == 0 && frmColor.Color.B == 255)
                    btnColor.BackColor = Color.FromArgb(255, 1, 255);
                else
                    btnColor.BackColor = frmColor.Color;
                btnColor.Invalidate();
            }
            frmColor.Dispose();
            EnableButtons();
        }

        #region Decimal Field event support
        private void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.Decimal_KeyPress(sender, e);
        }
        private void Decimal_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Helper.Decimal_Validating(sender, e);
            EnableButtons();
        }
        // FIX : limit the field multiplier to values between 0.01 and 163
        private void fieldMultiplier_TextChanged(object sender, EventArgs e)
        {
            decimal result;
            decimal.TryParse(fieldMultiplier.Text, out result);
            bool isOk = result >= (decimal)0.01 && result <= (decimal)163.0; // we have 14 bits of value hence 163.83 rounded down
            fieldMultiplier.BackColor = !isOk ? Color.LightPink : Color.FromArgb(215, 251, 193);
            EnableButtons();
        }
        // END:
        #endregion

        private void fieldName_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        // FIX: DE1907 TA2616 Allow Game Color instead of Level Color
        private Color lastColor;
        private void cbUseGameColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseGameColor.Checked)
            {
                lastColor = btnColor.BackColor;
                btnColor.BackColor = Color.FromArgb(0);
                btnColor.Enabled = false;
            }
            else
            {
                btnColor.BackColor = lastColor;
                btnColor.Enabled = true;
            }
            EnableButtons();
        }
        // END: DE1907 TA2616 Allow Game Color instead of Level Color
        
        /// RALLY US4547
        /// <summary>
        /// Actions that occur when the selected index changes on the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxPaperColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!_updatingList)
            //{
            //    if (comboBoxPaperColor.SelectedIndex != -1)
            //        _selectedPaperColor = (string)comboBoxPaperColor.SelectedItem;
            //    else
            //        _selectedPaperColor = null;
            //}
            EnableButtons();
        }
    }
}