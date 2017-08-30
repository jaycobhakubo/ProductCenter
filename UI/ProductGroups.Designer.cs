namespace GTI.Modules.ProductCenter.UI
{
    partial class ProductGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductGroups));
            this.listViewGroups = new GTI.Controls.GTIListView();
            this.columnGroup = new System.Windows.Forms.ColumnHeader();
            this.columnActive = new System.Windows.Forms.ColumnHeader();
            this.btnChangeName = new GTI.Controls.ImageButton();
            this.btnOnOff = new GTI.Controls.ImageButton();
            this.btnNewGroup = new GTI.Controls.ImageButton();
            this.btnShowHide = new GTI.Controls.ImageButton();
            this.btnDone = new GTI.Controls.ImageButton();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // listViewGroups
            // 
            this.listViewGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnGroup,
            this.columnActive});
            this.listViewGroups.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.listViewGroups.FullRowSelect = true;
            this.listViewGroups.HideSelection = false;
            this.listViewGroups.Location = new System.Drawing.Point(100, 0);
            this.listViewGroups.MultiSelect = false;
            this.listViewGroups.Name = "listViewGroups";
            this.listViewGroups.OwnerDraw = true;
            this.listViewGroups.SelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue;
            this.listViewGroups.Size = new System.Drawing.Size(378, 227);
            this.listViewGroups.SortColumn = 0;
            this.listViewGroups.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewGroups.TabIndex = 0;
            this.listViewGroups.UseCompatibleStateImageBehavior = false;
            this.listViewGroups.View = System.Windows.Forms.View.Details;
            this.listViewGroups.SelectedIndexChanged += new System.EventHandler(this.listViewGroups_SelectedIndexChanged);
            this.listViewGroups.DoubleClick += new System.EventHandler(this.btnChangeName_Click);
            // 
            // columnGroup
            // 
            this.columnGroup.Tag = "alpha";
            this.columnGroup.Text = "Group";
            this.columnGroup.Width = 280;
            // 
            // columnActive
            // 
            this.columnActive.Tag = "alpha";
            this.columnActive.Text = "Active";
            this.columnActive.Width = 78;
            // 
            // btnChangeName
            // 
            this.btnChangeName.BackColor = System.Drawing.Color.Transparent;
            this.btnChangeName.FocusColor = System.Drawing.Color.Black;
            this.btnChangeName.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
            this.btnChangeName.ForeColor = System.Drawing.Color.Black;
            this.btnChangeName.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnChangeName.ImageNormal")));
            this.btnChangeName.ImagePressed = ((System.Drawing.Image)(resources.GetObject("btnChangeName.ImagePressed")));
            this.btnChangeName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChangeName.Location = new System.Drawing.Point(6, 63);
            this.btnChangeName.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnChangeName.Name = "btnChangeName";
            this.btnChangeName.Size = new System.Drawing.Size(88, 34);
            this.btnChangeName.TabIndex = 5;
            this.btnChangeName.Text = "&Rename";
            this.btnChangeName.UseVisualStyleBackColor = false;
            this.btnChangeName.Click += new System.EventHandler(this.btnChangeName_Click);
            // 
            // btnOnOff
            // 
            this.btnOnOff.BackColor = System.Drawing.Color.Transparent;
            this.btnOnOff.FocusColor = System.Drawing.Color.Black;
            this.btnOnOff.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
            this.btnOnOff.ForeColor = System.Drawing.Color.Black;
            this.btnOnOff.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnOnOff.ImageNormal")));
            this.btnOnOff.ImagePressed = ((System.Drawing.Image)(resources.GetObject("btnOnOff.ImagePressed")));
            this.btnOnOff.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOnOff.Location = new System.Drawing.Point(6, 113);
            this.btnOnOff.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnOnOff.Name = "btnOnOff";
            this.btnOnOff.Size = new System.Drawing.Size(88, 34);
            this.btnOnOff.TabIndex = 4;
            this.btnOnOff.Text = "Deacti&vate";
            this.btnOnOff.UseVisualStyleBackColor = false;
            this.btnOnOff.Click += new System.EventHandler(this.btnOnOff_Click);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.BackColor = System.Drawing.Color.Transparent;
            this.btnNewGroup.FocusColor = System.Drawing.Color.Black;
            this.btnNewGroup.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
            this.btnNewGroup.ForeColor = System.Drawing.Color.Black;
            this.btnNewGroup.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnNewGroup.ImageNormal")));
            this.btnNewGroup.ImagePressed = ((System.Drawing.Image)(resources.GetObject("btnNewGroup.ImagePressed")));
            this.btnNewGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNewGroup.Location = new System.Drawing.Point(6, 12);
            this.btnNewGroup.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(88, 34);
            this.btnNewGroup.TabIndex = 3;
            this.btnNewGroup.Text = "&New";
            this.btnNewGroup.UseVisualStyleBackColor = false;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // btnShowHide
            // 
            this.btnShowHide.BackColor = System.Drawing.Color.Transparent;
            this.btnShowHide.FocusColor = System.Drawing.Color.Black;
            this.btnShowHide.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
            this.btnShowHide.ForeColor = System.Drawing.Color.Black;
            this.btnShowHide.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnShowHide.ImageNormal")));
            this.btnShowHide.ImagePressed = ((System.Drawing.Image)(resources.GetObject("btnShowHide.ImagePressed")));
            this.btnShowHide.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShowHide.Location = new System.Drawing.Point(6, 163);
            this.btnShowHide.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.Size = new System.Drawing.Size(88, 43);
            this.btnShowHide.TabIndex = 6;
            this.btnShowHide.Text = "Show &Inactive";
            this.btnShowHide.UseVisualStyleBackColor = false;
            this.btnShowHide.Click += new System.EventHandler(this.btnShowHide_Click);
            // 
            // btnDone
            // 
            this.btnDone.BackColor = System.Drawing.Color.Transparent;
            this.btnDone.FocusColor = System.Drawing.Color.Black;
            this.btnDone.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
            this.btnDone.ForeColor = System.Drawing.Color.Black;
            this.btnDone.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnDone.ImageNormal")));
            this.btnDone.ImagePressed = ((System.Drawing.Image)(resources.GetObject("btnDone.ImagePressed")));
            this.btnDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDone.Location = new System.Drawing.Point(313, 233);
            this.btnDone.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(62, 34);
            this.btnDone.TabIndex = 7;
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FocusColor = System.Drawing.Color.Black;
            this.btnCancel.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageNormal")));
            this.btnCancel.ImagePressed = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImagePressed")));
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(406, 233);
            this.btnCancel.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 34);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProductGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 275);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnShowHide);
            this.Controls.Add(this.btnChangeName);
            this.Controls.Add(this.btnOnOff);
            this.Controls.Add(this.btnNewGroup);
            this.Controls.Add(this.listViewGroups);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductGroups";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Groups";
            this.ResumeLayout(false);

        }

        #endregion

        private GTI.Controls.GTIListView listViewGroups;
        private System.Windows.Forms.ColumnHeader columnGroup;
        private System.Windows.Forms.ColumnHeader columnActive;
        private GTI.Controls.ImageButton btnChangeName;
        private GTI.Controls.ImageButton btnOnOff;
        private GTI.Controls.ImageButton btnNewGroup;
        private GTI.Controls.ImageButton btnShowHide;
        private GTI.Controls.ImageButton btnDone;
        private GTI.Controls.ImageButton btnCancel;
    }
}