namespace GTI.Modules.ProductCenter.UI
{
    partial class MenusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenusForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MenuTreeView = new System.Windows.Forms.TreeView();
            this.formsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignPageToDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastePageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_chkShowDaily = new System.Windows.Forms.CheckBox();
            this.DeviceTypeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.DeviceInfoLabel = new System.Windows.Forms.Label();
            this.DragInfoLabel = new System.Windows.Forms.Label();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.MenuSetupGroupBox = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMenuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addPageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.assignPageToDeviceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pastePageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.formsContextMenu.SuspendLayout();
            this.MenuSetupGroupBox.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.buttonContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.MenuTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.m_chkShowDaily);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Panel2.Controls.Add(this.DeviceTypeLinkLabel);
            this.splitContainer1.Panel2.Controls.Add(this.DeviceInfoLabel);
            this.splitContainer1.Panel2.Controls.Add(this.DragInfoLabel);
            this.splitContainer1.Panel2.Controls.Add(this.ButtonPanel);
            this.splitContainer1.Panel2.Controls.Add(this.splitter1);
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.TabStop = false;
            // 
            // MenuTreeView
            // 
            this.MenuTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.MenuTreeView.ContextMenuStrip = this.formsContextMenu;
            resources.ApplyResources(this.MenuTreeView, "MenuTreeView");
            this.MenuTreeView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.MenuTreeView.HideSelection = false;
            this.MenuTreeView.Name = "MenuTreeView";
            this.MenuTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("MenuTreeView.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("MenuTreeView.Nodes1")))});
            this.MenuTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AfterSelectMenuList);
            this.MenuTreeView.DoubleClick += new System.EventHandler(this.EditMenuClick);
            this.MenuTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_menuList_KeyDown);
            // 
            // formsContextMenu
            // 
            this.formsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMenuToolStripMenuItem,
            this.editMenuToolStripMenuItem,
            this.copyMenuToolStripMenuItem,
            this.pasteMenuToolStripMenuItem,
            this.deleteMenuToolStripMenuItem,
            this.toolStripSeparator1,
            this.addPageToolStripMenuItem,
            this.assignPageToDeviceToolStripMenuItem,
            this.copyPageToolStripMenuItem,
            this.pastePageToolStripMenuItem,
            this.deletePageToolStripMenuItem});
            this.formsContextMenu.Name = "contextMenuMenus";
            resources.ApplyResources(this.formsContextMenu, "formsContextMenu");
            // 
            // addMenuToolStripMenuItem
            // 
            this.addMenuToolStripMenuItem.Name = "addMenuToolStripMenuItem";
            resources.ApplyResources(this.addMenuToolStripMenuItem, "addMenuToolStripMenuItem");
            this.addMenuToolStripMenuItem.Click += new System.EventHandler(this.AddMenuClick);
            // 
            // editMenuToolStripMenuItem
            // 
            this.editMenuToolStripMenuItem.Name = "editMenuToolStripMenuItem";
            resources.ApplyResources(this.editMenuToolStripMenuItem, "editMenuToolStripMenuItem");
            this.editMenuToolStripMenuItem.Click += new System.EventHandler(this.EditMenuClick);
            // 
            // copyMenuToolStripMenuItem
            // 
            this.copyMenuToolStripMenuItem.Name = "copyMenuToolStripMenuItem";
            resources.ApplyResources(this.copyMenuToolStripMenuItem, "copyMenuToolStripMenuItem");
            this.copyMenuToolStripMenuItem.Click += new System.EventHandler(this.CopyMenuClick);
            // 
            // pasteMenuToolStripMenuItem
            // 
            this.pasteMenuToolStripMenuItem.Name = "pasteMenuToolStripMenuItem";
            resources.ApplyResources(this.pasteMenuToolStripMenuItem, "pasteMenuToolStripMenuItem");
            this.pasteMenuToolStripMenuItem.Click += new System.EventHandler(this.PasteMenuClick);
            // 
            // deleteMenuToolStripMenuItem
            // 
            this.deleteMenuToolStripMenuItem.Name = "deleteMenuToolStripMenuItem";
            resources.ApplyResources(this.deleteMenuToolStripMenuItem, "deleteMenuToolStripMenuItem");
            this.deleteMenuToolStripMenuItem.Click += new System.EventHandler(this.DeleteMenuClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // addPageToolStripMenuItem
            // 
            this.addPageToolStripMenuItem.Name = "addPageToolStripMenuItem";
            resources.ApplyResources(this.addPageToolStripMenuItem, "addPageToolStripMenuItem");
            this.addPageToolStripMenuItem.Click += new System.EventHandler(this.AddPageClick);
            // 
            // assignPageToDeviceToolStripMenuItem
            // 
            this.assignPageToDeviceToolStripMenuItem.Name = "assignPageToDeviceToolStripMenuItem";
            resources.ApplyResources(this.assignPageToDeviceToolStripMenuItem, "assignPageToDeviceToolStripMenuItem");
            this.assignPageToDeviceToolStripMenuItem.Click += new System.EventHandler(this.AssignPageToDevice);
            // 
            // copyPageToolStripMenuItem
            // 
            this.copyPageToolStripMenuItem.Name = "copyPageToolStripMenuItem";
            resources.ApplyResources(this.copyPageToolStripMenuItem, "copyPageToolStripMenuItem");
            this.copyPageToolStripMenuItem.Click += new System.EventHandler(this.CopyPageClick);
            // 
            // pastePageToolStripMenuItem
            // 
            this.pastePageToolStripMenuItem.Name = "pastePageToolStripMenuItem";
            resources.ApplyResources(this.pastePageToolStripMenuItem, "pastePageToolStripMenuItem");
            this.pastePageToolStripMenuItem.Click += new System.EventHandler(this.PastePageClick);
            // 
            // deletePageToolStripMenuItem
            // 
            this.deletePageToolStripMenuItem.Name = "deletePageToolStripMenuItem";
            resources.ApplyResources(this.deletePageToolStripMenuItem, "deletePageToolStripMenuItem");
            this.deletePageToolStripMenuItem.Click += new System.EventHandler(this.DeletePageClick);
            // 
            // m_chkShowDaily
            // 
            resources.ApplyResources(this.m_chkShowDaily, "m_chkShowDaily");
            this.m_chkShowDaily.ForeColor = System.Drawing.Color.Black;
            this.m_chkShowDaily.Name = "m_chkShowDaily";
            this.m_chkShowDaily.UseVisualStyleBackColor = true;
            this.m_chkShowDaily.CheckedChanged += new System.EventHandler(this.ShowDaily_CheckedChanged);
            // 
            // DeviceTypeLinkLabel
            // 
            resources.ApplyResources(this.DeviceTypeLinkLabel, "DeviceTypeLinkLabel");
            this.DeviceTypeLinkLabel.Name = "DeviceTypeLinkLabel";
            this.DeviceTypeLinkLabel.TabStop = true;
            this.DeviceTypeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // DeviceInfoLabel
            // 
            resources.ApplyResources(this.DeviceInfoLabel, "DeviceInfoLabel");
            this.DeviceInfoLabel.Name = "DeviceInfoLabel";
            // 
            // DragInfoLabel
            // 
            resources.ApplyResources(this.DragInfoLabel, "DragInfoLabel");
            this.DragInfoLabel.Name = "DragInfoLabel";
            // 
            // ButtonPanel
            // 
            resources.ApplyResources(this.ButtonPanel, "ButtonPanel");
            this.ButtonPanel.BackColor = System.Drawing.SystemColors.Window;
            this.ButtonPanel.Name = "ButtonPanel";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(222)))), ((int)(((byte)(237)))));
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // MenuSetupGroupBox
            // 
            this.MenuSetupGroupBox.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.MenuSetupGroupBox, "MenuSetupGroupBox");
            this.MenuSetupGroupBox.Controls.Add(this.splitContainer1);
            this.MenuSetupGroupBox.ForeColor = System.Drawing.Color.Black;
            this.MenuSetupGroupBox.Name = "MenuSetupGroupBox";
            this.MenuSetupGroupBox.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(222)))), ((int)(((byte)(237)))));
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.mainMenuStrip.Name = "mainMenuStrip";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMenuToolStripMenuItem1,
            this.editMenuToolStripMenuItem1,
            this.copyMenuToolStripMenuItem1,
            this.pasteMenuToolStripMenuItem1,
            this.deleteMenuToolStripMenuItem1,
            this.toolStripSeparator2,
            this.addPageToolStripMenuItem1,
            this.assignPageToDeviceToolStripMenuItem1,
            this.copyPageToolStripMenuItem1,
            this.pastePageToolStripMenuItem1,
            this.deletePageToolStripMenuItem1});
            this.editToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.editToolStripMenuItem.MergeIndex = 1;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // addMenuToolStripMenuItem1
            // 
            this.addMenuToolStripMenuItem1.Name = "addMenuToolStripMenuItem1";
            resources.ApplyResources(this.addMenuToolStripMenuItem1, "addMenuToolStripMenuItem1");
            this.addMenuToolStripMenuItem1.Click += new System.EventHandler(this.AddMenuClick);
            // 
            // editMenuToolStripMenuItem1
            // 
            this.editMenuToolStripMenuItem1.Name = "editMenuToolStripMenuItem1";
            resources.ApplyResources(this.editMenuToolStripMenuItem1, "editMenuToolStripMenuItem1");
            this.editMenuToolStripMenuItem1.Click += new System.EventHandler(this.EditMenuClick);
            // 
            // copyMenuToolStripMenuItem1
            // 
            this.copyMenuToolStripMenuItem1.Name = "copyMenuToolStripMenuItem1";
            resources.ApplyResources(this.copyMenuToolStripMenuItem1, "copyMenuToolStripMenuItem1");
            this.copyMenuToolStripMenuItem1.Click += new System.EventHandler(this.CopyMenuClick);
            // 
            // pasteMenuToolStripMenuItem1
            // 
            this.pasteMenuToolStripMenuItem1.Name = "pasteMenuToolStripMenuItem1";
            resources.ApplyResources(this.pasteMenuToolStripMenuItem1, "pasteMenuToolStripMenuItem1");
            this.pasteMenuToolStripMenuItem1.Click += new System.EventHandler(this.PasteMenuClick);
            // 
            // deleteMenuToolStripMenuItem1
            // 
            this.deleteMenuToolStripMenuItem1.Name = "deleteMenuToolStripMenuItem1";
            resources.ApplyResources(this.deleteMenuToolStripMenuItem1, "deleteMenuToolStripMenuItem1");
            this.deleteMenuToolStripMenuItem1.Click += new System.EventHandler(this.DeleteMenuClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // addPageToolStripMenuItem1
            // 
            this.addPageToolStripMenuItem1.Name = "addPageToolStripMenuItem1";
            resources.ApplyResources(this.addPageToolStripMenuItem1, "addPageToolStripMenuItem1");
            this.addPageToolStripMenuItem1.Click += new System.EventHandler(this.AddPageClick);
            // 
            // assignPageToDeviceToolStripMenuItem1
            // 
            this.assignPageToDeviceToolStripMenuItem1.Name = "assignPageToDeviceToolStripMenuItem1";
            resources.ApplyResources(this.assignPageToDeviceToolStripMenuItem1, "assignPageToDeviceToolStripMenuItem1");
            this.assignPageToDeviceToolStripMenuItem1.Click += new System.EventHandler(this.AssignPageToDevice);
            // 
            // copyPageToolStripMenuItem1
            // 
            this.copyPageToolStripMenuItem1.Name = "copyPageToolStripMenuItem1";
            resources.ApplyResources(this.copyPageToolStripMenuItem1, "copyPageToolStripMenuItem1");
            this.copyPageToolStripMenuItem1.Click += new System.EventHandler(this.CopyPageClick);
            // 
            // pastePageToolStripMenuItem1
            // 
            this.pastePageToolStripMenuItem1.Name = "pastePageToolStripMenuItem1";
            resources.ApplyResources(this.pastePageToolStripMenuItem1, "pastePageToolStripMenuItem1");
            this.pastePageToolStripMenuItem1.Click += new System.EventHandler(this.PastePageClick);
            // 
            // deletePageToolStripMenuItem1
            // 
            this.deletePageToolStripMenuItem1.Name = "deletePageToolStripMenuItem1";
            resources.ApplyResources(this.deletePageToolStripMenuItem1, "deletePageToolStripMenuItem1");
            this.deletePageToolStripMenuItem1.Click += new System.EventHandler(this.DeletePageClick);
            // 
            // buttonContextMenu
            // 
            this.buttonContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButtonToolStripMenuItem,
            this.editButtonToolStripMenuItem,
            this.copyButtonToolStripMenuItem,
            this.pasteButtonToolStripMenuItem,
            this.deleteButtonToolStripMenuItem});
            this.buttonContextMenu.Name = "contextMenuButton";
            resources.ApplyResources(this.buttonContextMenu, "buttonContextMenu");
            // 
            // addButtonToolStripMenuItem
            // 
            this.addButtonToolStripMenuItem.Name = "addButtonToolStripMenuItem";
            resources.ApplyResources(this.addButtonToolStripMenuItem, "addButtonToolStripMenuItem");
            this.addButtonToolStripMenuItem.Click += new System.EventHandler(this.addButtonToolStripMenuItem_Click);
            // 
            // editButtonToolStripMenuItem
            // 
            this.editButtonToolStripMenuItem.Name = "editButtonToolStripMenuItem";
            resources.ApplyResources(this.editButtonToolStripMenuItem, "editButtonToolStripMenuItem");
            this.editButtonToolStripMenuItem.Click += new System.EventHandler(this.editButtonToolStripMenuItem_Click);
            // 
            // copyButtonToolStripMenuItem
            // 
            this.copyButtonToolStripMenuItem.Name = "copyButtonToolStripMenuItem";
            resources.ApplyResources(this.copyButtonToolStripMenuItem, "copyButtonToolStripMenuItem");
            this.copyButtonToolStripMenuItem.Click += new System.EventHandler(this.copyButtonToolStripMenuItem_Click);
            // 
            // pasteButtonToolStripMenuItem
            // 
            this.pasteButtonToolStripMenuItem.Name = "pasteButtonToolStripMenuItem";
            resources.ApplyResources(this.pasteButtonToolStripMenuItem, "pasteButtonToolStripMenuItem");
            this.pasteButtonToolStripMenuItem.Click += new System.EventHandler(this.pasteButtonToolStripMenuItem_Click);
            // 
            // deleteButtonToolStripMenuItem
            // 
            this.deleteButtonToolStripMenuItem.Name = "deleteButtonToolStripMenuItem";
            resources.ApplyResources(this.deleteButtonToolStripMenuItem, "deleteButtonToolStripMenuItem");
            this.deleteButtonToolStripMenuItem.Click += new System.EventHandler(this.deleteButtonToolStripMenuItem_Click);
            // 
            // MenusForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.MenuSetupGroupBox);
            this.Controls.Add(this.mainMenuStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MenusForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.formsContextMenu.ResumeLayout(false);
            this.MenuSetupGroupBox.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.buttonContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox MenuSetupGroupBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView MenuTreeView;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label DragInfoLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip formsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastePageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMenuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editMenuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyMenuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasteMenuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addPageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyPageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pastePageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deletePageToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip buttonContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignPageToDeviceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem assignPageToDeviceToolStripMenuItem;
        private System.Windows.Forms.LinkLabel DeviceTypeLinkLabel;
        private System.Windows.Forms.Label DeviceInfoLabel;
        private System.Windows.Forms.CheckBox m_chkShowDaily;
    }
}