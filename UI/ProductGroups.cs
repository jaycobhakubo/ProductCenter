using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.Shared;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ProductGroups : GradientForm
    {
        public ProductGroups()
        {
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            LoadProductGroups();
        }

        private bool hideInactive = true;
        private List<ProductGroupItem> groups;

        private void LoadProductGroups()
        {
            groups = GetProductGroupMessage.GetList();
            RefreshListViewGroups();
            btnChangeName.Enabled = false;
            btnOnOff.Enabled = false;
        }

        private void SaveProductGroups()
        {
            foreach (var group in groups)
            {
                if (group.IsModified)
                {
                    // FIX : DE3187 handle in use product groups
                    GTIServerReturnCode rc = (GTIServerReturnCode)SetProductGrouptMessage.Save(group);
                    if (rc != GTIServerReturnCode.Success )
                    {
                        string msg = String.Format(Resources.ProductGroupInUse, group.ProdGroupName);
                        MessageForm.Show(msg, "ALERT", MessageFormTypes.OK);
                    }
                    // END: DE3187
                    group.IsModified = false;
                }
            }
        }

        private void RefreshListViewGroups()
        {
            listViewGroups.Items.Clear();
            listViewGroups.BeginUpdate();
            foreach (var group in groups)
            {
                if (hideInactive && !group.IsActive)
                {
                    continue;
                }

                // Add the item to the list
                ListViewItem itmX = listViewGroups.Items.Add(group.ProdGroupName);
                itmX.SubItems.Add(group.IsActive.ToString());
                itmX.Tag = group;
            }
            listViewGroups.EndUpdate();
        }

        private bool ProgramGroupExists(string strProgramName)
        {
            ListViewItem lvi = listViewGroups.FindItemWithText(strProgramName);
            if (lvi != null)
            {
                if (strProgramName.ToLowerInvariant().CompareTo(lvi.Text.ToLowerInvariant()) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetGroupName(string strMode, string initialName)
        {
            bool retry = true;
            TextEntryForm frm = new TextEntryForm { Text = strMode + Resources.ProductGroup, Description = Resources.EnterProductGroupName };
            if (!string.IsNullOrEmpty(initialName)) frm.TextResult = initialName;
            while (retry)
            {
                // Prompt user for program name
                if (frm.ShowDialog(this) != DialogResult.OK)
                {
                    return null;
                }

                // Make sure the program name is unique, if not, make them redo it
                if (ProgramGroupExists(frm.TextResult))
                    MessageForm.Show(this, Resources.ProductGroupExists);
                else
                    retry = false;
            }
            return frm.TextResult;
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            string groupName = GetGroupName(Resources.Add, null);
            if (groupName == null) return;
            ProductGroupItem group = new ProductGroupItem
                                     {IsActive = true, IsModified = true, ProdGroupId = 0, ProdGroupName = groupName};
            groups.Add(group);
            RefreshListViewGroups();
            SelectProductGroup(groupName);
        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
            ProductGroupItem group = (ProductGroupItem)listViewGroups.SelectedItems[0].Tag;
            string groupName = GetGroupName(Resources.Rename, group.ProdGroupName);
            if (groupName == null) return;
            group.IsModified = true;
            group.ProdGroupName = groupName;
            RefreshListViewGroups();
            SelectProductGroup(groupName);
        }

        private void SelectProductGroup(string groupName)
        {
            foreach (ListViewItem item in listViewGroups.Items)
            {
                if (item.Text == groupName)
                {
                    item.Selected = true;
                    return;
                }
            }
            btnChangeName.Enabled = false;
            btnOnOff.Enabled = false;
        }

        private void btnOnOff_Click(object sender, EventArgs e)
        {
            ProductGroupItem group = (ProductGroupItem)listViewGroups.SelectedItems[0].Tag;
            group.IsActive = !group.IsActive;
            group.IsModified = true;
            btnOnOff.Text = (group.IsActive) ? Resources.Deactivate : Resources.Activate;
            RefreshListViewGroups();
            SelectProductGroup(group.ProdGroupName);
        }

        private void listViewGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewGroups.SelectedItems.Count == 0)
            {
                btnChangeName.Enabled = false;
                btnOnOff.Enabled = false;
                return;
            }
            btnChangeName.Enabled = true;
            btnOnOff.Enabled = true;
            ProductGroupItem group = (ProductGroupItem)listViewGroups.SelectedItems[0].Tag;
            btnOnOff.Text = (group.IsActive) ? Resources.Deactivate : Resources.Activate;
        }

        private void btnShowHide_Click(object sender, EventArgs e)
        {
            hideInactive = !hideInactive;
            btnShowHide.Text = (hideInactive) ? Resources.ShowInactive : Resources.HideInactive;
            RefreshListViewGroups();
            btnChangeName.Enabled = false;
            btnOnOff.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            SaveProductGroups();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
