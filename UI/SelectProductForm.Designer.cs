namespace GTI.Modules.ProductCenter.UI
{
    partial class SelectProductForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectProductForm));
            this.label1 = new System.Windows.Forms.Label();
            this.m_productList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cancel = new GTI.Controls.ImageButton();
            this.m_next = new GTI.Controls.ImageButton();
            this.label4 = new System.Windows.Forms.Label();
            this.m_productType = new System.Windows.Forms.Label();
            this.m_productGroup = new System.Windows.Forms.Label();
            this.m_salesSource = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // m_productList
            // 
            this.m_productList.BackColor = System.Drawing.SystemColors.Window;
            this.m_productList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_productList, "m_productList");
            this.m_productList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_productList.FormattingEnabled = true;
            this.m_productList.Name = "m_productList";
            this.m_productList.Sorted = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
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
            // m_next
            // 
            resources.ApplyResources(this.m_next, "m_next");
            this.m_next.BackColor = System.Drawing.Color.Transparent;
            this.m_next.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_next.FocusColor = System.Drawing.Color.Black;
            this.m_next.ForeColor = System.Drawing.Color.Black;
            this.m_next.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_next.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_next.Name = "m_next";
            this.m_next.UseVisualStyleBackColor = false;
            this.m_next.Click += new System.EventHandler(this.m_next_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // m_productType
            // 
            resources.ApplyResources(this.m_productType, "m_productType");
            this.m_productType.BackColor = System.Drawing.Color.Transparent;
            this.m_productType.Name = "m_productType";
            // 
            // m_productGroup
            // 
            resources.ApplyResources(this.m_productGroup, "m_productGroup");
            this.m_productGroup.BackColor = System.Drawing.Color.Transparent;
            this.m_productGroup.Name = "m_productGroup";
            // 
            // m_salesSource
            // 
            resources.ApplyResources(this.m_salesSource, "m_salesSource");
            this.m_salesSource.BackColor = System.Drawing.Color.Transparent;
            this.m_salesSource.Name = "m_salesSource";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // SelectProductForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.m_salesSource);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_productGroup);
            this.Controls.Add(this.m_productType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_next);
            this.Controls.Add(this.m_cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_productList);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SelectProductForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_productList;
        private System.Windows.Forms.Label label2;
        private GTI.Controls.ImageButton m_cancel;
        private GTI.Controls.ImageButton m_next;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label m_productType;
        private System.Windows.Forms.Label m_productGroup;
        private System.Windows.Forms.Label m_salesSource;
        private System.Windows.Forms.Label label5;
    }
}