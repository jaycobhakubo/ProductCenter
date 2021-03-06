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
            this.mainPnl = new System.Windows.Forms.Panel();
            this.labelScanCodesText = new System.Windows.Forms.Label();
            this.btnEditScanCodes = new GTI.Controls.ImageButton();
            this.labelScanCodes = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.usedByAccrualsLbl = new System.Windows.Forms.Label();
            this.buttonsPnl = new System.Windows.Forms.Panel();
            this.cardColorSetCombo = new System.Windows.Forms.ComboBox();
            this.cardColorSetLbl = new System.Windows.Forms.Label();
            this.mainPnl.SuspendLayout();
            this.buttonsPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboProductTypes
            // 
            resources.ApplyResources(this.cboProductTypes, "cboProductTypes");
            this.cboProductTypes.BackColor = System.Drawing.SystemColors.Window;
            this.cboProductTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductTypes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboProductTypes.FormattingEnabled = true;
            this.cboProductTypes.Name = "cboProductTypes";
            this.cboProductTypes.Sorted = true;
            this.cboProductTypes.SelectedValueChanged += new System.EventHandler(this.CboProductTypesSelectedValueChanged);
            // 
            // m_productName
            // 
            resources.ApplyResources(this.m_productName, "m_productName");
            this.m_productName.BackColor = System.Drawing.SystemColors.Window;
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
            resources.ApplyResources(this.m_accept, "m_accept");
            this.m_accept.BackColor = System.Drawing.Color.Transparent;
            this.m_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_accept.FocusColor = System.Drawing.Color.Black;
            this.m_accept.ForeColor = System.Drawing.Color.Black;
            this.m_accept.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.m_accept.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.m_accept.Name = "m_accept";
            this.m_accept.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
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
            this.m_cancel.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.m_cancel.UseVisualStyleBackColor = false;
            this.m_cancel.Click += new System.EventHandler(this.m_cancel_Click);
            // 
            // cboProductGroups
            // 
            resources.ApplyResources(this.cboProductGroups, "cboProductGroups");
            this.cboProductGroups.BackColor = System.Drawing.SystemColors.Window;
            this.cboProductGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            resources.ApplyResources(this.cboSalesSources, "cboSalesSources");
            this.cboSalesSources.BackColor = System.Drawing.SystemColors.Window;
            this.cboSalesSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            resources.ApplyResources(this.cboPaperLayouts, "cboPaperLayouts");
            this.cboPaperLayouts.BackColor = System.Drawing.SystemColors.Window;
            this.cboPaperLayouts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            resources.ApplyResources(this.cboPermFile, "cboPermFile");
            this.cboPermFile.BackColor = System.Drawing.SystemColors.Window;
            this.cboPermFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            // mainPnl
            // 
            resources.ApplyResources(this.mainPnl, "mainPnl");
            this.mainPnl.BackColor = System.Drawing.Color.Transparent;
            this.mainPnl.Controls.Add(this.cardColorSetLbl);
            this.mainPnl.Controls.Add(this.cardColorSetCombo);
            this.mainPnl.Controls.Add(this.labelScanCodesText);
            this.mainPnl.Controls.Add(this.btnEditScanCodes);
            this.mainPnl.Controls.Add(this.labelScanCodes);
            this.mainPnl.Controls.Add(this.labelProductName);
            this.mainPnl.Controls.Add(this.m_chkBarcodedPaper);
            this.mainPnl.Controls.Add(this.chkbxIsValidate);
            this.mainPnl.Controls.Add(this.labelProductType);
            this.mainPnl.Controls.Add(this.cboPermFile);
            this.mainPnl.Controls.Add(this.labelSalesSource);
            this.mainPnl.Controls.Add(this.labelPermFile);
            this.mainPnl.Controls.Add(this.m_productName);
            this.mainPnl.Controls.Add(this.cboProductTypes);
            this.mainPnl.Controls.Add(this.cboSalesSources);
            this.mainPnl.Controls.Add(this.labelProductGroup);
            this.mainPnl.Controls.Add(this.cboProductGroups);
            this.mainPnl.Controls.Add(this.cboPaperLayouts);
            this.mainPnl.Controls.Add(this.labelPaperLayout);
            this.mainPnl.Name = "mainPnl";
            // 
            // labelScanCodesText
            // 
            resources.ApplyResources(this.labelScanCodesText, "labelScanCodesText");
            this.labelScanCodesText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelScanCodesText.Name = "labelScanCodesText";
            // 
            // btnEditScanCodes
            // 
            resources.ApplyResources(this.btnEditScanCodes, "btnEditScanCodes");
            this.btnEditScanCodes.FocusColor = System.Drawing.Color.Black;
            this.btnEditScanCodes.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnEditScanCodes.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnEditScanCodes.Name = "btnEditScanCodes";
            this.btnEditScanCodes.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.btnEditScanCodes.Click += new System.EventHandler(this.btnEditScanCodes_Click);
            // 
            // labelScanCodes
            // 
            resources.ApplyResources(this.labelScanCodes, "labelScanCodes");
            this.labelScanCodes.Name = "labelScanCodes";
            // 
            // usedByAccrualsLbl
            // 
            resources.ApplyResources(this.usedByAccrualsLbl, "usedByAccrualsLbl");
            this.usedByAccrualsLbl.BackColor = System.Drawing.Color.Transparent;
            this.usedByAccrualsLbl.Name = "usedByAccrualsLbl";
            // 
            // buttonsPnl
            // 
            resources.ApplyResources(this.buttonsPnl, "buttonsPnl");
            this.buttonsPnl.BackColor = System.Drawing.Color.Transparent;
            this.buttonsPnl.Controls.Add(this.m_cancel);
            this.buttonsPnl.Controls.Add(this.m_accept);
            this.buttonsPnl.Name = "buttonsPnl";
            // 
            // cardColorSetCombo
            // 
            resources.ApplyResources(this.cardColorSetCombo, "cardColorSetCombo");
            this.cardColorSetCombo.BackColor = System.Drawing.SystemColors.Window;
            this.cardColorSetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cardColorSetCombo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cardColorSetCombo.FormattingEnabled = true;
            this.cardColorSetCombo.Name = "cardColorSetCombo";
            this.cardColorSetCombo.Sorted = true;
            // 
            // cardColorSetLbl
            // 
            resources.ApplyResources(this.cardColorSetLbl, "cardColorSetLbl");
            this.cardColorSetLbl.BackColor = System.Drawing.Color.Transparent;
            this.cardColorSetLbl.Name = "cardColorSetLbl";
            // 
            // ProductDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.buttonsPnl);
            this.Controls.Add(this.usedByAccrualsLbl);
            this.Controls.Add(this.mainPnl);
            this.Controls.Add(this.m_chkActive);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProductDetailForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Shown += new System.EventHandler(this.ProductDetailForm_Shown);
            this.mainPnl.ResumeLayout(false);
            this.mainPnl.PerformLayout();
            this.buttonsPnl.ResumeLayout(false);
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
        private System.Windows.Forms.Panel mainPnl;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label usedByAccrualsLbl;
        private System.Windows.Forms.Label labelScanCodes;
        private System.Windows.Forms.Label labelScanCodesText;
        private Controls.ImageButton btnEditScanCodes;
        private System.Windows.Forms.Panel buttonsPnl;
        private System.Windows.Forms.Label cardColorSetLbl;
        private System.Windows.Forms.ComboBox cardColorSetCombo;

    }
}