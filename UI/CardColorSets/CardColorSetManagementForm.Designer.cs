namespace GTI.Modules.ProductCenter.UI
{
    partial class CardColorSetManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardColorSetManagementForm));
            this.mainGB = new System.Windows.Forms.GroupBox();
            this.createSetBtn = new GTI.Controls.ImageButton();
            this.copySetBtn = new GTI.Controls.ImageButton();
            this.detailPnl = new System.Windows.Forms.Panel();
            this.associatedProductsLbl = new System.Windows.Forms.Label();
            this.associatedProductsTxt = new System.Windows.Forms.TextBox();
            this.pagesTC = new System.Windows.Forms.TabControl();
            this.dimensionsLbl = new System.Windows.Forms.Label();
            this.dimensionsTLP = new System.Windows.Forms.TableLayoutPanel();
            this.colsAnyAllChk = new System.Windows.Forms.CheckBox();
            this.pageCountLbl = new System.Windows.Forms.Label();
            this.rowCountLbl = new System.Windows.Forms.Label();
            this.colCountLbl = new System.Windows.Forms.Label();
            this.rowsAnyAllChk = new System.Windows.Forms.CheckBox();
            this.pagesAnyAllChk = new System.Windows.Forms.CheckBox();
            this.colsLbl = new System.Windows.Forms.Label();
            this.colsIncreaseBtn = new GTI.Controls.ImageButton();
            this.rowsIncreaseBtn = new GTI.Controls.ImageButton();
            this.pagesIncreaseBtn = new GTI.Controls.ImageButton();
            this.rowsLbl = new System.Windows.Forms.Label();
            this.pagesLbl = new System.Windows.Forms.Label();
            this.colsDecreaseBtn = new GTI.Controls.ImageButton();
            this.rowsDecreaseBtn = new GTI.Controls.ImageButton();
            this.pagesDecreaseBtn = new GTI.Controls.ImageButton();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.setNameLbl = new System.Windows.Forms.Label();
            this.addColorBtn = new GTI.Controls.ImageButton();
            this.colorPaletteDGV = new System.Windows.Forms.DataGridView();
            this.colorsLbl = new System.Windows.Forms.Label();
            this.cancelBtn = new GTI.Controls.ImageButton();
            this.resetBtn = new GTI.Controls.ImageButton();
            this.saveChangesBtn = new GTI.Controls.ImageButton();
            this.cardColorSetReadOnlyLbl = new System.Windows.Forms.Label();
            this.editSetBtn = new GTI.Controls.ImageButton();
            this.cardColorSetCombo = new System.Windows.Forms.ComboBox();
            this.cardColorSetLbl = new System.Windows.Forms.Label();
            this.positionColorCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.productLoaderBGW = new System.ComponentModel.BackgroundWorker();
            this.mainGB.SuspendLayout();
            this.detailPnl.SuspendLayout();
            this.dimensionsTLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPaletteDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // mainGB
            // 
            this.mainGB.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.mainGB, "mainGB");
            this.mainGB.Controls.Add(this.createSetBtn);
            this.mainGB.Controls.Add(this.copySetBtn);
            this.mainGB.Controls.Add(this.detailPnl);
            this.mainGB.Controls.Add(this.cardColorSetReadOnlyLbl);
            this.mainGB.Controls.Add(this.editSetBtn);
            this.mainGB.Controls.Add(this.cardColorSetCombo);
            this.mainGB.Controls.Add(this.cardColorSetLbl);
            this.mainGB.ForeColor = System.Drawing.Color.Black;
            this.mainGB.Name = "mainGB";
            this.mainGB.TabStop = false;
            // 
            // createSetBtn
            // 
            resources.ApplyResources(this.createSetBtn, "createSetBtn");
            this.createSetBtn.FocusColor = System.Drawing.Color.Black;
            this.createSetBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("createSetBtn.ImageNormal")));
            this.createSetBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("createSetBtn.ImagePressed")));
            this.createSetBtn.Name = "createSetBtn";
            this.createSetBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.createSetBtn.UseVisualStyleBackColor = false;
            this.createSetBtn.Click += new System.EventHandler(this.createSetBtn_Click);
            // 
            // copySetBtn
            // 
            resources.ApplyResources(this.copySetBtn, "copySetBtn");
            this.copySetBtn.FocusColor = System.Drawing.Color.Black;
            this.copySetBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("copySetBtn.ImageNormal")));
            this.copySetBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("copySetBtn.ImagePressed")));
            this.copySetBtn.Name = "copySetBtn";
            this.copySetBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.copySetBtn.UseVisualStyleBackColor = false;
            this.copySetBtn.Click += new System.EventHandler(this.copySetBtn_Click);
            // 
            // detailPnl
            // 
            resources.ApplyResources(this.detailPnl, "detailPnl");
            this.detailPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.detailPnl.Controls.Add(this.associatedProductsLbl);
            this.detailPnl.Controls.Add(this.associatedProductsTxt);
            this.detailPnl.Controls.Add(this.pagesTC);
            this.detailPnl.Controls.Add(this.dimensionsLbl);
            this.detailPnl.Controls.Add(this.dimensionsTLP);
            this.detailPnl.Controls.Add(this.nameTxt);
            this.detailPnl.Controls.Add(this.setNameLbl);
            this.detailPnl.Controls.Add(this.addColorBtn);
            this.detailPnl.Controls.Add(this.colorPaletteDGV);
            this.detailPnl.Controls.Add(this.colorsLbl);
            this.detailPnl.Controls.Add(this.cancelBtn);
            this.detailPnl.Controls.Add(this.resetBtn);
            this.detailPnl.Controls.Add(this.saveChangesBtn);
            this.detailPnl.Name = "detailPnl";
            // 
            // associatedProductsLbl
            // 
            resources.ApplyResources(this.associatedProductsLbl, "associatedProductsLbl");
            this.associatedProductsLbl.Name = "associatedProductsLbl";
            // 
            // associatedProductsTxt
            // 
            resources.ApplyResources(this.associatedProductsTxt, "associatedProductsTxt");
            this.associatedProductsTxt.Name = "associatedProductsTxt";
            this.associatedProductsTxt.ReadOnly = true;
            // 
            // pagesTC
            // 
            resources.ApplyResources(this.pagesTC, "pagesTC");
            this.pagesTC.Name = "pagesTC";
            this.pagesTC.SelectedIndex = 0;
            // 
            // dimensionsLbl
            // 
            resources.ApplyResources(this.dimensionsLbl, "dimensionsLbl");
            this.dimensionsLbl.Name = "dimensionsLbl";
            // 
            // dimensionsTLP
            // 
            this.dimensionsTLP.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.dimensionsTLP, "dimensionsTLP");
            this.dimensionsTLP.Controls.Add(this.colsAnyAllChk, 4, 2);
            this.dimensionsTLP.Controls.Add(this.pageCountLbl, 2, 0);
            this.dimensionsTLP.Controls.Add(this.rowCountLbl, 2, 1);
            this.dimensionsTLP.Controls.Add(this.colCountLbl, 2, 2);
            this.dimensionsTLP.Controls.Add(this.rowsAnyAllChk, 4, 1);
            this.dimensionsTLP.Controls.Add(this.pagesAnyAllChk, 4, 0);
            this.dimensionsTLP.Controls.Add(this.colsLbl, 0, 2);
            this.dimensionsTLP.Controls.Add(this.colsIncreaseBtn, 3, 2);
            this.dimensionsTLP.Controls.Add(this.rowsIncreaseBtn, 3, 1);
            this.dimensionsTLP.Controls.Add(this.pagesIncreaseBtn, 3, 0);
            this.dimensionsTLP.Controls.Add(this.rowsLbl, 0, 1);
            this.dimensionsTLP.Controls.Add(this.pagesLbl, 0, 0);
            this.dimensionsTLP.Controls.Add(this.colsDecreaseBtn, 1, 2);
            this.dimensionsTLP.Controls.Add(this.rowsDecreaseBtn, 1, 1);
            this.dimensionsTLP.Controls.Add(this.pagesDecreaseBtn, 1, 0);
            this.dimensionsTLP.Name = "dimensionsTLP";
            // 
            // colsAnyAllChk
            // 
            resources.ApplyResources(this.colsAnyAllChk, "colsAnyAllChk");
            this.colsAnyAllChk.Checked = true;
            this.colsAnyAllChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.colsAnyAllChk.Name = "colsAnyAllChk";
            this.colsAnyAllChk.UseVisualStyleBackColor = true;
            this.colsAnyAllChk.CheckedChanged += new System.EventHandler(this.colsAnyAllChk_CheckedChanged);
            // 
            // pageCountLbl
            // 
            resources.ApplyResources(this.pageCountLbl, "pageCountLbl");
            this.pageCountLbl.Name = "pageCountLbl";
            // 
            // rowCountLbl
            // 
            resources.ApplyResources(this.rowCountLbl, "rowCountLbl");
            this.rowCountLbl.Name = "rowCountLbl";
            // 
            // colCountLbl
            // 
            resources.ApplyResources(this.colCountLbl, "colCountLbl");
            this.colCountLbl.Name = "colCountLbl";
            // 
            // rowsAnyAllChk
            // 
            resources.ApplyResources(this.rowsAnyAllChk, "rowsAnyAllChk");
            this.rowsAnyAllChk.Checked = true;
            this.rowsAnyAllChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rowsAnyAllChk.Name = "rowsAnyAllChk";
            this.rowsAnyAllChk.UseVisualStyleBackColor = true;
            this.rowsAnyAllChk.CheckedChanged += new System.EventHandler(this.rowsAnyAllChk_CheckedChanged);
            // 
            // pagesAnyAllChk
            // 
            resources.ApplyResources(this.pagesAnyAllChk, "pagesAnyAllChk");
            this.pagesAnyAllChk.Checked = true;
            this.pagesAnyAllChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pagesAnyAllChk.Name = "pagesAnyAllChk";
            this.pagesAnyAllChk.UseVisualStyleBackColor = true;
            this.pagesAnyAllChk.CheckedChanged += new System.EventHandler(this.pagesAnyAllChk_CheckedChanged);
            // 
            // colsLbl
            // 
            resources.ApplyResources(this.colsLbl, "colsLbl");
            this.colsLbl.Name = "colsLbl";
            // 
            // colsIncreaseBtn
            // 
            resources.ApplyResources(this.colsIncreaseBtn, "colsIncreaseBtn");
            this.colsIncreaseBtn.FocusColor = System.Drawing.Color.Black;
            this.colsIncreaseBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("colsIncreaseBtn.ImageNormal")));
            this.colsIncreaseBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("colsIncreaseBtn.ImagePressed")));
            this.colsIncreaseBtn.Name = "colsIncreaseBtn";
            this.colsIncreaseBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.colsIncreaseBtn.UseVisualStyleBackColor = false;
            this.colsIncreaseBtn.Click += new System.EventHandler(this.colsIncreaseBtn_Click);
            // 
            // rowsIncreaseBtn
            // 
            resources.ApplyResources(this.rowsIncreaseBtn, "rowsIncreaseBtn");
            this.rowsIncreaseBtn.FocusColor = System.Drawing.Color.Black;
            this.rowsIncreaseBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("rowsIncreaseBtn.ImageNormal")));
            this.rowsIncreaseBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("rowsIncreaseBtn.ImagePressed")));
            this.rowsIncreaseBtn.Name = "rowsIncreaseBtn";
            this.rowsIncreaseBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.rowsIncreaseBtn.UseVisualStyleBackColor = false;
            this.rowsIncreaseBtn.Click += new System.EventHandler(this.rowsIncreaseBtn_Click);
            // 
            // pagesIncreaseBtn
            // 
            resources.ApplyResources(this.pagesIncreaseBtn, "pagesIncreaseBtn");
            this.pagesIncreaseBtn.FocusColor = System.Drawing.Color.Black;
            this.pagesIncreaseBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("pagesIncreaseBtn.ImageNormal")));
            this.pagesIncreaseBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("pagesIncreaseBtn.ImagePressed")));
            this.pagesIncreaseBtn.Name = "pagesIncreaseBtn";
            this.pagesIncreaseBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.pagesIncreaseBtn.UseVisualStyleBackColor = false;
            this.pagesIncreaseBtn.Click += new System.EventHandler(this.pagesIncreaseBtn_Click);
            // 
            // rowsLbl
            // 
            resources.ApplyResources(this.rowsLbl, "rowsLbl");
            this.rowsLbl.Name = "rowsLbl";
            // 
            // pagesLbl
            // 
            resources.ApplyResources(this.pagesLbl, "pagesLbl");
            this.pagesLbl.Name = "pagesLbl";
            // 
            // colsDecreaseBtn
            // 
            resources.ApplyResources(this.colsDecreaseBtn, "colsDecreaseBtn");
            this.colsDecreaseBtn.FocusColor = System.Drawing.Color.Black;
            this.colsDecreaseBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("colsDecreaseBtn.ImageNormal")));
            this.colsDecreaseBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("colsDecreaseBtn.ImagePressed")));
            this.colsDecreaseBtn.Name = "colsDecreaseBtn";
            this.colsDecreaseBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.colsDecreaseBtn.UseVisualStyleBackColor = false;
            this.colsDecreaseBtn.Click += new System.EventHandler(this.colsDecreaseBtn_Click);
            // 
            // rowsDecreaseBtn
            // 
            resources.ApplyResources(this.rowsDecreaseBtn, "rowsDecreaseBtn");
            this.rowsDecreaseBtn.FocusColor = System.Drawing.Color.Black;
            this.rowsDecreaseBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("rowsDecreaseBtn.ImageNormal")));
            this.rowsDecreaseBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("rowsDecreaseBtn.ImagePressed")));
            this.rowsDecreaseBtn.Name = "rowsDecreaseBtn";
            this.rowsDecreaseBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.rowsDecreaseBtn.UseVisualStyleBackColor = false;
            this.rowsDecreaseBtn.Click += new System.EventHandler(this.rowsDecreaseBtn_Click);
            // 
            // pagesDecreaseBtn
            // 
            resources.ApplyResources(this.pagesDecreaseBtn, "pagesDecreaseBtn");
            this.pagesDecreaseBtn.FocusColor = System.Drawing.Color.Black;
            this.pagesDecreaseBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("pagesDecreaseBtn.ImageNormal")));
            this.pagesDecreaseBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("pagesDecreaseBtn.ImagePressed")));
            this.pagesDecreaseBtn.Name = "pagesDecreaseBtn";
            this.pagesDecreaseBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.pagesDecreaseBtn.UseVisualStyleBackColor = false;
            this.pagesDecreaseBtn.Click += new System.EventHandler(this.pagesDecreaseBtn_Click);
            // 
            // nameTxt
            // 
            resources.ApplyResources(this.nameTxt, "nameTxt");
            this.nameTxt.Name = "nameTxt";
            // 
            // setNameLbl
            // 
            resources.ApplyResources(this.setNameLbl, "setNameLbl");
            this.setNameLbl.Name = "setNameLbl";
            // 
            // addColorBtn
            // 
            resources.ApplyResources(this.addColorBtn, "addColorBtn");
            this.addColorBtn.FocusColor = System.Drawing.Color.Black;
            this.addColorBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("addColorBtn.ImageNormal")));
            this.addColorBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("addColorBtn.ImagePressed")));
            this.addColorBtn.Name = "addColorBtn";
            this.addColorBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.addColorBtn.UseVisualStyleBackColor = false;
            this.addColorBtn.Click += new System.EventHandler(this.addColorBtn_Click);
            // 
            // colorPaletteDGV
            // 
            this.colorPaletteDGV.AllowUserToAddRows = false;
            this.colorPaletteDGV.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.colorPaletteDGV, "colorPaletteDGV");
            this.colorPaletteDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.colorPaletteDGV.Name = "colorPaletteDGV";
            this.colorPaletteDGV.ReadOnly = true;
            this.colorPaletteDGV.RowHeadersVisible = false;
            this.colorPaletteDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.colorPaletteDGV_CellClick);
            this.colorPaletteDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.colorPaletteDGV_CellEndEdit);
            // 
            // colorsLbl
            // 
            resources.ApplyResources(this.colorsLbl, "colorsLbl");
            this.colorsLbl.Name = "colorsLbl";
            // 
            // cancelBtn
            // 
            resources.ApplyResources(this.cancelBtn, "cancelBtn");
            this.cancelBtn.FocusColor = System.Drawing.Color.Black;
            this.cancelBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("cancelBtn.ImageNormal")));
            this.cancelBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("cancelBtn.ImagePressed")));
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // resetBtn
            // 
            resources.ApplyResources(this.resetBtn, "resetBtn");
            this.resetBtn.FocusColor = System.Drawing.Color.Black;
            this.resetBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("resetBtn.ImageNormal")));
            this.resetBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("resetBtn.ImagePressed")));
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.resetBtn.UseVisualStyleBackColor = false;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // saveChangesBtn
            // 
            resources.ApplyResources(this.saveChangesBtn, "saveChangesBtn");
            this.saveChangesBtn.FocusColor = System.Drawing.Color.Black;
            this.saveChangesBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("saveChangesBtn.ImageNormal")));
            this.saveChangesBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("saveChangesBtn.ImagePressed")));
            this.saveChangesBtn.Name = "saveChangesBtn";
            this.saveChangesBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.saveChangesBtn.UseVisualStyleBackColor = false;
            this.saveChangesBtn.Click += new System.EventHandler(this.saveChangesBtn_Click);
            // 
            // cardColorSetReadOnlyLbl
            // 
            resources.ApplyResources(this.cardColorSetReadOnlyLbl, "cardColorSetReadOnlyLbl");
            this.cardColorSetReadOnlyLbl.Name = "cardColorSetReadOnlyLbl";
            // 
            // editSetBtn
            // 
            resources.ApplyResources(this.editSetBtn, "editSetBtn");
            this.editSetBtn.FocusColor = System.Drawing.Color.Black;
            this.editSetBtn.ImageNormal = ((System.Drawing.Image)(resources.GetObject("editSetBtn.ImageNormal")));
            this.editSetBtn.ImagePressed = ((System.Drawing.Image)(resources.GetObject("editSetBtn.ImagePressed")));
            this.editSetBtn.Name = "editSetBtn";
            this.editSetBtn.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.editSetBtn.UseVisualStyleBackColor = false;
            this.editSetBtn.Click += new System.EventHandler(this.editSetBtn_Click);
            // 
            // cardColorSetCombo
            // 
            resources.ApplyResources(this.cardColorSetCombo, "cardColorSetCombo");
            this.cardColorSetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cardColorSetCombo.FormattingEnabled = true;
            this.cardColorSetCombo.Name = "cardColorSetCombo";
            this.cardColorSetCombo.SelectedValueChanged += new System.EventHandler(this.cardColorSetCombo_SelectedValueChanged);
            // 
            // cardColorSetLbl
            // 
            resources.ApplyResources(this.cardColorSetLbl, "cardColorSetLbl");
            this.cardColorSetLbl.Name = "cardColorSetLbl";
            // 
            // positionColorCMS
            // 
            this.positionColorCMS.Name = "positionColorCMS";
            resources.ApplyResources(this.positionColorCMS, "positionColorCMS");
            // 
            // CardColorSetManagementForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.mainGB);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CardColorSetManagementForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Activated += new System.EventHandler(this.this_Activated);
            this.Deactivate += new System.EventHandler(this.this_Deactivate);
            this.mainGB.ResumeLayout(false);
            this.mainGB.PerformLayout();
            this.detailPnl.ResumeLayout(false);
            this.detailPnl.PerformLayout();
            this.dimensionsTLP.ResumeLayout(false);
            this.dimensionsTLP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPaletteDGV)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.GroupBox mainGB;
        private GTI.Controls.ImageButton editSetBtn;
        private System.Windows.Forms.ComboBox cardColorSetCombo;
        private System.Windows.Forms.Label cardColorSetLbl;
        private System.Windows.Forms.Label cardColorSetReadOnlyLbl;
        private System.Windows.Forms.Panel detailPnl;
        private GTI.Controls.ImageButton cancelBtn;
        private GTI.Controls.ImageButton resetBtn;
        private GTI.Controls.ImageButton saveChangesBtn;
        private System.Windows.Forms.Label colorsLbl;
        private System.Windows.Forms.DataGridView colorPaletteDGV;
        private GTI.Controls.ImageButton addColorBtn;
        private GTI.Controls.ImageButton copySetBtn;
        private GTI.Controls.ImageButton createSetBtn;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.Label setNameLbl;
        private System.Windows.Forms.TableLayoutPanel dimensionsTLP;
        private System.Windows.Forms.CheckBox colsAnyAllChk;
        private System.Windows.Forms.Label pageCountLbl;
        private System.Windows.Forms.Label rowCountLbl;
        private System.Windows.Forms.Label colCountLbl;
        private System.Windows.Forms.CheckBox rowsAnyAllChk;
        private System.Windows.Forms.CheckBox pagesAnyAllChk;
        private System.Windows.Forms.Label colsLbl;
        private GTI.Controls.ImageButton colsIncreaseBtn;
        private GTI.Controls.ImageButton rowsIncreaseBtn;
        private GTI.Controls.ImageButton pagesIncreaseBtn;
        private System.Windows.Forms.Label rowsLbl;
        private System.Windows.Forms.Label pagesLbl;
        private GTI.Controls.ImageButton colsDecreaseBtn;
        private GTI.Controls.ImageButton rowsDecreaseBtn;
        private GTI.Controls.ImageButton pagesDecreaseBtn;
        private System.Windows.Forms.Label dimensionsLbl;
        private System.Windows.Forms.TabControl pagesTC;
        private System.Windows.Forms.ContextMenuStrip positionColorCMS;
        private System.Windows.Forms.Label associatedProductsLbl;
        private System.Windows.Forms.TextBox associatedProductsTxt;
        private System.ComponentModel.BackgroundWorker productLoaderBGW;


    }
}