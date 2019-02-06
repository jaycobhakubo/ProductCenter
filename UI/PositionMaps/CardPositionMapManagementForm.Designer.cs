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
            this.mainGB = new System.Windows.Forms.GroupBox();
            this.importCmd = new System.Windows.Forms.Button();
            this.revertCmd = new System.Windows.Forms.Button();
            this.saveCmd = new System.Windows.Forms.Button();
            this.positionMapsDGV = new System.Windows.Forms.DataGridView();
            this.mapNameDGVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allowOnElecDGVChk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mapUUIDDGVCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importBGW = new System.ComponentModel.BackgroundWorker();
            this.mainGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.positionMapsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // mainGB
            // 
            this.mainGB.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.mainGB, "mainGB");
            this.mainGB.Controls.Add(this.importCmd);
            this.mainGB.Controls.Add(this.revertCmd);
            this.mainGB.Controls.Add(this.saveCmd);
            this.mainGB.Controls.Add(this.positionMapsDGV);
            this.mainGB.Name = "mainGB";
            this.mainGB.TabStop = false;
            // 
            // importCmd
            // 
            resources.ApplyResources(this.importCmd, "importCmd");
            this.importCmd.Name = "importCmd";
            this.importCmd.UseVisualStyleBackColor = false;
            this.importCmd.Click += new System.EventHandler(this.importCmd_Click);
            // 
            // revertCmd
            // 
            resources.ApplyResources(this.revertCmd, "revertCmd");
            this.revertCmd.Name = "revertCmd";
            this.revertCmd.UseVisualStyleBackColor = false;
            this.revertCmd.Click += new System.EventHandler(this.revertCmd_Click);
            // 
            // saveCmd
            // 
            resources.ApplyResources(this.saveCmd, "saveCmd");
            this.saveCmd.Name = "saveCmd";
            this.saveCmd.UseVisualStyleBackColor = false;
            this.saveCmd.Click += new System.EventHandler(this.saveCmd_Click);
            // 
            // positionMapsDGV
            // 
            this.positionMapsDGV.AllowUserToAddRows = false;
            this.positionMapsDGV.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.positionMapsDGV, "positionMapsDGV");
            this.positionMapsDGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.positionMapsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.positionMapsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mapNameDGVC,
            this.allowOnElecDGVChk,
            this.mapUUIDDGVCol});
            this.positionMapsDGV.Name = "positionMapsDGV";
            this.positionMapsDGV.RowHeadersVisible = false;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox mainGB;
        private System.Windows.Forms.DataGridView positionMapsDGV;
        private System.Windows.Forms.Button importCmd;
        private System.Windows.Forms.Button revertCmd;
        private System.Windows.Forms.Button saveCmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn mapNameDGVC;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowOnElecDGVChk;
        private System.Windows.Forms.DataGridViewTextBoxColumn mapUUIDDGVCol;
        private System.ComponentModel.BackgroundWorker importBGW;
    }
}
