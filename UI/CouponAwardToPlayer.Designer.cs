namespace GTI.Modules.ProductCenter.UI
{
    partial class CouponAwardToPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouponAwardToPlayer));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtbxFirstName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbxLastName = new System.Windows.Forms.TextBox();
            this.lblSavedSuccessfully = new System.Windows.Forms.Label();
            this.imageButton1 = new GTI.Controls.ImageButton();
            this.imgbtnAdd = new GTI.Controls.ImageButton();
            this.txtbxCardNumber = new System.Windows.Forms.TextBox();
            this.rdoByCardNumber = new System.Windows.Forms.RadioButton();
            this.rdoByName = new System.Windows.Forms.RadioButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.radioButton1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Checked = true;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.rdoByName_CheckedChanged);
            this.radioButton1.Enter += new System.EventHandler(this.rdoByName_Enter);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtbxFirstName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtbxLastName);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // txtbxFirstName
            // 
            resources.ApplyResources(this.txtbxFirstName, "txtbxFirstName");
            this.txtbxFirstName.Name = "txtbxFirstName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtbxLastName
            // 
            resources.ApplyResources(this.txtbxLastName, "txtbxLastName");
            this.txtbxLastName.Name = "txtbxLastName";
            // 
            // lblSavedSuccessfully
            // 
            resources.ApplyResources(this.lblSavedSuccessfully, "lblSavedSuccessfully");
            this.lblSavedSuccessfully.BackColor = System.Drawing.Color.Transparent;
            this.lblSavedSuccessfully.Name = "lblSavedSuccessfully";
            // 
            // imageButton1
            // 
            this.imageButton1.BackColor = System.Drawing.Color.Transparent;
            this.imageButton1.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imageButton1, "imageButton1");
            this.imageButton1.ForeColor = System.Drawing.Color.Black;
            this.imageButton1.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imageButton1.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.UseVisualStyleBackColor = false;
            this.imageButton1.Click += new System.EventHandler(this.imageButton1_Click);
            this.imageButton1.Enter += new System.EventHandler(this.rdoByName_Enter);
            // 
            // imgbtnAdd
            // 
            this.imgbtnAdd.BackColor = System.Drawing.Color.Transparent;
            this.imgbtnAdd.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnAdd, "imgbtnAdd");
            this.imgbtnAdd.ForeColor = System.Drawing.Color.Black;
            this.imgbtnAdd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnAdd.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.imgbtnAdd.Name = "imgbtnAdd";
            this.imgbtnAdd.UseVisualStyleBackColor = false;
            this.imgbtnAdd.Click += new System.EventHandler(this.imgbtnAdd_Click);
            // 
            // txtbxCardNumber
            // 
            resources.ApplyResources(this.txtbxCardNumber, "txtbxCardNumber");
            this.txtbxCardNumber.Name = "txtbxCardNumber";
            this.txtbxCardNumber.Enter += new System.EventHandler(this.rdoByName_Enter);
            this.txtbxCardNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbxCardNumber_KeyUp);
            // 
            // rdoByCardNumber
            // 
            resources.ApplyResources(this.rdoByCardNumber, "rdoByCardNumber");
            this.rdoByCardNumber.BackColor = System.Drawing.Color.Transparent;
            this.rdoByCardNumber.Name = "rdoByCardNumber";
            this.rdoByCardNumber.UseVisualStyleBackColor = false;
            this.rdoByCardNumber.CheckedChanged += new System.EventHandler(this.rdoByName_CheckedChanged);
            // 
            // rdoByName
            // 
            resources.ApplyResources(this.rdoByName, "rdoByName");
            this.rdoByName.Checked = true;
            this.rdoByName.Name = "rdoByName";
            this.rdoByName.TabStop = true;
            this.rdoByName.UseVisualStyleBackColor = true;
            this.rdoByName.CheckedChanged += new System.EventHandler(this.rdoByName_CheckedChanged);
            this.rdoByName.Enter += new System.EventHandler(this.rdoByName_Enter);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.txtbxCardNumber);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.imgbtnAdd);
            this.panel4.Controls.Add(this.rdoByCardNumber);
            this.panel4.Controls.Add(this.imageButton1);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // CouponAwardToPlayer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblSavedSuccessfully);
            this.Controls.Add(this.rdoByName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouponAwardToPlayer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbxCardNumber;
        private System.Windows.Forms.TextBox txtbxLastName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbxFirstName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoByCardNumber;
        private System.Windows.Forms.RadioButton rdoByName;
        private Controls.ImageButton imageButton1;
        private Controls.ImageButton imgbtnAdd;
        public System.Windows.Forms.Label lblSavedSuccessfully;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;

    }
}