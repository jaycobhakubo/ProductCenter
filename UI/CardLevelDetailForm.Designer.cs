namespace GTI.Modules.ProductCenter.UI
{
    partial class CardLevelDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardLevelDetailForm));
            this.fieldName = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblMultiplier = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnAccept = new GTI.Controls.ImageButton();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.fieldMultiplier = new System.Windows.Forms.TextBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.cbUseGameColor = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblPaperColor = new System.Windows.Forms.Label();
            this.listBoxPaperColor = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // fieldName
            // 
            this.fieldName.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.fieldName, "fieldName");
            this.fieldName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fieldName.Name = "fieldName";
            this.fieldName.TextChanged += new System.EventHandler(this.fieldName_TextChanged);
            // 
            // lblColor
            // 
            this.lblColor.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblColor, "lblColor");
            this.lblColor.Name = "lblColor";
            // 
            // lblMultiplier
            // 
            this.lblMultiplier.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblMultiplier, "lblMultiplier");
            this.lblMultiplier.Name = "lblMultiplier";
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
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
            this.btnAccept.Click += new System.EventHandler(this.m_accept_Click);
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
            this.btnCancel.Click += new System.EventHandler(this.m_cancel_Click);
            // 
            // fieldMultiplier
            // 
            this.fieldMultiplier.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.fieldMultiplier, "fieldMultiplier");
            this.fieldMultiplier.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fieldMultiplier.Name = "fieldMultiplier";
            this.toolTip1.SetToolTip(this.fieldMultiplier, resources.GetString("fieldMultiplier.ToolTip"));
            this.fieldMultiplier.TextChanged += new System.EventHandler(this.fieldMultiplier_TextChanged);
            this.fieldMultiplier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Decimal_KeyPress);
            this.fieldMultiplier.Validating += new System.ComponentModel.CancelEventHandler(this.Decimal_Validating);
            // 
            // btnColor
            // 
            resources.ApplyResources(this.btnColor, "btnColor");
            this.btnColor.Name = "btnColor";
            this.toolTip1.SetToolTip(this.btnColor, resources.GetString("btnColor.ToolTip"));
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.ColorClick);
            // 
            // cbUseGameColor
            // 
            resources.ApplyResources(this.cbUseGameColor, "cbUseGameColor");
            this.cbUseGameColor.BackColor = System.Drawing.Color.Transparent;
            this.cbUseGameColor.Name = "cbUseGameColor";
            this.cbUseGameColor.UseVisualStyleBackColor = false;
            this.cbUseGameColor.CheckedChanged += new System.EventHandler(this.cbUseGameColor_CheckedChanged);
            // 
            // lblPaperColor
            // 
            this.lblPaperColor.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblPaperColor, "lblPaperColor");
            this.lblPaperColor.Name = "lblPaperColor";
            // 
            // listBoxPaperColor
            // 
            this.listBoxPaperColor.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPaperColor, "listBoxPaperColor");
            this.listBoxPaperColor.Name = "listBoxPaperColor";
            this.listBoxPaperColor.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            // 
            // CardLevelDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.listBoxPaperColor);
            this.Controls.Add(this.lblPaperColor);
            this.Controls.Add(this.cbUseGameColor);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.fieldMultiplier);
            this.Controls.Add(this.fieldName);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.lblMultiplier);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CardLevelDetailForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TextBox fieldName;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblMultiplier;
        private System.Windows.Forms.Label lblName;
        private GTI.Controls.ImageButton btnAccept;
        private GTI.Controls.ImageButton btnCancel;
        private System.Windows.Forms.TextBox fieldMultiplier;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.CheckBox cbUseGameColor;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblPaperColor;
        private System.Windows.Forms.ListBox listBoxPaperColor;

    }
}