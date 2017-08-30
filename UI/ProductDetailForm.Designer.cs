namespace GTI.Modules.ProductCenter.UI
{
    partial class ProductDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductDetailForm));
            this.cboProductTypes = new System.Windows.Forms.ComboBox();
            this.m_productName = new System.Windows.Forms.TextBox();
            this.labelProductType = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.m_accept = new GTI.Controls.ImageButton();
            this.m_cancel = new GTI.Controls.ImageButton();
            this.cboProductGroups = new System.Windows.Forms.ComboBox();
            this.labelProductGroup = new System.Windows.Forms.Label();
            this.cboSalesSources = new System.Windows.Forms.ComboBox();
            this.labelSalesSource = new System.Windows.Forms.Label();
            this.labelPaperLayout = new System.Windows.Forms.Label();
            this.cboPaperLayouts = new System.Windows.Forms.ComboBox();
            this.m_chkActive = new System.Windows.Forms.CheckBox();
            this.m_chkBarcodedPaper = new System.Windows.Forms.CheckBox();
            this.cboPermFile = new System.Windows.Forms.ComboBox();
            this.labelPermFile = new System.Windows.Forms.Label();
            this.chkbxIsValidate = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.usedByAccrualsLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboProductTypes
            // 
            this.cboProductTypes.BackColor = System.Drawing.SystemColors.Window;
            this.cboProductTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboProductTypes, "cboProductTypes");
            this.cboProductTypes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboProductTypes.FormattingEnabled = true;
            this.cboProductTypes.Name = "cboProductTypes";
            this.cboProductTypes.Sorted = true;
            this.cboProductTypes.SelectedValueChanged += new System.EventHandler(this.CboProductTypesSelectedValueChanged);
            // 
            // m_productName
            // 
            this.m_productName.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.m_productName, "m_productName");
            this.m_productName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_productName.Name = "m_productName";
            // 
            // labelProductType
            // 
            resources.ApplyResources(this.labelProductType, "labelProductType");
            this.labelProductType.BackColor = System.Drawing.Color.Transparent;
            this.labelProductType.Name = "labelProductType";
            // 
            // labelProductName
            // 
            resources.ApplyResources(this.labelProductName, "labelProductName");
            this.labelProductName.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName.Name = "labelProductName";
            // 
            // m_accept
            // 
            this.m_accept.BackColor = System.Drawing.Color.Transparent;
            this.m_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_accept.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.m_accept, "m_accept");
            this.m_accept.ForeColor = System.Drawing.Color.Black;
            this.m_accept.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_accept.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_accept.Name = "m_accept";
            this.m_accept.UseVisualStyleBackColor = false;
            this.m_accept.Click += new System.EventHandler(this.m_accept_Click);
            // 
            // m_cancel
            // 
            this.m_cancel.BackColor = System.Drawing.Color.Transparent;
            this.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancel.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.m_cancel, "m_cancel");
            this.m_cancel.ForeColor = System.Drawing.Color.Black;
            this.m_cancel.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_cancel.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_cancel.Name = "m_cancel";
            this.m_cancel.UseVisualStyleBackColor = false;
            this.m_cancel.Click += new System.EventHandler(this.m_cancel_Click);
            // 
            // cboProductGroups
            // 
            this.cboProductGroups.BackColor = System.Drawing.SystemColors.Window;
            this.cboProductGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboProductGroups, "cboProductGroups");
            this.cboProductGroups.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboProductGroups.FormattingEnabled = true;
            this.cboProductGroups.Name = "cboProductGroups";
            this.cboProductGroups.Sorted = true;
            // 
            // labelProductGroup
            // 
            resources.ApplyResources(this.labelProductGroup, "labelProductGroup");
            this.labelProductGroup.BackColor = System.Drawing.Color.Transparent;
            this.labelProductGroup.Name = "labelProductGroup";
            // 
            // cboSalesSources
            // 
            this.cboSalesSources.BackColor = System.Drawing.SystemColors.Window;
            this.cboSalesSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboSalesSources, "cboSalesSources");
            this.cboSalesSources.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboSalesSources.FormattingEnabled = true;
            this.cboSalesSources.Name = "cboSalesSources";
            this.cboSalesSources.Sorted = true;
            // 
            // labelSalesSource
            // 
            resources.ApplyResources(this.labelSalesSource, "labelSalesSource");
            this.labelSalesSource.BackColor = System.Drawing.Color.Transparent;
            this.labelSalesSource.Name = "labelSalesSource";
            // 
            // labelPaperLayout
            // 
            resources.ApplyResources(this.labelPaperLayout, "labelPaperLayout");
            this.labelPaperLayout.BackColor = System.Drawing.Color.Transparent;
            this.labelPaperLayout.Name = "labelPaperLayout";
            // 
            // cboPaperLayouts
            // 
            this.cboPaperLayouts.BackColor = System.Drawing.SystemColors.Window;
            this.cboPaperLayouts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboPaperLayouts, "cboPaperLayouts");
            this.cboPaperLayouts.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPaperLayouts.FormattingEnabled = true;
            this.cboPaperLayouts.Name = "cboPaperLayouts";
            this.cboPaperLayouts.Sorted = true;
            // 
            // m_chkActive
            // 
            resources.ApplyResources(this.m_chkActive, "m_chkActive");
            this.m_chkActive.BackColor = System.Drawing.Color.Transparent;
            this.m_chkActive.Name = "m_chkActive";
            this.m_chkActive.UseVisualStyleBackColor = false;
            // 
            // m_chkBarcodedPaper
            // 
            resources.ApplyResources(this.m_chkBarcodedPaper, "m_chkBarcodedPaper");
            this.m_chkBarcodedPaper.BackColor = System.Drawing.Color.Transparent;
            this.m_chkBarcodedPaper.Name = "m_chkBarcodedPaper";
            this.m_chkBarcodedPaper.UseVisualStyleBackColor = false;
            this.m_chkBarcodedPaper.CheckedChanged += new System.EventHandler(this.m_chkBarcodedPaper_CheckedChanged);
            // 
            // cboPermFile
            // 
            this.cboPermFile.BackColor = System.Drawing.SystemColors.Window;
            this.cboPermFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboPermFile, "cboPermFile");
            this.cboPermFile.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPermFile.FormattingEnabled = true;
            this.cboPermFile.Name = "cboPermFile";
            this.cboPermFile.Sorted = true;
            this.cboPermFile.MouseHover += new System.EventHandler(this.cboPermFile_MouseHover);
            // 
            // labelPermFile
            // 
            resources.ApplyResources(this.labelPermFile, "labelPermFile");
            this.labelPermFile.BackColor = System.Drawing.Color.Transparent;
            this.labelPermFile.Name = "labelPermFile";
            // 
            // chkbxIsValidate
            // 
            resources.ApplyResources(this.chkbxIsValidate, "chkbxIsValidate");
            this.chkbxIsValidate.BackColor = System.Drawing.Color.Transparent;
            this.chkbxIsValidate.Name = "chkbxIsValidate";
            this.chkbxIsValidate.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.labelProductName);
            this.panel1.Controls.Add(this.m_chkBarcodedPaper);
            this.panel1.Controls.Add(this.chkbxIsValidate);
            this.panel1.Controls.Add(this.labelProductType);
            this.panel1.Controls.Add(this.cboPermFile);
            this.panel1.Controls.Add(this.labelSalesSource);
            this.panel1.Controls.Add(this.labelPermFile);
            this.panel1.Controls.Add(this.m_productName);
            this.panel1.Controls.Add(this.cboProductTypes);
            this.panel1.Controls.Add(this.cboSalesSources);
            this.panel1.Controls.Add(this.labelProductGroup);
            this.panel1.Controls.Add(this.cboProductGroups);
            this.panel1.Controls.Add(this.cboPaperLayouts);
            this.panel1.Controls.Add(this.labelPaperLayout);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // usedByAccrualsLbl
            // 
            this.usedByAccrualsLbl.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.usedByAccrualsLbl, "usedByAccrualsLbl");
            this.usedByAccrualsLbl.Name = "usedByAccrualsLbl";
            // 
            // ProductDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.usedByAccrualsLbl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_chkActive);
            this.Controls.Add(this.m_accept);
            this.Controls.Add(this.m_cancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProductDetailForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ComboBox cboProductTypes;
        private System.Windows.Forms.TextBox m_productName;
        private System.Windows.Forms.Label labelProductType;
        private System.Windows.Forms.Label labelProductName;
        private GTI.Controls.ImageButton m_accept;
        private GTI.Controls.ImageButton m_cancel;
        private System.Windows.Forms.ComboBox cboProductGroups;
        private System.Windows.Forms.Label labelProductGroup;
        private System.Windows.Forms.ComboBox cboSalesSources;
        private System.Windows.Forms.Label labelSalesSource;
        private System.Windows.Forms.Label labelPaperLayout;
        private System.Windows.Forms.ComboBox cboPaperLayouts;
        private System.Windows.Forms.CheckBox m_chkActive;
        private System.Windows.Forms.CheckBox m_chkBarcodedPaper;
        private System.Windows.Forms.ComboBox cboPermFile;
        private System.Windows.Forms.Label labelPermFile;
        private System.Windows.Forms.CheckBox chkbxIsValidate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label usedByAccrualsLbl;

    }
}