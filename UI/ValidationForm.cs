using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTI.Modules.ProductCenter.Business;
using GTI.Modules.Shared;
using GTI.Modules.Shared.Business;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ValidationForm : GradientForm
    {
        private PackageItem m_currentServerValidationPackage;
        private int m_originalCardCount;
        private int m_originalMaxPerTransactions;
        private ProductCenterSettings m_settings;

        public ValidationForm()
        {
            InitializeComponent();

            var cardCountList = new List<int> { 0, 1, 3, 6 };
            cboCardCount.DataSource = cardCountList;
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //check for null
            if (cboDefaultValidationPackage.SelectedItem == null)
            {
                return;
            }

            //if the same then we do not need to save.
            if (!IsModified())
            {
                return;
            }

            //save default validation message
            SendValidationSettingsMessageToServer();

            //hide save button
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            //update current sever settings
            m_currentServerValidationPackage = (PackageItem)cboDefaultValidationPackage.SelectedItem;
            m_originalCardCount = (int)cboCardCount.SelectedItem;
            m_originalMaxPerTransactions = int.Parse(txtMaxPerSession.Text);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cboDefaultValidationPackage.SelectedItem = m_currentServerValidationPackage;
            cboCardCount.SelectedItem = m_originalCardCount;
            txtMaxPerSession.Text = m_originalMaxPerTransactions.ToString();
        }


        /// <summary>
        /// Handles the SelectedValueChanged event of the cbo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbo_SelectedValueChanged(object sender, EventArgs e)
        {
            //enable save button
            var isModified = IsModified();
            btnSave.Enabled = isModified;
            btnCancel.Enabled = isModified;
        }

        /// <summary>
        /// Handles the KeyPress event of the txtMaxPerSession control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtMaxPerSession_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) ||
                (e.KeyChar == (char)Keys.Back) ||
                e.KeyChar == (char)Keys.Delete) ||
                e.KeyChar == '.')
            {
                e.Handled = true;
            }

            if (txtMaxPerSession.Text.Length > 1 &&
                e.KeyChar != (char)Keys.Back &&
                e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtMaxPerSession control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtMaxPerSession_TextChanged(object sender, EventArgs e)
        {
            //check for empty string
            if (string.IsNullOrEmpty(txtMaxPerSession.Text))
            {
                //disable save button if empty
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                return;
            }

            //check for modified            
            var isModified = IsModified();
            btnSave.Enabled = isModified;
            btnCancel.Enabled = isModified;
        }

        /// <summary>
        /// Determines whether this instance is modified.
        /// </summary>
        /// <returns></returns>
        private bool IsModified()
        {
            int maxTrans;
            //if the same then we do not need to save
            if ((PackageItem)cboDefaultValidationPackage.SelectedItem == m_currentServerValidationPackage &&
                (int)cboCardCount.SelectedItem == m_originalCardCount &&
                int.TryParse(txtMaxPerSession.Text, out maxTrans) &&
                m_originalMaxPerTransactions == maxTrans)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends the default validation message to server.
        /// </summary>
        private void SendValidationSettingsMessageToServer()
        {
            try
            {
                //check for null
                if (cboDefaultValidationPackage.SelectedItem == null)
                {
                    return;
                }

                //send server message to set validation package
                var message = new SetDefaultValidationPackageMessage(((PackageItem)cboDefaultValidationPackage.SelectedItem).PackageId);
                message.Send();

                //Init settings to save
                //Product Validation Card Count
                //Max Validation Per Transaction
                List<SettingValue> arrSettings = new List<SettingValue>();
                SettingValue s = new SettingValue
                {
                    Id = (int) Setting.ProductValidationCardCount,
                    Value = cboCardCount.SelectedItem.ToString()
                };

                arrSettings.Add(s);

                s.Id = (int)Setting.MaxValidationPerTransaction;
                s.Value = txtMaxPerSession.Text;
                arrSettings.Add(s);

                SetSystemSettingsMessage msg = new SetSystemSettingsMessage(arrSettings.ToArray());

                //send validation settings message
                msg.Send();

                //update settings
                m_settings.CardCountValidation = (int)cboCardCount.SelectedItem;
                m_settings.MaxValidationsPerTransaction = int.Parse(txtMaxPerSession.Text);
            }
            catch (Exception ex)
            {
                MessageForm.Show("Unable to set validation settings. " + ex.Message, "Validation", MessageFormTypes.OK, 0);
            }
        }

        /// <summary>
        /// Initializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Initialize(ProductCenterSettings settings)
        {
            m_settings = settings;

            //get list from server
            var message = new GetValidationPackagesMessage();
            message.Send();

            //clear list
            cboDefaultValidationPackage.Items.Clear();

            //create new list
            foreach (var validationPackage in message.ValidationPackages)
            {
                cboDefaultValidationPackage.Items.Add(validationPackage);
            }

            //set default
            m_currentServerValidationPackage = message.DefaultValidationPackage;
            m_originalCardCount = m_settings.CardCountValidation;
            m_originalMaxPerTransactions = m_settings.MaxValidationsPerTransaction;

            cboDefaultValidationPackage.SelectedItem = message.DefaultValidationPackage;
            cboCardCount.SelectedItem = m_settings.CardCountValidation;
            txtMaxPerSession.Text = m_settings.MaxValidationsPerTransaction.ToString();
        }
    }
}
