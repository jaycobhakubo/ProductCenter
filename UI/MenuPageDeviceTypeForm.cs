using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class MenuPageDeviceTypeForm : GradientForm
    {
        #region Private Members

        /// <summary>
        /// The devices that are available to be assigned to sales menu pages
        /// </summary>
        private List<Device> m_availableDevices;

        #endregion

        #region Public Properties

        /// <summary>
        /// The list of devices selected in the UI
        /// </summary>
        public List<Device> SelectedDevices
        {
            get
            {
                List<Device> selectedDevices = new List<Device>();
                if (!cbAllDevices.Checked) // "all" is an empty list
                {
                    foreach (var item in devicesLsBx.CheckedItems)
                        selectedDevices.Add((Device)item);
                }

                return selectedDevices;
            }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="availableDevices"></param>
        public MenuPageDeviceTypeForm(List<Device> availableDevices = null, List<Device> selectedDevices = null)
        {
            m_availableDevices = availableDevices;

            InitializeComponent();

            Application.Idle += OnIdle;
            AcceptButton = btnAccept;
            CancelButton = btnCancel;

            if (m_availableDevices == null)
            {
                m_availableDevices = new List<Device>();
                m_availableDevices.Add(Device.POS);
                m_availableDevices.Add(Device.POSManagement);
                m_availableDevices.Add(Device.POSPortable);
                m_availableDevices.Add(Device.VLTBingoKiosk);
                m_availableDevices.Add(Device.AdvancedPOSKiosk);
                m_availableDevices.Add(Device.SimplePOSKiosk);
                m_availableDevices.Add(Device.BuyAgainKiosk);
                m_availableDevices.Add(Device.HybridKiosk);
            }

            try
            {
                List<Device> activeDevices = new List<Device>(GetDeviceTypeDataMessage.GetDeviceTypeData());
                List<Device> copyList = new List<Device>(m_availableDevices);

                foreach (var device in copyList) // can't remove from the list we're iterating through
                {
                    if (!activeDevices.Contains(device))
                        m_availableDevices.Remove(device);
                }
            }
            catch (Exception ex)
            {
                MessageForm.Show("Error looking up devices: " + ex.Message);
            }

            foreach (var device in m_availableDevices)
                devicesLsBx.Items.Add(device);
            if (selectedDevices != null)
                foreach (var device in selectedDevices)
                    if(devicesLsBx.Items.Contains(device)) // if it's an invalid item, don't try to select it
                        devicesLsBx.SetItemChecked(devicesLsBx.Items.IndexOf(device), true);

            if (selectedDevices == null || selectedDevices.Count == 0)
                cbAllDevices.Checked = true;
        }
        
        #region Events

        private void MenuPageDeviceTypeForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Actions that occur when the form is idle. Checks to see if the "accept" button should be enabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnIdle(object sender, EventArgs e)
        {
            btnAccept.Enabled = cbAllDevices.Checked || devicesLsBx.CheckedItems.Count != 0;
        }

        /// <summary>
        /// Actions that occur when the "all devices" checkbox's status changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAllDevices_CheckedChanged(object sender, EventArgs e)
        {
            devicesLsBx.Enabled = !cbAllDevices.Checked;
            for (int i = 0; i < devicesLsBx.Items.Count; i++)
                devicesLsBx.SetItemChecked(i, cbAllDevices.Checked);
        }

        /// <summary>
        /// Actions that occur when the "cancel" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Actions that occur when the "Accept" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            DialogResult = DialogResult.OK;

            Close();
        }
        
        /// <summary>
        /// Actions that occur when an item is clicked. Automatically changes the check state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devicesLsBx_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs) // should always be this..
            {
                MouseEventArgs args = e as MouseEventArgs;
                int selectedIndex = devicesLsBx.SelectedIndex;
                devicesLsBx.SelectedIndex = -1; // for some reason, if the device is already selected, it's not fond of changing it's checked status...
                if (selectedIndex >= 0)
                {
                    var rect = devicesLsBx.GetItemRectangle(selectedIndex);
                    if (rect.Contains(args.Location))
                    {
                        bool curSelected = devicesLsBx.GetItemChecked(selectedIndex);
                        devicesLsBx.SetItemChecked(selectedIndex, !curSelected);
                    }
                }
            }
        }

        #endregion


    }
}
