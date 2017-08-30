namespace GTI.Modules.ProductCenter.UI
{
    partial class MenuPageDeviceTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPageDeviceTypeForm));
            this.btnAccept = new GTI.Controls.ImageButton();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.label2 = new System.Windows.Forms.Label();
            this.devicesLsBx = new System.Windows.Forms.CheckedListBox();
            this.cbAllDevices = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
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
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // devicesLsBx
            // 
            resources.ApplyResources(this.devicesLsBx, "devicesLsBx");
            this.devicesLsBx.FormattingEnabled = true;
            this.devicesLsBx.Name = "devicesLsBx";
            this.devicesLsBx.Click += new System.EventHandler(this.devicesLsBx_Click);
            // 
            // cbAllDevices
            // 
            resources.ApplyResources(this.cbAllDevices, "cbAllDevices");
            this.cbAllDevices.BackColor = System.Drawing.Color.Transparent;
            this.cbAllDevices.Name = "cbAllDevices";
            this.cbAllDevices.UseVisualStyleBackColor = false;
            this.cbAllDevices.CheckedChanged += new System.EventHandler(this.cbAllDevices_CheckedChanged);
            // 
            // MenuPageDeviceTypeForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.cbAllDevices);
            this.Controls.Add(this.devicesLsBx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MenuPageDeviceTypeForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.MenuPageDeviceTypeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ImageButton btnAccept;
        private Controls.ImageButton btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox devicesLsBx;
        private System.Windows.Forms.CheckBox cbAllDevices;
    }
}