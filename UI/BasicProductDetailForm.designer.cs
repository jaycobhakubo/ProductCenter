namespace GTI.Modules.ProductCenter.UI
{
    partial class BasicProductDetailForm
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
            this.groupPricing = new System.Windows.Forms.GroupBox();
            this.txtAltPrice = new GTI.Controls.TextBoxNumeric();
            this.lblAltPrice = new System.Windows.Forms.Label();
            this.txtQuantity = new GTI.Controls.TextBoxNumeric();
            this.txtPrice = new GTI.Controls.TextBoxNumeric();
            this.checkBoxTaxed = new System.Windows.Forms.CheckBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.groupPoints = new System.Windows.Forms.GroupBox();
            this.checkBoxPointQualify = new System.Windows.Forms.CheckBox();
            this.txtPointsToRedeem = new GTI.Controls.TextBoxNumeric();
            this.txtPointsPerDollar = new GTI.Controls.TextBoxNumeric();
            this.txtPointsPerQuantity = new GTI.Controls.TextBoxNumeric();
            this.lblPointsPerQuantity = new System.Windows.Forms.Label();
            this.lblPointsToRedeem = new System.Windows.Forms.Label();
            this.lblPointsPerDollarLabel = new System.Windows.Forms.Label();
            this.btnDone = new GTI.Controls.ImageButton();
            this.btnCancel = new GTI.Controls.ImageButton();
            this.btnAdd = new GTI.Controls.ImageButton();
            this.lblProductType = new System.Windows.Forms.Label();
            this.lblProductName1 = new System.Windows.Forms.Label();
            this.groupProduct = new System.Windows.Forms.GroupBox();
            this.lblNoteValidation = new System.Windows.Forms.Label();
            this.txtProductType = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.Label();
            this.lblReduceAtRegister = new System.Windows.Forms.Label();
            this.groupPricing.SuspendLayout();
            this.groupPoints.SuspendLayout();
            this.groupProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPricing
            // 
            this.groupPricing.BackColor = System.Drawing.Color.Transparent;
            this.groupPricing.Controls.Add(this.txtAltPrice);
            this.groupPricing.Controls.Add(this.lblAltPrice);
            this.groupPricing.Controls.Add(this.txtQuantity);
            this.groupPricing.Controls.Add(this.txtPrice);
            this.groupPricing.Controls.Add(this.checkBoxTaxed);
            this.groupPricing.Controls.Add(this.lblPrice);
            this.groupPricing.Controls.Add(this.lblQuantity);
            this.groupPricing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPricing.Location = new System.Drawing.Point(14, 322);
            this.groupPricing.Name = "groupPricing";
            this.groupPricing.Size = new System.Drawing.Size(339, 160);
            this.groupPricing.TabIndex = 1;
            this.groupPricing.TabStop = false;
            this.groupPricing.Text = "Pricing";
            // 
            // txtAltPrice
            // 
            this.txtAltPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtAltPrice.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAltPrice.Location = new System.Drawing.Point(194, 60);
            this.txtAltPrice.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtAltPrice.MaxLength = 16;
            this.txtAltPrice.Name = "txtAltPrice";
            this.txtAltPrice.Precision = 2;
            this.txtAltPrice.Size = new System.Drawing.Size(105, 26);
            this.txtAltPrice.TabIndex = 100;
            this.txtAltPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAltPrice
            // 
            this.lblAltPrice.AutoSize = true;
            this.lblAltPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblAltPrice.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblAltPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAltPrice.Location = new System.Drawing.Point(120, 60);
            this.lblAltPrice.Name = "lblAltPrice";
            this.lblAltPrice.Size = new System.Drawing.Size(70, 20);
            this.lblAltPrice.TabIndex = 101;
            this.lblAltPrice.Text = "Alt Price";
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtQuantity.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(194, 125);
            this.txtQuantity.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
            this.txtQuantity.MaxLength = 10;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Precision = 2;
            this.txtQuantity.Size = new System.Drawing.Size(105, 26);
            this.txtQuantity.TabIndex = 2;
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPrice.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(194, 26);
            this.txtPrice.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPrice.MaxLength = 16;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Precision = 2;
            this.txtPrice.Size = new System.Drawing.Size(105, 26);
            this.txtPrice.TabIndex = 0;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBoxTaxed
            // 
            this.checkBoxTaxed.AutoSize = true;
            this.checkBoxTaxed.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.checkBoxTaxed.Location = new System.Drawing.Point(137, 92);
            this.checkBoxTaxed.Name = "checkBoxTaxed";
            this.checkBoxTaxed.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxTaxed.Size = new System.Drawing.Size(71, 24);
            this.checkBoxTaxed.TabIndex = 1;
            this.checkBoxTaxed.Text = "Taxed";
            this.checkBoxTaxed.UseVisualStyleBackColor = true;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrice.Location = new System.Drawing.Point(145, 26);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(45, 20);
            this.lblPrice.TabIndex = 99;
            this.lblPrice.Text = "Price";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantity.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQuantity.Location = new System.Drawing.Point(120, 125);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(70, 20);
            this.lblQuantity.TabIndex = 99;
            this.lblQuantity.Text = "Quantity";
            // 
            // groupPoints
            // 
            this.groupPoints.BackColor = System.Drawing.Color.Transparent;
            this.groupPoints.Controls.Add(this.checkBoxPointQualify);
            this.groupPoints.Controls.Add(this.txtPointsToRedeem);
            this.groupPoints.Controls.Add(this.txtPointsPerDollar);
            this.groupPoints.Controls.Add(this.txtPointsPerQuantity);
            this.groupPoints.Controls.Add(this.lblPointsPerQuantity);
            this.groupPoints.Controls.Add(this.lblPointsToRedeem);
            this.groupPoints.Controls.Add(this.lblPointsPerDollarLabel);
            this.groupPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPoints.Location = new System.Drawing.Point(14, 149);
            this.groupPoints.Name = "groupPoints";
            this.groupPoints.Size = new System.Drawing.Size(339, 147);
            this.groupPoints.TabIndex = 0;
            this.groupPoints.TabStop = false;
            this.groupPoints.Text = "Points";
            // 
            // checkBoxPointQualify
            // 
            this.checkBoxPointQualify.AutoSize = true;
            this.checkBoxPointQualify.Checked = true;
            this.checkBoxPointQualify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPointQualify.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.checkBoxPointQualify.Location = new System.Drawing.Point(60, 117);
            this.checkBoxPointQualify.Name = "checkBoxPointQualify";
            this.checkBoxPointQualify.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxPointQualify.Size = new System.Drawing.Size(148, 24);
            this.checkBoxPointQualify.TabIndex = 100;
            this.checkBoxPointQualify.Text = "Qualifying Spend";
            this.checkBoxPointQualify.UseVisualStyleBackColor = true;
            // 
            // txtPointsToRedeem
            // 
            this.txtPointsToRedeem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsToRedeem.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsToRedeem.Location = new System.Drawing.Point(194, 85);
            this.txtPointsToRedeem.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsToRedeem.MaxLength = 16;
            this.txtPointsToRedeem.Name = "txtPointsToRedeem";
            this.txtPointsToRedeem.Precision = 2;
            this.txtPointsToRedeem.Size = new System.Drawing.Size(105, 26);
            this.txtPointsToRedeem.TabIndex = 2;
            this.txtPointsToRedeem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPointsPerDollar
            // 
            this.txtPointsPerDollar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsPerDollar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsPerDollar.Location = new System.Drawing.Point(194, 51);
            this.txtPointsPerDollar.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsPerDollar.MaxLength = 16;
            this.txtPointsPerDollar.Name = "txtPointsPerDollar";
            this.txtPointsPerDollar.Precision = 2;
            this.txtPointsPerDollar.Size = new System.Drawing.Size(105, 26);
            this.txtPointsPerDollar.TabIndex = 1;
            this.txtPointsPerDollar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPointsPerQuantity
            // 
            this.txtPointsPerQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(251)))), ((int)(((byte)(193)))));
            this.txtPointsPerQuantity.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointsPerQuantity.Location = new System.Drawing.Point(194, 19);
            this.txtPointsPerQuantity.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Decimal;
            this.txtPointsPerQuantity.MaxLength = 16;
            this.txtPointsPerQuantity.Name = "txtPointsPerQuantity";
            this.txtPointsPerQuantity.Precision = 2;
            this.txtPointsPerQuantity.Size = new System.Drawing.Size(105, 26);
            this.txtPointsPerQuantity.TabIndex = 0;
            this.txtPointsPerQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPointsPerQuantity
            // 
            this.lblPointsPerQuantity.AutoSize = true;
            this.lblPointsPerQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsPerQuantity.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsPerQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsPerQuantity.Location = new System.Drawing.Point(90, 19);
            this.lblPointsPerQuantity.Name = "lblPointsPerQuantity";
            this.lblPointsPerQuantity.Size = new System.Drawing.Size(99, 20);
            this.lblPointsPerQuantity.TabIndex = 99;
            this.lblPointsPerQuantity.Text = "Per Quantity";
            // 
            // lblPointsToRedeem
            // 
            this.lblPointsToRedeem.AutoSize = true;
            this.lblPointsToRedeem.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsToRedeem.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsToRedeem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsToRedeem.Location = new System.Drawing.Point(100, 85);
            this.lblPointsToRedeem.Name = "lblPointsToRedeem";
            this.lblPointsToRedeem.Size = new System.Drawing.Size(89, 20);
            this.lblPointsToRedeem.TabIndex = 99;
            this.lblPointsToRedeem.Text = "To Redeem";
            // 
            // lblPointsPerDollarLabel
            // 
            this.lblPointsPerDollarLabel.AutoSize = true;
            this.lblPointsPerDollarLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblPointsPerDollarLabel.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblPointsPerDollarLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPointsPerDollarLabel.Location = new System.Drawing.Point(111, 51);
            this.lblPointsPerDollarLabel.Name = "lblPointsPerDollarLabel";
            this.lblPointsPerDollarLabel.Size = new System.Drawing.Size(78, 20);
            this.lblPointsPerDollarLabel.TabIndex = 99;
            this.lblPointsPerDollarLabel.Text = "Per Dollar";
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
            this.btnDone.Location = new System.Drawing.Point(133, 507);
            this.btnDone.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(99, 30);
            this.btnDone.TabIndex = 3;
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
            this.btnCancel.Location = new System.Drawing.Point(246, 507);
            this.btnCancel.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.CancelClick);
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
            this.btnAdd.Location = new System.Drawing.Point(20, 502);
            this.btnAdd.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(99, 40);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add Another Product";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            // lblProductName1
            // 
            this.lblProductName1.AutoSize = true;
            this.lblProductName1.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName1.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblProductName1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProductName1.Location = new System.Drawing.Point(6, 22);
            this.lblProductName1.Name = "lblProductName1";
            this.lblProductName1.Size = new System.Drawing.Size(109, 20);
            this.lblProductName1.TabIndex = 99;
            this.lblProductName1.Text = "Product Name";
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
            this.groupProduct.Size = new System.Drawing.Size(339, 131);
            this.groupProduct.TabIndex = 99;
            this.groupProduct.TabStop = false;
            this.groupProduct.Text = "Product Information";
            // 
            // lblNoteValidation
            // 
            this.lblNoteValidation.Location = new System.Drawing.Point(12, 93);
            this.lblNoteValidation.Name = "lblNoteValidation";
            this.lblNoteValidation.Size = new System.Drawing.Size(161, 18);
            this.lblNoteValidation.TabIndex = 105;
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
            // lblReduceAtRegister
            // 
            this.lblReduceAtRegister.AutoSize = true;
            this.lblReduceAtRegister.BackColor = System.Drawing.Color.Transparent;
            this.lblReduceAtRegister.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold);
            this.lblReduceAtRegister.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblReduceAtRegister.Location = new System.Drawing.Point(10, 299);
            this.lblReduceAtRegister.Name = "lblReduceAtRegister";
            this.lblReduceAtRegister.Size = new System.Drawing.Size(149, 20);
            this.lblReduceAtRegister.TabIndex = 106;
            this.lblReduceAtRegister.Text = "Reduced at register";
            this.lblReduceAtRegister.Visible = false;
            // 
            // BasicProductDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 548);
            this.ControlBox = false;
            this.Controls.Add(this.lblReduceAtRegister);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupProduct);
            this.Controls.Add(this.groupPoints);
            this.Controls.Add(this.groupPricing);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "BasicProductDetailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Details";
            this.Load += new System.EventHandler(this.BasicProductDetailForm_Load);
            this.groupPricing.ResumeLayout(false);
            this.groupPricing.PerformLayout();
            this.groupPoints.ResumeLayout(false);
            this.groupPoints.PerformLayout();
            this.groupProduct.ResumeLayout(false);
            this.groupProduct.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label lblReduceAtRegister;
        private Controls.TextBoxNumeric txtQuantity;
        private Controls.TextBoxNumeric txtPrice;
        private Controls.TextBoxNumeric txtPointsToRedeem;
        private Controls.TextBoxNumeric txtPointsPerDollar;
        private Controls.TextBoxNumeric txtPointsPerQuantity;
        private System.Windows.Forms.Label lblNoteValidation;
        private Controls.TextBoxNumeric txtAltPrice;
        private System.Windows.Forms.Label lblAltPrice;
        private System.Windows.Forms.CheckBox checkBoxPointQualify;
    }
}