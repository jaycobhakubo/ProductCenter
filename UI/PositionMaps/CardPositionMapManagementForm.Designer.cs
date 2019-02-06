namespace GTI.Modules.ProductCenter.UI.PositionMaps
{
    partial class CardPositionMapManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardPositionMapManagementForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainGB = new System.Windows.Forms.GroupBox();
            this.importCmd = new GTI.Controls.ImageButton();
            this.saveCmd = new GTI.Controls.ImageButton();
            this.revertCmd = new GTI.Controls.ImageButton();
            this.positionMapsDGV = new System.Windows.Forms.DataGridView();
            this.mapNameDGVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allowOnElecDGVChk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mapUUIDDGVCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importBGW = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.positionMapsDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainGB
            // 
            this.mainGB.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.mainGB, "mainGB");
            this.mainGB.Controls.Add(this.splitContainer1);
            this.mainGB.Name = "mainGB";
            this.mainGB.TabStop = false;
            // 
            // importCmd
            // 
            resources.ApplyResources(this.importCmd, "importCmd");
            this.importCmd.FocusColor = System.Drawing.Color.Black;
            this.importCmd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.importCmd.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.importCmd.Name = "importCmd";
            this.importCmd.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.importCmd.Click += new System.EventHandler(this.importCmd_Click);
            // 
            // saveCmd
            // 
            resources.ApplyResources(this.saveCmd, "saveCmd");
            this.saveCmd.FocusColor = System.Drawing.Color.Black;
            this.saveCmd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.saveCmd.ImagePressed = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonDown;
            this.saveCmd.Name = "saveCmd";
            this.saveCmd.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.saveCmd.Click += new System.EventHandler(this.saveCmd_Click);
            // 
            // revertCmd
            // 
            resources.ApplyResources(this.revertCmd, "revertCmd");
            this.revertCmd.FocusColor = System.Drawing.Color.Black;
            this.revertCmd.ImageNormal = global::GTI.Modules.ProductCenter.Properties.Resources.BlueButtonUp;
            this.revertCmd.Name = "revertCmd";
            this.revertCmd.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.revertCmd.Click += new System.EventHandler(this.revertCmd_Click);
            // 
            // positionMapsDGV
            // 
            this.positionMapsDGV.AllowUserToAddRows = false;
            this.positionMapsDGV.AllowUserToDeleteRows = false;
            this.positionMapsDGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.positionMapsDGV.BackgroundColor = System.Drawing.Color.White;
            this.positionMapsDGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.positionMapsDGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.positionMapsDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.positionMapsDGV, "positionMapsDGV");
            this.positionMapsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.positionMapsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mapNameDGVC,
            this.allowOnElecDGVChk,
            this.mapUUIDDGVCol});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.positionMapsDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.positionMapsDGV.GridColor = System.Drawing.SystemColors.Control;
            this.positionMapsDGV.MultiSelect = false;
            this.positionMapsDGV.Name = "positionMapsDGV";
            this.positionMapsDGV.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.positionMapsDGV.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.positionMapsDGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.positionMapsDGV_CellValueChanged);
            this.positionMapsDGV.CurrentCellDirtyStateChanged += new System.EventHandler(this.positionMapsDGV_CurrentCellDirtyStateChanged);
            // 
            // mapNameDGVC
            // 
            this.mapNameDGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.mapNameDGVC, "mapNameDGVC");
            this.mapNameDGVC.Name = "mapNameDGVC";
            // 
            // allowOnElecDGVChk
            // 
            this.allowOnElecDGVChk.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.allowOnElecDGVChk.FalseValue = "";
            resources.ApplyResources(this.allowOnElecDGVChk, "allowOnElecDGVChk");
            this.allowOnElecDGVChk.Name = "allowOnElecDGVChk";
            this.allowOnElecDGVChk.TrueValue = "";
            // 
            // mapUUIDDGVCol
            // 
            this.mapUUIDDGVCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.mapUUIDDGVCol, "mapUUIDDGVCol");
            this.mapUUIDDGVCol.Name = "mapUUIDDGVCol";
            this.mapUUIDDGVCol.ReadOnly = true;
            // 
            // importBGW
            // 
            this.importBGW.WorkerReportsProgress = true;
            this.importBGW.WorkerSupportsCancellation = true;
            this.importBGW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.importBGW_DoWork);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.positionMapsDGV);
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.importCmd);
            this.splitContainer1.Panel2.Controls.Add(this.revertCmd);
            this.splitContainer1.Panel2.Controls.Add(this.saveCmd);
            // 
            // CardPositionMapManagementForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.mainGB);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CardPositionMapManagementForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.mainGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.positionMapsDGV)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox mainGB;
        private System.Windows.Forms.DataGridView positionMapsDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn mapNameDGVC;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowOnElecDGVChk;
        private System.Windows.Forms.DataGridViewTextBoxColumn mapUUIDDGVCol;
        private System.ComponentModel.BackgroundWorker importBGW;
        private Controls.ImageButton revertCmd;
        private Controls.ImageButton saveCmd;
        private Controls.ImageButton importCmd;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
