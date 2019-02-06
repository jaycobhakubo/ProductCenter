namespace GTI.Modules.ProductCenter.UI
{
    partial class ScanCodeEditForm
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
            this.m_btnClose = new GTI.Controls.ImageButton();
            this.m_lbScanCodes = new System.Windows.Forms.ListBox();
            this.m_lblScanCodes = new System.Windows.Forms.Label();
            this.m_btnRemove = new GTI.Controls.ImageButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_btnAdd = new GTI.Controls.ImageButton();
            this.m_txtNewScanCode = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnClose
            // 
            this.m_btnClose.BackColor = System.Drawing.Color.Transparent;
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.FocusColor = System.Drawing.Color.Black;
            this.m_btnClose.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnClose.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_btnClose.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_btnClose.Location = new System.Drawing.Point(482, 382);
            this.m_btnClose.MinimumSize = new System.Drawing.Size(30, 30);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.RepeatRate = 150;
            this.m_btnClose.RepeatWhenHeldFor = 750;
            this.m_btnClose.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.m_btnClose.Size = new System.Drawing.Size(119, 42);
            this.m_btnClose.TabIndex = 0;
            this.m_btnClose.Text = "Close";
            this.m_btnClose.UseVisualStyleBackColor = false;
            this.m_btnClose.Click += new System.EventHandler(this.m_btnClose_Click);
            this.m_btnClose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_btnClose_KeyDown);
            // 
            // m_lbScanCodes
            // 
            this.m_lbScanCodes.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbScanCodes.FormattingEnabled = true;
            this.m_lbScanCodes.IntegralHeight = false;
            this.m_lbScanCodes.ItemHeight = 15;
            this.m_lbScanCodes.Location = new System.Drawing.Point(17, 38);
            this.m_lbScanCodes.Name = "m_lbScanCodes";
            this.m_lbScanCodes.Size = new System.Drawing.Size(584, 190);
            this.m_lbScanCodes.TabIndex = 2;
            // 
            // m_lblScanCodes
            // 
            this.m_lblScanCodes.AutoSize = true;
            this.m_lblScanCodes.BackColor = System.Drawing.Color.Transparent;
            this.m_lblScanCodes.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblScanCodes.Location = new System.Drawing.Point(13, 13);
            this.m_lblScanCodes.Name = "m_lblScanCodes";
            this.m_lblScanCodes.Size = new System.Drawing.Size(95, 22);
            this.m_lblScanCodes.TabIndex = 1;
            this.m_lblScanCodes.Text = "Scan Codes";
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.m_btnRemove.FocusColor = System.Drawing.Color.Black;
            this.m_btnRemove.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnRemove.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_btnRemove.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_btnRemove.Location = new System.Drawing.Point(17, 382);
            this.m_btnRemove.MinimumSize = new System.Drawing.Size(30, 30);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.RepeatRate = 150;
            this.m_btnRemove.RepeatWhenHeldFor = 750;
            this.m_btnRemove.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.m_btnRemove.Size = new System.Drawing.Size(167, 42);
            this.m_btnRemove.TabIndex = 4;
            this.m_btnRemove.Text = "Remove selected\r\nscan code";
            this.m_btnRemove.UseVisualStyleBackColor = false;
            this.m_btnRemove.Click += new System.EventHandler(this.m_btnRemove_Click);
            this.m_btnRemove.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_btnRemove_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.m_btnAdd);
            this.groupBox1.Controls.Add(this.m_txtNewScanCode);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 244);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 118);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New scan code";
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.FocusColor = System.Drawing.Color.Black;
            this.m_btnAdd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_btnAdd.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_btnAdd.Location = new System.Drawing.Point(469, 70);
            this.m_btnAdd.MinimumSize = new System.Drawing.Size(30, 30);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.RepeatRate = 150;
            this.m_btnAdd.RepeatWhenHeldFor = 750;
            this.m_btnAdd.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.m_btnAdd.Size = new System.Drawing.Size(119, 42);
            this.m_btnAdd.TabIndex = 1;
            this.m_btnAdd.Text = "Add";
            this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
            this.m_btnAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_btnAdd_KeyDown);
            // 
            // m_txtNewScanCode
            // 
            this.m_txtNewScanCode.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtNewScanCode.Location = new System.Drawing.Point(5, 32);
            this.m_txtNewScanCode.Name = "m_txtNewScanCode";
            this.m_txtNewScanCode.Size = new System.Drawing.Size(584, 22);
            this.m_txtNewScanCode.TabIndex = 0;
            // 
            // ScanCodeEditForm
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(616, 434);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_btnRemove);
            this.Controls.Add(this.m_lblScanCodes);
            this.Controls.Add(this.m_lbScanCodes);
            this.Controls.Add(this.m_btnClose);
            this.DrawAsGradient = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanCodeEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scan Code Maintenance";
            this.Shown += new System.EventHandler(this.ScanCodeEditForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ImageButton m_btnClose;
        private System.Windows.Forms.ListBox m_lbScanCodes;
        private System.Windows.Forms.Label m_lblScanCodes;
        private Controls.ImageButton m_btnRemove;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.ImageButton m_btnAdd;
        private System.Windows.Forms.TextBox m_txtNewScanCode;
    }
}
