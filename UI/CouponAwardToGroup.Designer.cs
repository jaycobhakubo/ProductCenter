namespace GTI.Modules.ProductCenter.UI
{
    partial class CouponAwardToGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouponAwardToGroup));
            this.cmbxGroupList = new System.Windows.Forms.ComboBox();
            this.imgbtnAdd = new GTI.Controls.ImageButton();
            this.imageButton1 = new GTI.Controls.ImageButton();
            this.lblGroupList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbxGroupList
            // 
            this.cmbxGroupList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbxGroupList, "cmbxGroupList");
            this.cmbxGroupList.FormattingEnabled = true;
            this.cmbxGroupList.Name = "cmbxGroupList";
            this.cmbxGroupList.SelectedIndexChanged += new System.EventHandler(this.cmbxGroupList_SelectedIndexChanged);
            // 
            // imgbtnAdd
            // 
            this.imgbtnAdd.BackColor = System.Drawing.Color.Transparent;
            this.imgbtnAdd.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imgbtnAdd, "imgbtnAdd");
            this.imgbtnAdd.ForeColor = System.Drawing.Color.Black;
            this.imgbtnAdd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imgbtnAdd.Name = "imgbtnAdd";
            this.imgbtnAdd.UseVisualStyleBackColor = false;
            this.imgbtnAdd.Click += new System.EventHandler(this.imgbtnAdd_Click);
            // 
            // imageButton1
            // 
            this.imageButton1.BackColor = System.Drawing.Color.Transparent;
            this.imageButton1.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.imageButton1, "imageButton1");
            this.imageButton1.ForeColor = System.Drawing.Color.Black;
            this.imageButton1.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.UseVisualStyleBackColor = false;
            this.imageButton1.Click += new System.EventHandler(this.imageButton1_Click);
            // 
            // lblGroupList
            // 
            resources.ApplyResources(this.lblGroupList, "lblGroupList");
            this.lblGroupList.BackColor = System.Drawing.Color.Transparent;
            this.lblGroupList.Name = "lblGroupList";
            // 
            // CouponAwardToGroup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.lblGroupList);
            this.Controls.Add(this.imageButton1);
            this.Controls.Add(this.imgbtnAdd);
            this.Controls.Add(this.cmbxGroupList);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouponAwardToGroup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbxGroupList;
        private Controls.ImageButton imgbtnAdd;
        private Controls.ImageButton imageButton1;
        private System.Windows.Forms.Label lblGroupList;
    }
}