namespace GTI.Modules.ProductCenter.UI
{
    partial class BingoProductDetailForm
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
            this.groupProduct = new System.Windows.Forms.GroupBox();
            this.lblNoteValidation = new System.Windows.Forms.Label();
            this.txtProductType = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.groupBoxCards = new System.Windows.Forms.GroupBox();
            this.txtCardCount = new GTI.Controls.TextBoxNumeric();
            this.lblCardLevelList = new System.Windows.Forms.Label();
            this.cboCardLevelList = new System.Windows.Forms.ComboBox();
            this.lblCardTypeList = new System.Windows.Forms.Label();
            this.cboCardTypeList = new System.Windows.Forms.ComboBox();
            this.lblCardCount = new System.Windows.Forms.Label();
            this.groupBoxGame = new System.Windows.Forms.GroupBox();
            this.lblGameTypeList = new System.Windows.Forms.Label();
            this.lblGameCategoryList = new System.Windows.Forms.Label();
            this.cboGameCategoryList = new System.Windows.Forms.ComboBox();
            this.cboGameTypeList = new System.Windows.Forms.ComboBox();
            this.groupBoxPoints = new System.Windows.Forms.GroupBox();
            this.checkBoxPointQualify = new System.Windows.Forms.CheckBox();
            this.txtPointsToRedeem = new GTI.Controls.TextBoxNumeric();
            this.txtPointsPerDollar = new GTI.Controls.TextBoxNumeric();
            this.txtPointsPerQuantity = new GTI.Controls.TextBoxNumeric();
            this.lblPointsPerQuantity = new System.Windows.Forms.Label();
            this.lblPointsPerDollarLabel = new System.Windows.Forms.Label();
            this.lblPointsToRedeem = new System.Windows.Forms.Label();
            this.groupPricing = new System.Windows.Forms.GroupBox();
            this.txtAltPrice = new GTI.Controls.TextBoxNumeric();
            this.lblAltPrice = new System.Windows.Forms.Label();
            this.txtQuantity = new GTI.Controls.TextBoxNumeric();
            this.txtPrice = new GTI.Controls.TextBoxNumeric();
            this.lblPrice = new System.Windows.Forms.Label();
            this.checkBoxTaxed = new System.Windows.Forms.CheckBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.btnAdd = new GTI.Controls.ImageButton();
            this.btnDone = new GTI.Controls.ImageButton();
            this.groupProduct.SuspendLayout();
            this.groupBoxCards.SuspendLayout();
            this.groupBoxGame.SuspendLayout();
            this.groupBoxPoints.SuspendLayout();
            this.groupPricing.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupProduct
            // 
            this.groupProduct.BackColor = System.Drawing.Color.Transparent;
            this.groupProduct.Controls.Add(this.lblNoteValidation);
            this.groupProduct.Controls.Add(this.txtProductType);
            this.groupProduct.Controls.Add(this.txtProductName);
            this.groupProduct.Controls.Add(this.lblProductName);
            this.groupProduct.Controls.Add(this.lblProductType);
            this.groupProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupProduct.Location = new System.Drawing.Point(14, 12);
            this.groupProduct.Name = "groupProduct";
            this.groupProduct.Size = new System.Drawing.Size(533, 131);
            this.groupProduct.TabIndex = 99;
            this.groupProduct.TabStop = false;
            this.groupProduct.Text = "Product Information";
            // 
            // lblNoteValidation
            // 
            this.lblNoteValidation.Location = new System.Drawing.Point(12, 93);
            this.lblNoteValidation.Name = "lblNoteValidation";
            this.lblNoteValidation.Size = new System.Drawing.Size(161, 18);
            this.lblNoteValidation.TabIndex = 102;
            this.lblNoteValidation.Text = "This product is validated";
            // 
            // txtProductType
            // 
            this.txtProductType.AutoSize = true;
            this.txtProductType.BackColor = System.Drawing.Color.Transparent;
            this.txtProductType.Font = new System.Drawing.Font("Trebuchet MS", 11F);
            this.txtProductType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtProductType.Location = new System.Drawing.Point(121, 60);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(104, 20);
            this.txtProductType.TabIndex = 101;
            this.txtProductType.Text = "Product Name";
            // 
            // txtProductName
            // 
            this.txtProductName.AutoSize = true;
            this.txtProductName.BackColor = System.Drawing.Color.Transparent;
            this.txtProductName.Font = new System.Drawing.Font("Trebuchet MS", 11F);
            this.txtProductName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtProductName.Location = new System.Drawing.Point(121, 22);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(104, 20);
            this.txtProductName.TabIndex = 100;
            this.txtProductName.Text = "Product Name";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblProductName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProductName.Location = new System.Drawing.Point(6, 22);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(109, 20);
            this.lblProductName.TabIndex = 99;
            this.lblProductName.Text = "Product Name";
            // 
            // lblProductType
            // 
            this.lblProductType.AutoSize = true;
            this.lblProductType.BackColor = System.Drawing.Color.Transparent;
            this.lblProductType.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblProductType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProductType.Location = new System.Drawing.Point(11, 60);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(104, 20);
            this.lblProductType.TabIndex = 99;
            this.lblProductType.Text = "Product Type";
            // 
            // groupBoxCards
            // 
            this.groupBoxCards.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxCards.Controls.Add(this.txtCardCount);
            this.groupBoxCards.Controls.Add(this.lblCardLevelList);
            this.groupBoxCards.Controls.Add(this.cboCardLevelList);
            this.groupBoxCards.Controls.Add(this.lblCardTypeList);
            this.groupBoxCards.Controls.Add(this.cboCardTypeList);
            this.groupBoxCards.Controls.Add(this.lblCardCount);
            this.groupBoxCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCards.Location = new System.Drawing.Point(15, 159);
            this.groupBoxCards.Name = "groupBoxCards";
            this.groupBoxCards.Size = new System.Drawing.Size(266, 137);
            this.groupBoxCards.TabIndex = 0;
            this.groupBoxCards.TabStop = false;
            this.groupBoxCards.Text = "Cards";
            // 
            // txtCardCount
            // 
            this.txtCardCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtCardCount.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardCount.Location = new System.Drawing.Point(59, 101);
            this.txtCardCount.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
            this.txtCardCount.MaxLength = 10;
            this.txtCardCount.Name = "txtCardCount";
            this.txtCardCount.Precision = 2;
            this.txtCardCount.Size = new System.Drawing.Size(195, 26);
            this.txtCardCount.TabIndex = 2;
            // 
            // lblCardLevelList
            // 
            this.lblCardLevelList.AutoSize = true;
            this.lblCardLevelList.BackColor = System.Drawing.Color.Transparent;
            this.lblCardLevelList.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardLevelList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardLevelList.Location = new System.Drawing.Point(6, 26);
            this.lblCardLevelList.Name = "lblCardLevelList";
            this.lblCardLevelList.Size = new System.Drawing.Size(48, 20);
            this.lblCardLevelList.TabIndex = 99;
            this.lblCardLevelList.Text = "Level";
            // 
            // cboCardLevelList
            // 
            this.cboCardLevelList.BackColor = System.Drawing.SystemColors.Window;
            this.cboCardLevelList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCardLevelList.Font = new System.Drawing.Font("Trebuchet MS", 11F);
            this.cboCardLevelList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCardLevelList.FormattingEnabled = true;
            this.cboCardLevelList.Location = new System.Drawing.Point(60, 22);
            this.cboCardLevelList.Name = "cboCardLevelList";
            this.cboCardLevelList.Size = new System.Drawing.Size(195, 28);
            this.cboCardLevelList.Sorted = true;
            this.cboCardLevelList.TabIndex = 0;
            // 
            // lblCardTypeList
            // 
            this.lblCardTypeList.AutoSize = true;
            this.lblCardTypeList.BackColor = System.Drawing.Color.Transparent;
            this.lblCardTypeList.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardTypeList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardTypeList.Location = new System.Drawing.Point(10, 63);
            this.lblCardTypeList.Name = "lblCardTypeList";
            this.lblCardTypeList.Size = new System.Drawing.Size(44, 20);
            this.lblCardTypeList.TabIndex = 99;
            this.lblCardTypeList.Text = "Type";
            // 
            // cboCardTypeList
            // 
            this.cboCardTypeList.BackColor = System.Drawing.SystemColors.Window;
            this.cboCardTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCardTypeList.Font = new System.Drawing.Font("Trebuchet MS", 11F);
            this.cboCardTypeList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCardTypeList.FormattingEnabled = true;
            this.cboCardTypeList.Location = new System.Drawing.Point(60, 59);
            this.cboCardTypeList.Name = "cboCardTypeList";
            this.cboCardTypeList.Size = new System.Drawing.Size(195, 28);
            this.cboCardTypeList.Sorted = true;
            this.cboCardTypeList.TabIndex = 1;
            // 
            // lblCardCount
            // 
            this.lblCardCount.AutoSize = true;
            this.lblCardCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCardCount.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardCount.Location = new System.Drawing.Point(4, 100);
            this.lblCardCount.Name = "lblCardCount";
            this.lblCardCount.Size = new System.Drawing.Size(50, 20);
            this.lblCardCount.TabIndex = 99;
            this.lblCardCount.Text = "Count";
            // 
            // groupBoxGame
            // 
            this.groupBoxGame.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxGame.Controls.Add(this.lblGameTypeList);
            this.groupBoxGame.Controls.Add(this.lblGameCategoryList);
            this.groupBoxGame.Controls.Add(this.cboGameCategoryList);
            this.groupBoxGame.Controls.Add(this.cboGameTypeList);
            this.groupBoxGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxGame.Location = new System.Drawing.Point(287, 159);
            this.groupBoxGame.Name = "groupBoxGame";
            this.groupBoxGame.Size = new System.Drawing.Size(266, 137);
            this.groupBoxGame.TabIndex = 2;
            this.groupBoxGame.TabStop = false;
            this.groupBoxGame.Text = "Game";
            // 
            // lblGameTypeList
            // 
            this.lblGameTypeList.AutoSize = true;
            this.lblGameTypeList.BackColor = System.Drawing.Color.Transparent;
            this.lblGameTypeList.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblGameTypeList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGameTypeList.Location = new System.Drawing.Point(23, 60);
            this.lblGameTypeList.Name = "lblGameTypeList";
            this.lblGameTypeList.Size = new System.Drawing.Size(44, 20);
            this.lblGameTypeList.TabIndex = 100;
            this.lblGameTypeList.Text = "Type";
            // 
            // lblGameCategoryList
            // 
            this.lblGameCategoryList.AutoSize = true;
            this.lblGameCategoryList.BackColor = System.Drawing.Color.Transparent;
            this.lblGameCategoryList.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblGameCategoryList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGameCategoryList.Location = new System.Drawing.Point(6, 22);
            this.lblGameCategoryList.Name = "lblGameCategoryList";
            this.lblGameCategoryList.Size = new System.Drawing.Size(71, 20);
            this.lblGameCategoryList.TabIndex = 99;
            this.lblGameCategoryList.Text = "Category";
            // 
            // cboGameCategoryList
            // 
            this.cboGameCategoryList.BackColor = System.Drawing.SystemColors.Window;
            this.cboGameCategoryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGameCategoryList.Font = new System.Drawing.Font("Trebuchet MS", 11F);
            this.cboGameCategoryList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboGameCategoryList.FormattingEnabled = true;
            this.cboGameCategoryList.Location = new System.Drawing.Point(90, 18);
            this.cboGameCategoryList.Name = "cboGameCategoryList";
            this.cboGameCategoryList.Size = new System.Drawing.Size(165, 28);
            this.cboGameCategoryList.Sorted = true;
            this.cboGameCategoryList.TabIndex = 0;
            // 
            // cboGameTypeList
            // 
            this.cboGameTypeList.BackColor = System.Drawing.SystemColors.Window;
            this.cboGameTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGameTypeList.Font = new System.Drawing.Font("Trebuchet MS", 11F);
            this.cboGameTypeList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboGameTypeList.FormattingEnabled = true;
            this.cboGameTypeList.Location = new System.Drawing.Point(90, 60);
            this.cboGameTypeList.Name = "cboGameTypeList";
            this.cboGameTypeList.Size = new System.Drawing.Size(165, 28);
            this.cboGameTypeList.Sorted = true;
            this.cboGameTypeList.TabIndex = 1;
            this.cboGameTypeList.SelectedValueChanged += new System.EventHandler(this.cboGameTypeList_SelectedValueChanged);
            // 
            // groupBoxPoints
            // 
            this.groupBoxPoints.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxPoints.Controls.Add(this.checkBoxPointQualify);
            this.groupBoxPoints.Controls.Add(this.txtPointsToRedeem);
            this.groupBoxPoints.Controls.Add(this.txtPointsPerDollar);
            this.groupBoxPoints.Controls.Add(this.txtPointsPerQuantity);
            this.groupBoxPoints.Controls.Add(this.lblPointsPerQuantity);
            this.groupBoxPoints.Controls.Add(this.lblPointsPerDollarLabel);
            this.groupBoxPoints.Controls.Add(this.lblPointsToRedeem);
            this.groupBoxPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPoints.Location = new System.Drawing.Point(14, 302);
            this.groupBoxPoints.Name = "groupBoxPoints";
            this.groupBoxPoints.Size = new System.Drawing.Size(267, 171);
            this.groupBoxPoints.TabIndex = 1;
            this.groupBoxPoints.TabStop = false;
            this.groupBoxPoints.Text = "Points";
            // 
            // checkBoxPointQualify
            // 
            this.checkBoxPointQualify.AutoSize = true;
            this.checkBoxPointQualify.Checked = true;
            this.checkBoxPointQualify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPointQualify.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.checkBoxPointQualify.Location = new System.Drawing.Point(9, 135);
            this.checkBoxPointQualify.Name = "checkBoxPointQualify";
            this.checkBoxPointQualify.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxPointQualify.Size = new System.Drawing.Size(148, 24);
            this.checkBoxPointQualify.TabIndex = 3;
            this.checkBoxPointQualify.Text = "Qualifying Spend";
            this.checkBoxPointQualify.UseVisualStyleBackColor = true;
            // 
            // txtPointsToRedeem
            // 
            this.txtPointsToRedeem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsToRedeem.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsToRedeem.Location = new System.Drawing.Point(142, 103);
            this.txtPointsToRedeem.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsToRedeem.MaxLength = 16;
            this.txtPointsToRedeem.Name = "txtPointsToRedeem";
            this.txtPointsToRedeem.Precision = 2;
            this.txtPointsToRedeem.Size = new System.Drawing.Size(114, 26);
            this.txtPointsToRedeem.TabIndex = 2;
            this.txtPointsToRedeem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPointsPerDollar
            // 
            this.txtPointsPerDollar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsPerDollar.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsPerDollar.Location = new System.Drawing.Point(142, 61);
            this.txtPointsPerDollar.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsPerDollar.MaxLength = 16;
            this.txtPointsPerDollar.Name = "txtPointsPerDollar";
            this.txtPointsPerDollar.Precision = 2;
            this.txtPointsPerDollar.Size = new System.Drawing.Size(114, 26);
            this.txtPointsPerDollar.TabIndex = 1;
            this.txtPointsPerDollar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPointsPerQuantity
            // 
            this.txtPointsPerQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsPerQuantity.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsPerQuantity.Location = new System.Drawing.Point(142, 22);
            this.txtPointsPerQuantity.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsPerQuantity.MaxLength = 16;
            this.txtPointsPerQuantity.Name = "txtPointsPerQuantity";
            this.txtPointsPerQuantity.Precision = 2;
            this.txtPointsPerQuantity.Size = new System.Drawing.Size(114, 26);
            this.txtPointsPerQuantity.TabIndex = 0;
            this.txtPointsPerQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPointsPerQuantity
            // 
            this.lblPointsPerQuantity.AutoSize = true;
            this.lblPointsPerQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsPerQuantity.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsPerQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsPerQuantity.Location = new System.Drawing.Point(37, 25);
            this.lblPointsPerQuantity.Name = "lblPointsPerQuantity";
            this.lblPointsPerQuantity.Size = new System.Drawing.Size(99, 20);
            this.lblPointsPerQuantity.TabIndex = 99;
            this.lblPointsPerQuantity.Text = "Per Quantity";
            // 
            // lblPointsPerDollarLabel
            // 
            this.lblPointsPerDollarLabel.AutoSize = true;
            this.lblPointsPerDollarLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsPerDollarLabel.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsPerDollarLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsPerDollarLabel.Location = new System.Drawing.Point(58, 62);
            this.lblPointsPerDollarLabel.Name = "lblPointsPerDollarLabel";
            this.lblPointsPerDollarLabel.Size = new System.Drawing.Size(78, 20);
            this.lblPointsPerDollarLabel.TabIndex = 99;
            this.lblPointsPerDollarLabel.Text = "Per Dollar";
            // 
            // lblPointsToRedeem
            // 
            this.lblPointsToRedeem.AutoSize = true;
            this.lblPointsToRedeem.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsToRedeem.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsToRedeem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsToRedeem.Location = new System.Drawing.Point(47, 103);
            this.lblPointsToRedeem.Name = "lblPointsToRedeem";
            this.lblPointsToRedeem.Size = new System.Drawing.Size(89, 20);
            this.lblPointsToRedeem.TabIndex = 99;
            this.lblPointsToRedeem.Text = "To Redeem";
            // 
            // groupPricing
            // 
            this.groupPricing.BackColor = System.Drawing.Color.Transparent;
            this.groupPricing.Controls.Add(this.txtAltPrice);
            this.groupPricing.Controls.Add(this.lblAltPrice);
            this.groupPricing.Controls.Add(this.txtQuantity);
            this.groupPricing.Controls.Add(this.txtPrice);
            this.groupPricing.Controls.Add(this.lblPrice);
            this.groupPricing.Controls.Add(this.checkBoxTaxed);
            this.groupPricing.Controls.Add(this.lblQuantity);
            this.groupPricing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.groupPricing.Location = new System.Drawing.Point(287, 302);
            this.groupPricing.Name = "groupPricing";
            this.groupPricing.Size = new System.Drawing.Size(266, 171);
            this.groupPricing.TabIndex = 3;
            this.groupPricing.TabStop = false;
            this.groupPricing.Text = "Pricing";
            // 
            // txtAltPrice
            // 
            this.txtAltPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtAltPrice.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAltPrice.Location = new System.Drawing.Point(90, 56);
            this.txtAltPrice.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtAltPrice.MaxLength = 16;
            this.txtAltPrice.Name = "txtAltPrice";
            this.txtAltPrice.Precision = 2;
            this.txtAltPrice.Size = new System.Drawing.Size(114, 26);
            this.txtAltPrice.TabIndex = 1;
            this.txtAltPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAltPrice
            // 
            this.lblAltPrice.AutoSize = true;
            this.lblAltPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblAltPrice.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblAltPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAltPrice.Location = new System.Drawing.Point(16, 57);
            this.lblAltPrice.Name = "lblAltPrice";
            this.lblAltPrice.Size = new System.Drawing.Size(70, 20);
            this.lblAltPrice.TabIndex = 101;
            this.lblAltPrice.Text = "Alt Price";
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtQuantity.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(90, 119);
            this.txtQuantity.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
            this.txtQuantity.MaxLength = 10;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Precision = 2;
            this.txtQuantity.Size = new System.Drawing.Size(114, 26);
            this.txtQuantity.TabIndex = 3;
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPrice.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(90, 21);
            this.txtPrice.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPrice.MaxLength = 16;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Precision = 2;
            this.txtPrice.Size = new System.Drawing.Size(114, 26);
            this.txtPrice.TabIndex = 0;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrice.Location = new System.Drawing.Point(41, 21);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(45, 20);
            this.lblPrice.TabIndex = 99;
            this.lblPrice.Text = "Price";
            // 
            // checkBoxTaxed
            // 
            this.checkBoxTaxed.AutoSize = true;
            this.checkBoxTaxed.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.checkBoxTaxed.Location = new System.Drawing.Point(34, 89);
            this.checkBoxTaxed.Name = "checkBoxTaxed";
            this.checkBoxTaxed.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxTaxed.Size = new System.Drawing.Size(71, 24);
            this.checkBoxTaxed.TabIndex = 2;
            this.checkBoxTaxed.Text = "Taxed";
            this.checkBoxTaxed.UseVisualStyleBackColor = true;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantity.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQuantity.Location = new System.Drawing.Point(19, 122);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(70, 20);
            this.lblQuantity.TabIndex = 99;
            this.lblQuantity.Text = "Quantity";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FocusColor = System.Drawing.Color.Black;
            this.btnCancel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnCancel.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(416, 484);
            this.btnCancel.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShowFocus = false;
            this.btnCancel.Size = new System.Drawing.Size(130, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FocusColor = System.Drawing.Color.Black;
            this.btnAdd.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnAdd.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(15, 479);
            this.btnAdd.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 40);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add Another Product";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDone
            // 
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDone.BackColor = System.Drawing.Color.Transparent;
            this.btnDone.FocusColor = System.Drawing.Color.Black;
            this.btnDone.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDone.ForeColor = System.Drawing.Color.Black;
            this.btnDone.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnDone.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDone.Location = new System.Drawing.Point(256, 484);
            this.btnDone.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(130, 30);
            this.btnDone.TabIndex = 5;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.DoneClick);
            // 
            // BingoProductDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 528);
            this.ControlBox = false;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupProduct);
            this.Controls.Add(this.groupBoxCards);
            this.Controls.Add(this.groupBoxGame);
            this.Controls.Add(this.groupBoxPoints);
            this.Controls.Add(this.groupPricing);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "BingoProductDetailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Electronic Product Details";
            this.Load += new System.EventHandler(this.BingoProductDetailForm_Load);
            this.groupProduct.ResumeLayout(false);
            this.groupProduct.PerformLayout();
            this.groupBoxCards.ResumeLayout(false);
            this.groupBoxCards.PerformLayout();
            this.groupBoxGame.ResumeLayout(false);
            this.groupBoxGame.PerformLayout();
            this.groupBoxPoints.ResumeLayout(false);
            this.groupBoxPoints.PerformLayout();
            this.groupPricing.ResumeLayout(false);
            this.groupPricing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupProduct;
        private GTI.Controls.ImageButton btnCancel;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.ComboBox cboGameTypeList;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblCardCount;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.ComboBox cboCardTypeList;
        private System.Windows.Forms.Label lblCardTypeList;
        private System.Windows.Forms.ComboBox cboGameCategoryList;
        private System.Windows.Forms.Label lblGameCategoryList;
        private System.Windows.Forms.ComboBox cboCardLevelList;
        private System.Windows.Forms.Label lblCardLevelList;
        private System.Windows.Forms.Label lblPointsPerDollarLabel;
        private System.Windows.Forms.Label lblPointsToRedeem;
        private System.Windows.Forms.Label lblPointsPerQuantity;
        private System.Windows.Forms.GroupBox groupBoxGame;
        private System.Windows.Forms.GroupBox groupBoxPoints;
        private System.Windows.Forms.GroupBox groupBoxCards;
        private System.Windows.Forms.CheckBox checkBoxTaxed;
        private System.Windows.Forms.GroupBox groupPricing;
        private GTI.Controls.ImageButton btnAdd;
        private System.Windows.Forms.Label txtProductName;
        private System.Windows.Forms.Label txtProductType;
        private Controls.TextBoxNumeric txtCardCount;
        private Controls.TextBoxNumeric txtPointsToRedeem;
        private Controls.TextBoxNumeric txtPointsPerDollar;
        private Controls.TextBoxNumeric txtPointsPerQuantity;
        private Controls.TextBoxNumeric txtQuantity;
        private Controls.TextBoxNumeric txtPrice;
        private Controls.ImageButton btnDone;
        private System.Windows.Forms.Label lblNoteValidation;
        private System.Windows.Forms.Label lblGameTypeList;
        private Controls.TextBoxNumeric txtAltPrice;
        private System.Windows.Forms.Label lblAltPrice;
        private System.Windows.Forms.CheckBox checkBoxPointQualify;
    }
}