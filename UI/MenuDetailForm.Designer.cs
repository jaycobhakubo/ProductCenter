namespace GTI.Modules.ProductCenter.UI
{
    partial class MenuDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuDetailForm));
            this.m_menuName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_accept = new GTI.Controls.ImageButton();
            this.m_cancel = new GTI.Controls.ImageButton();
            this.m_menuTypeList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_menuName
            // 
            this.m_menuName.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.m_menuName, "m_menuName");
            this.m_menuName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_menuName.Name = "m_menuName";
            this.m_menuName.TextChanged += new System.EventHandler(this.m_menuName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
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
            // m_menuTypeList
            // 
            this.m_menuTypeList.BackColor = System.Drawing.SystemColors.Window;
            this.m_menuTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_menuTypeList, "m_menuTypeList");
            this.m_menuTypeList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_menuTypeList.FormattingEnabled = true;
            this.m_menuTypeList.Name = "m_menuTypeList";
            this.m_menuTypeList.Sorted = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // MenuDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_menuTypeList);
            this.Controls.Add(this.m_menuName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_accept);
            this.Controls.Add(this.m_cancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MenuDetailForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_menuName;
        private System.Windows.Forms.Label label1;
        private GTI.Controls.ImageButton m_accept;
        private GTI.Controls.ImageButton m_cancel;
        private System.Windows.Forms.ComboBox m_menuTypeList;
        private System.Windows.Forms.Label label2;

    }
}