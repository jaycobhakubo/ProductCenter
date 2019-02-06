namespace GTI.Modules.ProductCenter.UI
{
    partial class PaperProductDetailForm
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
            this.groupBoxCards = new System.Windows.Forms.GroupBox();
            this.txtCardCount = new GTI.Controls.TextBoxNumeric();
            this.lblCardLevelList = new System.Windows.Forms.Label();
            this.cboCardLevelList = new System.Windows.Forms.ComboBox();
            this.lblCardTypeList = new System.Windows.Forms.Label();
            this.cboCardTypeList = new System.Windows.Forms.ComboBox();
            this.lblCardCount = new System.Windows.Forms.Label();
            this.groupBoxGame = new System.Windows.Forms.GroupBox();
            this.lblGameCategoryList = new System.Windows.Forms.Label();
            this.cboGameCategoryList = new System.Windows.Forms.ComboBox();
            this.btnAdd = new GTI.Controls.ImageButton();
            this.groupProduct = new System.Windows.Forms.GroupBox();
            this.lblNoteValidation = new System.Windows.Forms.Label();
            this.txtProductType = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.Label();
            this.lblProductName1 = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.groupPoints = new System.Windows.Forms.GroupBox();
            this.checkBoxPointQualify = new System.Windows.Forms.CheckBox();
            this.txtPointsPerQuantity = new GTI.Controls.TextBoxNumeric();
            this.txtPointsPerDollar = new GTI.Controls.TextBoxNumeric();
            this.txtPointsToRedeem = new GTI.Controls.TextBoxNumeric();
            this.lblPointsPerQuantity = new System.Windows.Forms.Label();
            this.lblPointsToRedeem = new System.Windows.Forms.Label();
            this.lblPointsPerDollarLabel = new System.Windows.Forms.Label();
            this.groupPricing = new System.Windows.Forms.GroupBox();
            this.lblTaxed = new System.Windows.Forms.Label();
            this.lblPrepaid = new System.Windows.Forms.Label();
            this.checkBoxPrepaid = new System.Windows.Forms.CheckBox();
            this.txtAltPrice = new GTI.Controls.TextBoxNumeric();
            this.lblAltPrice = new System.Windows.Forms.Label();
            this.txtPrice = new GTI.Controls.TextBoxNumeric();
            this.txtQuantity = new GTI.Controls.TextBoxNumeric();
            this.checkBoxTaxed = new System.Windows.Forms.CheckBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnDone = new GTI.Controls.ImageButton();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.groupBoxCards.SuspendLayout();
            this.groupBoxGame.SuspendLayout();
            this.groupProduct.SuspendLayout();
            this.groupPoints.SuspendLayout();
            this.groupPricing.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBoxCards.Location = new System.Drawing.Point(14, 150);
            this.groupBoxCards.Name = "groupBoxCards";
            this.groupBoxCards.Size = new System.Drawing.Size(266, 129);
            this.groupBoxCards.TabIndex = 1;
            this.groupBoxCards.TabStop = false;
            this.groupBoxCards.Text = "Cards";
            // 
            // txtCardCount
            // 
            this.txtCardCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtCardCount.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardCount.Location = new System.Drawing.Point(61, 92);
            this.txtCardCount.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
            this.txtCardCount.MaxLength = 10;
            this.txtCardCount.Name = "txtCardCount";
            this.txtCardCount.Precision = 2;
            this.txtCardCount.Size = new System.Drawing.Size(195, 26);
            this.txtCardCount.TabIndex = 2;
            this.txtCardCount.Visible = false;
            // 
            // lblCardLevelList
            // 
            this.lblCardLevelList.BackColor = System.Drawing.Color.Transparent;
            this.lblCardLevelList.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardLevelList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardLevelList.Location = new System.Drawing.Point(6, 22);
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
            this.cboCardLevelList.Location = new System.Drawing.Point(60, 18);
            this.cboCardLevelList.Name = "cboCardLevelList";
            this.cboCardLevelList.Size = new System.Drawing.Size(195, 28);
            this.cboCardLevelList.Sorted = true;
            this.cboCardLevelList.TabIndex = 0;
            // 
            // lblCardTypeList
            // 
            this.lblCardTypeList.BackColor = System.Drawing.Color.Transparent;
            this.lblCardTypeList.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardTypeList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardTypeList.Location = new System.Drawing.Point(10, 59);
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
            this.cboCardTypeList.Location = new System.Drawing.Point(60, 55);
            this.cboCardTypeList.Name = "cboCardTypeList";
            this.cboCardTypeList.Size = new System.Drawing.Size(195, 28);
            this.cboCardTypeList.Sorted = true;
            this.cboCardTypeList.TabIndex = 1;
            // 
            // lblCardCount
            // 
            this.lblCardCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCardCount.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardCount.Location = new System.Drawing.Point(4, 96);
            this.lblCardCount.Name = "lblCardCount";
            this.lblCardCount.Size = new System.Drawing.Size(50, 20);
            this.lblCardCount.TabIndex = 99;
            this.lblCardCount.Text = "Count";
            this.lblCardCount.Visible = false;
            // 
            // groupBoxGame
            // 
            this.groupBoxGame.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxGame.Controls.Add(this.lblGameCategoryList);
            this.groupBoxGame.Controls.Add(this.cboGameCategoryList);
            this.groupBoxGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxGame.Location = new System.Drawing.Point(286, 150);
            this.groupBoxGame.Name = "groupBoxGame";
            this.groupBoxGame.Size = new System.Drawing.Size(261, 129);
            this.groupBoxGame.TabIndex = 3;
            this.groupBoxGame.TabStop = false;
            this.groupBoxGame.Text = "Game";
            // 
            // lblGameCategoryList
            // 
            this.lblGameCategoryList.BackColor = System.Drawing.Color.Transparent;
            this.lblGameCategoryList.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblGameCategoryList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGameCategoryList.Location = new System.Drawing.Point(13, 21);
            this.lblGameCategoryList.Name = "lblGameCategoryList";
            this.lblGameCategoryList.Size = new System.Drawing.Size(71, 20);
            this.lblGameCategoryList.TabIndex = 0;
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
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAdd.FocusColor = System.Drawing.Color.Black;
            this.btnAdd.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnAdd.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(13, 479);
            this.btnAdd.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.btnAdd.Size = new System.Drawing.Size(130, 40);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add Another Product";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupProduct
            // 
            this.groupProduct.BackColor = System.Drawing.Color.Transparent;
            this.groupProduct.Controls.Add(this.lblNoteValidation);
            this.groupProduct.Controls.Add(this.txtProductType);
            this.groupProduct.Controls.Add(this.txtProductName);
            this.groupProduct.Controls.Add(this.lblProductName1);
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
            this.lblNoteValidation.TabIndex = 106;
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
            this.txtProductType.TabIndex = 104;
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
            this.txtProductName.TabIndex = 103;
            this.txtProductName.Text = "Product Name";
            // 
            // lblProductName1
            // 
            this.lblProductName1.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName1.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblProductName1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProductName1.Location = new System.Drawing.Point(6, 22);
            this.lblProductName1.Name = "lblProductName1";
            this.lblProductName1.Size = new System.Drawing.Size(109, 20);
            this.lblProductName1.TabIndex = 99;
            this.lblProductName1.Text = "Product Name";
            // 
            // lblProductType
            // 
            this.lblProductType.BackColor = System.Drawing.Color.Transparent;
            this.lblProductType.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblProductType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProductType.Location = new System.Drawing.Point(11, 60);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(104, 20);
            this.lblProductType.TabIndex = 99;
            this.lblProductType.Text = "Product Type";
            // 
            // groupPoints
            // 
            this.groupPoints.BackColor = System.Drawing.Color.Transparent;
            this.groupPoints.Controls.Add(this.checkBoxPointQualify);
            this.groupPoints.Controls.Add(this.txtPointsPerQuantity);
            this.groupPoints.Controls.Add(this.txtPointsPerDollar);
            this.groupPoints.Controls.Add(this.txtPointsToRedeem);
            this.groupPoints.Controls.Add(this.lblPointsPerQuantity);
            this.groupPoints.Controls.Add(this.lblPointsToRedeem);
            this.groupPoints.Controls.Add(this.lblPointsPerDollarLabel);
            this.groupPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPoints.Location = new System.Drawing.Point(14, 287);
            this.groupPoints.Name = "groupPoints";
            this.groupPoints.Size = new System.Drawing.Size(266, 171);
            this.groupPoints.TabIndex = 2;
            this.groupPoints.TabStop = false;
            this.groupPoints.Text = "Points";
            // 
            // checkBoxPointQualify
            // 
            this.checkBoxPointQualify.Checked = true;
            this.checkBoxPointQualify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPointQualify.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.checkBoxPointQualify.Location = new System.Drawing.Point(6, 138);
            this.checkBoxPointQualify.Name = "checkBoxPointQualify";
            this.checkBoxPointQualify.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxPointQualify.Size = new System.Drawing.Size(148, 24);
            this.checkBoxPointQualify.TabIndex = 3;
            this.checkBoxPointQualify.Text = "Qualifying Spend";
            this.checkBoxPointQualify.UseVisualStyleBackColor = true;
            // 
            // txtPointsPerQuantity
            // 
            this.txtPointsPerQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsPerQuantity.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsPerQuantity.Location = new System.Drawing.Point(140, 23);
            this.txtPointsPerQuantity.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsPerQuantity.MaxLength = 16;
            this.txtPointsPerQuantity.Name = "txtPointsPerQuantity";
            this.txtPointsPerQuantity.Precision = 2;
            this.txtPointsPerQuantity.Size = new System.Drawing.Size(114, 26);
            this.txtPointsPerQuantity.TabIndex = 0;
            this.txtPointsPerQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPointsPerDollar
            // 
            this.txtPointsPerDollar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsPerDollar.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsPerDollar.Location = new System.Drawing.Point(140, 60);
            this.txtPointsPerDollar.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsPerDollar.MaxLength = 16;
            this.txtPointsPerDollar.Name = "txtPointsPerDollar";
            this.txtPointsPerDollar.Precision = 2;
            this.txtPointsPerDollar.Size = new System.Drawing.Size(114, 26);
            this.txtPointsPerDollar.TabIndex = 1;
            this.txtPointsPerDollar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPointsToRedeem
            // 
            this.txtPointsToRedeem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsToRedeem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsToRedeem.Location = new System.Drawing.Point(140, 97);
            this.txtPointsToRedeem.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsToRedeem.MaxLength = 16;
            this.txtPointsToRedeem.Name = "txtPointsToRedeem";
            this.txtPointsToRedeem.Precision = 2;
            this.txtPointsToRedeem.Size = new System.Drawing.Size(114, 26);
            this.txtPointsToRedeem.TabIndex = 2;
            this.txtPointsToRedeem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPointsPerQuantity
            // 
            this.lblPointsPerQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsPerQuantity.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsPerQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsPerQuantity.Location = new System.Drawing.Point(36, 23);
            this.lblPointsPerQuantity.Name = "lblPointsPerQuantity";
            this.lblPointsPerQuantity.Size = new System.Drawing.Size(99, 20);
            this.lblPointsPerQuantity.TabIndex = 99;
            this.lblPointsPerQuantity.Text = "Per Quantity";
            // 
            // lblPointsToRedeem
            // 
            this.lblPointsToRedeem.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsToRedeem.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsToRedeem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsToRedeem.Location = new System.Drawing.Point(48, 97);
            this.lblPointsToRedeem.Name = "lblPointsToRedeem";
            this.lblPointsToRedeem.Size = new System.Drawing.Size(89, 20);
            this.lblPointsToRedeem.TabIndex = 99;
            this.lblPointsToRedeem.Text = "To Redeem";
            // 
            // lblPointsPerDollarLabel
            // 
            this.lblPointsPerDollarLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsPerDollarLabel.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsPerDollarLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsPerDollarLabel.Location = new System.Drawing.Point(56, 60);
            this.lblPointsPerDollarLabel.Name = "lblPointsPerDollarLabel";
            this.lblPointsPerDollarLabel.Size = new System.Drawing.Size(78, 20);
            this.lblPointsPerDollarLabel.TabIndex = 99;
            this.lblPointsPerDollarLabel.Text = "Per Dollar";
            // 
            // groupPricing
            // 
            this.groupPricing.BackColor = System.Drawing.Color.Transparent;
            this.groupPricing.Controls.Add(this.lblTaxed);
            this.groupPricing.Controls.Add(this.lblPrepaid);
            this.groupPricing.Controls.Add(this.checkBoxPrepaid);
            this.groupPricing.Controls.Add(this.txtAltPrice);
            this.groupPricing.Controls.Add(this.lblAltPrice);
            this.groupPricing.Controls.Add(this.txtPrice);
            this.groupPricing.Controls.Add(this.txtQuantity);
            this.groupPricing.Controls.Add(this.checkBoxTaxed);
            this.groupPricing.Controls.Add(this.lblPrice);
            this.groupPricing.Controls.Add(this.lblQuantity);
            this.groupPricing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPricing.Location = new System.Drawing.Point(286, 287);
            this.groupPricing.Name = "groupPricing";
            this.groupPricing.Size = new System.Drawing.Size(261, 171);
            this.groupPricing.TabIndex = 4;
            this.groupPricing.TabStop = false;
            this.groupPricing.Text = "Pricing";
            // 
            // lblTaxed
            // 
            this.lblTaxed.BackColor = System.Drawing.Color.Transparent;
            this.lblTaxed.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblTaxed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTaxed.Location = new System.Drawing.Point(45, 110);
            this.lblTaxed.Name = "lblTaxed";
            this.lblTaxed.Size = new System.Drawing.Size(60, 20);
            this.lblTaxed.TabIndex = 106;
            this.lblTaxed.Text = "Taxed";
            this.lblTaxed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPrepaid
            // 
            this.lblPrepaid.BackColor = System.Drawing.Color.Transparent;
            this.lblPrepaid.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrepaid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrepaid.Location = new System.Drawing.Point(40, 86);
            this.lblPrepaid.Name = "lblPrepaid";
            this.lblPrepaid.Size = new System.Drawing.Size(65, 20);
            this.lblPrepaid.TabIndex = 105;
            this.lblPrepaid.Text = "Prepaid";
            this.lblPrepaid.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkBoxPrepaid
            // 
            this.checkBoxPrepaid.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxPrepaid.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.checkBoxPrepaid.Location = new System.Drawing.Point(111, 85);
            this.checkBoxPrepaid.Name = "checkBoxPrepaid";
            this.checkBoxPrepaid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxPrepaid.Size = new System.Drawing.Size(25, 24);
            this.checkBoxPrepaid.TabIndex = 2;
            this.checkBoxPrepaid.UseVisualStyleBackColor = true;
            // 
            // txtAltPrice
            // 
            this.txtAltPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtAltPrice.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAltPrice.Location = new System.Drawing.Point(111, 51);
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
            this.lblAltPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblAltPrice.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblAltPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAltPrice.Location = new System.Drawing.Point(35, 55);
            this.lblAltPrice.Name = "lblAltPrice";
            this.lblAltPrice.Size = new System.Drawing.Size(70, 20);
            this.lblAltPrice.TabIndex = 101;
            this.lblAltPrice.Text = "Alt Price";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPrice.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(111, 19);
            this.txtPrice.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPrice.MaxLength = 16;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Precision = 2;
            this.txtPrice.Size = new System.Drawing.Size(114, 26);
            this.txtPrice.TabIndex = 0;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtQuantity.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(111, 135);
            this.txtQuantity.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
            this.txtQuantity.MaxLength = 10;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Precision = 2;
            this.txtQuantity.Size = new System.Drawing.Size(114, 26);
            this.txtQuantity.TabIndex = 4;
            // 
            // checkBoxTaxed
            // 
            this.checkBoxTaxed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxTaxed.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.checkBoxTaxed.Location = new System.Drawing.Point(111, 109);
            this.checkBoxTaxed.Name = "checkBoxTaxed";
            this.checkBoxTaxed.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxTaxed.Size = new System.Drawing.Size(25, 24);
            this.checkBoxTaxed.TabIndex = 3;
            this.checkBoxTaxed.UseVisualStyleBackColor = true;
            // 
            // lblPrice
            // 
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrice.Location = new System.Drawing.Point(60, 23);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(45, 20);
            this.lblPrice.TabIndex = 99;
            this.lblPrice.Text = "Price";
            // 
            // lblQuantity
            // 
            this.lblQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantity.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQuantity.Location = new System.Drawing.Point(35, 139);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(70, 20);
            this.lblQuantity.TabIndex = 99;
            this.lblQuantity.Text = "Quantity";
            // 
            // btnDone
            // 
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDone.BackColor = System.Drawing.Color.Transparent;
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDone.FocusColor = System.Drawing.Color.Black;
            this.btnDone.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDone.ForeColor = System.Drawing.Color.Black;
            this.btnDone.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.btnDone.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.btnDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDone.Location = new System.Drawing.Point(256, 486);
            this.btnDone.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnDone.Name = "btnDone";
            this.btnDone.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.btnDone.Size = new System.Drawing.Size(130, 30);
            this.btnDone.TabIndex = 6;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.DoneClick);
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
            this.btnCancel.Location = new System.Drawing.Point(416, 486);
            this.btnCancel.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(130, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // PaperProductDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 528);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxCards);
            this.Controls.Add(this.groupBoxGame);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupProduct);
            this.Controls.Add(this.groupPoints);
            this.Controls.Add(this.groupPricing);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "PaperProductDetailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Paper Product Details";
            this.Load += new System.EventHandler(this.PaperProductDetailForm_Load);
            this.groupBoxCards.ResumeLayout(false);
            this.groupBoxCards.PerformLayout();
            this.groupBoxGame.ResumeLayout(false);
            this.groupProduct.ResumeLayout(false);
            this.groupProduct.PerformLayout();
            this.groupPoints.ResumeLayout(false);
            this.groupPoints.PerformLayout();
            this.groupPricing.ResumeLayout(false);
            this.groupPricing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupPricing;
        private GTI.Controls.ImageButton btnDone;
        private GTI.Controls.ImageButton btnCancel;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblPointsPerDollarLabel;
        private System.Windows.Forms.Label lblPointsToRedeem;
        private System.Windows.Forms.Label lblPointsPerQuantity;
        private System.Windows.Forms.GroupBox groupPoints;
        private System.Windows.Forms.CheckBox checkBoxTaxed;
        private GTI.Controls.ImageButton btnAdd;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.Label lblProductName1;
        private System.Windows.Forms.GroupBox groupProduct;
        private System.Windows.Forms.Label txtProductType;
        private System.Windows.Forms.Label txtProductName;
        private System.Windows.Forms.GroupBox groupBoxGame;
        private System.Windows.Forms.Label lblGameCategoryList;
        private System.Windows.Forms.ComboBox cboGameCategoryList;
        private Controls.TextBoxNumeric txtPrice;
        private Controls.TextBoxNumeric txtQuantity;
        private Controls.TextBoxNumeric txtPointsPerQuantity;
        private Controls.TextBoxNumeric txtPointsPerDollar;
        private Controls.TextBoxNumeric txtPointsToRedeem;
        private System.Windows.Forms.Label lblNoteValidation;
        private System.Windows.Forms.GroupBox groupBoxCards;
        private Controls.TextBoxNumeric txtCardCount;
        private System.Windows.Forms.Label lblCardLevelList;
        private System.Windows.Forms.ComboBox cboCardLevelList;
        private System.Windows.Forms.Label lblCardTypeList;
        private System.Windows.Forms.ComboBox cboCardTypeList;
        private System.Windows.Forms.Label lblCardCount;
        private Controls.TextBoxNumeric txtAltPrice;
        private System.Windows.Forms.Label lblAltPrice;
        private System.Windows.Forms.CheckBox checkBoxPointQualify;
        private System.Windows.Forms.CheckBox checkBoxPrepaid;
        private System.Windows.Forms.Label lblTaxed;
        private System.Windows.Forms.Label lblPrepaid;
    }
}