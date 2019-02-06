namespace GTI.Modules.ProductCenter.UI
{
    partial class ProgressPopup
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
            if(disposing && (components != null))
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
            this.theProgressBar = new System.Windows.Forms.ProgressBar();
            this.theCancelCmd = new System.Windows.Forms.Button();
            this.theProgressMessageLabel = new System.Windows.Forms.Label();
            this.theTitleLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // theProgressBar
            // 
            this.theProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.theProgressBar.Location = new System.Drawing.Point(38, 95);
            this.theProgressBar.Name = "theProgressBar";
            this.theProgressBar.Size = new System.Drawing.Size(332, 23);
            this.theProgressBar.TabIndex = 0;
            // 
            // theCancelCmd
            // 
            this.theCancelCmd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.theCancelCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.theCancelCmd.Location = new System.Drawing.Point(167, 134);
            this.theCancelCmd.Name = "theCancelCmd";
            this.theCancelCmd.Size = new System.Drawing.Size(75, 23);
            this.theCancelCmd.TabIndex = 1;
            this.theCancelCmd.Text = "Cancel";
            this.theCancelCmd.UseVisualStyleBackColor = true;
            this.theCancelCmd.Click += new System.EventHandler(this.theCancelCmd_Click);
            // 
            // theProgressMessageLabel
            // 
            this.theProgressMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.theProgressMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.theProgressMessageLabel.Location = new System.Drawing.Point(38, 40);
            this.theProgressMessageLabel.Name = "theProgressMessageLabel";
            this.theProgressMessageLabel.Size = new System.Drawing.Size(332, 52);
            this.theProgressMessageLabel.TabIndex = 2;
            this.theProgressMessageLabel.Text = "-";
            this.theProgressMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // theTitleLabel
            // 
            this.theTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.theTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.theTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.theTitleLabel.Location = new System.Drawing.Point(38, 17);
            this.theTitleLabel.Name = "theTitleLabel";
            this.theTitleLabel.Size = new System.Drawing.Size(332, 23);
            this.theTitleLabel.TabIndex = 2;
            this.theTitleLabel.Text = "-";
            this.theTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.theCancelCmd);
            this.panel1.Controls.Add(this.theProgressMessageLabel);
            this.panel1.Controls.Add(this.theProgressBar);
            this.panel1.Controls.Add(this.theTitleLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 178);
            this.panel1.TabIndex = 3;
            // 
            // ProgressPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 178);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProgressPopup";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgressPopup";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar theProgressBar;
        private System.Windows.Forms.Button theCancelCmd;
        private System.Windows.Forms.Label theProgressMessageLabel;
        private System.Windows.Forms.Label theTitleLabel;
        private System.Windows.Forms.Panel panel1;
    }
}