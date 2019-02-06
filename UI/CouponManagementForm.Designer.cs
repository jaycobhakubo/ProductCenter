namespace GTI.Modules.ProductCenter.UI
{
    partial class CouponManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouponManagementForm));
            this.imgbtnAdd = new GTI.Controls.ImageButton();
            this.dtgviewCoupon = new System.Windows.Forms.DataGridView();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gtiListViewCoupon = new GTI.Controls.GTIListView();
            this.NameClm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartDateClm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndDateClm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AwardClm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuCoupon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imgbtnAwardGroup = new GTI.Controls.ImageButton();
            this.imgbtnExpiredComp = new GTI.Controls.ImageButton();
            this.imgbtnUpdate = new GTI.Controls.ImageButton();
            this.imgbtnDelete = new GTI.Controls.ImageButton();
            this.imgbtnAwardToPlayer = new GTI.Controls.ImageButton();
            this.imgbtnAwardToAllPlayers = new GTI.Controls.ImageButton();
            this.lblUnableToDeleteCoupon = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblSavedSuccessfully = new System.Windows.Forms.Label();
            this.menuStripCoupon = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCouponToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TypeClm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueClm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.dtgviewCoupon)).BeginInit();
            this.contextMenuCoupon.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStripCoupon.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgbtnAdd
            // 
            this.imgbtnAdd.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnAdd, "imgbtnAdd");
            this.imgbtnAdd.ForeColor = System.Drawing.Color.Black;
            this.imgbtnAdd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnAdd.Name = "imgbtnAdd";
            this.imgbtnAdd.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.imgbtnAdd.Click += new System.EventHandler(this.AddCouponClick);
            this.imgbtnAdd.Enter += new System.EventHandler(this.imgbtnAdd_Enter);
            // 
            // dtgviewCoupon
            // 
            this.dtgviewCoupon.AllowUserToAddRows = false;
            this.dtgviewCoupon.AllowUserToDeleteRows = false;
            this.dtgviewCoupon.AllowUserToOrderColumns = true;
            this.dtgviewCoupon.AllowUserToResizeColumns = false;
            this.dtgviewCoupon.AllowUserToResizeRows = false;
            this.dtgviewCoupon.BackgroundColor = System.Drawing.Color.White;
            this.dtgviewCoupon.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            resources.ApplyResources(this.dtgviewCoupon, "dtgviewCoupon");
            this.dtgviewCoupon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgviewCoupon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cName,
            this.StartDate,
            this.EndDate,
            this.Value});
            this.dtgviewCoupon.MultiSelect = false;
            this.dtgviewCoupon.Name = "dtgviewCoupon";
            this.dtgviewCoupon.ReadOnly = true;
            this.dtgviewCoupon.RowHeadersVisible = false;
            this.dtgviewCoupon.RowTemplate.Height = 30;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cName.DataPropertyName = "cName";
            resources.ApplyResources(this.cName, "cName");
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // StartDate
            // 
            this.StartDate.DataPropertyName = "StartDate";
            resources.ApplyResources(this.StartDate, "StartDate");
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            // 
            // EndDate
            // 
            this.EndDate.DataPropertyName = "EndDate";
            resources.ApplyResources(this.EndDate, "EndDate");
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            resources.ApplyResources(this.Value, "Value");
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // gtiListViewCoupon
            // 
            this.gtiListViewCoupon.AllowEraseBackground = true;
            this.gtiListViewCoupon.CheckBoxes = true;
            this.gtiListViewCoupon.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameClm,
            this.TypeClm,
            this.ValueClm,
            this.StartDateClm,
            this.EndDateClm,
            this.AwardClm});
            this.gtiListViewCoupon.ContextMenuStrip = this.contextMenuCoupon;
            resources.ApplyResources(this.gtiListViewCoupon, "gtiListViewCoupon");
            this.gtiListViewCoupon.FullRowSelect = true;
            this.gtiListViewCoupon.GridLines = true;
            this.gtiListViewCoupon.MultiSelect = false;
            this.gtiListViewCoupon.Name = "gtiListViewCoupon";
            this.gtiListViewCoupon.OwnerDraw = true;
            this.gtiListViewCoupon.SelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue;
            this.gtiListViewCoupon.SortColumn = 0;
            this.gtiListViewCoupon.UseCompatibleStateImageBehavior = false;
            this.gtiListViewCoupon.UseOwnerDrawnSubItemMethod = true;
            this.gtiListViewCoupon.View = System.Windows.Forms.View.Details;
            this.gtiListViewCoupon.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.gtiListViewCoupon_DrawSubItem);
            this.gtiListViewCoupon.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.gtiListViewCoupon_ItemSelectionChanged);
            this.gtiListViewCoupon.DoubleClick += new System.EventHandler(this.gtiListViewCoupon_DoubleClick);
            this.gtiListViewCoupon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gtiListViewCoupon_KeyDown);
            // 
            // NameClm
            // 
            resources.ApplyResources(this.NameClm, "NameClm");
            // 
            // StartDateClm
            // 
            resources.ApplyResources(this.StartDateClm, "StartDateClm");
            // 
            // EndDateClm
            // 
            resources.ApplyResources(this.EndDateClm, "EndDateClm");
            // 
            // AwardClm
            // 
            resources.ApplyResources(this.AwardClm, "AwardClm");
            // 
            // contextMenuCoupon
            // 
            resources.ApplyResources(this.contextMenuCoupon, "contextMenuCoupon");
            this.contextMenuCoupon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.delToolStripMenuItem});
            this.contextMenuCoupon.Name = "contextMenuCoupon";
            this.contextMenuCoupon.Click += new System.EventHandler(this.contextMenuCoupon_Click);
            this.contextMenuCoupon.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.contextMenuCoupon_PreviewKeyDown);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            this.addToolStripMenuItem.Click += new System.EventHandler(this.AddCouponClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem2_Click);
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            resources.ApplyResources(this.delToolStripMenuItem, "delToolStripMenuItem");
            this.delToolStripMenuItem.Click += new System.EventHandler(this.imgbtnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.lblUnableToDeleteCoupon);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.lblSavedSuccessfully);
            this.groupBox1.Controls.Add(this.gtiListViewCoupon);
            this.groupBox1.Controls.Add(this.dtgviewCoupon);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.imgbtnAwardGroup);
            this.panel1.Controls.Add(this.imgbtnExpiredComp);
            this.panel1.Controls.Add(this.imgbtnUpdate);
            this.panel1.Controls.Add(this.imgbtnAdd);
            this.panel1.Controls.Add(this.imgbtnDelete);
            this.panel1.Controls.Add(this.imgbtnAwardToPlayer);
            this.panel1.Controls.Add(this.imgbtnAwardToAllPlayers);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // imgbtnAwardGroup
            // 
            this.imgbtnAwardGroup.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnAwardGroup, "imgbtnAwardGroup");
            this.imgbtnAwardGroup.ForeColor = System.Drawing.Color.Black;
            this.imgbtnAwardGroup.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnAwardGroup.Name = "imgbtnAwardGroup";
            this.imgbtnAwardGroup.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.imgbtnAwardGroup.Click += new System.EventHandler(this.imgbtnAwardGroup_Click);
            // 
            // imgbtnExpiredComp
            // 
            this.imgbtnExpiredComp.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnExpiredComp, "imgbtnExpiredComp");
            this.imgbtnExpiredComp.ForeColor = System.Drawing.Color.Black;
            this.imgbtnExpiredComp.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnExpiredComp.Name = "imgbtnExpiredComp";
            this.imgbtnExpiredComp.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.imgbtnExpiredComp.Click += new System.EventHandler(this.imgbtnExpiredComp_Click);
            // 
            // imgbtnUpdate
            // 
            this.imgbtnUpdate.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnUpdate, "imgbtnUpdate");
            this.imgbtnUpdate.ForeColor = System.Drawing.Color.Black;
            this.imgbtnUpdate.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnUpdate.Name = "imgbtnUpdate";
            this.imgbtnUpdate.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.imgbtnUpdate.Click += new System.EventHandler(this.editToolStripMenuItem2_Click);
            // 
            // imgbtnDelete
            // 
            this.imgbtnDelete.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnDelete, "imgbtnDelete");
            this.imgbtnDelete.ForeColor = System.Drawing.Color.Black;
            this.imgbtnDelete.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnDelete.Name = "imgbtnDelete";
            this.imgbtnDelete.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.imgbtnDelete.Click += new System.EventHandler(this.imgbtnDelete_Click);
            // 
            // imgbtnAwardToPlayer
            // 
            this.imgbtnAwardToPlayer.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnAwardToPlayer, "imgbtnAwardToPlayer");
            this.imgbtnAwardToPlayer.ForeColor = System.Drawing.Color.Black;
            this.imgbtnAwardToPlayer.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnAwardToPlayer.Name = "imgbtnAwardToPlayer";
            this.imgbtnAwardToPlayer.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.imgbtnAwardToPlayer.Click += new System.EventHandler(this.imageButton1_Click_1);
            // 
            // imgbtnAwardToAllPlayers
            // 
            this.imgbtnAwardToAllPlayers.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnAwardToAllPlayers, "imgbtnAwardToAllPlayers");
            this.imgbtnAwardToAllPlayers.ForeColor = System.Drawing.Color.Black;
            this.imgbtnAwardToAllPlayers.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnAwardToAllPlayers.Name = "imgbtnAwardToAllPlayers";
            this.imgbtnAwardToAllPlayers.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.imgbtnAwardToAllPlayers.Click += new System.EventHandler(this.imgbtnAwardToAllPlayer_Click);
            // 
            // lblUnableToDeleteCoupon
            // 
            resources.ApplyResources(this.lblUnableToDeleteCoupon, "lblUnableToDeleteCoupon");
            this.lblUnableToDeleteCoupon.BackColor = System.Drawing.Color.Transparent;
            this.lblUnableToDeleteCoupon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblUnableToDeleteCoupon.Name = "lblUnableToDeleteCoupon";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblSavedSuccessfully
            // 
            resources.ApplyResources(this.lblSavedSuccessfully, "lblSavedSuccessfully");
            this.lblSavedSuccessfully.BackColor = System.Drawing.Color.Transparent;
            this.lblSavedSuccessfully.ForeColor = System.Drawing.Color.Black;
            this.lblSavedSuccessfully.Name = "lblSavedSuccessfully";
            // 
            // menuStripCoupon
            // 
            this.menuStripCoupon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1});
            resources.ApplyResources(this.menuStripCoupon, "menuStripCoupon");
            this.menuStripCoupon.Name = "menuStripCoupon";
            this.menuStripCoupon.Enter += new System.EventHandler(this.menuStripCoupon_Enter);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.editToolStripMenuItem2,
            this.deleteCouponToolStripMenuItem});
            resources.ApplyResources(this.editToolStripMenuItem1, "editToolStripMenuItem1");
            this.editToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.editToolStripMenuItem1.MergeIndex = 1;
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            resources.ApplyResources(this.addToolStripMenuItem1, "addToolStripMenuItem1");
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.AddCouponClick);
            // 
            // editToolStripMenuItem2
            // 
            this.editToolStripMenuItem2.Name = "editToolStripMenuItem2";
            resources.ApplyResources(this.editToolStripMenuItem2, "editToolStripMenuItem2");
            this.editToolStripMenuItem2.Click += new System.EventHandler(this.editToolStripMenuItem2_Click);
            // 
            // deleteCouponToolStripMenuItem
            // 
            this.deleteCouponToolStripMenuItem.Name = "deleteCouponToolStripMenuItem";
            resources.ApplyResources(this.deleteCouponToolStripMenuItem, "deleteCouponToolStripMenuItem");
            this.deleteCouponToolStripMenuItem.Click += new System.EventHandler(this.imgbtnDelete_Click);
            // 
            // TypeClm
            // 
            resources.ApplyResources(this.TypeClm, "TypeClm");
            // 
            // ValueClm
            // 
            resources.ApplyResources(this.ValueClm, "ValueClm");
            // 
            // CouponManagementForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.menuStripCoupon);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStripCoupon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouponManagementForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Activated += new System.EventHandler(this.CouponManagementForm_Activated);
            this.Load += new System.EventHandler(this.CouponManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgviewCoupon)).EndInit();
            this.contextMenuCoupon.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStripCoupon.ResumeLayout(false);
            this.menuStripCoupon.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ImageButton imgbtnAdd;
        public System.Windows.Forms.DataGridView dtgviewCoupon;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private Controls.GTIListView gtiListViewCoupon;
        public System.Windows.Forms.ColumnHeader NameClm;
        public System.Windows.Forms.ColumnHeader StartDateClm;
        public System.Windows.Forms.ColumnHeader EndDateClm;
        public System.Windows.Forms.ColumnHeader AwardClm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuCoupon;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStripCoupon;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem2;
        private Controls.ImageButton imgbtnAwardToPlayer;
        public System.Windows.Forms.Label lblSavedSuccessfully;
        private System.Windows.Forms.CheckBox checkBox1;
        private Controls.ImageButton imgbtnAwardToAllPlayers;
        private Controls.ImageButton imgbtnDelete;
        public System.Windows.Forms.Label lblUnableToDeleteCoupon;
        private System.Windows.Forms.ToolStripMenuItem delToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCouponToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private Controls.ImageButton imgbtnUpdate;
        private Controls.ImageButton imgbtnExpiredComp;
        private Controls.ImageButton imgbtnAwardGroup;
        private System.Windows.Forms.ColumnHeader TypeClm;
        private System.Windows.Forms.ColumnHeader ValueClm;


    }
}