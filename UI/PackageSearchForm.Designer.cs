namespace GTI.Modules.ProductCenter.UI
{
    partial class PackageSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageSearchForm));
            this.m_productName = new System.Windows.Forms.TextBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.m_accept = new GTI.Controls.ImageButton();
            this.m_cancel = new GTI.Controls.ImageButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_productName
            // 
            this.m_productName.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.m_productName, "m_productName");
            this.m_productName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_productName.Name = "m_productName";
            // 
            // labelProductName
            // 
            resources.ApplyResources(this.labelProductName, "labelProductName");
            this.labelProductName.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName.Name = "labelProductName";
            // 
            // m_accept
            // 
            resources.ApplyResources(this.m_accept, "m_accept");
            this.m_accept.BackColor = System.Drawing.Color.Transparent;
            this.m_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_accept.FocusColor = System.Drawing.Color.Black;
            this.m_accept.ForeColor = System.Drawing.Color.Black;
            this.m_accept.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_accept.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_accept.Name = "m_accept";
            this.m_accept.UseVisualStyleBackColor = false;
            this.m_accept.Click += new System.EventHandler(this.m_accept_Click);
            // 
            // m_cancel
            // 
            resources.ApplyResources(this.m_cancel, "m_cancel");
            this.m_cancel.BackColor = System.Drawing.Color.Transparent;
            this.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancel.FocusColor = System.Drawing.Color.Black;
            this.m_cancel.ForeColor = System.Drawing.Color.Black;
            this.m_cancel.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_cancel.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_cancel.Name = "m_cancel";
            this.m_cancel.UseVisualStyleBackColor = false;
            this.m_cancel.Click += new System.EventHandler(this.m_cancel_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // PackageSearchForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_productName);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.m_accept);
            this.Controls.Add(this.m_cancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PackageSearchForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TextBox m_productName;
        private System.Windows.Forms.Label labelProductName;
        private GTI.Controls.ImageButton m_accept;
        private GTI.Controls.ImageButton m_cancel;
        private System.Windows.Forms.Label label1;

    }
}