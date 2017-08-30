namespace GTI.Modules.ProductCenter.UI
{
    partial class ButtonDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonDetailForm));
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblButtonGraphic = new System.Windows.Forms.Label();
            this.FunctionComboBox = new System.Windows.Forms.ComboBox();
            this.DiscountComboBox = new System.Windows.Forms.ComboBox();
            this.PackageComboBox = new System.Windows.Forms.ComboBox();
            this.btnDelete = new GTI.Controls.ImageButton();
            this.btnAccept = new GTI.Controls.ImageButton();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.PackageRB = new System.Windows.Forms.RadioButton();
            this.DiscountRB = new System.Windows.Forms.RadioButton();
            this.FunctionRB = new System.Windows.Forms.RadioButton();
            this.checkBoxPlayerRequired = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyLocked = new System.Windows.Forms.CheckBox();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.listViewProducts = new GTI.Controls.GTIListView();
            this.productNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cardMediaHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.imgButtonGraphic = new GTI.Controls.ImageButton();
            this.btnSelect = new GTI.Controls.ImageButton();
            this.groupBoxMode.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.KeyTextBox, "KeyTextBox");
            this.KeyTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.KeyTextBox.Name = "KeyTextBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // lblButtonGraphic
            // 
            resources.ApplyResources(this.lblButtonGraphic, "lblButtonGraphic");
            this.lblButtonGraphic.BackColor = System.Drawing.Color.Transparent;
            this.lblButtonGraphic.Name = "lblButtonGraphic";
            // 
            // FunctionComboBox
            // 
            this.FunctionComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.FunctionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.FunctionComboBox, "FunctionComboBox");
            this.FunctionComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FunctionComboBox.FormattingEnabled = true;
            this.FunctionComboBox.Name = "FunctionComboBox";
            this.FunctionComboBox.Sorted = true;
            this.FunctionComboBox.SelectedIndexChanged += new System.EventHandler(this.FunctionComboBox_SelectedIndexChanged);
            // 
            // DiscountComboBox
            // 
            this.DiscountComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.DiscountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.DiscountComboBox, "DiscountComboBox");
            this.DiscountComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.DiscountComboBox.FormattingEnabled = true;
            this.DiscountComboBox.Name = "DiscountComboBox";
            this.DiscountComboBox.Sorted = true;
            this.DiscountComboBox.SelectedIndexChanged += new System.EventHandler(this.DiscountComboBox_SelectedIndexChanged);
            // 
            // PackageComboBox
            // 
            this.PackageComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.PackageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.PackageComboBox, "PackageComboBox");
            this.PackageComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.PackageComboBox.FormattingEnabled = true;
            this.PackageComboBox.Name = "PackageComboBox";
            this.PackageComboBox.Sorted = true;
            this.PackageComboBox.SelectedIndexChanged += new System.EventHandler(this.PackageComboBox_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FocusColor = System.Drawing.Color.Black;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnDelete.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.DeleteClick);
            // 
            // btnAccept
            // 
            resources.ApplyResources(this.btnAccept, "btnAccept");
            this.btnAccept.BackColor = System.Drawing.Color.Transparent;
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.FocusColor = System.Drawing.Color.Black;
            this.btnAccept.ForeColor = System.Drawing.Color.Black;
            this.btnAccept.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnAccept.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.AcceptClick);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FocusColor = System.Drawing.Color.Black;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnCancel.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // PackageRB
            // 
            resources.ApplyResources(this.PackageRB, "PackageRB");
            this.PackageRB.BackColor = System.Drawing.Color.Transparent;
            this.PackageRB.Name = "PackageRB";
            this.PackageRB.TabStop = true;
            this.PackageRB.UseVisualStyleBackColor = false;
            this.PackageRB.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // DiscountRB
            // 
            this.DiscountRB.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.DiscountRB, "DiscountRB");
            this.DiscountRB.Name = "DiscountRB";
            this.DiscountRB.TabStop = true;
            this.DiscountRB.UseVisualStyleBackColor = false;
            this.DiscountRB.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // FunctionRB
            // 
            resources.ApplyResources(this.FunctionRB, "FunctionRB");
            this.FunctionRB.BackColor = System.Drawing.Color.Transparent;
            this.FunctionRB.Name = "FunctionRB";
            this.FunctionRB.TabStop = true;
            this.FunctionRB.UseVisualStyleBackColor = false;
            this.FunctionRB.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // checkBoxPlayerRequired
            // 
            resources.ApplyResources(this.checkBoxPlayerRequired, "checkBoxPlayerRequired");
            this.checkBoxPlayerRequired.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPlayerRequired.Name = "checkBoxPlayerRequired";
            this.checkBoxPlayerRequired.UseVisualStyleBackColor = false;
            // 
            // checkBoxKeyLocked
            // 
            resources.ApplyResources(this.checkBoxKeyLocked, "checkBoxKeyLocked");
            this.checkBoxKeyLocked.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxKeyLocked.Name = "checkBoxKeyLocked";
            this.checkBoxKeyLocked.UseVisualStyleBackColor = false;
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxMode.Controls.Add(this.PackageRB);
            this.groupBoxMode.Controls.Add(this.FunctionRB);
            this.groupBoxMode.Controls.Add(this.DiscountRB);
            this.groupBoxMode.Controls.Add(this.FunctionComboBox);
            this.groupBoxMode.Controls.Add(this.DiscountComboBox);
            this.groupBoxMode.Controls.Add(this.PackageComboBox);
            resources.ApplyResources(this.groupBoxMode, "groupBoxMode");
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.TabStop = false;
            // 
            // listViewProducts
            // 
            this.listViewProducts.AllowEraseBackground = true;
            this.listViewProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.productNameHeader,
            this.cardMediaHeader});
            this.listViewProducts.FullRowSelect = true;
            this.listViewProducts.GridLines = true;
            resources.ApplyResources(this.listViewProducts, "listViewProducts");
            this.listViewProducts.MultiSelect = false;
            this.listViewProducts.Name = "listViewProducts";
            this.listViewProducts.OwnerDraw = true;
            this.listViewProducts.SortColumn = 0;
            this.listViewProducts.UseCompatibleStateImageBehavior = false;
            this.listViewProducts.View = System.Windows.Forms.View.Details;
            // 
            // productNameHeader
            // 
            this.productNameHeader.Tag = "alpha";
            resources.ApplyResources(this.productNameHeader, "productNameHeader");
            // 
            // cardMediaHeader
            // 
            this.cardMediaHeader.Tag = "alpha";
            resources.ApplyResources(this.cardMediaHeader, "cardMediaHeader");
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.imgButtonGraphic);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.lblButtonGraphic);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // imgButtonGraphic
            // 
            this.imgButtonGraphic.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgButtonGraphic, "imgButtonGraphic");
            this.imgButtonGraphic.Name = "imgButtonGraphic";
            this.imgButtonGraphic.Stretch = false;
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnSelect.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnSelect.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.SelectClick);
            // 
            // ButtonDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.listViewProducts);
            this.Controls.Add(this.checkBoxKeyLocked);
            this.Controls.Add(this.checkBoxPlayerRequired);
            this.Controls.Add(this.groupBoxMode);
            this.Controls.Add(this.KeyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ButtonDetailForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.ButtonDetailForm_Load);
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

       
       

        #endregion

        private System.Windows.Forms.TextBox KeyTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblButtonGraphic;
        private System.Windows.Forms.ComboBox FunctionComboBox;
        private System.Windows.Forms.ComboBox DiscountComboBox;
        private System.Windows.Forms.ComboBox PackageComboBox;
        private GTI.Controls.ImageButton btnDelete;
        private GTI.Controls.ImageButton btnAccept;
        private GTI.Controls.ImageButton btnCancel;
        private System.Windows.Forms.RadioButton PackageRB;
        private System.Windows.Forms.RadioButton DiscountRB;
        private System.Windows.Forms.RadioButton FunctionRB;
        private System.Windows.Forms.CheckBox checkBoxPlayerRequired;
        private System.Windows.Forms.CheckBox checkBoxKeyLocked;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private GTI.Controls.GTIListView listViewProducts;
        private System.Windows.Forms.ColumnHeader productNameHeader;
        private System.Windows.Forms.ColumnHeader cardMediaHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.ImageButton btnSelect;
        private Controls.ImageButton imgButtonGraphic;

    }
}