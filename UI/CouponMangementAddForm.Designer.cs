namespace GTI.Modules.ProductCenter.UI
{
    partial class CouponMangementAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouponMangementAddForm));
            this.label11 = new System.Windows.Forms.Label();
            this.txtbxMinimumSpendToQualify = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBxSessCount = new System.Windows.Forms.TextBox();
            this.txtBxUnlockSpend = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblHelpDisplay = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPerValue = new System.Windows.Forms.Label();
            this.radiobtnAwardType = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.radiobtnAwardType2 = new System.Windows.Forms.RadioButton();
            this.cmbxCouponType = new System.Windows.Forms.ComboBox();
            this.txtbxMaxUsage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbxAMPMEnd = new System.Windows.Forms.ComboBox();
            this.cmbxMinEnd = new System.Windows.Forms.ComboBox();
            this.cmbxHourEnd = new System.Windows.Forms.ComboBox();
            this.cmbxAMPMStart = new System.Windows.Forms.ComboBox();
            this.cmbxMinStart = new System.Windows.Forms.ComboBox();
            this.cmbxHourStart = new System.Windows.Forms.ComboBox();
            this.lblSavedSuccessfully = new System.Windows.Forms.Label();
            this.txtbxValue = new System.Windows.Forms.TextBox();
            this.dtepickerCouponEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtepickerCouponStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtbxCouponName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ignoreValChkBx = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.packageExcludeFromQualifyCkCboBx = new GTI.Controls.CheckedComboBox();
            this.productExcludeFromQualifyCkCboBx = new GTI.Controls.CheckedComboBox();
            this.imgbtnAccept = new GTI.Controls.ImageButton();
            this.imgbtnCancel = new GTI.Controls.ImageButton();
            this.cmbxCouponPackage = new GTI.Controls.CheckedComboBox();
            this.lblUnableToEditCoupon = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Name = "label11";
            // 
            // txtbxMinimumSpendToQualify
            // 
            resources.ApplyResources(this.txtbxMinimumSpendToQualify, "txtbxMinimumSpendToQualify");
            this.txtbxMinimumSpendToQualify.Name = "txtbxMinimumSpendToQualify";
            this.txtbxMinimumSpendToQualify.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDecimal_KeyPress);
            this.txtbxMinimumSpendToQualify.Leave += new System.EventHandler(this.txtbxMinSpendValue_Leave);
            this.txtbxMinimumSpendToQualify.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxMinSpendValue_Validating);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.txtBxSessCount);
            this.panel1.Controls.Add(this.txtBxUnlockSpend);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // txtBxSessCount
            // 
            resources.ApplyResources(this.txtBxSessCount, "txtBxSessCount");
            this.txtBxSessCount.Name = "txtBxSessCount";
            this.txtBxSessCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbxInt_KeyPress);
            this.txtBxSessCount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBxInt_KeyUp);
            // 
            // txtBxUnlockSpend
            // 
            resources.ApplyResources(this.txtBxUnlockSpend, "txtBxUnlockSpend");
            this.txtBxUnlockSpend.Name = "txtBxUnlockSpend";
            this.txtBxUnlockSpend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDecimal_KeyPress);
            this.txtBxUnlockSpend.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbxValue_KeyUp);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // lblHelpDisplay
            // 
            resources.ApplyResources(this.lblHelpDisplay, "lblHelpDisplay");
            this.lblHelpDisplay.BackColor = System.Drawing.Color.Transparent;
            this.lblHelpDisplay.Name = "lblHelpDisplay";
            this.lblHelpDisplay.Click += new System.EventHandler(this.helpLbl_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // lblPerValue
            // 
            resources.ApplyResources(this.lblPerValue, "lblPerValue");
            this.lblPerValue.BackColor = System.Drawing.Color.Transparent;
            this.lblPerValue.Name = "lblPerValue";
            // 
            // radiobtnAwardType
            // 
            resources.ApplyResources(this.radiobtnAwardType, "radiobtnAwardType");
            this.radiobtnAwardType.BackColor = System.Drawing.Color.Transparent;
            this.radiobtnAwardType.Name = "radiobtnAwardType";
            this.radiobtnAwardType.UseVisualStyleBackColor = false;
            this.radiobtnAwardType.CheckedChanged += new System.EventHandler(this.radiobtnAwardType_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // radiobtnAwardType2
            // 
            resources.ApplyResources(this.radiobtnAwardType2, "radiobtnAwardType2");
            this.radiobtnAwardType2.BackColor = System.Drawing.Color.Transparent;
            this.radiobtnAwardType2.Name = "radiobtnAwardType2";
            this.radiobtnAwardType2.UseVisualStyleBackColor = false;
            // 
            // cmbxCouponType
            // 
            this.cmbxCouponType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbxCouponType, "cmbxCouponType");
            this.cmbxCouponType.FormattingEnabled = true;
            this.cmbxCouponType.Name = "cmbxCouponType";
            this.cmbxCouponType.SelectedIndexChanged += new System.EventHandler(this.cmbxCouponType_SelectedIndexChanged);
            // 
            // txtbxMaxUsage
            // 
            resources.ApplyResources(this.txtbxMaxUsage, "txtbxMaxUsage");
            this.txtbxMaxUsage.Name = "txtbxMaxUsage";
            this.txtbxMaxUsage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbxInt_KeyPress);
            this.txtbxMaxUsage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBxInt_KeyUp);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // cmbxAMPMEnd
            // 
            this.cmbxAMPMEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbxAMPMEnd, "cmbxAMPMEnd");
            this.cmbxAMPMEnd.FormattingEnabled = true;
            this.cmbxAMPMEnd.Name = "cmbxAMPMEnd";
            this.cmbxAMPMEnd.TextChanged += new System.EventHandler(this.cmbxHourStart_TextChanged);
            // 
            // cmbxMinEnd
            // 
            this.cmbxMinEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbxMinEnd, "cmbxMinEnd");
            this.cmbxMinEnd.FormattingEnabled = true;
            this.cmbxMinEnd.Name = "cmbxMinEnd";
            this.cmbxMinEnd.TextChanged += new System.EventHandler(this.cmbxHourStart_TextChanged);
            // 
            // cmbxHourEnd
            // 
            this.cmbxHourEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbxHourEnd, "cmbxHourEnd");
            this.cmbxHourEnd.FormattingEnabled = true;
            this.cmbxHourEnd.Name = "cmbxHourEnd";
            this.cmbxHourEnd.TextChanged += new System.EventHandler(this.cmbxHourStart_TextChanged);
            // 
            // cmbxAMPMStart
            // 
            this.cmbxAMPMStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbxAMPMStart, "cmbxAMPMStart");
            this.cmbxAMPMStart.FormattingEnabled = true;
            this.cmbxAMPMStart.Name = "cmbxAMPMStart";
            this.cmbxAMPMStart.TextChanged += new System.EventHandler(this.cmbxHourStart_TextChanged);
            // 
            // cmbxMinStart
            // 
            this.cmbxMinStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbxMinStart, "cmbxMinStart");
            this.cmbxMinStart.FormattingEnabled = true;
            this.cmbxMinStart.Name = "cmbxMinStart";
            this.cmbxMinStart.TextChanged += new System.EventHandler(this.cmbxHourStart_TextChanged);
            // 
            // cmbxHourStart
            // 
            this.cmbxHourStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbxHourStart, "cmbxHourStart");
            this.cmbxHourStart.FormattingEnabled = true;
            this.cmbxHourStart.Name = "cmbxHourStart";
            this.cmbxHourStart.TextChanged += new System.EventHandler(this.cmbxHourStart_TextChanged);
            // 
            // lblSavedSuccessfully
            // 
            resources.ApplyResources(this.lblSavedSuccessfully, "lblSavedSuccessfully");
            this.lblSavedSuccessfully.BackColor = System.Drawing.Color.Transparent;
            this.lblSavedSuccessfully.Name = "lblSavedSuccessfully";
            // 
            // txtbxValue
            // 
            resources.ApplyResources(this.txtbxValue, "txtbxValue");
            this.txtbxValue.Name = "txtbxValue";
            this.txtbxValue.Enter += new System.EventHandler(this.ClearErrorOnEnterEvent);
            this.txtbxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDecimal_KeyPress);
            this.txtbxValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbxValue_KeyUp);
            this.txtbxValue.Leave += new System.EventHandler(this.txtbxValue_Leave);
            this.txtbxValue.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxValue_Validating);
            // 
            // dtepickerCouponEndDate
            // 
            resources.ApplyResources(this.dtepickerCouponEndDate, "dtepickerCouponEndDate");
            this.dtepickerCouponEndDate.Name = "dtepickerCouponEndDate";
            this.dtepickerCouponEndDate.ValueChanged += new System.EventHandler(this.dtepickerCouponStartDate_ValueChanged);
            this.dtepickerCouponEndDate.Enter += new System.EventHandler(this.ClearErrorOnEnterEvent);
            // 
            // dtepickerCouponStartDate
            // 
            resources.ApplyResources(this.dtepickerCouponStartDate, "dtepickerCouponStartDate");
            this.dtepickerCouponStartDate.Name = "dtepickerCouponStartDate";
            this.dtepickerCouponStartDate.ValueChanged += new System.EventHandler(this.dtepickerCouponStartDate_ValueChanged);
            this.dtepickerCouponStartDate.Enter += new System.EventHandler(this.ClearErrorOnEnterEvent);
            this.dtepickerCouponStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtepickerCouponStartDate_Validating);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // lblValue
            // 
            this.lblValue.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblValue, "lblValue");
            this.lblValue.Name = "lblValue";
            // 
            // txtbxCouponName
            // 
            resources.ApplyResources(this.txtbxCouponName, "txtbxCouponName");
            this.txtbxCouponName.Name = "txtbxCouponName";
            this.txtbxCouponName.Enter += new System.EventHandler(this.ClearErrorOnEnterEvent);
            this.txtbxCouponName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbxCouponName_KeyUp);
            this.txtbxCouponName.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxCouponName_Validating);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Name = "label13";
            // 
            // ignoreValChkBx
            // 
            resources.ApplyResources(this.ignoreValChkBx, "ignoreValChkBx");
            this.ignoreValChkBx.BackColor = System.Drawing.Color.Transparent;
            this.ignoreValChkBx.Name = "ignoreValChkBx";
            this.ignoreValChkBx.UseVisualStyleBackColor = false;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Name = "label15";
            // 
            // packageExcludeFromQualifyCkCboBx
            // 
            this.packageExcludeFromQualifyCkCboBx.CheckOnClick = true;
            this.packageExcludeFromQualifyCkCboBx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.packageExcludeFromQualifyCkCboBx.DropDownHeight = 1;
            resources.ApplyResources(this.packageExcludeFromQualifyCkCboBx, "packageExcludeFromQualifyCkCboBx");
            this.packageExcludeFromQualifyCkCboBx.FormattingEnabled = true;
            this.packageExcludeFromQualifyCkCboBx.Name = "packageExcludeFromQualifyCkCboBx";
            this.packageExcludeFromQualifyCkCboBx.ValueSeparator = ", ";
            this.packageExcludeFromQualifyCkCboBx.TextChanged += new System.EventHandler(this.excludeFromQualifyCheckedComboBox_TextChanged);
            // 
            // productExcludeFromQualifyCkCboBx
            // 
            this.productExcludeFromQualifyCkCboBx.CheckOnClick = true;
            this.productExcludeFromQualifyCkCboBx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.productExcludeFromQualifyCkCboBx.DropDownHeight = 1;
            resources.ApplyResources(this.productExcludeFromQualifyCkCboBx, "productExcludeFromQualifyCkCboBx");
            this.productExcludeFromQualifyCkCboBx.FormattingEnabled = true;
            this.productExcludeFromQualifyCkCboBx.Name = "productExcludeFromQualifyCkCboBx";
            this.productExcludeFromQualifyCkCboBx.ValueSeparator = ", ";
            this.productExcludeFromQualifyCkCboBx.TextChanged += new System.EventHandler(this.excludeFromQualifyCheckedComboBox_TextChanged);
            // 
            // imgbtnAccept
            // 
            resources.ApplyResources(this.imgbtnAccept, "imgbtnAccept");
            this.imgbtnAccept.BackColor = System.Drawing.Color.Transparent;
            this.imgbtnAccept.FocusColor = System.Drawing.Color.Black;
            this.imgbtnAccept.ForeColor = System.Drawing.Color.Black;
            this.imgbtnAccept.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnAccept.ImagePressed = ((System.Drawing.Image)(resources.GetObject("imgbtnAccept.ImagePressed")));
            this.imgbtnAccept.Name = "imgbtnAccept";
            this.imgbtnAccept.RepeatRate = 150;
            this.imgbtnAccept.RepeatWhenHeldFor = 750;
            this.imgbtnAccept.UseVisualStyleBackColor = false;
            this.imgbtnAccept.Click += new System.EventHandler(this.imgbtnAccept_Click);
            this.imgbtnAccept.Enter += new System.EventHandler(this.ClearErrorOnEnterEvent);
            // 
            // imgbtnCancel
            // 
            resources.ApplyResources(this.imgbtnCancel, "imgbtnCancel");
            this.imgbtnCancel.BackColor = System.Drawing.Color.Transparent;
            this.imgbtnCancel.FocusColor = System.Drawing.Color.Black;
            this.imgbtnCancel.ForeColor = System.Drawing.Color.Black;
            this.imgbtnCancel.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnCancel.ImagePressed = ((System.Drawing.Image)(resources.GetObject("imgbtnCancel.ImagePressed")));
            this.imgbtnCancel.Name = "imgbtnCancel";
            this.imgbtnCancel.RepeatRate = 150;
            this.imgbtnCancel.RepeatWhenHeldFor = 750;
            this.imgbtnCancel.UseVisualStyleBackColor = false;
            this.imgbtnCancel.Click += new System.EventHandler(this.imgbtnCancel_Click);
            this.imgbtnCancel.Enter += new System.EventHandler(this.ClearErrorOnEnterEvent);
            // 
            // cmbxCouponPackage
            // 
            this.cmbxCouponPackage.CheckOnClick = true;
            this.cmbxCouponPackage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbxCouponPackage.DropDownHeight = 1;
            resources.ApplyResources(this.cmbxCouponPackage, "cmbxCouponPackage");
            this.cmbxCouponPackage.FormattingEnabled = true;
            this.cmbxCouponPackage.Name = "cmbxCouponPackage";
            this.cmbxCouponPackage.ValueSeparator = ", ";
            this.cmbxCouponPackage.TextChanged += new System.EventHandler(this.cmbxCouponPackage_TextChanged);
            // 
            // lblUnableToEditCoupon
            // 
            resources.ApplyResources(this.lblUnableToEditCoupon, "lblUnableToEditCoupon");
            this.lblUnableToEditCoupon.BackColor = System.Drawing.Color.Transparent;
            this.lblUnableToEditCoupon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblUnableToEditCoupon.Name = "lblUnableToEditCoupon";
            // 
            // CouponMangementAddForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ControlBox = false;
            this.Controls.Add(this.lblUnableToEditCoupon);
            this.Controls.Add(this.cmbxCouponPackage);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.ignoreValChkBx);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.packageExcludeFromQualifyCkCboBx);
            this.Controls.Add(this.productExcludeFromQualifyCkCboBx);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtbxMinimumSpendToQualify);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHelpDisplay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPerValue);
            this.Controls.Add(this.radiobtnAwardType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.radiobtnAwardType2);
            this.Controls.Add(this.cmbxCouponType);
            this.Controls.Add(this.txtbxMaxUsage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbxAMPMEnd);
            this.Controls.Add(this.cmbxMinEnd);
            this.Controls.Add(this.cmbxHourEnd);
            this.Controls.Add(this.cmbxAMPMStart);
            this.Controls.Add(this.cmbxMinStart);
            this.Controls.Add(this.cmbxHourStart);
            this.Controls.Add(this.lblSavedSuccessfully);
            this.Controls.Add(this.txtbxValue);
            this.Controls.Add(this.dtepickerCouponEndDate);
            this.Controls.Add(this.imgbtnAccept);
            this.Controls.Add(this.dtepickerCouponStartDate);
            this.Controls.Add(this.imgbtnCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.txtbxCouponName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouponMangementAddForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.CouponMangementAddForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ImageButton imgbtnAccept;
        private Controls.ImageButton imgbtnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtepickerCouponEndDate;
        private System.Windows.Forms.DateTimePicker dtepickerCouponStartDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtbxCouponName;
        private System.Windows.Forms.TextBox txtbxValue;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.Label lblSavedSuccessfully;
        private System.Windows.Forms.ComboBox cmbxAMPMStart;
        private System.Windows.Forms.ComboBox cmbxMinStart;
        private System.Windows.Forms.ComboBox cmbxHourStart;
        private System.Windows.Forms.ComboBox cmbxAMPMEnd;
        private System.Windows.Forms.ComboBox cmbxMinEnd;
        private System.Windows.Forms.ComboBox cmbxHourEnd;
        private System.Windows.Forms.TextBox txtbxMaxUsage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbxCouponType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPerValue;
        private System.Windows.Forms.Label lblHelpDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBxSessCount;
        private System.Windows.Forms.TextBox txtBxUnlockSpend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radiobtnAwardType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radiobtnAwardType2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtbxMinimumSpendToQualify;
        private System.Windows.Forms.Label label8;
        private Controls.CheckedComboBox productExcludeFromQualifyCkCboBx;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private Controls.CheckedComboBox packageExcludeFromQualifyCkCboBx;
        private System.Windows.Forms.CheckBox ignoreValChkBx;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private Controls.CheckedComboBox cmbxCouponPackage;
        public System.Windows.Forms.Label lblUnableToEditCoupon;
    }
}