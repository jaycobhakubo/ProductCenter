namespace GTI.Modules.ProductCenter.UI
{
    partial class PackageDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageDetailForm));
            this.txtReceipt = new System.Windows.Forms.TextBox();
            this.txtPackageName = new System.Windows.Forms.TextBox();
            this.lblReceipt = new System.Windows.Forms.Label();
            this.lblPackageName = new System.Windows.Forms.Label();
            this.btnAccept = new GTI.Controls.ImageButton();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.chkChargeDeviceFee = new System.Windows.Forms.CheckBox();
            this.OverrideValidationCalculationCheckbox = new System.Windows.Forms.CheckBox();
            this.ValidationQuantityTextbox = new System.Windows.Forms.TextBox();
            this.ValidationQuantityLabel = new System.Windows.Forms.Label();
            this.ScanCodeText = new System.Windows.Forms.Label();
            this.ScanCodeEditButton = new GTI.Controls.ImageButton();
            this.ScanCodeLabel = new System.Windows.Forms.Label();
            this.RequiresValidationCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtReceipt
            // 
            this.txtReceipt.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtReceipt, "txtReceipt");
            this.txtReceipt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtReceipt.Name = "txtReceipt";
            this.txtReceipt.TextChanged += new System.EventHandler(this.OnReceiptTextChanged);
            this.txtReceipt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtReceipt_PreviewKeyDown);
            // 
            // txtPackageName
            // 
            this.txtPackageName.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtPackageName, "txtPackageName");
            this.txtPackageName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPackageName.Name = "txtPackageName";
            this.txtPackageName.TextChanged += new System.EventHandler(this.OnPackageNameChanged);
            // 
            // lblReceipt
            // 
            resources.ApplyResources(this.lblReceipt, "lblReceipt");
            this.lblReceipt.BackColor = System.Drawing.Color.Transparent;
            this.lblReceipt.Name = "lblReceipt";
            // 
            // lblPackageName
            // 
            resources.ApplyResources(this.lblPackageName, "lblPackageName");
            this.lblPackageName.BackColor = System.Drawing.Color.Transparent;
            this.lblPackageName.Name = "lblPackageName";
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
            this.btnAccept.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
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
            this.btnCancel.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // chkChargeDeviceFee
            // 
            resources.ApplyResources(this.chkChargeDeviceFee, "chkChargeDeviceFee");
            this.chkChargeDeviceFee.BackColor = System.Drawing.Color.Transparent;
            this.chkChargeDeviceFee.Name = "chkChargeDeviceFee";
            this.chkChargeDeviceFee.UseVisualStyleBackColor = false;
            this.chkChargeDeviceFee.CheckedChanged += new System.EventHandler(this.chkChargeDeviceFee_CheckedChanged);
            // 
            // OverrideValidationCalculationCheckbox
            // 
            resources.ApplyResources(this.OverrideValidationCalculationCheckbox, "OverrideValidationCalculationCheckbox");
            this.OverrideValidationCalculationCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.OverrideValidationCalculationCheckbox.Name = "OverrideValidationCalculationCheckbox";
            this.OverrideValidationCalculationCheckbox.UseVisualStyleBackColor = false;
            this.OverrideValidationCalculationCheckbox.CheckedChanged += new System.EventHandler(this.OverrideValidationCalculationCheckbox_CheckedChanged);
            // 
            // ValidationQuantityTextbox
            // 
            this.ValidationQuantityTextbox.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.ValidationQuantityTextbox, "ValidationQuantityTextbox");
            this.ValidationQuantityTextbox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ValidationQuantityTextbox.Name = "ValidationQuantityTextbox";
            this.ValidationQuantityTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidationQuantityTextbox_KeyPress);
            // 
            // ValidationQuantityLabel
            // 
            resources.ApplyResources(this.ValidationQuantityLabel, "ValidationQuantityLabel");
            this.ValidationQuantityLabel.BackColor = System.Drawing.Color.Transparent;
            this.ValidationQuantityLabel.Name = "ValidationQuantityLabel";
            // 
            // ScanCodeText
            // 
            this.ScanCodeText.BackColor = System.Drawing.Color.Transparent;
            this.ScanCodeText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.ScanCodeText, "ScanCodeText");
            this.ScanCodeText.Name = "ScanCodeText";
            // 
            // ScanCodeEditButton
            // 
            this.ScanCodeEditButton.BackColor = System.Drawing.Color.Transparent;
            this.ScanCodeEditButton.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.ScanCodeEditButton, "ScanCodeEditButton");
            this.ScanCodeEditButton.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.ScanCodeEditButton.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.ScanCodeEditButton.Name = "ScanCodeEditButton";
            this.ScanCodeEditButton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.ScanCodeEditButton.UseVisualStyleBackColor = false;
            this.ScanCodeEditButton.Click += new System.EventHandler(this.ScanCodeEditButton_Click);
            // 
            // ScanCodeLabel
            // 
            this.ScanCodeLabel.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.ScanCodeLabel, "ScanCodeLabel");
            this.ScanCodeLabel.Name = "ScanCodeLabel";
            // 
            // RequiresValidationCheckbox
            // 
            this.RequiresValidationCheckbox.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.RequiresValidationCheckbox, "RequiresValidationCheckbox");
            this.RequiresValidationCheckbox.Name = "RequiresValidationCheckbox";
            this.RequiresValidationCheckbox.UseVisualStyleBackColor = false;
            this.RequiresValidationCheckbox.CheckedChanged += new System.EventHandler(this.chkChargeDeviceFee_CheckedChanged);
            // 
            // PackageDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.RequiresValidationCheckbox);
            this.Controls.Add(this.ScanCodeText);
            this.Controls.Add(this.ScanCodeEditButton);
            this.Controls.Add(this.ScanCodeLabel);
            this.Controls.Add(this.ValidationQuantityTextbox);
            this.Controls.Add(this.ValidationQuantityLabel);
            this.Controls.Add(this.OverrideValidationCalculationCheckbox);
            this.Controls.Add(this.chkChargeDeviceFee);
            this.Controls.Add(this.txtReceipt);
            this.Controls.Add(this.txtPackageName);
            this.Controls.Add(this.lblReceipt);
            this.Controls.Add(this.lblPackageName);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PackageDetailForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Shown += new System.EventHandler(this.PackageDetailForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TextBox txtReceipt;
        private System.Windows.Forms.TextBox txtPackageName;
        private System.Windows.Forms.Label lblReceipt;
        private System.Windows.Forms.Label lblPackageName;
        private GTI.Controls.ImageButton btnAccept;
        private GTI.Controls.ImageButton btnCancel;
        private System.Windows.Forms.CheckBox chkChargeDeviceFee;
        private System.Windows.Forms.CheckBox OverrideValidationCalculationCheckbox;
        private System.Windows.Forms.TextBox ValidationQuantityTextbox;
        private System.Windows.Forms.Label ValidationQuantityLabel;
        private System.Windows.Forms.Label ScanCodeText;
        private Controls.ImageButton ScanCodeEditButton;
        private System.Windows.Forms.Label ScanCodeLabel;
        private System.Windows.Forms.CheckBox RequiresValidationCheckbox;

    }
}