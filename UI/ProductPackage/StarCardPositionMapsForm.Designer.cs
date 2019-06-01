namespace GTI.Modules.ProductCenter.UI.ProductPackage
{
    partial class StarCardPositionMapsForm
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
            this.btnDone = new GTI.Controls.ImageButton();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.starCountsLst = new GTI.Controls.GTIListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.starPositionMapsSelector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStarCount = new GTI.Controls.TextBoxNumeric();
            this.label2 = new System.Windows.Forms.Label();
            this.addBtn = new GTI.Controls.ImageButton();
            this.label3 = new System.Windows.Forms.Label();
            this.starCodeSelector = new System.Windows.Forms.ComboBox();
            this.starCountPriorityIncreaseBtn = new GTI.Controls.ImageButton();
            this.starCountPriorityDecreaseBtn = new GTI.Controls.ImageButton();
            this.removeBtn = new GTI.Controls.ImageButton();
            this.lblStarsPerCard = new System.Windows.Forms.Label();
            this.lblTotalStarsOnCard = new System.Windows.Forms.Label();
            this.lblStarTotalText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDone.BackColor = System.Drawing.Color.Transparent;
            this.btnDone.FocusColor = System.Drawing.Color.Black;
            this.btnDone.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDone.ForeColor = System.Drawing.Color.Black;
            this.btnDone.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnDone.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDone.Location = new System.Drawing.Point(138, 440);
            this.btnDone.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnDone.Name = "btnDone";
            this.btnDone.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.btnDone.Size = new System.Drawing.Size(130, 30);
            this.btnDone.TabIndex = 7;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FocusColor = System.Drawing.Color.Black;
            this.btnCancel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnCancel.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(298, 440);
            this.btnCancel.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.btnCancel.ShowFocus = false;
            this.btnCancel.Size = new System.Drawing.Size(130, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // starCountsLst
            // 
            this.starCountsLst.AllowEraseBackground = true;
            this.starCountsLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.starCountsLst.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.starCountsLst.FullRowSelect = true;
            this.starCountsLst.GridLines = true;
            this.starCountsLst.Location = new System.Drawing.Point(6, 67);
            this.starCountsLst.Name = "starCountsLst";
            this.starCountsLst.Size = new System.Drawing.Size(386, 248);
            this.starCountsLst.SortColumn = 0;
            this.starCountsLst.TabIndex = 9;
            this.starCountsLst.UseCompatibleStateImageBehavior = false;
            this.starCountsLst.View = System.Windows.Forms.View.Details;
            this.starCountsLst.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.starCountsLst_ColumnWidthChanging);
            this.starCountsLst.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.starCountsLst_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Star Type";
            this.columnHeader1.Width = 320;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Count";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 62;
            // 
            // starPositionMapsSelector
            // 
            this.starPositionMapsSelector.BackColor = System.Drawing.SystemColors.Window;
            this.starPositionMapsSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.starPositionMapsSelector.Font = new System.Drawing.Font("Trebuchet MS", 11F);
            this.starPositionMapsSelector.ForeColor = System.Drawing.SystemColors.WindowText;
            this.starPositionMapsSelector.FormattingEnabled = true;
            this.starPositionMapsSelector.Location = new System.Drawing.Point(6, 33);
            this.starPositionMapsSelector.Name = "starPositionMapsSelector";
            this.starPositionMapsSelector.Size = new System.Drawing.Size(386, 28);
            this.starPositionMapsSelector.Sorted = true;
            this.starPositionMapsSelector.TabIndex = 10;
            this.starPositionMapsSelector.SelectedIndexChanged += new System.EventHandler(this.starPositionMapsSelector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 100;
            this.label1.Text = "Star Definition";
            // 
            // txtStarCount
            // 
            this.txtStarCount.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStarCount.Location = new System.Drawing.Point(233, 392);
            this.txtStarCount.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
            this.txtStarCount.MaxLength = 4;
            this.txtStarCount.Name = "txtStarCount";
            this.txtStarCount.Precision = 2;
            this.txtStarCount.Size = new System.Drawing.Size(69, 26);
            this.txtStarCount.TabIndex = 101;
            this.txtStarCount.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(2, 367);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 102;
            this.label2.Text = "Star Type";
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.Transparent;
            this.addBtn.FocusColor = System.Drawing.Color.Black;
            this.addBtn.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.addBtn.ForeColor = System.Drawing.Color.Black;
            this.addBtn.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.addBtn.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.addBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.addBtn.Location = new System.Drawing.Point(322, 390);
            this.addBtn.MinimumSize = new System.Drawing.Size(30, 30);
            this.addBtn.Name = "addBtn";
            this.addBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.addBtn.Size = new System.Drawing.Size(106, 30);
            this.addBtn.TabIndex = 104;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(229, 367);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 106;
            this.label3.Text = "Count";
            // 
            // starCodeSelector
            // 
            this.starCodeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.starCodeSelector.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.starCodeSelector.FormattingEnabled = true;
            this.starCodeSelector.Location = new System.Drawing.Point(6, 390);
            this.starCodeSelector.Name = "starCodeSelector";
            this.starCodeSelector.Size = new System.Drawing.Size(195, 30);
            this.starCodeSelector.TabIndex = 107;
            // 
            // starCountPriorityIncreaseBtn
            // 
            this.starCountPriorityIncreaseBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.starCountPriorityIncreaseBtn.BackColor = System.Drawing.Color.Transparent;
            this.starCountPriorityIncreaseBtn.Enabled = false;
            this.starCountPriorityIncreaseBtn.FitImageIcon = true;
            this.starCountPriorityIncreaseBtn.FocusColor = System.Drawing.Color.Black;
            this.starCountPriorityIncreaseBtn.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.starCountPriorityIncreaseBtn.ForeColor = System.Drawing.Color.Black;
            this.starCountPriorityIncreaseBtn.ImageDisabled = global::GTI.Modules.ProductCenter.Properties.Resources.GrayFlatButtonUp;
            this.starCountPriorityIncreaseBtn.ImageIcon = global::GTI.Modules.ProductCenter.Properties.Resources.ArrowUp;
            this.starCountPriorityIncreaseBtn.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.starCountPriorityIncreaseBtn.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.starCountPriorityIncreaseBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.starCountPriorityIncreaseBtn.Location = new System.Drawing.Point(395, 139);
            this.starCountPriorityIncreaseBtn.MinimumSize = new System.Drawing.Size(30, 30);
            this.starCountPriorityIncreaseBtn.Name = "starCountPriorityIncreaseBtn";
            this.starCountPriorityIncreaseBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.starCountPriorityIncreaseBtn.Size = new System.Drawing.Size(40, 40);
            this.starCountPriorityIncreaseBtn.TabIndex = 110;
            this.starCountPriorityIncreaseBtn.UseVisualStyleBackColor = false;
            this.starCountPriorityIncreaseBtn.Click += new System.EventHandler(this.starCountPriorityIncreaseBtn_Click);
            // 
            // starCountPriorityDecreaseBtn
            // 
            this.starCountPriorityDecreaseBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.starCountPriorityDecreaseBtn.BackColor = System.Drawing.Color.Transparent;
            this.starCountPriorityDecreaseBtn.Enabled = false;
            this.starCountPriorityDecreaseBtn.FitImageIcon = true;
            this.starCountPriorityDecreaseBtn.FocusColor = System.Drawing.Color.Black;
            this.starCountPriorityDecreaseBtn.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.starCountPriorityDecreaseBtn.ForeColor = System.Drawing.Color.Black;
            this.starCountPriorityDecreaseBtn.ImageDisabled = global::GTI.Modules.ProductCenter.Properties.Resources.GrayFlatButtonUp;
            this.starCountPriorityDecreaseBtn.ImageIcon = global::GTI.Modules.ProductCenter.Properties.Resources.ArrowDown;
            this.starCountPriorityDecreaseBtn.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.starCountPriorityDecreaseBtn.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.starCountPriorityDecreaseBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.starCountPriorityDecreaseBtn.Location = new System.Drawing.Point(395, 197);
            this.starCountPriorityDecreaseBtn.MinimumSize = new System.Drawing.Size(30, 30);
            this.starCountPriorityDecreaseBtn.Name = "starCountPriorityDecreaseBtn";
            this.starCountPriorityDecreaseBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.starCountPriorityDecreaseBtn.Size = new System.Drawing.Size(40, 40);
            this.starCountPriorityDecreaseBtn.TabIndex = 111;
            this.starCountPriorityDecreaseBtn.UseVisualStyleBackColor = false;
            this.starCountPriorityDecreaseBtn.Click += new System.EventHandler(this.starCountPriorityDecreaseBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.removeBtn.BackColor = System.Drawing.Color.Transparent;
            this.removeBtn.Enabled = false;
            this.removeBtn.FocusColor = System.Drawing.Color.Black;
            this.removeBtn.Font = new System.Drawing.Font("Trebuchet MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeBtn.ForeColor = System.Drawing.Color.Black;
            this.removeBtn.ImageDisabled = global::GTI.Modules.ProductCenter.Properties.Resources.GrayFlatButtonUp;
            this.removeBtn.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.removeBtn.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.removeBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.removeBtn.Location = new System.Drawing.Point(395, 66);
            this.removeBtn.MinimumSize = new System.Drawing.Size(30, 30);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.removeBtn.Size = new System.Drawing.Size(40, 40);
            this.removeBtn.TabIndex = 112;
            this.removeBtn.Text = "X";
            this.removeBtn.UseVisualStyleBackColor = false;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // lblStarsPerCard
            // 
            this.lblStarsPerCard.BackColor = System.Drawing.Color.Transparent;
            this.lblStarsPerCard.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarsPerCard.Location = new System.Drawing.Point(163, 13);
            this.lblStarsPerCard.Name = "lblStarsPerCard";
            this.lblStarsPerCard.Size = new System.Drawing.Size(229, 17);
            this.lblStarsPerCard.TabIndex = 113;
            this.lblStarsPerCard.Text = "Stars per card:";
            this.lblStarsPerCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStarsPerCard.Visible = false;
            // 
            // lblTotalStarsOnCard
            // 
            this.lblTotalStarsOnCard.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalStarsOnCard.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStarsOnCard.Location = new System.Drawing.Point(321, 317);
            this.lblTotalStarsOnCard.Name = "lblTotalStarsOnCard";
            this.lblTotalStarsOnCard.Size = new System.Drawing.Size(70, 23);
            this.lblTotalStarsOnCard.TabIndex = 114;
            this.lblTotalStarsOnCard.Text = "0";
            this.lblTotalStarsOnCard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblTotalStarsOnCard.Visible = false;
            // 
            // lblStarTotalText
            // 
            this.lblStarTotalText.BackColor = System.Drawing.Color.Transparent;
            this.lblStarTotalText.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarTotalText.Location = new System.Drawing.Point(229, 318);
            this.lblStarTotalText.Name = "lblStarTotalText";
            this.lblStarTotalText.Size = new System.Drawing.Size(86, 23);
            this.lblStarTotalText.TabIndex = 115;
            this.lblStarTotalText.Text = "Total Stars:";
            this.lblStarTotalText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStarTotalText.Visible = false;
            // 
            // StarCardPositionMapsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(440, 482);
            this.ControlBox = false;
            this.Controls.Add(this.lblStarTotalText);
            this.Controls.Add(this.lblTotalStarsOnCard);
            this.Controls.Add(this.lblStarsPerCard);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.starCountPriorityDecreaseBtn);
            this.Controls.Add(this.starCountPriorityIncreaseBtn);
            this.Controls.Add(this.starCodeSelector);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStarCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.starPositionMapsSelector);
            this.Controls.Add(this.starCountsLst);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "StarCardPositionMapsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure star card";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ImageButton btnDone;
        private Controls.ImageButton btnCancel;
        private Controls.GTIListView starCountsLst;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ComboBox starPositionMapsSelector;
        private System.Windows.Forms.Label label1;
        private Controls.TextBoxNumeric txtStarCount;
        private System.Windows.Forms.Label label2;
        private Controls.ImageButton addBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox starCodeSelector;
        private Controls.ImageButton starCountPriorityIncreaseBtn;
        private Controls.ImageButton starCountPriorityDecreaseBtn;
        private Controls.ImageButton removeBtn;
        private System.Windows.Forms.Label lblStarsPerCard;
        private System.Windows.Forms.Label lblTotalStarsOnCard;
        private System.Windows.Forms.Label lblStarTotalText;
    }
}